namespace Merchain.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Merchain.Data.Common.Repositories;
    using Merchain.Data.Models;
    using Merchain.Services.Mapping;
    using Merchain.Services.Data.Interfaces;

    public class SettingsService : ISettingsService
    {
        private readonly IDeletableEntityRepository<Setting> settingsRepository;

        public SettingsService(IDeletableEntityRepository<Setting> settingsRepository)
        {
            this.settingsRepository = settingsRepository;
        }

        public int GetCount()
        {
            return this.settingsRepository.All().Count();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.settingsRepository.All().To<T>().ToList();
        }
    }
}
