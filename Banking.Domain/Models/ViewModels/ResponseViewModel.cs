using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Banking.Domain.Models.ViewModels
{
    public class ResponseViewModel<T>
    {
        public bool Success { get; set; }

        public T? Data { get; set; }

        public string? Message { get; set; }

        public ResponseViewModel()
        {
        }

        public ResponseViewModel(bool isSuccess, T dataObject)
        {
            Success = isSuccess;
            Data = dataObject;
        }

        public ResponseViewModel(bool isSuccess,
            T dataObject,
            string message)
        {
            Success = isSuccess;
            Data = dataObject;
            Message = message;
        }
    }
}
