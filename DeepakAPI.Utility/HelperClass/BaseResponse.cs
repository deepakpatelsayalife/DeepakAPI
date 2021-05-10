using System;
using System.Collections.Generic;
using System.Text;

namespace DeepakAPI.Utility.HelperClass
{
    public class BaseResponse
    {
        #region Base Response Constructor
        public BaseResponse()
        {
            Data = null;
            Message = null;
        }
        #endregion

        #region Getter Setter of Main Data Model
        /// <summary>
        /// Returns Particular Response Data.
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// Returns Message
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Returns Status Code
        /// </summary>
        public long StatusCode { get; set; }
        #endregion
    }
}
