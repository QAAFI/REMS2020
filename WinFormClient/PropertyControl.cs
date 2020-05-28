using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace WindowsClient
{
    public partial class PropertyControl : UserControl
    {
        public event EventHandler<PropertyChangedEventArgs> PropertyChanged;

        public string Table { get; set; }

        public string Property
        {
            get
            {
                return propertyLabel.Text;
            }
            set
            {
                propertyLabel.Text = value;
            }
        }

        public string Value
        {
            get
            {
                return valueBox.Text;
            }
            set
            {
                valueBox.Text = value;
            }
        }        

        public PropertyControl()
        {
            InitializeComponent();

            valueBox.LostFocus += ValueBoxTextChanged;
        }

        private void ValueBoxTextChanged(object sender, EventArgs e)
        {
            var args = new PropertyChangedEventArgs(Table, Property, Value);
            PropertyChanged?.Invoke(this, args);
        }
    }

    public class PropertyChangedEventArgs : EventArgs
    {
        public string TableName { get; private set; }

        public string PropertyName { get; private set; }

        public string NewValue { get; private set; }

        public PropertyChangedEventArgs(string table, string property, string value)
        {
            TableName = table;
            PropertyName = property;
            NewValue = value;
        }
    }
}

