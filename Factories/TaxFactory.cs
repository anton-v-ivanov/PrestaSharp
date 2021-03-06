using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Bukimedia.PrestaSharp.Factories
{
    public class TaxFactory : RestSharpFactory
    {
        public TaxFactory(string BaseUrl, string Account, string SecretKey)
            : base(BaseUrl, Account, SecretKey)
        {
        }

        public Task<Entities.tax> Get(long TaxId)
        {
        	
            RestRequest request = this.RequestForGet("taxes", TaxId, "tax");
            return this.Execute<Entities.tax>(request);
        }

        public async Task<Entities.tax> Add(Entities.tax Tax)
        {
            long? idAux = Tax.id;
            Tax.id = null;
            List<PrestaSharp.Entities.PrestaShopEntity> Entities = new List<PrestaSharp.Entities.PrestaShopEntity>();
            Entities.Add(Tax);
            RestRequest request = this.RequestForAdd("taxes", Entities);
            Entities.tax aux = await this.Execute<Entities.tax>(request);
            Tax.id = idAux;
            return await this.Get((long)aux.id);
        }

        public Task Update(Entities.tax Tax)
        {
            RestRequest request = this.RequestForUpdate("taxes", Tax.id, Tax);
            return this.Execute<Entities.tax>(request);
        }

        public Task Delete(long TaxId)
        {
            RestRequest request = this.RequestForDelete("taxes", TaxId);
            return this.Execute<Entities.tax>(request);
        }

        public Task Delete(Entities.tax Tax)
        {
            return this.Delete((long)Tax.id);
        }

        public Task<List<long>> GetIds()
        {
            RestRequest request = this.RequestForGet("taxes", null, "prestashop");
            return this.ExecuteForGetIds<List<long>>(request, "tax");
        }
        
        /// <summary>
        /// More information about filtering: http://doc.prestashop.com/display/PS14/Chapter+8+-+Advanced+Use
        /// </summary>
        /// <param name="Filter">Example: key:name value:Apple</param>
        /// <param name="Sort">Field_ASC or Field_DESC. Example: name_ASC or name_DESC</param>
        /// <param name="Limit">Example: 5 limit to 5. 9,5 Only include the first 5 elements starting from the 10th element.</param>
        /// <returns></returns>
        public Task<List<Entities.tax>> GetByFilter(Dictionary<string, string> Filter, string Sort, string Limit)
        {
        	
            RestRequest request = this.RequestForFilter("taxes", "full", Filter, Sort, Limit, "taxes");
            return this.ExecuteForFilter<List<Entities.tax>>(request);
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
            RestRequest request = this.RequestForFilter("taxes", "[id]", Filter, Sort, Limit, "taxes");
            List<PrestaSharp.Entities.FilterEntities.tax> aux = await this.Execute<List<PrestaSharp.Entities.FilterEntities.tax>>(request);
            return (from t in aux select t.id).ToList<long>();
        }

        /// <summary>
        /// Get all tax.
        /// </summary>
        /// <returns>A list of tax</returns>
        public Task<List<Entities.tax>> GetAll()
        {
            return this.GetByFilter(null, null, null);
        }
        
        /// <summary>
        /// Add a list of tax.
        /// </summary>
        /// <param name="TaxRuleGroups"></param>
        /// <returns></returns>
        public Task<List<Entities.tax>> AddList(List<Entities.tax> Taxes)
        {
            List<PrestaSharp.Entities.PrestaShopEntity> Entities = new List<PrestaSharp.Entities.PrestaShopEntity>();
            foreach (Entities.tax Tax in Taxes)
            {
                Tax.id = null;
                Entities.Add(Tax);
            }
            RestRequest request = this.RequestForAdd("taxes", Entities);
            return this.Execute<List<Entities.tax>>(request);
        }        
    }
}
