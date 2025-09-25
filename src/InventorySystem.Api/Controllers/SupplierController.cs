using AutoMapper;
using InventorySystem.Application.Features.Categories.Dtos;
using InventorySystem.Application.Features.Suppliers.Commands.Create;
using InventorySystem.Application.Features.Suppliers.Commands.Update;
using InventorySystem.Application.Features.Suppliers.Dtos;
using InventorySystem.Application.Features.Suppliers.Queries.GetSuppliers;
using InventorySystem.Application.Features.Suppliers.Queries.GetSuppliersCategories;
using InventorySystem.Application.Features.Suppliers.Queries.SupplierDetails;
using InventorySystem.Application.Features.Suppliers.Queries.SupplierLookUp;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Results;

namespace InventorySystem.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{ version:apiVersion}/[controller]")]
    [ApiController]
    public class SuppliersController(IMediator mediator, IMapper mapper) : ControllerBase
    {


        [HttpGet("look-up")]
        public async Task<ActionResult<SuccessWithData<List<SupplierLookupDto>>>> GetSuppliersLookUp()
        {
            var result = await mediator.Send(new SupplierLookUpQuery());


            return Ok(ApiResponseFactory.Success(result));
        }



        [HttpGet("{id}/categories")]
        public async Task<ActionResult<SuccessWithData<IEnumerable<CategoryDto>>>> GetSupplierCategories(int id)
        {
            var query = new GetSupplierCategoriesQuery(id);
            var result = await mediator.Send(query);
            return Ok(ApiResponseFactory.Success(result));
        }


        [HttpGet("{id}")]

        public async Task<ActionResult<SuccessWithData<SupplierDetailDto>>> GetSupplierById(int id)
        {

            var query = new GetSupplierDetailsQuery(id);

            var result = await mediator.Send(query);

            return Ok(ApiResponseFactory.Success(result));

        }

        [HttpPut("{id}")]

        public async Task<ActionResult<SuccessWithData<SupplierDto>>> UpdateSupplier(int id, [FromBody] UpdateSupplierCommand command)
        {

            var request = mapper.Map<UpdateSupplierRequest>(command);
            request.Id = id;

            var result = await mediator.Send(request);
            return Ok(ApiResponseFactory.Success(result));

        }


        [HttpGet]

        public async Task<ActionResult<SuccessWithData<PaginatedResult<SupplierDto>>>> GetAllSuppliers([FromQuery] GetSuppliersQuery query)
        {

            var result = await mediator.Send(query);
            return Ok(ApiResponseFactory.Success(result));
        }



        [HttpPost]

        public async Task<ActionResult<SuccessWithData<SupplierDto>>> CreateSupplier([FromBody] CreateSupplierCommand command)
        {

            var result = await mediator.Send(command);
            return Ok(ApiResponseFactory.Success(result));


        }

    }
}
