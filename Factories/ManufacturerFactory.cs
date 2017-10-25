using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Bukimedia.PrestaSharp.Factories
{
    public class ManufacturerFactory : RestSharpFactory
    {
        public ManufacturerFactory(string BaseUrl, string Account, string SecretKey)
            : base(BaseUrl, Account, SecretKey)
        {
        }

        public Task<Entities.manufacturer> Get(long ManufacturerId)
        {
            RestRequest request = this.RequestForGet("manufacturers", ManufacturerId, "manufacturer");
            return this.Execute<Entities.manufacturer>(request);
        }

        public async Task<Entities.manufacturer> Add(Entities.manufacturer Manufacturer)
        {
            long? idAux = Manufacturer.id;
            Manufacturer.id = null;
            List<PrestaSharp.Entities.PrestaShopEntity> Entities = new List<PrestaSharp.Entities.PrestaShopEntity>();
            Entities.Add(Manufacturer);
            RestRequest request = this.RequestForAdd("manufacturers", Entities);
            Entities.manufacturer aux = await this.Execute<Entities.manufacturer>(request);
            Manufacturer.id = idAux;
            return await this.Get((long)aux.id);
        }

        public Task Update(Entities.manufacturer Manufacturer)
        {
            RestRequest request = this.RequestForUpdate("manufacturers", Manufacturer.id, Manufacturer);
            return this.Execute<Entities.manufacturer>(request);
        }

        public Task Delete(long ManufacturerId)
        {
            RestRequest request = this.RequestForDelete("manufacturers", ManufacturerId);
            return this.Execute<Entities.manufacturer>(request);
        }

        public Task Delete(Entities.manufacturer Manufacturer)
        {
            return this.Delete((long)Manufacturer.id);
        }

        public Task<List<long>> GetIds()
        {
            RestRequest request = this.RequestForGet("manufacturers", null, "prestashop");
            return this.ExecuteForGetIds<List<long>>(request, "manufacturer");
        }
        
        /// <summary>
        /// More information about filtering: http://doc.prestashop.com/display/PS14/Chapter+8+-+Advanced+Use
        /// </summary>
        /// <param name="Filter">Example: key:name value:Apple</param>
        /// <param name="Sort">Field_ASC or Field_DESC. Example: name_ASC or name_DESC</param>
        /// <param name="Limit">Example: 5 limit to 5. 9,5 Only include the first 5 elements starting from the 10th element.</param>
        /// <returns></returns>
        public Task<List<Entities.manufacturer>> GetByFilter(Dictionary<string, string> Filter, string Sort, string Limit)
        {
            RestRequest request = this.RequestForFilter("manufacturers", "full", Filter, Sort, Limit, "manufacturers");
            return this.ExecuteForFilter<List<Entities.manufacturer>>(request);
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
            RestRequest request = this.RequestForFilter("manufacturers", "[id]", Filter, Sort, Limit, "manufacturers");
            List<PrestaSharp.Entities.FilterEntities.manufacturer> aux = await this.Execute<List<PrestaSharp.Entities.FilterEntities.manufacturer>>(request);
            return (List<long>)(from t in aux select t.id).ToList<long>();
        }

        /// <summary>
        /// Get all manufacturers.
        /// </summary>
        /// <returns>A list of manufacturers</returns>
        public Task<List<Entities.manufacturer>> GetAll()
        {
            return this.GetByFilter(null, null, null);
        }
        
        /// <summary>
        /// Add a list of manufacturers.
        /// </summary>
        /// <param name="Manufacturers"></param>
        /// <returns></returns>
        public Task<List<Entities.manufacturer>> AddList(List<Entities.manufacturer> Manufacturers)
        {
            List<PrestaSharp.Entities.PrestaShopEntity> Entities = new List<PrestaSharp.Entities.PrestaShopEntity>();
            foreach (Entities.manufacturer Manufacturer in Manufacturers)
            {
                Manufacturer.id = null;
                Entities.Add(Manufacturer);
            }
            RestRequest request = this.RequestForAdd("manufacturers", Entities);
            return this.Execute<List<Entities.manufacturer>>(request);
        }        
    }
}
