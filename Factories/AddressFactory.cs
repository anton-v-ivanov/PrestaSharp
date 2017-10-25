using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Bukimedia.PrestaSharp.Entities.FilterEntities;

namespace Bukimedia.PrestaSharp.Factories
{
	public class AddressFactory : RestSharpFactory
	{
		public AddressFactory(string BaseUrl, string Account, string SecretKey)
            : base(BaseUrl, Account, SecretKey)
        {
        }

		public Task<Entities.address> Get(long AddressId)
        {
			RestRequest request = this.RequestForGet("addresses", AddressId, "address");
			return this.Execute<Entities.address>(request);
        }

		public async Task<Entities.address> Add(Entities.address Address)
        {
			long? idAux = Address.id;
			Address.id = null;
            List<PrestaSharp.Entities.PrestaShopEntity> Entities = new List<PrestaSharp.Entities.PrestaShopEntity>();
			Entities.Add(Address);
			RestRequest request = this.RequestForAdd("addresses", Entities);
			Entities.address aux = await this.Execute<Entities.address>(request);
			Address.id = idAux;
            return await this.Get((long)aux.id);
        }

		public Task Update(Entities.address Address)
        {
			RestRequest request = this.RequestForUpdate("addresses", Address.id, Address);
            return this.Execute<Entities.address>(request);
        }

		public Task Delete(long AddressId)
        {
			RestRequest request = this.RequestForDelete("addresses", AddressId);
            return this.Execute<Entities.address>(request);
        }

		public Task Delete(Entities.address Address)
        {
			return this.Delete((long)Address.id);
        }

        public Task<List<long>> GetIds()
        {
			RestRequest request = this.RequestForGet("addresses", null, "prestashop");
			return this.ExecuteForGetIds<List<long>>(request, "address");
        }
        
        /// <summary>
        /// More information about filtering: http://doc.prestashop.com/display/PS14/Chapter+8+-+Advanced+Use
        /// </summary>
        /// <param name="Filter">Example: key:name value:Apple</param>
        /// <param name="Sort">Field_ASC or Field_DESC. Example: name_ASC or name_DESC</param>
        /// <param name="Limit">Example: 5 limit to 5. 9,5 Only include the first 5 elements starting from the 10th element.</param>
        /// <returns></returns>
		public Task<List<Entities.address>> GetByFilter(Dictionary<string, string> Filter, string Sort, string Limit)
        {
			RestRequest request = this.RequestForFilter("addresses", "full", Filter, Sort, Limit, "addresses");
            return this.ExecuteForFilter<List<Entities.address>>(request);
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
            var request = this.RequestForFilter("addresses", "[id]", Filter, Sort, Limit, "addresses");
            var aux = await this.Execute<List<address>>(request);
            return (from t in aux select t.id).ToList();
        }

        /// <summary>
		/// Get all addresses.
        /// </summary>
		/// <returns>A list of addresses</returns>
        public Task<List<Entities.address>> GetAll()
        {
            return this.GetByFilter(null, null, null);
        }

        /// <summary>
		/// Add a list of addresses.
        /// </summary>
		/// <param name="Addresses"></param>
        /// <returns></returns>
		public Task<List<Entities.address>> AddList(List<Entities.address> Addresses)
        {
            var entities = new List<PrestaSharp.Entities.PrestaShopEntity>();
			foreach (Entities.address Address in Addresses)
            {
				Address.id = null;
				entities.Add(Address);
            }
			var request = this.RequestForAdd("addresses", entities);
			return this.Execute<List<Entities.address>>(request);
        }
	}
}
