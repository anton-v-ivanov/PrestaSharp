using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bukimedia.PrestaSharp.Factories
{
    public class CurrencyFactory : RestSharpFactory
    {
        public CurrencyFactory(string BaseUrl, string Account, string SecretKey)
            : base(BaseUrl, Account, SecretKey)
        {
        }

        public Task<Entities.currency> Get(long CurrencyId)
        {
            RestRequest request = this.RequestForGet("currencies", CurrencyId, "currency");
            return this.Execute<Entities.currency>(request);
        }

        public async Task<Entities.currency> Add(Entities.currency Currency)
        {
            long? idAux = Currency.id;
            Currency.id = null;
            List<PrestaSharp.Entities.PrestaShopEntity> Entities = new List<PrestaSharp.Entities.PrestaShopEntity>();
            Entities.Add(Currency);
            RestRequest request = this.RequestForAdd("currencies", Entities);
            Entities.currency aux = await this.Execute<Entities.currency>(request);
            Currency.id = idAux;
            return await this.Get((long)aux.id);
        }

        public Task Update(Entities.currency Currency)
        {
            RestRequest request = this.RequestForUpdate("currencies", Currency.id, Currency);
            return this.Execute<Entities.currency>(request);
        }

        public Task Delete(long CurrencyId)
        {
            RestRequest request = this.RequestForDelete("currencies", CurrencyId);
            return this.Execute<Entities.currency>(request);
        }

        public Task Delete(Entities.currency Currency)
        {
            return this.Delete((long)Currency.id);
        }

        public Task<List<long>> GetIds()
        {
            RestRequest request = this.RequestForGet("currencies", null, "prestashop");
            return this.ExecuteForGetIds<List<long>>(request, "currency");
        }
        
        /// <summary>
        /// More information about filtering: http://doc.prestashop.com/display/PS14/Chapter+8+-+Advanced+Use
        /// </summary>
        /// <param name="Filter">Example: key:name value:Apple</param>
        /// <param name="Sort">Field_ASC or Field_DESC. Example: name_ASC or name_DESC</param>
        /// <param name="Limit">Example: 5 limit to 5. 9,5 Only include the first 5 elements starting from the 10th element.</param>
        /// <returns></returns>
        public Task<List<Entities.currency>> GetByFilter(Dictionary<string, string> Filter, string Sort, string Limit)
        {
            RestRequest request = this.RequestForFilter("currencies", "full", Filter, Sort, Limit, "currencies");
            return this.ExecuteForFilter<List<Entities.currency>>(request);
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
            RestRequest request = this.RequestForFilter("currencies", "[id]", Filter, Sort, Limit, "currencies");
            List<PrestaSharp.Entities.FilterEntities.currency> aux = await this.Execute<List<PrestaSharp.Entities.FilterEntities.currency>>(request);
            return (List<long>)(from t in aux select t.id).ToList<long>();
        }

        /// <summary>
        /// Get all currencies.
        /// </summary>
        /// <returns>A list of currencies</returns>
        public Task<List<Entities.currency>> GetAll()
        {
            return this.GetByFilter(null, null, null);
        }
        
        /// <summary>
        /// Add a list of currencies.
        /// </summary>
        /// <param name="Currencies"></param>
        /// <returns></returns>
        public Task<List<Entities.currency>> AddList(List<Entities.currency> Currencies)
        {
            List<PrestaSharp.Entities.PrestaShopEntity> Entities = new List<PrestaSharp.Entities.PrestaShopEntity>();
            foreach (Entities.currency Currency in Currencies)
            {
                Currency.id = null;
                Entities.Add(Currency);
            }
            RestRequest request = this.RequestForAdd("currencies", Entities);
            return this.Execute<List<Entities.currency>>(request);
        }        
    }
}
