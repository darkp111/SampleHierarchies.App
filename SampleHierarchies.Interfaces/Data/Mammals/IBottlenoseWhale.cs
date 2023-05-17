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
        public bool IsEcholocation { get; set; }
        public string EcholocationDescription { get; set; }
        public bool IsToothedWhale { get; set; }
        public string ToothedWhaleDescription { get; set; }
        public int LongLifeSpan { get; set; }
        public bool IsSociableBehavior { get; set; }
        public string SociableBehaviorDescription { get; set; }
        public string FeedsOnSquid { get; set; }

        #endregion
    }
}
