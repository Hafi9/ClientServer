using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Room
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public int Floor { get; set; }
        public int Capacity { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedTime { get; set; }
    }
}
