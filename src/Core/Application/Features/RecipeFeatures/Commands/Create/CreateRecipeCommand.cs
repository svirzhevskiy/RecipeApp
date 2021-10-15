using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Repositories;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Features.RecipeFeatures.Commands.Create
{
    public class CreateRecipeCommand : IRequest<Recipe>
    {
        public string Title { get; set; } = "";
        public string Instructions { get; set; } = "";
        public List<string> Ingredients { get; set; } = new();
        public DateTime CreatedAt { get; set; }
        public string Image { get; set; } = "";

        public class Handler : IRequestHandler<CreateRecipeCommand, Recipe>
        {
            private readonly IBaseRepository<Recipe> _repository;
            private readonly IBaseRepository<Image> _imageRepository;
            private readonly IMapper _mapper;

            public Handler(IBaseRepository<Recipe> repository, IMapper mapper, IBaseRepository<Image> imageRepository)
            {
                _repository = repository;
                _mapper = mapper;
                _imageRepository = imageRepository;
            }

            public async Task<Recipe> Handle(
                CreateRecipeCommand request,
                CancellationToken cancellationToken)
            {
                var entity = _mapper.Map<Recipe>(request);

                if (string.IsNullOrEmpty(request.Image))
                {
                    var image = new Image { Content = Convert.FromBase64String(request.Image) };

                    entity.Image = await _imageRepository.Create(image, cancellationToken);
                }

                var recipe = await _repository.Create(entity, cancellationToken);

                return _mapper.Map<Recipe>(recipe);
            }
        }
    }
}