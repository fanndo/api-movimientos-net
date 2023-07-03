using System;
using System.Threading.Tasks;

namespace ApiMovimientos.Repository
{
    public interface ICacheRepository
    {
        Task<T> GetAsync<T>(string key);
        Task SetAsync<T>(string key, T value, TimeSpan expiration);
    }
}
