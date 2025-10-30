
namespace Mstech.ViewModel.DTO
{
    public class CityViewModel
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? PhonePrefix { get; set; }
        public string? PostalCodeFormat { get; set; }
        public string? PostalCodeRegex { get; set; }
        public string? ProvinceId { get; set; }
        public ProvinceViewModel? Province { get; set; }

    }
}