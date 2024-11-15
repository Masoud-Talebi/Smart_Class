using System.ComponentModel.DataAnnotations;

namespace Smart_Class.Web.Application.Dtos
{
    public class ClassDto : BaseDto
    {
        [Display(Name = "نام کلاس")]
        public string? Name { get; set; }
        [Display(Name = "ایپی کلاس")]
        public required string Class_IP { get; set; }
        [Display(Name = "کد کلاس")]
        public int Code { get; set; }
        [Display(Name = "تاریخ کلاس")]
        public string? Time_Holding { get; set; }
        [Display(Name = "زمان شروع")]
        public string? start_Time { get; set; }
        [Display(Name = "زمان پایان")]
        public string? End_Time { get; set; }
        public Guid? TeacherId { get; set; }
        public TeacherDto? Teacher { get; set; }

        //Navigations

    }
    public class AddClassDto
    {
        [Display(Name = "نام کلاس")]
        public string? Name { get; set; }
        [Display(Name = "ایپی کلاس")]
        public required string Class_IP { get; set; }
        [Display(Name = "کد کلاس")]
        public int Code { get; set; }
        [Display(Name = "تاریخ کلاس")]
        public DateTime? Time_Holding { get; set; }
        [Display(Name = "زمان شروع")]
        public TimeSpan? start_Time { get; set; }
        [Display(Name = "زمان پایان")]
        public TimeSpan? End_Time { get; set; }
        public Guid? TeacherId { get; set; }

    }
    public class UpdateClassDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public required string Class_IP { get; set; }
        public int Code { get; set; }
        public DateTime? Time_Holding { get; set; }
        public TimeSpan? start_Time { get; set; }
        public TimeSpan? End_Time { get; set; }
        public Guid? TeacherId { get; set; }

    }
}