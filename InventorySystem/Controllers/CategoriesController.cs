using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Features.Categories.Commands.Create;
using Project.Application.Features.Categories.Commands.Delete;
using Project.Application.Features.Categories.Commands.Update;
using Project.Application.Features.Categories.Dtos;
using Project.Application.Features.Categories.Queries.CategoryLookUp;
using Project.Application.Features.Categories.Queries.GetCategory;
using Shared;

namespace InventorySystem.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{ version:apiVersion}/[controller]")]
    [ApiController]
    public class CategoriesController(IMediator mediator) : ControllerBase
    {





        /// <summary>
        /// Creates a new product category.
        /// </summary>
        /// <remarks>
        /// Only Admin and Manager roles can access this endpoint.
        /// </remarks>


        [HttpPost]

        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<SuccessWithData<CategoryDto>>> CreateCategory(CreateCategoryCommand command)

        {

            var result = await mediator.Send(command);
            return Ok(ApiResponseFactory.Success(result));

        }



        /// <summary>
        /// Gets details of a category by its ID.
        /// </summary>
        /// <remarks>
        /// Accessible by Admin and Manager.
        /// </remarks>

        [Authorize(Roles = "Admin,Manager")]

        [HttpGet("id/{id}")]




        public async Task<ActionResult<SuccessWithData<CategoryDetailsDto>>> GetCategoryWithId([FromRoute] int id)
        {

            var query = new GetCategoryByIdQuery(id);
            var result = await mediator.Send(query);


            return Ok(ApiResponseFactory.Success(result));
        }


        /// <summary>
        /// Gets a list of categories with optional name filtering.
        /// </summary>
        /// <remarks>
        /// Accessible by Admin and Manager.
        /// </remarks>

        [HttpGet]
        [Authorize(Roles = "Admin,Manager")]



        public async Task<ActionResult<SuccessWithData<IEnumerable<CategoryDto>>>> GetCategories([FromQuery] GetCategoryByNameQuery query)
        {


            var result = await mediator.Send(query);


            return Ok(ApiResponseFactory.Success(result));

        }





        /// <summary>
        /// Gets a lightweight list of categories for dropdowns/lookups.
        /// </summary>
        /// <remarks>
        /// Accessible by all roles (Admin, Manager, Sales, Warehouse).
        /// </remarks>

        [HttpGet("look-up")]
        [Authorize(Roles = "Admin,Manager,Salesperson,Warehouse")]

        public async Task<ActionResult<SuccessWithData<List<CategoryDto>>>> GetCategoryLookUp()
        {
            var result = await mediator.Send(new GetCategoryLookUpQuery());


            return Ok(ApiResponseFactory.Success(result));
        }


        /// <summary>
        /// Updates a category by its ID.
        /// </summary>
        /// <remarks>
        /// Only Admin and Manager roles can access this endpoint.
        /// </remarks>

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<SuccessWithData<CategoryDetailsDto>>> UpdateCategory(int id, [FromBody] UpdateCategoryCommand command)
        {



            var result = await mediator.Send(new
                UpdateCategoryRequest
            { Description = command.Description, Id = id, Name = command.Name });


            return Ok(ApiResponseFactory.Success(result));
        }


        /// <summary>
        /// Deletes a category by its ID.
        /// </summary>
        /// <remarks>
        /// Only Admins can delete categories.
        /// </remarks>

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]


        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {

            var result = await mediator.Send(new DeleteCategoryCommand(id));


            return NoContent();
        }

    }


}
