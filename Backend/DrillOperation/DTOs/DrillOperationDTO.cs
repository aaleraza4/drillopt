namespace DrillOperation.DTOs
{
    public class DrillOperationDTO
    {
        public int? Id { get; set; }
        public string EventName { get; set; }

        public int StartInterval { get; set; }

        public int EndInterval { get; set; }

        public int EventType { get; set; }
    }
}
