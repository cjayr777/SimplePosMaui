using MySqlX.XDevAPI.CRUD;

namespace Proj.Util;

public abstract class SimpleResultReturn
{
    protected DataResult<T> Ok<T>(T data) => DataResult<T>.Ok(data);

    protected DataResult<T> Fail<T>(string message, T data = default)
        where T : new()
        => DataResult<T>.Fail(data ?? new T(), message);

    protected OpResult OpOk() => OpResult.Ok();

    protected OpResult OpFail(string message) => OpResult.Fail(message);
}

