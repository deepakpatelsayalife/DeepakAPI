using System;
using System.Collections.Generic;
using System.Text;

namespace DeepakAPI.Model.DTO
{
    public class UserDetailDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ForgotPasswordToken { get; set; }
        public string MobileNumber { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public int? ZipCode { get; set; }
        public string CreatedBy { get; set; }
        
    }
}
