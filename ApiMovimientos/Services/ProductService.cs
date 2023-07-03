using ApiMovimientos.Config;
using ApiMovimientos.Contracts;
using ApiMovimientos.Model;
using ApiMovimientos.Repository;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiMovimientos.Services
{
    public class ProductService : IProductService
    {
        private readonly IMongoCollection<ProductDetails> _productCollection;
        //private readonly IAsyncPolicy<ProductDetails> _productCachePolicy;
        //private readonly IReadOnlyPolicyRegistry<string> _productCachePolicy;
        private readonly ICacheRepository _cacheRepository;



        //public ProductService(IOptions<DbSettings> databaseSetting, IReadOnlyPolicyRegistry<string> productCachePolicy)
        public ProductService(IOptions<DbSettings> databaseSetting, ICacheRepository cacheRepository)

        {
            var mongoClient = new MongoClient( databaseSetting.Value.ConnectionString );
            var mongoDatabase = mongoClient.GetDatabase(databaseSetting.Value.DatabaseName);
            _productCollection = mongoDatabase.GetCollection<ProductDetails>(databaseSetting.Value.ProductCollectionName);
            //_productCachePolicy = productCachePolicy;
            _cacheRepository = cacheRepository;
        }


        public async Task AddProductAsync(ProductDetails productDetails)
        {
            await _productCollection.InsertOneAsync(productDetails);
        }

        public async Task DeleteProductAsync(string productId)
        {
            await _productCollection.DeleteOneAsync(x => x.Id == productId);
        }

        public async Task<ProductDetails> GetProductDetailByIdAsync(string productId)
        {
            try
            {
                var cacheKey = $"{nameof(GetProductDetailByIdAsync)}-{productId}";
                var productDetailCached = await _cacheRepository.GetAsync<ProductDetails>(cacheKey);

                if (productDetailCached != null)
                {
                    return productDetailCached;
                }

                // Si el valor no está en caché, lo recuperamos de la fuente de datos
                var productDetail = await GetCached(productId);

                // Almacenamos el valor en caché por 10 minutos
                await _cacheRepository.SetAsync(cacheKey, productDetail, TimeSpan.FromMinutes(10));

                return productDetail;

                //var cachePolicy = _productCachePolicy.Get<IAsyncPolicy<ProductDetails>>(typeof(ProductDetails).ToString());

                //var key = $"{nameof(GetProductDetailByIdAsync)}-{productId}";
                //var context = new Context(key);
                //var cachedGetPerfilProductor = await cachePolicy.ExecuteAsync(x => GetCached(productId), context);

                //return cachedGetPerfilProductor;
            }
            catch (Exception ex)
            {
                ex.Data.Add("additional data", $"the additional data { productId}. Ex 3");
                throw;
            }
        }

        private async Task<ProductDetails> GetCached(string productId)
        {
            try
            {
                return await _productCollection.Find(x => x.Id == productId).FirstOrDefaultAsync();
            }
            catch (System.Exception ex)
            {
                var message = $"Error al intentar obtener perfil de asesor cuyo ClientID: {productId}.";
                Log.Fatal(ex, message);
                throw;
            }
        }


        public async Task<IList<ProductDetails>> ProductListAsync()
        {
            return await _productCollection.Find(_ => true).ToListAsync();
        }

        public async Task UpdateProductAsync(string productId, ProductDetails productDetails)
        {
            await _productCollection.ReplaceOneAsync(x => x.Id == productId, productDetails);

        }
    }
}
