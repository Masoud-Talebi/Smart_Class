using System.ComponentModel.DataAnnotations;

namespace Smart_Class.Web.Application.Dtos
{
    public class TeacherDto : BaseDto
    {
        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public required string FirstName { get; set; }
        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public required string LastName { get; set; }
        [Display(Name = "کد ملی ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public required string SSID { get; set; }
        public string? FullName { get { return FirstName +" "+ LastName; } }
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string UserName { get; set; }
        public string Persian_RoleName { get; set; }
        public string RoleName { get; set; }
    }
    public class AddTeacherDto
    {
        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public required string FirstName { get; set; }
        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public required string LastName { get; set; }
        [Display(Name = "کد ملی ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public required string SSID { get; set; }
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string UserName { get; set; }
        [Display(Name = "رمز عبور")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Password { get; set; }
        [Display(Name = "تکرار رمز عبور")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "رمز عبور و تکرار ان با هم یکی نیست.")]
        public string RePassword { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "نقش")]
        public string RoleName { get; set; }
    }
    public class UpdateTeacherDto
    {
        public Guid Id { get; set; }
        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public required string FirstName { get; set; }
        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public required string LastName { get; set; }
        [Display(Name = "کد ملی ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public required string SSID { get; set; }
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Display(Name = "نقش")]
        public string RoleName { get; set; }
    }
    public class SigninUserDto
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string UserName { get; set; }
        [Display(Name = "رمز عبور")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
    public class ChangePasswordDto
    {
        public string UserId { get; set; }
        [Display(Name = "رمز عبور")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Password { get; set; }
        [Display(Name = " رمز عبور جدید ")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string NewPassword { get; set; }
    }
}
