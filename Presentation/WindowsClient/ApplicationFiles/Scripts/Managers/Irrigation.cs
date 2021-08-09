using Models.Interfaces;
using System;
using Models.Core;
using System.Collections.Generic;
using System.Text;
using Models.Soils;
using Models.PMF;
using Models;
using System.Xml.Serialization;
using APSIM.Shared.Utilities;

namespace Models
{
    [Serializable]
    public class Script : Model
    {
        [Link] private Zone zone;
        [Link] private Irrigation irrigation;
        [Link] private Soil soil;
        [Link] private IPhysical soilPhysical;
        [Link] private ISoilWater waterBalance;
        public double FASW { get; set; }
        public double WaterDeficit { get; set; }


        [Description(\"Crop to irrigate\")]
        public IPlant Crop { get; set; }

        [Description(\"Auto irrigation on?\")]
        public bool AutoIrrigationOn { get; set; }

        [Description(\"Threshold fraction available water (0-1)\")]
        public double FASWThreshold { get; set; }

        [Description(\"Soil depth (mm) to which fraction available water is calculated\")]
        public double FASWDepth { get; set; }

        [Description(\"Minimum weeks between irrigations\")]
        public double weeks { get; set; }

        [Description(\"Minimum days after sowing for first irrigation\")]
        public int afterSowing { get; set; }

        private double irrigationGap = 0;    // gap between irrigations

        [EventSubscribe(\"DoManagement\")]
        private void OnDoManagement(object sender, EventArgs e)
        {
            if (AutoIrrigationOn && Crop.IsAlive)
            {
                irrigationGap += 1;                // increment gap between irrigations
                CalculateFASW();                // calc FASW and WaterDeficit
                if ((FASW < FASWThreshold) && (irrigationGap >= weeks * 7))
                {
                    irrigation.Apply(WaterDeficit, depth: 0);
                    irrigationGap = 0;            // reset
                }
            }
            else
            {
                irrigationGap = weeks * 7 - afterSowing - 1;    // allow irrigation a number of days after it becomes alive/sown
            }
        }

        // Calculate the fraction of the potential available sw
        // Calculate the deficit amount from DUL
        private void CalculateFASW()
        {
            double[] LL15 = MathUtilities.Multiply(soilPhysical.LL15, soilPhysical.Thickness);
            double[] DUL = MathUtilities.Multiply(soilPhysical.DUL, soilPhysical.Thickness);

            int nlayr = GetLayerIndex(FASWDepth);
            double cumdep = MathUtilities.Sum(soilPhysical.Thickness, 0, nlayr, 0.0);    // tricky function that sums up to before nlayr

            double part_layer = MathUtilities.Divide((FASWDepth - cumdep), soilPhysical.Thickness[nlayr], 0.0);

            // note that results may be strange if swdep < ll15
            double avail_sw = (MathUtilities.Sum(waterBalance.SWmm, 0, nlayr, 0.0) + part_layer * waterBalance.SWmm[nlayr])
                            - (MathUtilities.Sum(LL15, 0, nlayr, 0.0) + part_layer * LL15[nlayr]);

            double pot_avail_sw = (MathUtilities.Sum(DUL, 0, nlayr, 0.0) + part_layer * DUL[nlayr])
                                - (MathUtilities.Sum(LL15, 0, nlayr, 0.0) + part_layer * LL15[nlayr]);

            FASW = MathUtilities.Divide(avail_sw, pot_avail_sw, 0.0);
            WaterDeficit = MathUtilities.Constrain(pot_avail_sw - avail_sw, 0.0, 100000);
        }

        // Get index of the layer that has this depth in it 
        private int GetLayerIndex(double pointDepth)
        {
            double[] cumThickness = soilPhysical.ThicknessCumulative;
            int layerIdx = 0;
            while ((layerIdx < cumThickness.Length) && (pointDepth > cumThickness[layerIdx]))
            {
                layerIdx += 1;
            }

            return layerIdx;
        }
    }
}
