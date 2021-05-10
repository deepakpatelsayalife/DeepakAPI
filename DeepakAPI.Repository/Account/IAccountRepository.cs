using DeepakAPI.Model.DTO;
using DeepakAPI.Utility.HelperClass;
using System.Threading.Tasks;

namespace DeepakAPI.Repository.Account
{
    public interface IAccountRepository
    {
        Task<BaseResponse> SignUpAsync(UserDetailDTO userDetailDTO);
        Task<BaseResponse> ValidateUserAsync(LoginDTO loginDTO);
    }
}