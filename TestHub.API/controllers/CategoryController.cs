using Application.dtos.respondsDto;
using Application.features.category.requests.commands;
using Application.features.category.requests.queries;
using Application.results.common;
using TestHub.API.responces;

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
    public async Task<ActionResult<List<CategoryDto>>> Get()
    {
        var response = await _mediator.Send(new GetCategoryListRequest());
        return BaseCommandResponse<List<CategoryDto>>.GetBaseCommandResponseMessage(response);
    }

    [HttpGet("{id:int}", Name = "GetCategory")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<CategoryDto>> GetCategory(int id)
    {
        var command = new GetCategoryDtoRequest { Id = id };
        var response = await _mediator.Send(command);
        return BaseCommandResponse<CategoryDto>.GetBaseCommandResponseMessage(response);
    }

    [HttpGet("getByTest/{id:int}", Name = "GetTestCategories")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<CategoryDto>>> GetCategoriesByTestId(int id)
    {
        var command = new GetCategoryListByTestIdRequest { Id = id };
        var response = await _mediator.Send(command);
        return BaseCommandResponse<List<CategoryDto>>.GetBaseCommandResponseMessage(response);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CategoryDto>> CreateCategory([FromBody] CategoryDto? categoryDto)
    {
        var command = new CreateCategoryCommand { CategoryDto = categoryDto };
        var response = await _mediator.Send(command);
        return BaseCommandResponse<CategoryDto>.GetBaseCommandResponseMessage(response);
    }

    [HttpDelete("{id:int}", Name = "DeleteCategory")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoryDto>> Delete(int id)
    {
        var command = new DeleteCategoryCommand { Id = id };
        var response = await _mediator.Send(command);
        return BaseCommandResponse<CategoryDto>.GetBaseCommandResponseMessage(response);
    }

    [HttpPut("{id:int}", Name = "UpdateCategory")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoryDto>> UpdateCategory(int id, CategoryDto? categoryDto)
    {
        var command = new UpdateCategoryCommand { Id = id, CategoryDto = categoryDto };
        var response = await _mediator.Send(command);
        return BaseCommandResponse<CategoryDto>.GetBaseCommandResponseMessage(response);
    }
}