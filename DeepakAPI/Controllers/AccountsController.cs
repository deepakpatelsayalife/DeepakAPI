using DeepakAPI.Model.DTO;
using DeepakAPI.Repository.Account;
using DeepakAPI.Utility.HelperClass;
using DeepakAPI.Utility.StringConstants;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DeepakAPI.Controllers
{
    [EnableCors("CORS")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        #region Private Variable(s)
        private readonly IAccountRepository _iAccountRepository;
        private IConfiguration _config;
        private readonly ILogger _logger;
        private readonly IStringConstants _iStringConstant;
        #endregion

        #region Constructor
        public AccountsController(IConfiguration config,
            IAccountRepository iAccountRepository,
            IStringConstants iStringConstant,
            ILogger<AccountsController> logger)
        {
            _iAccountRepository = iAccountRepository;
            _config = config;
            _logger = logger;
            _iStringConstant = iStringConstant;
        }
        #endregion

        #region Public Method(s)

        #region Auth Module
        /// <summary>
        /// For user login 
        /// </summary>
        /// <param name="loginDTO"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("login")]
        public async Task<BaseTokenModel> LoginAsync([FromBody] LoginDTO loginDTO)
        {
            BaseTokenModel baseTokenModel = new BaseTokenModel();
            try
            {
                if (loginDTO == null)
                {
                    baseTokenModel.Message = _iStringConstant.InvalidRequest;
                    baseTokenModel.StatusCode = (int)EnumList.ResponseType.Error;
                }
                else
                {
                    BaseResponse responseModel = await _iAccountRepository.ValidateUserAsync(loginDTO);
                    if (responseModel.StatusCode == (int)EnumList.ResponseType.Success)
                    {
                        UserTokenDTO userData = (UserTokenDTO)responseModel.Data;

                        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("KeyForSignInDeepakAPI@123"));
                        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                        //For calime which we get the data from accessToken
                        var claims = new[] {
                            new Claim("FirstName",userData.FirstName),
                            new Claim("LastName",userData.LastName),
                            new Claim("MobileNumber",userData.MobileNumber),
                            new Claim(ClaimTypes.NameIdentifier, userData.Id.ToString()),
                            new Claim("Email", userData.Email)
                            };

                        //Create token based on our claims and signingCredentials
                        var tokeOptions = new JwtSecurityToken(
                            issuer: _config["Jwt:ValidIssuer"],
                            audience: _config["Jwt:ValidIssuer"],
                            claims: claims,
                            expires: DateTime.Now.AddDays(1),
                            signingCredentials: signingCredentials
                        );
                        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

                        baseTokenModel.Id = userData.Id.ToString();
                        baseTokenModel.FirstName = userData.FirstName;
                        baseTokenModel.LastName = userData.LastName;
                        baseTokenModel.Email = userData.Email;
                        baseTokenModel.MobileNumber = userData.MobileNumber;
                        baseTokenModel.AccessToken = tokenString;
                        baseTokenModel.Message = _iStringConstant.LoginSuccessfull;
                        baseTokenModel.StatusCode = Convert.ToInt32(EnumList.ResponseType.Success);
                    }
                    else
                    {
                        baseTokenModel.Message = responseModel.Message;
                        baseTokenModel.StatusCode = responseModel.StatusCode;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                baseTokenModel.Message = ex.Message;
                baseTokenModel.StatusCode = Convert.ToInt32(EnumList.ResponseType.Exception);
            }
            return baseTokenModel;
        }

        /// <summary>
        /// For new user signup
        /// </summary>
        /// <param name="userDetail"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("signup")]
        public async Task<BaseResponse> SignupAsync(UserDetailDTO userDetail)
        {
            try
            {
                return await _iAccountRepository.SignUpAsync(userDetail);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusBuilder.ResponseFailStatus(null, ex.Message);
            }
        }


        #endregion

        #endregion


    }
}
