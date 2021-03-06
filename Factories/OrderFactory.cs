﻿using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bukimedia.PrestaSharp.Factories
{
    public class OrderFactory : RestSharpFactory
    {
        public OrderFactory(string BaseUrl, string Account, string SecretKey)
            : base(BaseUrl, Account, SecretKey)
        {
        }

        public Task<Entities.order> Get(long OrderId)
        {
            RestRequest request = this.RequestForGet("orders", OrderId, "order");
            return this.Execute<Entities.order>(request);
        }

        public async Task<Entities.order> Add(Entities.order Order)
        {
            long? idAux = Order.id;
            Order.id = null;
            List<PrestaSharp.Entities.PrestaShopEntity> Entities = new List<PrestaSharp.Entities.PrestaShopEntity>();
            Entities.Add(Order);
            RestRequest request = this.RequestForAdd("orders", Entities);
            Entities.order aux = await this.Execute<Entities.order>(request);
            Order.id = idAux;
            return await this.Get((long)aux.id);
        }

        public Task Update(Entities.order Order)
        {
            RestRequest request = this.RequestForUpdate("orders", Order.id, Order);
            return this.Execute<Entities.order>(request);
        }

        public Task Delete(long OrderId)
        {
            RestRequest request = this.RequestForDelete("orders", OrderId);
            return this.Execute<Entities.order>(request);
        }

        public Task Delete(Entities.order Order)
        {
            return this.Delete((long)Order.id);
        }

        public Task<List<long>> GetIds()
        {
            RestRequest request = this.RequestForGet("orders", null, "prestashop");
            return this.ExecuteForGetIds<List<long>>(request, "order");
        }
        
        /// <summary>
        /// More information about filtering: http://doc.prestashop.com/display/PS14/Chapter+8+-+Advanced+Use
        /// </summary>
        /// <param name="Filter">Example: key:name value:Apple</param>
        /// <param name="Sort">Field_ASC or Field_DESC. Example: name_ASC or name_DESC</param>
        /// <param name="Limit">Example: 5 limit to 5. 9,5 Only include the first 5 elements starting from the 10th element.</param>
        /// <returns></returns>
        public Task<List<Entities.order>> GetByFilter(Dictionary<string, string> Filter, string Sort, string Limit)
        {
            RestRequest request = this.RequestForFilter("orders", "full", Filter, Sort, Limit, "orders");
            return this.ExecuteForFilter<List<Entities.order>>(request);
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
            RestRequest request = this.RequestForFilter("orders", "[id]", Filter, Sort, Limit, "orders");
            List<PrestaSharp.Entities.FilterEntities.order> aux = await this.Execute<List<PrestaSharp.Entities.FilterEntities.order>>(request);
            return (List<long>)(from t in aux select t.id).ToList<long>();
        }

        /// <summary>
        /// Get all orders.
        /// </summary>
        /// <returns>A list of orders</returns>
        public Task<List<Entities.order>> GetAll()
        {
            return this.GetByFilter(null, null, null);
        }
        
        /// <summary>
        /// Add a list of orders.
        /// </summary>
        /// <param name="Orders"></param>
        /// <returns></returns>
        public Task<List<Entities.order>> AddList(List<Entities.order> Orders)
        {
            List<PrestaSharp.Entities.PrestaShopEntity> Entities = new List<PrestaSharp.Entities.PrestaShopEntity>();
            foreach (Entities.order Order in Orders)
            {
                Order.id = null;
                Entities.Add(Order);
            }
            RestRequest request = this.RequestForAdd("orders", Entities);
            return this.Execute<List<Entities.order>>(request);
        }        
    }
}
