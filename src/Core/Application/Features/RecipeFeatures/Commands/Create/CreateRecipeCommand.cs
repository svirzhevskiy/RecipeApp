using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Repositories;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.RecipeFeatures.Commands.Create
{
    public class CreateRecipeCommand : IRequest<Recipe>
    {
        public string Title { get; set; } = "";
        public string Instructions { get; set; } = "";
        public List<string> Ingredients { get; set; } = new();
        public DateTime CreatedAt { get; set; }
        public IFormFile File { get; set; } = null!;

        public class Handler : IRequestHandler<CreateRecipeCommand, Recipe>
        {
            private readonly IBaseRepository<Recipe> _repository;
            private readonly IMapper _mapper;

            public Handler(IBaseRepository<Recipe> repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<Recipe> Handle(
                CreateRecipeCommand request,
                CancellationToken cancellationToken)
            {
                var entity = _mapper.Map<Recipe>(request);

                var recipe = await _repository.Create(entity, cancellationToken);

                return _mapper.Map<Recipe>(recipe);
            }
        }
    }
}