using Bukimedia.PrestaSharp.Factories;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bukimedia.PrestaSharp.Entities;

namespace Bukimedia.PrestaSharp.Factories
{
    public class ImageFactory : RestSharpFactory
    {
        public ImageFactory(string BaseUrl, string Account, string SecretKey)
            : base(BaseUrl, Account, SecretKey)
        {
        }

        #region Protected methods

        protected Task<List<Entities.image>> GetAllImages(string Resource){
            RestRequest request = this.RequestForFilter("images/" + Resource, "full", null, null, null, "images");
            return this.Execute<List<Entities.image>>(request);
        }

        protected async Task<List<Entities.FilterEntities.declination>> GetImagesByInstance(string Resource, long Id)
        {
            RestRequest request = this.RequestForFilter("images/" + Resource + "/" + Id, "full", null, null, null, "image");
            List<Entities.FilterEntities.declination> Declinations = await this.Execute<List<Entities.FilterEntities.declination>>(request);
            List<Entities.FilterEntities.declination> AuxDeclinations = new List<Entities.FilterEntities.declination>();
            foreach (Entities.FilterEntities.declination Declination in Declinations)
            {
                if (!AuxDeclinations.Contains(Declination))
                {
                    AuxDeclinations.Add(Declination);
                }
            }
            return AuxDeclinations;
        }

        protected Task<image> AddImage(string Resource, long? Id, string ImagePath)
        {
            RestRequest request = RequestForAddImage(Resource, Id, ImagePath);
			return Execute<image>(request);
		}
        
        protected Task<image> AddImage(string Resource, long? Id, byte[] Image)
        {
            RestRequest request = this.RequestForAddImage(Resource, Id, Image);
			return Execute<image>(request);
		}

        protected async Task UpdateImage(string Resource, long? ResourceId, long? ImageId, string ImagePath)
        {
            await this.DeleteImage(Resource, ResourceId,ImageId);
            await this.AddImage(Resource, ResourceId, ImagePath);
        }

        protected Task DeleteImage(string Resource, long? ResourceId, long? ImageId)
        {
            RestRequest request = this.RequestForDeleteImage(Resource, ResourceId, ImageId);
            return this.Execute<Entities.image>(request);
        }

        #endregion Protected methods

        #region Manufacturer images

        public Task<List<Entities.image>> GetAllManufacturerImages()
        {
            return this.GetAllImages("manufacturers");
        }

        public Task AddManufacturerImage(long ManufacturerId, string ManufacturerImagePath)
        {
            return this.AddImage("manufacturers", ManufacturerId, ManufacturerImagePath);
        }
        
        public Task AddManufacturerImage(long ManufacturerId, byte[] ManufacturerImage)
        {
            return this.AddImage("manufacturers", ManufacturerId, ManufacturerImage);
        }

        public Task UpdateManufacturerImage(long ManufacturerId, string ManufacturerImagePath)
        {
            return this.UpdateImage("manufacturers", ManufacturerId, null, ManufacturerImagePath);
        }

        public Task DeleteManufacturerImage(long ManufacturerId)
        {
            return this.DeleteImage("manufacturers", ManufacturerId, null);
        }

        public Task<byte[]> GetManufacturerImage(long ManufacturerId, long ImageId)
        {
            RestRequest request = this.RequestForGet("images/manufacturers/" + ManufacturerId, ImageId, "");
            return this.ExecuteForImage(request);
        }

        #endregion Manufacturer images

        #region Product images

        public Task<List<image>> GetAllProductImages()
        {
            return this.GetAllImages("products");
        }

        public Task<List<Entities.FilterEntities.declination>> GetProductImages(long ProductId)
        {
            return this.GetImagesByInstance("products", ProductId);
        }

        public Task<image> AddProductImage(long ProductId, string ProductImagePath)
        {
            return AddImage("products", ProductId, ProductImagePath);
        }
        
        public Task<image> AddProductImage(long ProductId, byte[] ProductImage)
        {
            return AddImage("products", ProductId, ProductImage);
        }

        public Task UpdateProductImage(long ProductId, long ImageId, string ProductImagePath)
        {
            return this.UpdateImage("products", ProductId, ImageId, ProductImagePath);
        }

        public Task DeleteProductImage(long ProductId,long ImageId)
        {
            return this.DeleteImage("products", ProductId, ImageId);
        }

        public Task<byte[]> GetProductImage(long ProductId, long ImageId)
        {
            RestRequest request = this.RequestForGet("images/products/" + ProductId, ImageId, "");
            return this.ExecuteForImage(request);
        }

        #endregion Product images

        #region Category images

        public Task<List<Entities.image>> GetAllCategoryImages()
        {
            return this.GetAllImages("categories");
        }

        public Task AddCategoryImage(long? CategoryId, string CategoryImagePath)
        {
            return this.AddImage("categories", CategoryId, CategoryImagePath);
        }
        
        public Task AddCategoryImage(long? CategoryId, byte[] CategoryImage)
        {
            return this.AddImage("categories", CategoryId, CategoryImage);
        }

        public Task UpdateCategoryImage(long CategoryId, string CategoryImagePath)
        {
            return this.UpdateImage("categories", CategoryId, null, CategoryImagePath);
        }

        public Task DeleteCategoryImage(long CategoryID)
        {
            return this.DeleteImage("categories", CategoryID, null);
        }

        public Task<byte[]> GetCategoryImage(long CategoryId, long ImageId)
        {
            RestRequest request = this.RequestForGet("images/categories/" + CategoryId, ImageId, "");
            return this.ExecuteForImage(request);
        }

        #endregion Category images

    }
}
