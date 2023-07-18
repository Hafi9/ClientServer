using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("tb_m_accounts")]
    public class Account : BaseEntity
    {
        [Column("password", TypeName = "nvarchar(max)")]
        public string Password { get; set; }
        public bool IsDeleted { get; set; }

        [Column("otp")]
        public int OTP { get; set; }
        public bool IsUsed { get; set; }

        [Column("expired_time")]
        public DateTime ExpiredTime { get; set; }
    }
}
