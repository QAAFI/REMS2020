using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;

namespace REMS.Context
{
    public interface IEntity
    {
        void BuildModel(ModelBuilder modelBuilder);

        IEntity Create(object[] values, string[] names);

        bool CheckName(string name);
    }
}
