using Npgsql;
using System.Data;

namespace Proj.Util;

/// <summary>
/// PostgreHelper version 4 - 20260403
/// </summary>
public sealed class PostgreHelper
{
    private readonly string _conString;

    public PostgreHelper(string conString) => _conString = conString;

    // Helper to streamline dictionary creation
    public Dictionary<string, object?> Args(params (string Name, object? Value)[] values)
        => values.ToDictionary(v => v.Name, v => v.Value);

    #region Core Execution Engine

    private async Task<T> ExecuteInternalAsync<T>(string functionName, Dictionary<string, object?>? args, Func<NpgsqlCommand, Task<T>> action, bool isSetReturning = false)
    {
        // SELECT * FROM func() for tables, SELECT func() for scalars
        string sql = isSetReturning
            ? $"SELECT * FROM {functionName}({BuildArgsList(args)});"
            : $"SELECT {functionName}({BuildArgsList(args)});";

        await using var conn = new NpgsqlConnection(_conString);
        await using var cmd = new NpgsqlCommand(sql, conn);

        AddArgs(cmd, args);
        await conn.OpenAsync();

        return await action(cmd);
    }

    private static void AddArgs(NpgsqlCommand cmd, Dictionary<string, object?>? args)
    {
        if (args == null) return;
        foreach (var p in args)
            cmd.Parameters.AddWithValue(p.Key, p.Value ?? DBNull.Value);
    }

    private static string BuildArgsList(Dictionary<string, object?>? args) =>
        args == null || args.Count == 0 ? "" : string.Join(", ", args.Keys.Select(k => "@" + k));

    #endregion

    #region Public API

    public async Task<OpResult> CallFuncOpResultAsync(string functionName, Dictionary<string, object?>? args)
    {
        try
        {
            var result = await ExecuteInternalAsync(functionName, args, async (cmd) =>
                (await cmd.ExecuteScalarAsync())?.ToString());

            return string.IsNullOrWhiteSpace(result) ? OpResult.Ok() : OpResult.Fail(result);
        }
        catch (Exception ex)
        {
            return OpResult.Fail(ex.Message);
        }
    }

    public async Task<DataTable> CallFuncQueryAsync(string functionName, Dictionary<string, object?>? args)
    {
        return await ExecuteInternalAsync(functionName, args, async (cmd) =>
        {
            await using var reader = await cmd.ExecuteReaderAsync();
            var dt = new DataTable();
            dt.Load(reader);
            return dt;
        }, isSetReturning: true);
    }

    public async Task<List<T>> CallFuncListAsync<T>(
        string functionName,
        Dictionary<string, object?>? args,
        Func<IDataReader, T> map)
    {
        return await ExecuteInternalAsync(functionName, args, async (cmd) =>
        {
            var list = new List<T>();

            await using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                list.Add(map(reader));
            }

            return list;
        }, isSetReturning: true);
    }
    
    public async Task<T?> CallFuncSingleAsync<T>(
        string functionName,
        Dictionary<string, object?>? args,
        Func<IDataReader, T> map)
    {
        return await ExecuteInternalAsync(functionName, args, async (cmd) =>
        {
            await using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
                return map(reader);

            return default;
        }, isSetReturning: true);
    }

    #endregion
}