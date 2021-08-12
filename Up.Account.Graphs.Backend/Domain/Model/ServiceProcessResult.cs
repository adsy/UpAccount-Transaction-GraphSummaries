using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class ServiceProcessResult
    {
        public int ServiceProcessResultCode { get; set; }
        public string ServiceProcessResultMessage { get; set; }

        public bool IsSuccess => ServiceProcessResultCode < 300 && ServiceProcessResultCode > 199;

        public ServiceProcessResult()
        {
        }

        public ServiceProcessResult(int code)
        {
            ServiceProcessResultCode = code;
        }

        public ServiceProcessResult(int code, string message)
        {
            ServiceProcessResultCode = code;
            ServiceProcessResultMessage = message;
        }
    }

    public class ServiceProcessResult<T> : ServiceProcessResult
    {
        public T Data { get; set; }

        public ServiceProcessResult()
        {
        }

        public ServiceProcessResult(int code, string message, T data) : base(code, message)
        {
            Data = data;
        }
    }
}