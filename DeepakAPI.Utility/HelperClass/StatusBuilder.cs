using System;
using System.Collections.Generic;
using System.Text;
using static DeepakAPI.Utility.HelperClass.EnumList;

namespace DeepakAPI.Utility.HelperClass
{
    public static class StatusBuilder
    {
        /// <summary>
        /// Response  Success Message and Data
        /// </summary>
        /// <param name="data">Pass return data.</param>
        /// <param name="message">Pass Message</param>
        /// <returns></returns>
        public static BaseResponse ResponseSuccessStatus(object data, string message)
        {
            return new BaseResponse() { StatusCode = Convert.ToInt64(ResponseType.Success), Data = data, Message = message };
        }


        /// <summary>
        /// Returns Fail message with invalid message and null data
        /// </summary>
        /// <param name="data">Pass return data.</param>
        /// <param name="messageType">Pass Message</param>
        /// <returns></returns>
        public static BaseResponse ResponseFailStatus(object data, string message)
        {
            return new BaseResponse() { StatusCode = Convert.ToInt64(ResponseType.Error), Data = data, Message = message };
        }
    }
}
