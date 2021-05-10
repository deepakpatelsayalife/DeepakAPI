using DeepakAPI.Model.DTO;
using DeepakAPI.Model.Models;
using DeepakAPI.Utility.EmailSender;
using DeepakAPI.Utility.HelperClass;
using DeepakAPI.Utility.StringConstants;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace DeepakAPI.Repository.User
{
    public class UserRepository
    {

        #region Private Variable(s)
        private readonly IConfiguration _config;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IEmailSender _iEmailSender;
        private readonly IStringConstants _iStringConstants;
        #endregion


        #region Constructor
        public UserRepository(IConfiguration config,
            IStringConstants stringConstants,
            IHostingEnvironment hostingEnvironment,
            IEmailSender iEmailSender)
        {
            _config = config;
            _iStringConstants = stringConstants;
            _hostingEnvironment = hostingEnvironment;
            _iEmailSender = iEmailSender;
        }
        #endregion

        #region Public Methods
        public async Task<BaseResponse>UpdateProfileAsync(UserProfileDTO userProfileDTO, long longloggedInUserId)
        {
            try
            {
                BaseResponse baseResponse = new BaseResponse();
                UserTokenDTO userTokenAC = new UserTokenDTO();
                using (DeepakdbContext deepakDBContext = new DeepakdbContext())
                {

                    var data =await Task.Run(()=> deepakDBContext
                                     .UserDetail.FromSqlRaw("spUpdateUserDetail", 
                                     new SqlParameter("@Id", userProfileDTO.Id),
                                     new SqlParameter("@FirstName", userProfileDTO.FirstName),
                                     new SqlParameter("@LastName", userProfileDTO.LastName),
                                     new SqlParameter("@Email", userProfileDTO.Email),
                                     new SqlParameter("@MobileNumber", userProfileDTO.MobileNumber),
                                     new SqlParameter("@Address_Line1", userProfileDTO.AddressLine1),
                                     new SqlParameter("@Address_Line2", userProfileDTO.AddressLine2),
                                     new SqlParameter("@ZipCode", userProfileDTO.ZipCode),
                                     new SqlParameter("@@LoggedInUserId",longloggedInUserId)
                                     )
                                     );

                    if (data != null)
                    {
                        baseResponse.StatusCode = 1;
                        baseResponse.Message = _iStringConstants.AddedSuccessfully;

                        // Need to ask how to get DBCONTEXT status code from store procedure
                    }
                    if (baseResponse.StatusCode == 1)
                    {
                        return StatusBuilder.ResponseSuccessStatus(null, baseResponse.Message);
                    }

                    return StatusBuilder.ResponseFailStatus(null, baseResponse.Message);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #endregion


    }
}
