using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;

using Microsoft.EntityFrameworkCore.Metadata;

namespace REMS
{
    using Context;
    using Context.Entities;

    public interface IREMSDatabase
    {
        bool IsOpen { get; }

        IEnumerable<IEntity> Entities { get; }

        IEnumerable<string> Tables { get; }

        DataTable this[string name] { get; }

        //BindingList<IEntity> this[string name] { get; }

        void Create(string file);

        void Open(string file);

        void ImportData(string file);

        void ExportData(string file);

        void Save();

        void Close();
    }
}
