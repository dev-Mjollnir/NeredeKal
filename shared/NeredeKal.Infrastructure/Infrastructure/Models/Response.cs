namespace NeredeKal.Infrastructure.Infrastructure.Models
{
    public class Response<T>
    {
        public T Data { get; set; }
        public bool Succeeded { get; set; }
        public string Message { get; set; }

        public static Response<T> Fail(string errorMessage)
        {
            return new Response<T> { Succeeded = false, Message = errorMessage };
        }
        public static Response<T> Success(T data)
        {
            return new Response<T> { Succeeded = true, Data = data };
        }
    }
}
