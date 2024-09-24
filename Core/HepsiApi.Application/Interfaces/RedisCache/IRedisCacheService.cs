using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HepsiApi.Application.Interfaces.RedisCache
{
    public interface IRedisCacheService
    {
        Task<T> GetAsync<T>(string key);
        //cache in ne kadar süre kalacağını gösteren değer=Expirationtime
        Task SetAsync<T>(string key,T value,DateTime? expirationTime=null);
    }
}
