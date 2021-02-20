namespace Merchain.Services.Econt
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    using Merchain.Common.Helpers;
    using Merchain.Services.Econt.Models.Request;
    using Merchain.Services.Econt.Models.Response;
    using Merchain.Services.Enums;
    using Merchain.Services.Interfaces;
    using Microsoft.Extensions.Configuration;

    public class EcontService : IEcontService
    {
        private readonly IConfiguration configuration;

        public EcontService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<IQueryable<Office>> GetOffices()
        {
            SimpleRequest request = new SimpleRequest()
            {
                Credentials = new Credentials
                {
                    Username = this.configuration["Econt:DemoUsername"],
                    Password = this.configuration["Econt:DemoPassword"],
                },
                Type = EcontRequestType.Offices,
            };

            OfficesList officesList = await this.ProcessRequest(request);

            return officesList.Offices.Info.AsQueryable();
        }

        public async Task<Office> GetOffice(string officeId)
        {
            var offices = await this.GetOffices();

            return offices.FirstOrDefault(x => x.Id.Equals(officeId));
        }

        private async Task<OfficesList> ProcessRequest(SimpleRequest request)
        {
            string serializedRequest = XmlHelper.SerializeXml<SimpleRequest>(request);

            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("xml", serializedRequest),
            });

            using HttpClient client = new HttpClient { BaseAddress = new Uri(this.configuration["Econt:DemoBaseAddress"]) };

            HttpResponseMessage response = await client.PostAsync(this.configuration["Econt:OfficesRequestUrl"], formContent);
            string responseContent = await response.Content.ReadAsStringAsync();

            return XmlHelper.DeserializeXml<OfficesList>(responseContent);
        }
    }
}
