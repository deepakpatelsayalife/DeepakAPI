namespace DeepakAPI.Utility.StringConstants
{
    public interface IStringConstants
    {
        string MyConnectionString { get; }
        string InvalidRequest { get; }
        string LoginCredentailWrong { get; }
        string InvalidPassword { get; }
        string UserAccountDeleted { get; }
        string LoginSuccessfull { get; }
        string AlreadyExists { get; }
        string SignUpSuccessfull { get; }
        string SignUpFail { get; }
        string NoDataFound { get; }
        string AddedSuccessfully { get; }
        string FailToAdd { get; }
        string UpdatedSuccessfully { get; }
        string FailToUpdate { get; }
        string DeletedSuccessfully { get; }
        string FailToDelete { get; }
        string InvalidEmail { get; }
        string ResetPassword { get; }
        string ResetPasswordFailed { get; }
        string OtpSentSuccessfully { get; }
        string NewPasswordSent { get; }
    }
}