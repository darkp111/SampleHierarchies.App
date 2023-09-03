using SampleHierarchies.Interfaces.Data.Animals.Mammals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleHierarchies.Data.Mammals
{
    public class BottlenoseWhale : MammalBase, IBottlenoseWhale
    {
        #region Public Methods
        public override void Display()
        {
            Console.WriteLine($"My name is: {Name}, my age is: {Age}, Echolocation: {EcholocationDescription}, Toothed Whale: {ToothedWhaleDescription}, " +
                $"Long Life Span {LongLifeSpan}, Sociable Behavior: {SociableBehaviorDescription}, Feeds on squid: {FeedsOnSquid} ");
        }

        #endregion

        #region Ctor and Properties
        public bool IsEcholocation { get; set; }
        public string EcholocationDescription { get; set; }
        public bool IsToothedWhale { get; set; }
        public string ToothedWhaleDescription { get; set; }
        public int LongLifeSpan { get; set; }
        public bool IsSociableBehavior { get; set; }
        public string SociableBehaviorDescription { get; set; }
        public string FeedsOnSquid { get; set; }

        public BottlenoseWhale(string name, int age, bool isEcholocation, string echolocationDescription, bool isToothedWhale, string toothedWhaleDescription, int longLifeSpan,
             bool isSociableBehavior, string sociableBehaviorDescription, string feedsOnSquid) : base(name, age, MammalSpecies.BottlenoseWhale)
        {
            IsEcholocation = isEcholocation;
            EcholocationDescription = echolocationDescription;
            IsToothedWhale = isToothedWhale;
            ToothedWhaleDescription = toothedWhaleDescription;
            LongLifeSpan = longLifeSpan;
            IsSociableBehavior = isSociableBehavior;
            SociableBehaviorDescription = sociableBehaviorDescription;
            FeedsOnSquid = feedsOnSquid;
        }

        #endregion
    }
}
