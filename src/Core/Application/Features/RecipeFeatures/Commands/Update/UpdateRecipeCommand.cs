using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Features.RecipeFeatures.Queries;
using Application.Repositories;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Features.RecipeFeatures.Commands.Update
{
    public class UpdateRecipeCommand : IRequest<RecipeModel>
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = "";
        public string Instructions { get; set; } = "";
        public List<string> Ingredients { get; set; } = new();
        public DateTime CreatedAt { get; set; }
        public string Image { get; set; } = "";
        
        public class Handler : IRequestHandler<UpdateRecipeCommand, RecipeModel>
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

            public async Task<RecipeModel> Handle(
                UpdateRecipeCommand request,
                CancellationToken cancellationToken)
            {
                var entity = await _repository.GetById(request.Id, cancellationToken);

                if (entity is null)
                {
                    throw AppException.RecipeNotFound;
                }

                entity.Title = request.Title;
                entity.Instructions = request.Instructions;
                entity.Ingredients = request.Ingredients;
                entity.CreatedAt = request.CreatedAt;
                
                if (string.IsNullOrEmpty(request.Image))
                {
                    var oldImage = await _imageRepository.GetById(entity.ImageId, cancellationToken);

                    if (oldImage is not null)
                    {
                        oldImage.Content = Convert.FromBase64String(request.Image);
                        entity.Image = await _imageRepository.Update(oldImage, cancellationToken);
                    }
                    else
                    {
                        entity.Image = await _imageRepository.Create(new Image
                        {
                            Content = Convert.FromBase64String(request.Image)
                        }, cancellationToken);
                    }
                }

                entity = await _repository.Update(entity, cancellationToken);

                return _mapper.Map<RecipeModel>(entity);
            }
        }
    }
}