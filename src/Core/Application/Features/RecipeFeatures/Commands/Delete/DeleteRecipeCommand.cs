using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Repositories;
using Domain;
using MediatR;

namespace Application.Features.RecipeFeatures.Commands.Delete
{
    public class DeleteRecipeCommand : IRequest
    {
        public DeleteRecipeCommand(Guid id)
        {
            this.Id = id;
        }
        
        public Guid Id { get; set; }
        
        public class Handler : IRequestHandler<DeleteRecipeCommand, Unit>
        {
            private readonly IBaseRepository<Recipe> _repository;

            public Handler(IBaseRepository<Recipe> repository)
            {
                _repository = repository;
            }

            public async Task<Unit> Handle(
                DeleteRecipeCommand request, 
                CancellationToken cancellationToken)
            {
                var entity = await _repository.GetById(request.Id, cancellationToken);

                if (entity is null)
                {
                    throw AppException.RecipeNotFound;
                }

                await _repository.Delete(entity, cancellationToken);
                
                return Unit.Value;
            }
        }
    }
}