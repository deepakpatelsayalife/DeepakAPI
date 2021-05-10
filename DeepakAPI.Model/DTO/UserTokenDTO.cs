using System;
using System.Collections.Generic;
using System.Text;

namespace DeepakAPI.Model.DTO
{
    public class UserTokenDTO
    {
		public long Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string MobileNumber { get; set; }
		public string Password { get; set; }
		public int? ZipCode { get; set; }
		
		
	}
}
