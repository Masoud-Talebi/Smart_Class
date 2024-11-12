using Smart_Class.Web.Core;

namespace Smart_Class.Web.Core.Domain
{
    public class Presence_Log : Entity
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string SSID { get; set; }
        public Guid? ClassId { get; set; }

        //Navigations
        public Class? Class { get; set; }
    }
}
