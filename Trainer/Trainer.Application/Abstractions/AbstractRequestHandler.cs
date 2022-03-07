using AutoMapper;
using MediatR;
using Trainer.Application.Interfaces;

namespace Trainer.Application.Abstractions
{
    public abstract class AbstractRequestHandler
    {
        protected IMediator Mediator
        {
            get;
        }

        protected ITrainerDbContext DbContext
        {
            get;
        }

        protected IMapper Mapper
        {
            get;
        }

        public AbstractRequestHandler(
            IMediator mediator,
            ITrainerDbContext dbContext,
            IMapper mapper)
        {
            this.Mediator = mediator;
            this.DbContext = dbContext;
            this.Mapper = mapper;
        }
    }
}
