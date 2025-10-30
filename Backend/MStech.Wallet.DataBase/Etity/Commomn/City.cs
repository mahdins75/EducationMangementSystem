using Entity.Base;

namespace Mstech.Entity.Etity
{
    public class City : BaseEntity<int>
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? PhonePrefix { get; set; }
        public string? PostalCodeFormat { get; set; }
        public string? PostalCodeRegex { get; set; }
        public int? ProvinceId { get; set; }
        public Province Province { get; set; }

    }
}