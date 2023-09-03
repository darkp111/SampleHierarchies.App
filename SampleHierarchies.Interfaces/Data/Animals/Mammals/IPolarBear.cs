using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleHierarchies.Interfaces.Data.Animals.Mammals
{
    public interface IPolarBear : IMammal
    {
        #region Interface Members
        /// <summary>
        /// Kind of Bear
        /// </summary>
        public string KindOf { get; set; }
        /// <summary>
        /// Type of fur
        /// </summary>
        public string TypeOfFur { get; set; }
        /// <summary>
        /// Large of Paws
        /// </summary>
        public string LargePaws { get; set; }
        /// <summary>
        /// Type of diet
        /// </summary>
        public string TypeOfDiet { get; set; }
        /// <summary>
        /// Is Semi-aquatic
        /// </summary>
        public bool IsSemiAquatic { get; set; }
        /// <summary>
        /// Describe of Semi-Aquatic
        /// </summary>
        public string SemiAquaticDescribe { get; set; }
        /// <summary>
        /// DescribeOfSenseOfSmell
        /// </summary>
        public string ExcellentSenseOfSmell { get; set; }

        #endregion
    }
}
