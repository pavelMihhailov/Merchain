namespace Merchain.Web.ViewModels.Econt
{
    using Merchain.Services.Econt.Models.Response;
    using Merchain.Services.Mapping;

    public class OfficeViewModel : IMapFrom<Office>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string OfficeCode { get; set; }

        public string CountryCode { get; set; }

        public string CityId { get; set; }

        public string CityName { get; set; }

        public string PostCode { get; set; }

        public string SimpleAddress { get; set; }
    }
}
