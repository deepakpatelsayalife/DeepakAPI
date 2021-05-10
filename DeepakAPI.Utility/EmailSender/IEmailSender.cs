using System.Collections.Generic;

namespace DeepakAPI.Utility.EmailSender
{
    public interface IEmailSender
    {
        /// <summary>
        /// This Method Use For Sending Email For Confirming Account etc.
        /// </summary>
        /// <param name="body">It is Main Body Part of Email Which add Email Message.</param>
        /// <param name="replacements">Pass Replacement Word which replace in email</param>
        /// <param name="subject">Pass Subject of Email</param>
        /// <param name="toEmail">Pass Email Id which need to be Emailed</param>
        /// <param name="bccEmail">Pass bcc copy of Mail</param>
        /// <returns></returns>
        bool SendEmail(string body, Dictionary<string, string> replacements, string subject, string toEmail, string bccEmail);
    }
}