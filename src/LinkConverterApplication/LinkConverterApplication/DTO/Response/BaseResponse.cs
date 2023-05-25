using Microsoft.AspNetCore.Http;

namespace LinkConverterApplication.DTO.Response
{
    public class BaseResponse<T>
    {
        public int StatusCode { get; set; } = StatusCodes.Status200OK;
        public T Result { get; set; }
        public bool IsSuccess { get; set; } = false;
    }
}