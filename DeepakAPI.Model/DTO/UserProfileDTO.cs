using System;
using System.Collections.Generic;
using System.Text;

namespace DeepakAPI.Model.DTO
{
    public class UserProfileDTO
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public int? ZipCode { get; set; }
        public string ModifiedBy { get; set; }
    }
}
