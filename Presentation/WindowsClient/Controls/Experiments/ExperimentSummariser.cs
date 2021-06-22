using Rems.Application.CQRS;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsClient.Models;

namespace WindowsClient.Controls
{
    public partial class ExperimentSummariser : UserControl
    {
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

            var experiment = await QueryManager.Request(query);

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

            var sowing = await QueryManager.Request(new SowingSummary() { ExperimentId = id });
            sowingMethodBox.Content = sowing["Method"];
            sowingDateBox.Content = sowing["Date"];
            sowingDepthBox.Content = sowing["Depth"];
            sowingRowBox.Content = sowing["Row"];
            sowingPopBox.Content = sowing["Pop"];
        }
    }
}
