using System;
using System.Collections.Generic;
using System.Text;

namespace DeepakAPI.Utility.StringConstants
{
    public class StringConstants : IStringConstants
    {
        /// <summary>
        /// Login successfully
        /// </summary>
        public string LoginSuccessfull { get { return "Login successfully"; } }
        /// <summary>
        /// Invalid request, please try again!
        /// </summary>
        public string InvalidRequest { get { return "Invalid request, please try again!"; } }
        public string MyConnectionString { get { return "ConnectionStrings:ConnStr"; } }
        public string LoginCredentailWrong { get { return "Invalid email or password"; } }
        public string InvalidPassword { get { return "Invalid password"; } }
        public string UserAccountDeleted { get { return "Your account has been deleted, please contact to administrator"; } }
        public string AlreadyExists { get { return "already exists"; } }
        public string NoDataFound { get { return "No data found; Please try again!"; } }
        public string SignUpSuccessfull { get { return "Sign Up successfull"; } }
        public string SignUpFail { get { return "Sign Up fail, please try again!"; } }
        public string AddedSuccessfully { get { return "added successfully"; } }
        public string FailToAdd { get { return "Fail to add"; } }
        public string UpdatedSuccessfully { get { return "Updated successfully"; } }
        public string FailToUpdate { get { return "Fail to update"; } }
        public string ProfileUpdatedSuccessfully { get { return "Profile updated successfully"; } }
        public string FailToUpdateProfile { get { return "Fail to update profile"; } }
        public string DeletedSuccessfully { get { return "deleted successfully"; } }
        public string FailToDelete { get { return "Fail to delete"; } }
        public string InvalidEmail { get { return "invalid email address"; } }
        public string ResetPassword { get { return "Password reset successfull"; } }
        public string ResetPasswordFailed { get { return "Password reset failed"; } }
        public string OtpSentSuccessfully { get { return "OTP sent successfully"; } }
        public string NewPasswordSent { get { return "Hello, New password sent in your register email and mobile number. Thank you!"; } }
    }
}
