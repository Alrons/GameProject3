public class BaseResponse
{
    public int StatusCode { get; set; }
}

public class BaseResponse<T> : BaseResponse
{
    public T Result { get; set; }
}