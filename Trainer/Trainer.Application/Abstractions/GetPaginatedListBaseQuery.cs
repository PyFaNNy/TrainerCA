namespace Trainer.Application.Abstractions;

using MediatR;

public class GetPaginatedListBaseQuery<T> : IRequest<T>
{
    public int PageIndex
    {
        get; protected set;
    }

    public int PageSize
    {
        get; protected set;
    }

    public int Skip
    {
        get
        {
            return this.PageSize * (this.PageIndex - 1);
        }
    }

    protected GetPaginatedListBaseQuery(int? pageIndex, int? pageSize)
    {
        this.PageIndex = this.GetValue(pageIndex,1);
        this.PageSize = this.GetValue(pageSize, 10);
    }

    private int GetValue(int? value, int defaultValue)
    {
        return value == null || value < 1 ? defaultValue : value.Value;
    }
}
