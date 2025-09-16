using AutoMapper;
using InventorySystem.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using InventorySystem.Application.Features.Products.Commands.Create;
using InventorySystem.Application.Features.Products.Commands.Delete;
using InventorySystem.Application.Features.Products.Commands.Update;
using InventorySystem.Application.Features.Products.Dtos;
using InventorySystem.Application.Features.Products.queries.GetProducts;
using InventorySystem.Application.Features.Products.queries.GetProductsForSupplier;
using InventorySystem.Application.Features.Products.queries.GetSalesProducts;
using InventorySystem.Application.Features.Products.queries.ProductCreationData;
using Shared;
using Shared.Results;
using InventorySystem.Api.Dtos.Products;

namespace InventorySystem.Controllers
{

    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]

    [ApiController]
    public class ProductsController(IMediator mediator, IMapper mapper) : ControllerBase
    {





        /// <summary>
        /// Creates a new product with image upload support.
        /// Only Admins and Managers are allowed.
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]

        public async Task<ActionResult<SuccessWithData<ProductResultDto>>> CreateProduct([FromForm] CreateProductRequest request)
        {


            var imageDto = FileUploadHelper.ToFileUploadDto(request.Image);


            var command = new CreateProductCommand
            {
                Name = request.Name,
                Description = request.Description,
                Barcode = request.Barcode,
                Unit = request.Unit,
                CostPrice = request.CostPrice,
                SellingPrice = request.SellingPrice,
                MinimumStock = request.MinimumStock,
                CategoryId = request.CategoryId,
                QuantityInStock = request.QuantityInStock,
                Image = imageDto
            };

            var result = await mediator.Send(command);



            return Ok(ApiResponseFactory.Success(result));


        }


        /// <summary>
        /// Retrieves data required for creating a product (e.g. categories, units).
        /// </summary>
        [HttpGet("creation-data")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<SuccessWithData<ProductCreationDataDto>>> GetProductCreationData()
        {
            var result = await mediator.Send(new GetProductCreationDataQuery());


            return Ok(ApiResponseFactory.Success(result));
        }




        /// <summary>
        /// Updates an existing product.
        /// Only Admins and Managers are allowed.
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Manager")]

        public async Task<ActionResult<SuccessWithData<ProductResultDto>>> UpdateProduct(int id, [FromBody] UpdateProductRequest command)
        {

            var request = mapper.Map<UpdateProductCommand>(command);

            request.Id = id;

            var result = await mediator.Send(request);


            return Ok(ApiResponseFactory.Success(result));
        }





        /// <summary>
        /// Retrieves all products with pagination and filtering support.
        /// Accessible by Admins, Managers, Salespersons, and WareHouse
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin,Manager,Salesperson,WareHouse")]

        public async Task<ActionResult<SuccessWithData<PaginatedResult<ProductResultDto>>>> GetAllProducts([FromQuery] GetProductsQuery query)
        {


            var result = await mediator.Send(query);


            return Ok(ApiResponseFactory.Success(result));
        }


        [HttpGet("purchase-lookup")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<SuccessWithData<PaginatedResult<ProductPurchaseOrderLookUpDto>>>> GetPurchaseProductsLookup([FromQuery] GetPurchaseProductsLookupQuery query)
        {
            var result = await mediator.Send(query);
            return Ok(ApiResponseFactory.Success(result));
        }



        /// <summary>
        /// Soft deletes a product by its ID (marks as inactive).
        /// Only Admins are allowed.
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SoftDeleteProduct(int id)
        {
            await mediator.Send(new DeleteProductCommand(id));
            return NoContent(); // 204 No Content is standard for delete
        }


        /// <summary>
        /// Returns a paginated list of products for sales order dropdowns.
        /// Accessible by Admins, Managers, and Salespersons.
        /// </summary>
        [HttpGet("sales-lookup")]
        [Authorize(Roles = "Admin,Manager,Salesperson")]
        public async Task<ActionResult<SuccessWithData<PaginatedResult<ProductSalesLookupDto>>>> GetSalesProductsLookup([FromQuery] GetSalesProductsLookupQuery query)
        {
            var result = await mediator.Send(query);
            return Ok(ApiResponseFactory.Success(result));
        }


    }
}
