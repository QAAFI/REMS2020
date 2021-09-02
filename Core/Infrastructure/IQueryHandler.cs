using MediatR;
using Rems.Application.Common.Interfaces;
using System;
using System.Threading.Tasks;

namespace Rems.Infrastructure
{
    public interface IQueryHandler : IConfirmer
    {
        Task<T> Query<T>(IRequest<T> request);
    }
}
