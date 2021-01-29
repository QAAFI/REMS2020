using ExcelDataReader;
using MediatR;
using Microsoft.EntityFrameworkCore.Internal;

using Rems.Application.Common.Extensions;
using Rems.Application.CQRS;
using Rems.Infrastructure.Excel;

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using WindowsClient.Models;

namespace WindowsClient.Models
{
    public interface IValidater
    {
        bool Valid { get; }

        void Validate();
    }

    public class TableValidater : IValidater
    {
        public bool Valid { get; protected set; } = true;

        protected DataNode table;

        public TableValidater(DataNode node)
        {
            table = node;
        }

        public virtual void Validate()
        { }

        protected bool ValidateColumn(int index, string name)
        {
            if (!(table.Nodes[index] is DataNode node))
                throw new Exception("No valid DataNode at the given index");

            if (!(node.Tag is DataColumn col))
                throw new Exception("DataNode not tagged with a DataColumn.");

            if (col.ColumnName == name)
            {
                node.ValidateAll();
                return true;
            }
            else
            {
                node.Advice = new List<RichText>
                {
                    new RichText
                    { Text = "Column does not match the expected data\n", Color = Color.Black },
                    new RichText
                    { Text = "Expected: " + name + "\n", Color = Color.Black },
                    new RichText
                    { Text = "Detected: " + col.ColumnName + "\n", Color = Color.Black }
                };

                node.UpdateState("Override", "Warning");
                return false;
            }
        }

        protected void SetAdvice()
        {
            table.Advice = new List<RichText>
            {
                new RichText
                { Text = "This table contains mismatched columns. \n\n" +
                "To enable import, please verify that the highlighted (!) columns contain the expected data. \n\n" +
                "If necessary, ignore any (X) columns.", Color = Color.Black }
            };
        }
    }

    public class DesignValidater : TableValidater
    {
        public DesignValidater(DataNode node) : base(node)
        { }

        public override void Validate()
        {
            Valid = true;

            Valid &= ValidateColumn(0, "ExperimentId");
            Valid &= ValidateColumn(1, "Treatment");
            Valid &= ValidateColumn(2, "Repetition");
            Valid &= ValidateColumn(3, "Plot");

            if (!Valid)
                SetAdvice();
            else
                table.ValidateAll();
        }
    }

    public class DataValidater : TableValidater
    {
        public DataValidater(DataNode node) : base(node)
        { }

        public override void Validate()
        {
            Valid = true;

            Valid &= ValidateColumn(0, "ExperimentId");
            Valid &= ValidateColumn(1, "Plot");
            Valid &= ValidateColumn(2, "Date");
            Valid &= ValidateColumn(3, "Sample");

            if (!Valid)
                SetAdvice();
            else
                table.ValidateAll();
        }
    }

    public class MetValidater : TableValidater
    {
        public MetValidater(DataNode node) : base(node)
        { }

        public override void Validate()
        {
            Valid = true;

            Valid &= ValidateColumn(0, "MetStation");
            Valid &= ValidateColumn(1, "Date");

            if (!Valid)
                SetAdvice();
            else
                table.ValidateAll();
        }
    }

    public class SoilLayerValidater : TableValidater
    {
        public SoilLayerValidater(DataNode node) : base(node)
        { }

        public override void Validate()
        {
            Valid = true;

            Valid &= ValidateColumn(0, "ExperimentId");
            Valid &= ValidateColumn(1, "Treatment");
            Valid &= ValidateColumn(2, "Date");
            Valid &= ValidateColumn(3, "DepthFrom");
            Valid &= ValidateColumn(4, "DepthTo");

            if (!Valid)
                SetAdvice();
            else
                table.ValidateAll();
        }
    }

    public class OperationsValidater : TableValidater
    {
        public OperationsValidater(DataNode node) : base(node)
        { }

        public override void Validate()
        {
            Valid = true;

            Valid &= ValidateColumn(0, "ExperimentId");
            Valid &= ValidateColumn(1, "Treatment");

            if (!Valid)
                SetAdvice();
            else
                table.ValidateAll();
        }
    }
}
