using HepsiApi.Application.Interfaces.AutoMapper;
using HepsiApi.Application.Interfaces.UnitOfWorks;
using HepsiApi.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HepsiApi.Application.Features.Products.Command.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapperApp mapperApp;

        public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IMapperApp mapperApp)
        {
            this.unitOfWork = unitOfWork;
            this.mapperApp = mapperApp;
        }
        public async Task Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product= await unitOfWork.GetReadRepository<Product>().GetAsync(x=>x.Id==request.Id && !x.IsDeleted);

            var map=mapperApp.Map<Product, UpdateProductCommandRequest>(request);

            var productCategories= await unitOfWork.GetReadRepository<ProductCategory>()
                .GetAllAsync(x=>x.ProductId==product.Id);

            await unitOfWork.GetWriteRepository<ProductCategory>()
                .DeleteRangeAsync(productCategories);


            foreach (var categoryId in request.CategoryIds)
                await unitOfWork.GetWriteRepository<ProductCategory>()
                    .AddAsync(new() { CategoryId=categoryId, ProductId= product.Id});

            await unitOfWork.GetWriteRepository<Product>().UpdateAsync(map);
            await unitOfWork.SaveAsync();

        }
    }
}
