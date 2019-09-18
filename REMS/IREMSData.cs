using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace REMS
{
    public interface IREMSDatabase
    {
        bool IsOpen { get; }

        List<string> Tables { get; }

        DataTable this[string name] { get; }

        void Create(string file);

        void Open(string file);

        void ImportData(string file);

        void Save();

        void Close();

    }
}
