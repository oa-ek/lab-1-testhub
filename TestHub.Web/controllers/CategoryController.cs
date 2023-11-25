using Application.common.models;
using Application.features.category;
using Application.features.category.queries.getCategoriesDto;
using Microsoft.AspNetCore.Mvc;
using Application.features.category.queries.getCategoriesWithPaginationQuery;
using Domain.entities;
using MediatR;
using TestHub.Web.controllers.common;

namespace TestHub.Web.controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : BaseController
    {
        [HttpGet(nameof(GetCategoriesWithPagination))]
        public async Task<IActionResult> GetCategoriesWithPagination([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var query = new GetCategoriesWithPaginationQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var result = await Mediator.Send(query);
            return Ok(result);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var query = new GetCategoriesDtoQuery();
    
            var result = await Mediator.Send<GetCategoriesDtoQuery, Result<IEnumerable<CategoryDto>>>(query);
    
            if (result != null && result.Succeeded)
            {
                return Ok(result.Value);
            }
            else
            {
                return BadRequest(result?.Errors ?? new[] { "An error occurred." });
            }
        }

    }
}