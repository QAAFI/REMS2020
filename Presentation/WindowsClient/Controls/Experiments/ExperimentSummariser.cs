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
        /// <summary>
        /// Occurs when data is requested from the mediator
        /// </summary>
        public event Func<object, Task<object>> Query;

        /// <summary>
        /// Safely handles a query
        /// </summary>
        /// <typeparam name="T">The type of data requested</typeparam>
        /// <param name="query">The request object</param>
        private async Task<T> InvokeQuery<T>(IRequest<T> query) => (T)await Query(query);

        public ExperimentSummariser()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// Requests data on the given experiment and displays the results
        /// </summary>
        /// <param name="id">The experiment ID</param>
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
