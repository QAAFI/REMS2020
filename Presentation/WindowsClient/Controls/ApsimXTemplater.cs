using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rems.Infrastructure;
using Models.Core;
using LamarCodeGeneration.Util;
using System.Reflection;
using System.Collections;
using System.IO;
using Models.Core.ApsimFile;

namespace WindowsClient.Controls
{
    public partial class ApsimXTemplater : UserControl
    {
        public ApsimXTemplater()
        {
            InitializeComponent();

            modelView.AfterSelect += UpdateGridView;
            propertiesGrid.CellBeginEdit += OnCellBeginEdit;
            propertiesGrid.CellEndEdit += OnCellEdit;
        }

        private void OnCellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            var cell = propertiesGrid[e.ColumnIndex, e.RowIndex];
            cell.Tag = cell.Value;
        }

        private void OnCellEdit(object sender, DataGridViewCellEventArgs e)
        {
            var cell = propertiesGrid[e.ColumnIndex, e.RowIndex];

            try
            {  
                var info = propertiesGrid[0, e.RowIndex].Tag as PropertyInfo;

                var model = ((ApsimNode)modelView.SelectedNode).Model;

                if (cell.Value is null)
                {
                    info.SetValue(model, null);
                }
                else if (info.PropertyType.IsArray)
                {
                    var type = info.PropertyType.GetElementType();

                    var items = cell.Value
                        .ToString()
                        .Split(',')
                        .Select(s => Convert.ChangeType(s, type))
                        .ToArray();

                    var array = Array.CreateInstance(type, items.Length);
                    Array.Copy(items, array, items.Length);
                    info.SetValue(model, array);
                }
                else
                {
                    info.SetValue(model, Convert.ChangeType(cell.Value, info.PropertyType));
                }
            }
            catch (Exception error)
            {
                cell.Value = cell.Tag;
                MessageBox.Show(error.Message);
            }            
        }

        private void OnLoadClicked(object sender, EventArgs e)
        {
            using (var open = new OpenFileDialog())
            {
                //open.InitialDirectory = folder;
                open.Filter = "JSON (*.json)|*.json";

                if (open.ShowDialog() == DialogResult.OK)
                {
                    var model = JsonTools.LoadJson<IModel>(open.FileName);
                    LoadModelView(model);
                }
            }
        }

        private void LoadModelView(IModel model)
        {
            modelView.Nodes.Clear();
            var node = new ApsimNode(model);
            node.ExpandAll();
            modelView.Nodes.Add(node);
            Refresh();
        }

        private void OnSaveClicked(object sender, EventArgs e)
        {
            var node = modelView.TopNode as ApsimNode;
            File.WriteAllText("test.json", FileFormat.WriteToString(node.Model));
            
        }

        private void UpdateGridView(object sender, EventArgs e)
        {
            propertiesGrid.Rows.Clear();

            if (!(modelView.SelectedNode is ApsimNode node)) return;

            var properties = node.Model.GetType().GetProperties();

            int i = 0;
            foreach (var info in properties)
            {
                if (!info.HasAttribute<Models.Core.DescriptionAttribute>()) continue;
                if (info.Name == "Drainablemm") continue; // Required due to bug in Apsim models

                var value = info.GetValue(node.Model);
                AddRow(i, info, value);
                i++;
            }
        }

        private void AddRow(int index, PropertyInfo info, object value)
        {
            var row = new DataGridViewRow();

            var infocell = new DataGridViewTextBoxCell()
            {
                Value = info.Name,
                Tag = info
            };

            row.Cells.Add(infocell);
            row.Cells.Add(GetREMS());
            row.Cells.Add(ParseValue(value));

            propertiesGrid.Rows.Insert(index, row);
        }

        private DataGridViewTextBoxCell GetREMS()
        {
            // TODO: Work this out
            return new DataGridViewTextBoxCell();
        }

        private DataGridViewTextBoxCell ParseValue(object value)
        {
            var cell = new DataGridViewTextBoxCell();

            cell.Value = value;

            if (value is IEnumerable array)
            {
                cell.Value = string.Join(",", array.Cast<object>());
            }

            return cell;
        }
    }

    public class ApsimNode : TreeNode
    {
        public IModel Model { get; set; }

        public Dictionary<string, string> Map { get; }

        public ApsimNode(IModel model) : base(model.Name)
        {
            Model = model;
            Map = new Dictionary<string, string>();

            foreach (var child in model.Children)
            {
                var node = new ApsimNode(child);
                Nodes.Add(node);
            }
        }
    }
}
