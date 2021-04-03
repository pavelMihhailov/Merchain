namespace Merchain.Services.Econt
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Merchain.Common.Helpers;
    using Merchain.Services.Constants;
    using Merchain.Services.Econt.Models.Request;
    using Merchain.Services.Econt.Models.Response;
    using Merchain.Services.Interfaces;
    using Microsoft.Extensions.Configuration;

    public class EcontService : IEcontService
    {
        private readonly IConfiguration configuration;
        private readonly Credentials credentials;

        public EcontService(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.credentials = new Credentials
            {
                Username = this.configuration["Econt:DemoUsername"],
                Password = this.configuration["Econt:DemoPassword"],
            };
        }

        public async Task<IQueryable<Office>> GetOffices()
        {
            SimpleRequest request = new SimpleRequest()
            {
                Credentials = this.credentials,
                Type = EcontRequestType.Offices,
            };

            string serializedRequest = XmlHelper.SerializeXml<SimpleRequest>(request);

            OfficesList officesList = await this.ProcessOfficesRequest<OfficesList>(
                serializedRequest,
                this.configuration["Econt:DemoBaseAddress"],
                this.configuration["Econt:OfficesRequestUrl"]);

            return officesList.Offices.Info.AsQueryable();
        }

        public async Task<Office> GetOffice(string officeId)
        {
            var offices = await this.GetOffices();

            return offices.FirstOrDefault(x => x.Id.Equals(officeId));
        }

        public async Task<bool> ValidateAddress(string city, string address, string otherAddress)
        {
            ValidateAddressRequest request = new ValidateAddressRequest()
            {
                Credentials = this.credentials,
                Type = EcontRequestType.ValidateAddress,
                Address = new UserAddress
                {
                    City = city,
                    Street = address,
                    StreetOther = otherAddress,
                },
            };

            string serializedRequest = XmlHelper.SerializeXml<ValidateAddressRequest>(request);

            ValidateAddressResponse addressResponse = await this.ProcessOfficesRequest<ValidateAddressResponse>(
                serializedRequest,
                this.configuration["Econt:DemoBaseAddress"],
                this.configuration["Econt:ValidateAddressRequestUrl"]);

            if (addressResponse.Address.ValidationStatus.Equals(EcontValidationAddressStatus.Invalid))
            {
                return false;
            }

            return true;
        }

        private async Task<T> ProcessOfficesRequest<T>(string serializedRequest, string baseAddress, string endpointUrl)
            where T : class
        {
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("xml", serializedRequest),
            });

            using HttpClient client = new HttpClient { BaseAddress = new Uri(baseAddress) };

            HttpResponseMessage response = await client.PostAsync(endpointUrl, formContent);
            string responseContent = await response.Content.ReadAsStringAsync();

            return XmlHelper.DeserializeXml<T>(responseContent);
        }
    }
}
