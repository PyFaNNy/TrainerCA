namespace Trainer.Domain.Interfaces
{
    using System;

    public interface IBaseEntity
    {
        public Guid Id
        {
            get; set;
        }
    }
}
