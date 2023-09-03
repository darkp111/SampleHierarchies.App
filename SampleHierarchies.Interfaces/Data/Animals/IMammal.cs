namespace SampleHierarchies.Interfaces.Data.Animals;

/// <summary>
/// Mammal interface.
/// </summary>
public interface IMammal : IAnimal
{
    #region Interface Members

    /// <summary>
    /// Species of the animal.
    /// </summary>
    MammalSpecies Species { get; set; }

    #endregion // Interface Members
}
