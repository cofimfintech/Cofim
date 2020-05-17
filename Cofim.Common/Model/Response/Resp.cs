using System;
using System.Collections.Generic;
using System.Text;

namespace Cofim.Common.Model.Response
{
    public class Resp<T> where T : class
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public T Result       { get; set; }
    }//Class
}//Namespace
