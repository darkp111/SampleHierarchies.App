using SampleHierarchies.Interfaces.Data.Animals.Mammals;

namespace SampleHierarchies.Interfaces.Data.Animals;

/// <summary>
/// Mammals collection.
/// </summary>
public interface IMammals
{
    #region Interface Members

    /// <summary>
    /// Dogs collection.
    /// </summary>
    List<IDog> Dogs { get; set; }
    /// <summary>
    /// Polar Bears collection
    /// </summary>
    List<IPolarBear> PolarBears { get; set; }
    /// <summary>
    /// Lions collection
    /// </summary>
    List<ILion> Lions { get; set; }
    /// <summary>
    /// Bottlenose whales collection
    /// </summary>
    List<IBottlenoseWhale> BottlenoseWhales { get; set; }
    #endregion // Interface Members
}
