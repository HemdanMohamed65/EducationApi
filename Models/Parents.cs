using EduApplication.Models;

public partial class Parents
{
    public int parent_id { get; set; }

    public string? name { get; set; }

    public string email { get; set; }  

    public string password_hash { get; set; }

    public string? otp { get; set; }  

    public DateTime? otp_expiration_time { get; set; }

    public virtual ICollection<Child> Children { get; set; } = new List<Child>();
}
