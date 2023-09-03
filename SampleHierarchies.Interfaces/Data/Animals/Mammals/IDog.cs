namespace SampleHierarchies.Interfaces.Data.Animals.Mammals;

/// <summary>
/// Interface depicting a dog.
/// </summary>
public interface IDog : IMammal
{
    #region Interface Members
    /// <summary>
    /// Breed of dog.
    /// </summary>
    string Breed { get; set; }

    #endregion // Interface Members
}
