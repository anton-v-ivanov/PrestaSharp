using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bukimedia.PrestaSharp.Factories
{
    public class CountryFactory : RestSharpFactory
    {
        public CountryFactory(string BaseUrl, string Account, string SecretKey)
            : base(BaseUrl, Account, SecretKey)
        {
        }

        public Task<Entities.country> Get(long countryId)
        {
            RestRequest request = this.RequestForGet("countries", countryId, "country");
            return this.Execute<Entities.country>(request);
        }

        public async Task<Entities.country> Add(Entities.country Country)
        {
            long? idAux = Country.id;
            Country.id = null;
            List<PrestaSharp.Entities.PrestaShopEntity> Entities = new List<PrestaSharp.Entities.PrestaShopEntity>();
            Entities.Add(Country);
            RestRequest request = this.RequestForAdd("countries", Entities);
            Entities.country aux = await this.Execute<Entities.country>(request);
            Country.id = idAux;
            return await this.Get((long)aux.id);
        }

        public Task Update(Entities.country Country)
        {
            RestRequest request = this.RequestForUpdate("countries", Country.id, Country);
            return this.Execute<Entities.country>(request);
        }

        public Task Delete(long CountryId)
        {
            RestRequest request = this.RequestForDelete("countries", CountryId);
            return this.Execute<Entities.country>(request);
        }

        public Task Delete(Entities.country Country)
        {
            return this.Delete((long)Country.id);
        }

        public Task<List<long>> GetIds()
        {
            RestRequest request = this.RequestForGet("countries", null, "prestashop");
            return this.ExecuteForGetIds<List<long>>(request, "country");
        }
        
        /// <summary>
        /// More information about filtering: http://doc.prestashop.com/display/PS14/Chapter+8+-+Advanced+Use
        /// </summary>
        /// <param name="Filter">Example: key:name value:Apple</param>
        /// <param name="Sort">Field_ASC or Field_DESC. Example: name_ASC or name_DESC</param>
        /// <param name="Limit">Example: 5 limit to 5. 9,5 Only include the first 5 elements starting from the 10th element.</param>
        /// <returns></returns>
        public Task<List<Entities.country>> GetByFilter(Dictionary<string, string> Filter, string Sort, string Limit)
        {
            RestRequest request = this.RequestForFilter("countries", "full", Filter, Sort, Limit, "countries");
            return this.ExecuteForFilter<List<Entities.country>>(request);
        }

        /// <summary>
        /// More information about filtering: http://doc.prestashop.com/display/PS14/Chapter+8+-+Advanced+Use
        /// </summary>
        /// <param name="Filter">Example: key:name value:Apple</param>
        /// <param name="Sort">Field_ASC or Field_DESC. Example: name_ASC or name_DESC</param>
        /// <param name="Limit">Example: 5 limit to 5. 9,5 Only include the first 5 elements starting from the 10th element.</param>
        /// <returns></returns>
        public async Task<List<long>> GetIdsByFilter(Dictionary<string, string> Filter, string Sort, string Limit)
        {
            RestRequest request = this.RequestForFilter("countries", "[id]", Filter, Sort, Limit, "countries");
            List<PrestaSharp.Entities.FilterEntities.country> aux = await this.Execute<List<PrestaSharp.Entities.FilterEntities.country>>(request);
            return (from t in aux select t.id).ToList<long>();
        }

        /// <summary>
        /// Get all countries.
        /// </summary>
        /// <returns>A list of countries</returns>
        public Task<List<Entities.country>> GetAll()
        {
            return this.GetByFilter(null, null, null);
        }

        /// <summary>
        /// Add a list of countries.
        /// </summary>
        /// <param name="Countries"></param>
        /// <returns></returns>
        public Task<List<Entities.country>> AddList(List<Entities.country> Countries)
        {
            List<PrestaSharp.Entities.PrestaShopEntity> Entities = new List<PrestaSharp.Entities.PrestaShopEntity>();
            foreach (Entities.country Country in Countries)
            {
                Country.id = null;
                Entities.Add(Country);
            }
            RestRequest request = this.RequestForAdd("countries", Entities);
            return this.Execute<List<Entities.country>>(request);
        }
    }
}
