using System.Collections.Generic;

namespace ShopService.Book.Application.Wrapper
{
    public class Response<T>
    {
        public bool Succeeded {get; set;}
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public T Data { get; set; }
        public Response(){}
        public Response(T data, string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }
        public Response(string message = null)
        {
            Succeeded = false;
            Message = message;
        }
    }
}
