using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleHierarchies.Interfaces.Data.Animals.Mammals
{
    public interface ILion : IMammal
    {
        #region Interface Members
        /// <summary>
        /// Is the lion the apex predator
        /// </summary>
        public bool IsApexPredator { get; set; }
        /// <summary>
        /// Description of lion apex predator
        /// </summary>
        public string? ApexPredatorDescribe { get; set; }
        /// <summary>
        /// Is the lion a puck hunter
        /// </summary>
        public bool IsPuckHunter { get; set; }
        /// <summary>
        /// Description of lion puck hunting
        /// </summary>
        public string? PuckHunterDescribe { get; set; }
        /// <summary>
        /// Description of lion mane
        /// </summary>
        public string? Mane { get; set; }
        /// <summary>
        /// Is lion communicate with roaring
        /// </summary>
        public bool IsRoaringCommunication { get; set; }
        /// <summary>
        /// Describe of lion roar communicate
        /// </summary>
        public string? RoaringCommunicationDescribe { get; set; }
        /// <summary>
        /// Is lion defense his territory
        /// </summary>
        public bool IsTerritoryDefense { get; set; }
        /// <summary>
        /// Description of how lion defense his territory
        /// </summary>
        public string? TerritoryDefenseDescribe { get; set; }
        #endregion
    }
}
