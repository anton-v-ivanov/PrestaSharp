using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Bukimedia.PrestaSharp.Factories
{
    public class GroupFactory : RestSharpFactory
    {
        public GroupFactory(string BaseUrl, string Account, string SecretKey)
            : base(BaseUrl, Account, SecretKey)
        {
        }

        public Task<Entities.group> Get(long GroupId)
        {
            RestRequest request = this.RequestForGet("groups", GroupId, "group");
            return this.Execute<Entities.group>(request);
        }

        public async Task<Entities.group> Add(Entities.group Group)
        {
            long? idAux = Group.id;
            Group.id = null;
            List<PrestaSharp.Entities.PrestaShopEntity> Entities = new List<PrestaSharp.Entities.PrestaShopEntity>();
            Entities.Add(Group);
            RestRequest request = this.RequestForAdd("groups", Entities);
            Entities.group aux = await this.Execute<Entities.group>(request);
            Group.id = idAux;
            return await this.Get((long)aux.id);
        }

        public Task Update(Entities.group Group)
        {
            RestRequest request = this.RequestForUpdate("groups", Group.id, Group);
            return this.Execute<Entities.group>(request);
        }

        public Task Delete(long GroupId)
        {
            RestRequest request = this.RequestForDelete("groups", GroupId);
            return this.Execute<Entities.group>(request);
        }

        public Task Delete(Entities.group Group)
        {
            return this.Delete((long)Group.id);
        }

        public Task<List<long>> GetIds()
        {
            RestRequest request = this.RequestForGet("groups", null, "prestashop");
            return this.ExecuteForGetIds<List<long>>(request, "group");
        }
        
        /// <summary>
        /// More information about filtering: http://doc.prestashop.com/display/PS14/Chapter+8+-+Advanced+Use
        /// </summary>
        /// <param name="Filter">Example: key:name value:Apple</param>
        /// <param name="Sort">Field_ASC or Field_DESC. Example: name_ASC or name_DESC</param>
        /// <param name="Limit">Example: 5 limit to 5. 9,5 Only include the first 5 elements starting from the 10th element.</param>
        /// <returns></returns>
        public Task<List<Entities.group>> GetByFilter(Dictionary<string, string> Filter, string Sort, string Limit)
        {
            RestRequest request = this.RequestForFilter("groups", "full", Filter, Sort, Limit, "groups");
            return this.ExecuteForFilter<List<Entities.group>>(request);
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
            RestRequest request = this.RequestForFilter("groups", "[id]", Filter, Sort, Limit, "groups");
            List<PrestaSharp.Entities.FilterEntities.group> aux = await this.Execute<List<PrestaSharp.Entities.FilterEntities.group>>(request);
            return (List<long>)(from t in aux select t.id).ToList<long>();
        }

        /// <summary>
        /// Get all groups.
        /// </summary>
        /// <returns>A list of groups</returns>
        public Task<List<Entities.group>> GetAll()
        {
            return this.GetByFilter(null, null, null);
        }

        /// <summary>
        /// Add a list of groups.
        /// </summary>
        /// <param name="Groups"></param>
        /// <returns></returns>
        public Task<List<Entities.group>> AddList(List<Entities.group> Groups)
        {
            List<PrestaSharp.Entities.PrestaShopEntity> Entities = new List<PrestaSharp.Entities.PrestaShopEntity>();
            foreach (Entities.group Group in Groups)
            {
                Group.id = null;
                Entities.Add(Group);
            }
            RestRequest request = this.RequestForAdd("groups", Entities);
            return this.Execute<List<Entities.group>>(request);
        }
    }
}
