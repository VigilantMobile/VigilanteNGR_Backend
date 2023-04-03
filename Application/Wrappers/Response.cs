using System.Collections.Generic;

namespace Application.Wrappers
{
    public class Response<T>
    {
        public Response()
        {
        }

        public Response(T data, string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }

        public Response(string message)
        {
            Succeeded = false;
            Message = message;
        }

        public Response(string message, bool success = true)
        {
            Succeeded = success;
            Message = message;
        }

        public Response(T data, string message = null, bool success = false)
        {
            Succeeded = success;
            Message = message;
            Data = data;
        }

        //public Response(T data, int HttpStatusCode, string message = null, bool successStatus = false)
        //{
        //    Succeeded = successStatus;
        //    Message = message;
        //    Data = data;
        //}

        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public T Data { get; set; }
    }

    //List T Response 
    public class DataListResponse<T>
    {
        public DataListResponse()
        {
        }

        public DataListResponse(T data, string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }

        public DataListResponse(string message)
        {
            Succeeded = false;
            Message = message;
        }

        public DataListResponse(string message, bool success = true)
        {
            Succeeded = success;
            Message = message;
        }

        public DataListResponse(T data, string message = null, bool successStatus = false)
        {
            Succeeded = successStatus;
            Message = message;
            Data = data;
        }

        //public Response(T data, int HttpStatusCode, string message = null, bool successStatus = false)
        //{
        //    Succeeded = successStatus;
        //    Message = message;
        //    Data = data;
        //}

        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public T Data { get; set; }
    }
    public class Errors
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
    public class ErrorResponse
    {
        public string Message { get; set; }
    }

}
