using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.RecipeFeatures.Commands.Create;
using Application.Features.RecipeFeatures.Commands.Delete;
using Application.Features.RecipeFeatures.Commands.Update;
using Application.Features.RecipeFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

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
        public async Task<IActionResult> Get(int page, int itemsOnPage,
            CancellationToken cancellationToken, string? searchString = "")
            => Ok(await _mediator.Send(
                new GetAllRecipesQuery { Page = page, ItemsOnPage = itemsOnPage, SearchString = searchString },
                cancellationToken));

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Create(
            CreateRecipeCommand command,
            CancellationToken cancellationToken)
            => Created("", await _mediator.Send(command, cancellationToken));

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(
            Guid id,
            CancellationToken cancellationToken) 
            => Ok(await _mediator.Send(new DeleteRecipeCommand(id), cancellationToken));

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(
            UpdateRecipeCommand command,
            CancellationToken cancellationToken)
            => Ok(await _mediator.Send(command, cancellationToken));
    }
}