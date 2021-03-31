using System;

namespace Rems.Application.Common.Interfaces
{
    public interface IRemsDbContextFactory
    {
        IRemsDbContext Create();
    }
}
