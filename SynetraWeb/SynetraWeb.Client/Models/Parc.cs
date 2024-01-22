namespace SynetraWeb.Client.Models
{
    public class Parc
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsEnable { get; set; }
        public ICollection<Room>? rooms { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
