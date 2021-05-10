using System;
using System.Collections.Generic;
using System.Text;

namespace DeepakAPI.Utility.HelperClass
{
    public class EnumList
    {
        /// <summary>
        /// This enum is use for get response from api execution
        /// </summary>
        public enum ResponseType
        {
            Error = 0,
            Success = 1,
            Exception = 2,
            NotFound = 4
        }
    }
}
