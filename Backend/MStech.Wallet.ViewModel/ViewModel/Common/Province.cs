
namespace Mstech.ViewModel.DTO
{
    public class ProvinceViewModel
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? PhonePrefix { get; set; }
        public string? PostalCodeFormat { get; set; }
        public string? PostalCodeRegex { get; set; }
        public ICollection<CityViewModel>? Cities { get; set; }

    }
}