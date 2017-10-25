using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Bukimedia.PrestaSharp.Factories
{
    public class TagFactory : RestSharpFactory
    {
        public TagFactory(string BaseUrl, string Account, string SecretKey)
            : base(BaseUrl, Account, SecretKey)
        {
        }

        public Task<Entities.tag> Get(long TagId)
        {
            RestRequest request = this.RequestForGet("tags", TagId, "tag");
            return this.Execute<Entities.tag>(request);
        }

        public async Task<Entities.tag> Add(Entities.tag Tag)
        {
            long? idAux = Tag.id;
            Tag.id = null;
            List<PrestaSharp.Entities.PrestaShopEntity> Entities = new List<PrestaSharp.Entities.PrestaShopEntity>();
            Entities.Add(Tag);
            RestRequest request = this.RequestForAdd("tags", Entities);
            Entities.tag aux = await this.Execute<Entities.tag>(request);
            Tag.id = idAux;
            return await this.Get((long)aux.id);
        }
        
        public Task Update(Entities.tag Tag)
        {
            RestRequest request = this.RequestForUpdate("tags", Tag.id, Tag);
            return this.Execute<Entities.tag>(request);
        }

        public Task Delete(long TagId)
        {
            RestRequest request = this.RequestForDelete("tags", TagId);
            return this.Execute<Entities.tag>(request);
        }

        public Task Delete(Entities.tag Tag)
        {
            return this.Delete((long)Tag.id);
        }

        public Task<List<long>> GetIds()
        {
            RestRequest request = this.RequestForGet("tags", null, "prestashop");
            return this.ExecuteForGetIds<List<long>>(request, "tag");
        }

        /// <summary>
        /// More information about filtering: http://doc.prestashop.com/display/PS14/Chapter+8+-+Advanced+Use
        /// </summary>
        /// <param name="Filter">Example: key:name value:Apple</param>
        /// <param name="Sort">Field_ASC or Field_DESC. Example: name_ASC or name_DESC</param>
        /// <param name="Limit">Example: 5 limit to 5. 9,5 Only include the first 5 elements starting from the 10th element.</param>
        /// <returns></returns>
        public Task<List<Entities.tag>> GetByFilter(Dictionary<string, string> Filter, string Sort, string Limit)
        {
            RestRequest request = this.RequestForFilter("tags", "full", Filter, Sort, Limit, "tags");
            return this.ExecuteForFilter<List<Entities.tag>>(request);
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
            RestRequest request = this.RequestForFilter("tags", "[id]", Filter, Sort, Limit, "tags");
            List<PrestaSharp.Entities.FilterEntities.tag> aux = await this.Execute<List<PrestaSharp.Entities.FilterEntities.tag>>(request);
            return (List<long>)(from t in aux select t.id).ToList<long>();
        }

        /// <summary>
        /// Get all tags.
        /// </summary>
        /// <returns>A list of tags</returns>
        public Task<List<Entities.tag>> GetAll()
        {
            return this.GetByFilter(null, null, null);
        }

        /// <summary>
        /// Add a list of tags.
        /// </summary>
        /// <param name="Tags"></param>
        /// <returns></returns>
        public Task<List<Entities.tag>> AddList(List<Entities.tag> Tags)
        {
            List<PrestaSharp.Entities.PrestaShopEntity> Entities = new List<PrestaSharp.Entities.PrestaShopEntity>();
            foreach (Entities.tag Tag in Tags)
            {
                Tag.id = null;
                Entities.Add(Tag);
            }
            RestRequest request = this.RequestForAdd("tags", Entities);
            return this.Execute<List<Entities.tag>>(request);
        }
    }
}
