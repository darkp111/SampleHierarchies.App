using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleHierarchies.Interfaces.Data.Mammals
{
    public interface IBottlenoseWhale : IMammal
    {
        #region Interface members
        /// <summary>
        /// Is a whale have echolocation
        /// </summary>
        public bool IsEcholocation { get; set; }
        /// <summary>
        /// Description of echolocation
        /// </summary>
        public string EcholocationDescription { get; set; }
        /// <summary>
        /// Is a whale toothed
        /// </summary>
        public bool IsToothedWhale { get; set; }
        /// <summary>
        /// Description of whale toothed
        /// </summary>
        public string ToothedWhaleDescription { get; set; }
        /// <summary>
        /// How much a whale lives
        /// </summary>
        public int LongLifeSpan { get; set; }
        /// <summary>
        /// Is a whale sociable behavior
        /// </summary>
        public bool IsSociableBehavior { get; set; }
        /// <summary>
        /// Description of whale social behavior
        /// </summary>
        public string SociableBehaviorDescription { get; set; }
        /// <summary>
        /// Is a whale feed on squid
        /// </summary>
        public string FeedsOnSquid { get; set; }

        #endregion
    }
}
