using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Raven.Client.Documents;
using Raven.Client.Documents.Linq;
using Volo.Abp.RavenDB.Volo.Abp.Domain.Repositories.RavenDB;
using Volo.Abp.RavenDB.Volo.Abp.RavenDB;
using Volo.Abp.TestApp.Domain;

namespace Volo.Abp.RavenDB.Tests.Volo.Abp.TestApp.RavenDb
{
    public class CityRepository : RavenDbRepository<ITestAppRavenDbContext, City>, ICityRepository
    {
        public CityRepository(IRavenDbContextProvider<ITestAppRavenDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<City> FindByNameAsync(string name)
        {
            return await DbContext.AsyncSession.Query<City>().Where(c => c.Name == name).FirstOrDefaultAsync();
        }

        public async Task<List<Person>> GetPeopleInTheCityAsync(string cityName)
        {
            var city = await FindByNameAsync(cityName);
            return await DbContext.AsyncSession.Query<Person>().Where(p => p.CityId == city.Id).ToListAsync();
        }
    }
}