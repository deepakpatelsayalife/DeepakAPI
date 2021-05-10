using System;
using System.Collections.Generic;
using System.Text;

namespace DeepakAPI.Utility.HelperClass
{
    public class BaseTokenModel
    {
        /// <summary>
        /// For logged in user Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// For logged in user Firstname
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// For logged in user Lastname
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// For logged in user Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// For logged in user Address
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// For logged in user CityName
        /// </summary>
        public string CityName { get; set; }
        /// <summary>
        /// For logged in user ZipCode
        /// </summary>
        public int ZipCode { get; set; }
        /// <summary>
        /// For logged in user SocialSecurityNumber
        /// </summary>
        public string SocialSecurityNumber { get; set; }
        /// <summary>
        /// For logged in user MobileNumber
        /// </summary>
        public string MobileNumber { get; set; }
        /// <summary>
        /// For logged in user Token which user need to access all the data
        /// </summary>
        public string AccessToken { get; set; }
        /// <summary>
        /// For return status code like success or failure
        /// </summary>
        public long StatusCode { get; set; }
        /// <summary>
        /// Fore return message of success or failure
        /// </summary>
        public string Message { get; set; }
    }
}
