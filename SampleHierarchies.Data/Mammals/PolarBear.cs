using SampleHierarchies.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SampleHierarchies.Interfaces.Data.Animals.Mammals;

namespace SampleHierarchies.Data.Mammals
{
    public class PolarBear : MammalBase, IPolarBear
    {
        #region Public Methods

        /// <inheritdoc/>
        public override void Display()
        {
            Console.WriteLine($"My name is: {Name}, my age is: {Age}, I have {TypeOfFur} fur and I am a {KindOf} bear, " +
                $"My paws size is: {LargePaws}, My diet is: {TypeOfDiet}, Semi-Aquatic: {SemiAquaticDescribe}, Sence of smell: {ExcellentSenseOfSmell}");
        }

        #endregion

        #region Ctor and Properties

        public string KindOf { get; set; }
        public string TypeOfFur { get; set; }
        public string LargePaws { get; set; }
        public string TypeOfDiet { get; set; }
        public bool IsSemiAquatic { get; set; }
        public string SemiAquaticDescribe { get; set; }
        public string ExcellentSenseOfSmell { get; set; }

        public PolarBear(string name, int age, string kindOf, string typeOfFur, string largePaws, string typeOfDiet, bool isSemiAquatic, string semiAquaticDescribe, string excellentSenseOfSmell) : base(name, age, MammalSpecies.PolarBear)
        {
            KindOf = kindOf;
            TypeOfFur = typeOfFur;
            LargePaws = largePaws;
            TypeOfDiet = typeOfDiet;
            IsSemiAquatic = isSemiAquatic;
            SemiAquaticDescribe = semiAquaticDescribe;
            ExcellentSenseOfSmell = excellentSenseOfSmell;
        }
        #endregion
    }
}
