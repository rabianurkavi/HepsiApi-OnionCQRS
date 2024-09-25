using HepsiApi.Application.Features.Brands.Commands.CreateBrand;
using HepsiApi.Application.Features.Brands.Queries.GetAllBrands;
using HepsiApi.Application.Features.Products.Command.CreateProduct;
using HepsiApi.Application.Features.Products.Command.DeleteProduct;
using HepsiApi.Application.Features.Products.Command.UpdateProduct;
using HepsiApi.Application.Features.Products.Queries.GetAllProducts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HepsiApi.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator mediator;
        public ProductController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllProducts()
        {
            var response = await mediator.Send(new GetAllProductsQueryRequest());

            return Ok(response);

        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductCommandRequest request)
        {

            await mediator.Send(request);

            return Ok();

        }
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductCommandRequest request)
        {

            await mediator.Send(request);

            return Ok();

        }
        [HttpPost]
        public async Task<IActionResult> DeleteProduct(DeleteProductCommandRequest request)
        {

            await mediator.Send(request);

            return Ok();

        }
        [HttpPost]
        public async Task<IActionResult> CreateBrand(CreateBrandCommandRequest request)
        {
            await mediator.Send(request);
            return Ok();

        }
        [HttpGet]
        public async Task<IActionResult> GetAllBrand()
        {
            var response=await mediator.Send(new GetAllBrandsQueryRequest());
            return Ok(response);

        }
    }
}
