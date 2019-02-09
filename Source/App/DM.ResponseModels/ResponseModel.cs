using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.ResponseModels
{
    public class ResponseModel
    {
        protected object Data { get; set; }
        protected bool IsSuccess { get; set; }
        protected string Message { get; set; }

        public ResponseModel(object data, bool isSuccess=true, string message="Success")
        {
            Data = data;
            IsSuccess = isSuccess;
            Message = message;
        }
    }
}
