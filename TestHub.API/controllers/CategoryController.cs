using Application.features.category.requests.commands;
using Application.features.category.requests.queries;

namespace TestHub.API.controllers;

[Route("api/Category")]
[Produces("application/json")]
[ApiController]
public class CategoryController : Controller
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<List<CategoryDto>?> Get()
    {
        var response = await _mediator.Send(new GetCategoryListRequest());
        return response.ResponseObject;
    }

    [HttpGet("{id:int}", Name = "GetCategory")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<CategoryDto?> GetCategory(int id)
    {
        var command = new GetCategoryDtoRequest { Id = id };
        var response = await _mediator.Send(command);
        return response.ResponseObject;
    }
    
    [HttpGet("getByTest/{id:int}", Name = "GetTestCategories")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<List<CategoryDto>?> GetCategoriesByTestId(int id)
    {
        var command = new GetCategoryListByTestIdRequest { Id = id };
        var response = await _mediator.Send(command);
        return response.ResponseObject;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<BaseCommandResponse<CategoryDto>> CreateCategory([FromBody] CategoryDto? categoryDto)
    {
        var command = new CreateCategoryCommand { CategoryDto = categoryDto };
        var response = await _mediator.Send(command);
        return response;
    }
    
    [HttpDelete("{id:int}", Name = "DeleteCategory")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<BaseCommandResponse<CategoryDto>> Delete(int id)
    {
        var command = new DeleteCategoryCommand { Id = id };
        var response = await _mediator.Send(command);
        return response;
    }
    
    [HttpPut("{id:int}", Name = "UpdateCategory")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<BaseCommandResponse<CategoryDto>> UpdateCategory(int id, CategoryDto? categoryDto)
    {
        var command = new UpdateCategoryCommand { Id = id, CategoryDto = categoryDto };
        var response = await _mediator.Send(command);
        return response;
    }
}