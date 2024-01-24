using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SynetraWeb.Client.Models
{
    public class Computer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string IDProduct { get; set; }
        public string OperatingSystem { get; set; }
        public string Os { get; set; }
        public string CarteMere { get; set; }
        public string GPU { get; set; }
        [ForeignKey("Room")]
        public int? RoomId { get; set; }
        public Room? Room { get; set; }
        public bool Statut { get; set; }
        public bool IsActive { get; set; }
        public bool IsEnable { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
