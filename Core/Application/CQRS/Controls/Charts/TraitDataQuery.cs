using System;
using System.Linq;
using Rems.Application.Common;
using Rems.Application.Common.Interfaces;

namespace Rems.Application.CQRS
{
    /// <summary>
    /// Find all the data for a soil layer trait in a plot on the given date
    /// </summary>
    public abstract class TraitDataQuery<TX, TY> : ContextQuery<SeriesData<TX, TY>>
    {
        /// <summary>
        /// The trait
        /// </summary>
        public string TraitName { get; set; }
    }
}
