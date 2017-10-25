﻿using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bukimedia.PrestaSharp.Factories
{
    public class SupplierFactory : RestSharpFactory
    {
        public SupplierFactory(string BaseUrl, string Account, string SecretKey)
            : base(BaseUrl, Account, SecretKey)
        {
        }

        public Task<Entities.supplier> Get(long SupplierId)
        {
            RestRequest request = this.RequestForGet("suppliers", SupplierId, "supplier");
            return this.Execute<Entities.supplier>(request);
        }

        public async Task<Entities.supplier> Add(Entities.supplier Supplier)
        {
            long? idAux = Supplier.id;
            Supplier.id = null;
            List<PrestaSharp.Entities.PrestaShopEntity> Entities = new List<PrestaSharp.Entities.PrestaShopEntity>();
            Entities.Add(Supplier);
            RestRequest request = this.RequestForAdd("suppliers", Entities);
            Entities.supplier aux = await this.Execute<Entities.supplier>(request);
            Supplier.id = idAux;
            return await this.Get((long)aux.id);
        }

        public Task Update(Entities.supplier Supplier)
        {
            RestRequest request = this.RequestForUpdate("suppliers", Supplier.id, Supplier);
            return this.Execute<Entities.category>(request);
        }

        public Task Delete(long SupplierId)
        {
            RestRequest request = this.RequestForDelete("suppliers", SupplierId);
            return this.Execute<Entities.supplier>(request);
        }

        public Task Delete(Entities.supplier Supplier)
        {
            return this.Delete((long)Supplier.id);
        }

        public Task<List<long>> GetIds()
        {
            RestRequest request = this.RequestForGet("suppliers", null, "prestashop");
            return this.ExecuteForGetIds<List<long>>(request, "supplier");
        }

        /// <summary>
        /// More information about filtering: http://doc.prestashop.com/display/PS14/Chapter+8+-+Advanced+Use
        /// </summary>
        /// <param name="Filter">Example: key:name value:Apple</param>
        /// <param name="Sort">Field_ASC or Field_DESC. Example: name_ASC or name_DESC</param>
        /// <param name="Limit">Example: 5 limit to 5. 9,5 Only include the first 5 elements starting from the 10th element.</param>
        /// <returns></returns>
        public Task<List<Entities.supplier>> GetByFilter(Dictionary<string, string> Filter, string Sort, string Limit)
        {
            RestRequest request = this.RequestForFilter("suppliers", "full", Filter, Sort, Limit, "suppliers");
            return this.ExecuteForFilter<List<Entities.supplier>>(request);
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
            RestRequest request = this.RequestForFilter("suppliers", "[id]", Filter, Sort, Limit, "suppliers");
            List<PrestaSharp.Entities.FilterEntities.supplier> aux = await this.Execute<List<PrestaSharp.Entities.FilterEntities.supplier>>(request);
            return (List<long>)(from t in aux select t.id).ToList<long>();
        }

        /// <summary>
        /// Get all suppliers.
        /// </summary>
        /// <returns>A list of suppliers</returns>
        public Task<List<Entities.supplier>> GetAll()
        {
            return this.GetByFilter(null, null, null);
        }

        /// <summary>
        /// Add a list of suppliers.
        /// </summary>
        /// <param name="Suppliers"></param>
        /// <returns></returns>
        public Task<List<Entities.supplier>> AddList(List<Entities.supplier> Suppliers)
        {
            List<PrestaSharp.Entities.PrestaShopEntity> Entities = new List<PrestaSharp.Entities.PrestaShopEntity>();
            foreach (Entities.supplier Supplier in Suppliers)
            {
                Supplier.id = null;
                Entities.Add(Supplier);
            }
            RestRequest request = this.RequestForAdd("suppliers", Entities);
            return this.Execute<List<Entities.supplier>>(request);
        }
    }
}
