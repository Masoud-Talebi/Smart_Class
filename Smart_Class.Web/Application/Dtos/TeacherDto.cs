namespace Smart_Class.Web.Application.Dtos
{
    public class TeacherDto : BaseDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? FullName { get { return FirstName +" "+ LastName; } }
        public string SSID { get; set; }
    }
    public class AddTeacherDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string SSID { get; set; }
    }
    public class UpdateTeacherDto
    {
        public Guid Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string SSID { get; set; }
    }
}
