using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Role : BaseEntity
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
    }
}
