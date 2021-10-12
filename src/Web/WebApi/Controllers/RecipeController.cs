using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Features.RecipeFeatures.Queries;

namespace WebApi.Controllers
{
    public class RecipeController : BaseController
    {
        private readonly IMediator _mediator;

        public RecipeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery]GetAllRecipesQuery request)
            => Ok(await _mediator.Send(request));
    }
}