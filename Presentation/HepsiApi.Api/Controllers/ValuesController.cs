using HepsiApi.Application.Interfaces.UnitOfWorks;
using HepsiApi.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace HepsiApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public ValuesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _unitOfWork.GetReadRepository<Product>().GetAllAsync());
        }
    }
}
