﻿namespace Impulse.Core
{
    public class ServiceResult<T>
    {
        public T Response { get; set; }
        public int Status { get; set; }
        public Dictionary<string, string> Errors { get; set; }
        public string Description { get; set; }

        public static ServiceResult<T> ERROR(string key, string value, int status = 400) 
        {
            return new ServiceResult<T>
            {
                Response = default,
                Status = status,
                Description = "An Error has occured",
                Errors = new Dictionary<string, string> { { key, value } }
            };
        }public static ServiceResult<T> OK(string key, string value, int status) 
        {
            return new ServiceResult<T>
            {
                Response = default,
                Status = 200,
                Description = "Success",
                Errors = null
            };
        }

    }
}
