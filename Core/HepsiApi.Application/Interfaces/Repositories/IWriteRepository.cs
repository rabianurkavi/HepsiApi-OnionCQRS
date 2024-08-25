using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HepsiApi.Application.Interfaces.Repositories
{
    public interface IWriteRepository<T> where T : class
    {
        /*
         * Task<int> ları kaldırma sebebim: saveasync ten sonra gerçekleşmesini istiyorum int kısmını saveasync kısmını unitofworkle erişeceğimden dolayı burada kullanmıyorum zaten unitofworkte
         * unitofwork yapısında kullanacağız ki required kısımlarını kaldıracağız.
         * */
        Task AddAsync(T entity);
        //herhangi bir hata olduğunda hiç birini eklemiyor.
        Task AddRangeAsync(IList<T> entities);
        Task<T> UpdateAsync(T entity);
        //id alsaydık eğer id si integer değilse T entity olarak alalım şimdilik
        Task DeleteAsync(T entity);
        Task DeleteRangeAsync(IList<T> entities);


    }
}
