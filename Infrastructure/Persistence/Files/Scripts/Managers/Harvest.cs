using System;
using Models.Core;
using Models.PMF;

namespace Models
{
    [Serializable]
    public class Script : Model
    {
        [Link]
        private Clock clock;

        [Link]
        private IPlant crop;

        [EventSubscribe("DoManagement")]
        private void OnDoCalculations(object sender, EventArgs e)
        {
            if (crop.IsReadyForHarvesting)
            {
                crop.Harvest();
                crop.EndCrop();
            }
        }
    }
}
