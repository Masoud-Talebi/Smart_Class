namespace Smart_Class.Web.Application.Dtos
{
    public class BaseDto
    {
        public Guid Id { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public DateTime DeleteAt { get; set; }
    }
}