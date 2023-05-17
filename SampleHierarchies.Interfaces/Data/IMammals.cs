using SampleHierarchies.Interfaces.Data.Mammals;

namespace SampleHierarchies.Interfaces.Data;

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

    List<IPolarBear> PolarBears { get; set; }
    List<ILion> Lions { get; set; }

    List<IBottlenoseWhale> BottlenoseWhales { get; set; }
    #endregion // Interface Members
}
