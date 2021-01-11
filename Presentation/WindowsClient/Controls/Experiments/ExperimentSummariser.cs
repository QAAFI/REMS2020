using MediatR;
using Rems.Application.CQRS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsClient.Controls.Experiments
{
    public partial class ExperimentSummariser : UserControl
    {
        

        public event Func<object, Task<object>> Query;
        private async Task<T> InvokeQuery<T>(IRequest<T> query) => (T)await Query(query);

        public ExperimentSummariser()
        {
            InitializeComponent();
        }
        
        public async Task GetSummary(int id)
        {
            var query = new ExperimentSummary() { ExperimentId = id };

            var experiment = await InvokeQuery(query);

            descriptionBox.Text = experiment["Description"];
            //designBox.Text = experiment["Design"];
            cropBox.Content = experiment["Crop"];
            fieldBox.Content = experiment["Field"];
            metBox.Content = experiment["Met"];
            repsBox.Content = experiment["Reps"];
            ratingBox.Content = experiment["Rating"];
            startBox.Content = experiment["Start"];
            endBox.Content = experiment["End"];

            var items = experiment["List"].Split('\n');
            researchersBox.Items.Clear();
            researchersBox.Items.AddRange(items);

            notesBox.Text = experiment["Notes"];

            var sowing = await InvokeQuery(new SowingSummary() { ExperimentId = id });
            sowingMethodBox.Content = sowing["Method"];
            sowingDateBox.Content = sowing["Date"];
            sowingDepthBox.Content = sowing["Depth"];
            sowingRowBox.Content = sowing["Row"];
            sowingPopBox.Content = sowing["Pop"];
        }
    }
}
