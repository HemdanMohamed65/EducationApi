namespace EduApplication.Models
{
    public class ResetPasswordModel
    {
        public string Email { get; set; }
        public string OtpCode { get; set; }
        public string NewPassword { get; set; }
    }
}
