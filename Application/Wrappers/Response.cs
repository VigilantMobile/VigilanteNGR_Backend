﻿using Domain.Common.Enums;
using System.Collections.Generic;

namespace Application.Wrappers
{
    public class Response<T>
    {
        public Response()
        {
        }

        public Response(T data, string responsestatus, string message = null)
        {
            status = responsestatus;
            Message = message;
            Data = data;
        }

        public Response(string responsestatus, string message)
        {
            status = responsestatus;
            Message = message;
        }

        //public bool status { get; set; }
        public string status { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public T Data { get; set; }

        public static Response<T> NotFound(string message = "Record not found")
        {
            return new Response<T>
            {
                status = APIResponseStatus.fail.ToString(),
                Message = message,
                Data = default(T)
            };
        }

        /// <summary>
        /// Creates a success response
        /// </summary>
        public static Response<T> Success(T data, string message = "Operation completed successfully")
        {
            return new Response<T>
            {
                status = APIResponseStatus.success.ToString(),
                Message = message,
                Data = data
            };
        }

        /// <summary>
        /// Creates a failure response
        /// </summary>
        public static Response<T> Fail(string message, T data = default)
        {
            return new Response<T>
            {
                status = APIResponseStatus.fail.ToString(),
                Message = message,
                Data = data
            };
        }

        /// <summary>
        /// Checks if response represents a not found condition
        /// </summary>
        public bool IsNotFound =>
            status == APIResponseStatus.fail.ToString() && Data == null;

        /// <summary>
        /// Checks if response represents a success condition
        /// </summary>
        public bool IsSuccess =>
            status == APIResponseStatus.success.ToString();

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

        // New constructors and static methods for better null handling

        /// <summary>
        /// Creates a not found response with default message
        /// </summary>
       
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
