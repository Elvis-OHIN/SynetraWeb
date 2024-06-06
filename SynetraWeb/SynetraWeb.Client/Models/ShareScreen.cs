using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SynetraWeb.Client.Models
{
    public class ShareScreen
    {
        public string? CarteMere { get; set; }
        public string? ImageData { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
