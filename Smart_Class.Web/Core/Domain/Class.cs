namespace Smart_Class.Web.Core.Domain
{
    public class Class : Entity
    {
        public string? Name { get; set; }
        public required string Class_IP { get; set; }
        public int Code { get; set; }
        public DateTime? Time_Holding { get; set; }
        public DateTime? start_Time { get; set; }
        public DateTime? End_Time { get; set; }
        public Guid? TeacherId { get; set; }

        //Navigations
        public Teacher? Teacher { get; set; }
        public ICollection<Presence_Log>? presence_Logs { get; set; }
    }
}
