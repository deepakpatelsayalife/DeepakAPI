using DeepakAPI.Model.DTO;
using DeepakAPI.Model.Models;
using DeepakAPI.Utility.EmailSender;
using DeepakAPI.Utility.HelperClass;
using DeepakAPI.Utility.StringConstants;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace DeepakAPI.Repository.Account
{
    public class AccountRepository : IAccountRepository
    {
        #region Private Variable(s)
        private readonly IConfiguration _config;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IEmailSender _iEmailSender;
        private readonly IStringConstants _iStringConstants;
        #endregion

        #region Constructor
        public AccountRepository(IConfiguration config,
            IHostingEnvironment hostingEnvironment,
            IStringConstants iStringConstant,
            IEmailSender iEmailSender)
        {
            _config = config;
            _hostingEnvironment = hostingEnvironment;
            _iEmailSender = iEmailSender;
            _iStringConstants = iStringConstant;

        }
        #endregion




        #region  Auth Module
        /// <summary>
        /// This method is use for login user
        /// </summary>
        /// <param name="loginDTO">Login DTOs pass</param>
        /// <returns></returns>
        public async Task<BaseResponse> ValidateUserAsync(LoginDTO loginDTO)
        {
            try
            {
                string ConnectionString = _config[_iStringConstants.MyConnectionString];
                BaseResponse baseResponse = new BaseResponse();
                UserTokenDTO userTokenAC = new UserTokenDTO();
                using (DeepakdbContext deepakDBContext = new DeepakdbContext())
                {
                    /*var parameterOut=new SqlParameter
                    {
                        ParameterName=
                    }*/
                    var data = await deepakDBContext.UserDetail.FromSqlRaw("spGetUserDetailByEmail @Email=@p0", parameters: new[] { loginDTO.Email }).ToListAsync();
                    if (data != null)
                    {
                        data.ForEach(x => new UserTokenDTO()
                        {
                            Id = x.Id,
                            FirstName = x.FirstName,
                            LastName = x.LastName,
                            ZipCode = x.ZipCode,
                            Email = x.Email,
                            MobileNumber = x.MobileNumber,
                            Password = x.Password

                        }) ;
                        if (PasswordHashUtill.VerifyHashedPassword(userTokenAC.Password, loginDTO.Password))
                        {
                            baseResponse.StatusCode = (int)EnumList.ResponseType.Success;
                            baseResponse.Data = userTokenAC;
                        }
                        else
                        {
                            baseResponse.Message = _iStringConstants.InvalidPassword;
                            baseResponse.StatusCode = (int)EnumList.ResponseType.Error;
                            baseResponse.Data = null;
                        }
                    }
                    else
                    {
                        baseResponse.Message = _iStringConstants.LoginCredentailWrong;
                        baseResponse.StatusCode = (int)EnumList.ResponseType.Error;
                        baseResponse.Data = null;
                    }

                    return baseResponse;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// This method is use for new user signup
        /// </summary>
        /// <param name="userDetailDTO"></param>
        /// <returns></returns>
        public async Task<BaseResponse> SignUpAsync(UserDetailDTO userDetailDTO)
        {
            try
            {
                //string ConnectionString = _config[_iStringConstants.MyConnectionString];
                BaseResponse baseResponse = new BaseResponse();
                UserTokenDTO userTokenAC = new UserTokenDTO();
                using (DeepakdbContext deepakDBContext = new DeepakdbContext())
                {

                    //deepakDBContext.Query<T>().AsTracking().
                    var data =await Task.Run(()=> deepakDBContext
                                     .Database.ExecuteSqlRaw("spInsertUserDetail @FirstName=@p0, @LastName=@p1,@Email=@p2,@Password=@p3,@MobileNumber=@p4,@Address_Line1=@p5,@Address_Line2=@p6, @ZipCode=@p7, @CreatedBy=@p8", 
                                     parameters: new [] { userDetailDTO.FirstName ,userDetailDTO.LastName,userDetailDTO.Email,
                                         PasswordHashUtill.HashPassword( userDetailDTO.Password), userDetailDTO.MobileNumber, 
                                         userDetailDTO.AddressLine1,userDetailDTO.AddressLine2,userDetailDTO.ZipCode.ToString(),
                                         userDetailDTO.CreatedBy 
                                     }




                                     /*new SqlParameter("@FirstName", userDetailDTO.FirstName),
                                     new SqlParameter("@LastName", userDetailDTO.FirstName),
                                     new SqlParameter("@Email", userDetailDTO.Email),
                                     new SqlParameter("@Password",PasswordHashUtill.HashPassword( userDetailDTO.Password)),
                                     new SqlParameter("@MobileNumber", userDetailDTO.MobileNumber),
                                     new SqlParameter("@Address_Line1", userDetailDTO.AddressLine1),
                                     new SqlParameter("@Address_Line2", userDetailDTO.AddressLine2),
                                     new SqlParameter("@ZipCode", userDetailDTO.ZipCode),
                                     new SqlParameter("@CreatedBy", userDetailDTO.CreatedBy)*/
                                     ));
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

