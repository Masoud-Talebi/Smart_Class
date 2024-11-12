namespace Smart_Class.Web.Core
{
    public class Entity : IEntity
    {
        public Guid Id { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public DateTime? DeleteAt { get; set; }
    }
}
