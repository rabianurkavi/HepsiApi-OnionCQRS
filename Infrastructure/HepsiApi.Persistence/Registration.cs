using HepsiApi.Application.Interfaces.Repositories;
using HepsiApi.Persistence.Context;
using HepsiApi.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HepsiApi.Persistence
{
    public static class Registration
    {
        //core katmanındaki bir şeyi kullanmak için api de proje refaransı eklemeden bu yapıyı kullanabiliriz, mimariye göre core katmanı için apiye referans ekleyemeyiz.
        public static void AddPersistence(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
        }
    }
}
