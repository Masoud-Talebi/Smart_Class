namespace Smart_Class.Web.Application.Dtos
{
    public class ClassDto : BaseDto
    {
        public string? Name { get; set; }
        public required string Class_IP { get; set; }
        public int Code { get; set; }
        public string? Time_Holding { get; set; }
        public string? start_Time { get; set; }
        public string? End_Time { get; set; }
        public Guid? TeacherId { get; set; }
        public TeacherDto? Teacher { get; set; }

        //Navigations

    }
    public class AddClassDto
    {
        public string? Name { get; set; }
        public required string Class_IP { get; set; }
        public int Code { get; set; }
        public DateTime? Time_Holding { get; set; }
        public DateTime? start_Time { get; set; }
        public DateTime? End_Time { get; set; }
        public Guid? TeacherId { get; set; }

    }
    public class UpdateClassDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public required string Class_IP { get; set; }
        public int Code { get; set; }
        public DateTime? Time_Holding { get; set; }
        public DateTime? start_Time { get; set; }
        public DateTime? End_Time { get; set; }
        public Guid? TeacherId { get; set; }

    }
}