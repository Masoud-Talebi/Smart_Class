using Smart_Class.Web.Core;

namespace Smart_Class.Web.Core.Domain
{
    public class Teacher : Entity
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string SSID { get; set; }

        //Navigations
        public ICollection<Class>? Classes { get; set; }
    }
}