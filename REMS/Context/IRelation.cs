using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;

namespace REMS
{
    public interface IRelation
    {
        void Build(ModelBuilder modelBuilder);
    }
}
