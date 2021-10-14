﻿using System.Threading;
using System.Threading.Tasks;
using Application.Features.RecipeFeatures.Commands.Create;
using Application.Features.RecipeFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Get(
            [FromQuery]GetAllRecipesQuery request,
            CancellationToken cancellationToken)
            => Ok(await _mediator.Send(request, cancellationToken));

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(
            CreateRecipeCommand command,
            CancellationToken cancellationToken)
            => Created("", await _mediator.Send(command, cancellationToken));
    }
}