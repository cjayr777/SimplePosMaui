namespace Proj.Util;

public class SimplifiedResultReturn
{
    protected DataResult<T> Ok<T>(T data) => DataResult<T>.Ok(data);

    protected DataResult<T> Fail<T>(string message, T data = default)
        where T : new()
        => DataResult<T>.Fail(data ?? new T(), message);
}

