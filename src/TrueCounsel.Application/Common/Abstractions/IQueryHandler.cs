using System.Threading;
using System.Threading.Tasks;

namespace TrueCounsel.Application.Common.Abstractions
{
    public interface IQueryHandler<TQuery, TResult>
    {
        Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken = default);
    }
}
