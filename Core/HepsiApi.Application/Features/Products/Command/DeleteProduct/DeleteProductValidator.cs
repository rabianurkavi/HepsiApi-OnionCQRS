using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HepsiApi.Application.Features.Products.Command.DeleteProduct
{
    public class DeleteProductValidator:AbstractValidator<DeleteProductCommandRequest>
    {
        public DeleteProductValidator() 
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);
        }
    }
}
