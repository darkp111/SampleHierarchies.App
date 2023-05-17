using SampleHierarchies.Interfaces.Data.Mammals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleHierarchies.Data.Mammals
{
    public class Lion : MammalBase, ILion
    {
        #region Public Methods
        public override void Display()
        {
            Console.WriteLine($"My name is: {Name}, my age is: {Age}, Apex Predator: {ApexPredatorDescribe}, Puck Hunter: {PuckHunterDescribe}, " +
                $"Mane: {Mane}, Roaring Communication: {RoaringCommunicationDescribe}, Territory Defense: {TerritoryDefenseDescribe}");
        }

        #endregion

        #region Ctor and Properties
        public bool IsApexPredator { get; set; }
        public string? ApexPredatorDescribe { get; set; }
        public bool IsPuckHunter { get; set; }
        public string? PuckHunterDescribe { get; set; }
        public string? Mane { get; set; }
        public bool IsRoaringCommunication { get; set; }
        public string? RoaringCommunicationDescribe { get; set; }
        public bool IsTerritoryDefense { get; set; }
        public string? TerritoryDefenseDescribe { get; set; }

        public Lion(string name, int age, bool isApexPredator, string? apexPredatorDescribe, bool isPuckHunter, string? puckHunterDescribe,
            string? mane, bool isRoaringCommunication, string? roaringCommunicationDescribe, bool isTerritoryDefense, string? territoryDefenseDescribe) : base(name,age,MammalSpecies.Lion)
        {
            IsApexPredator = isApexPredator;
            ApexPredatorDescribe = apexPredatorDescribe;
            IsPuckHunter = isPuckHunter;
            PuckHunterDescribe = puckHunterDescribe;
            Mane = mane;
            IsRoaringCommunication = isRoaringCommunication;
            RoaringCommunicationDescribe = roaringCommunicationDescribe;
            IsTerritoryDefense = isTerritoryDefense;
            TerritoryDefenseDescribe = territoryDefenseDescribe;
        }


        #endregion

    }
}
