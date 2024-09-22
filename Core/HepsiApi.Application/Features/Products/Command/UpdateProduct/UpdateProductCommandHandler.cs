using HepsiApi.Application.Bases;
using HepsiApi.Application.Features.Products.Rules;
using HepsiApi.Application.Interfaces.AutoMapper;
using HepsiApi.Application.Interfaces.UnitOfWorks;
using HepsiApi.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HepsiApi.Application.Features.Products.Command.UpdateProduct
{
    public class UpdateProductCommandHandler :BaseHandler, IRequestHandler<UpdateProductCommandRequest,Unit>
    {


        public UpdateProductCommandHandler(IMapperApp mapperApp, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : base(mapperApp, unitOfWork, httpContextAccessor)
        {
   
        }
        public async Task<Unit> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product= await unitOfWork.GetReadRepository<Product>().GetAsync(x=>x.Id==request.Id && !x.IsDeleted);

            var map = mapper.Map<Product, UpdateProductCommandRequest>(request);

            var productCategories= await unitOfWork.GetReadRepository<ProductCategory>()
                .GetAllAsync(x=>x.ProductId==product.Id);

            await unitOfWork.GetWriteRepository<ProductCategory>()
                .DeleteRangeAsync(productCategories);


            foreach (var categoryId in request.CategoryIds)
                await unitOfWork.GetWriteRepository<ProductCategory>()
                    .AddAsync(new() { CategoryId=categoryId, ProductId= product.Id});

            await unitOfWork.GetWriteRepository<Product>().UpdateAsync(map);
            await unitOfWork.SaveAsync();
            return Unit.Value;
        }
    }
}
