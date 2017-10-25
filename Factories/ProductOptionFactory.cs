using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bukimedia.PrestaSharp.Factories
{
    public class ProductOptionFactory : RestSharpFactory
    {
        public ProductOptionFactory(string BaseUrl, string Account, string SecretKey)
            : base(BaseUrl, Account, SecretKey)
        {
        }

        public Task<Entities.product_option> Get(long ProductOptionValueId)
        {
            RestRequest request = this.RequestForGet("product_options", ProductOptionValueId, "product_option");
            return this.Execute<Entities.product_option>(request);
        }

        public async Task<Entities.product_option> Add(Entities.product_option ProductOption)
        {
            long? idAux = ProductOption.id;
            ProductOption.id = null;
            List<PrestaSharp.Entities.PrestaShopEntity> Entities = new List<PrestaSharp.Entities.PrestaShopEntity>();
            Entities.Add(ProductOption);
            RestRequest request = this.RequestForAdd("product_options", Entities);
            Entities.product_option aux = await this.Execute<Entities.product_option>(request);
            ProductOption.id = idAux;
            return await this.Get((long)aux.id);
        }

        public Task Update(Entities.product_option ProductOption)
        {
            RestRequest request = this.RequestForUpdate("product_options", ProductOption.id, ProductOption);
            return this.Execute<Entities.product_option>(request);
        }

        public Task Delete(long ProducOptiontId)
        {
            RestRequest request = this.RequestForDelete("product_options", ProducOptiontId);
            return this.Execute<Entities.product_option>(request);
        }

        public Task Delete(Entities.product_option ProductOption)
        {
            return this.Delete((long)ProductOption.id);
        }

        public Task<List<long>> GetIds()
        {
            RestRequest request = this.RequestForGet("product_options", null, "prestashop");
            return this.ExecuteForGetIds<List<long>>(request, "product_option");
        }

        /// <summary>
        /// More information about filtering: http://doc.prestashop.com/display/PS14/Chapter+8+-+Advanced+Use
        /// </summary>
        /// <param name="Filter">Example: key:name value:Apple</param>
        /// <param name="Sort">Field_ASC or Field_DESC. Example: name_ASC or name_DESC</param>
        /// <param name="Limit">Example: 5 limit to 5. 9,5 Only include the first 5 elements starting from the 10th element.</param>
        /// <returns></returns>
        public Task<List<Entities.product_option>> GetByFilter(Dictionary<string, string> Filter, string Sort, string Limit)
        {
            RestRequest request = this.RequestForFilter("product_options", "full", Filter, Sort, Limit, "product_options");
            return this.ExecuteForFilter<List<Entities.product_option>>(request);
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
            RestRequest request = this.RequestForFilter("product_options", "[id]", Filter, Sort, Limit, "product_options");
            List<PrestaSharp.Entities.FilterEntities.product_option> aux = await this.Execute<List<PrestaSharp.Entities.FilterEntities.product_option>>(request);
            return (List<long>)(from t in aux select t.id).ToList<long>();
        }

        /// <summary>
        /// Get all product_options.
        /// </summary>
        /// <returns>A list of product_options</returns>
        public Task<List<Entities.product_option>> GetAll()
        {
            return this.GetByFilter(null, null, null);
        }

        /// <summary>
        /// Add a list of product_options.
        /// </summary>
        /// <param name="ProductOptions"></param>
        /// <returns></returns>
        public Task<List<Entities.product_option>> AddList(List<Entities.product_option> ProductOptions)
        {
            List<PrestaSharp.Entities.PrestaShopEntity> Entities = new List<PrestaSharp.Entities.PrestaShopEntity>();
            foreach (Entities.product_option ProductOption in ProductOptions)
            {
                ProductOption.id = null;
                Entities.Add(ProductOption);
            }
            RestRequest request = this.RequestForAdd("product_options", Entities);
            return this.Execute<List<Entities.product_option>>(request);
        }
    }
}