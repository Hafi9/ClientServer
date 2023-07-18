using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class BaseEntity
    {
        [Key]
        public Guid Guid { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
