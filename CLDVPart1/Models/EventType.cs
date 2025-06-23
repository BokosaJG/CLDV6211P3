using System.ComponentModel.DataAnnotations;
namespace CLDVPart1.Models
{
    public class EventType
    {
        public int EventTypeID { get; set; }

        [Required]

        public string? Name { get; set; }
    }
}
