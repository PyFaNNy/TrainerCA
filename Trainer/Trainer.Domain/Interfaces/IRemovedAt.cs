namespace Trainer.Domain.Interfaces
{
    using System;

    public interface IRemovedAt
    {
        DateTime? RemovedAt
        {
            get;
            set;
        }
    }
}

