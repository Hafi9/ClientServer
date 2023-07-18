namespace API.Models
{
    public class University : BaseEntity
    {
        public Guid Guid { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
