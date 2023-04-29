using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrillOperation
{
    public class DrillOperation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string EventName { get; set; }
        public int StartInterval { get; set; }

        public int EndInterval { get; set; }

        public EventEnum EventType { get; set; }
    }
}
