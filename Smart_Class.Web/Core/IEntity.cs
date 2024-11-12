namespace Smart_Class.Web.Core
{
    public interface IEntity : IDateEntity
    {
        public Guid Id { get; set; }
        public bool Deleted { get; set; }
    }
    public interface IDateEntity
    {
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public DateTime? DeleteAt { get; set; }
    }
}
