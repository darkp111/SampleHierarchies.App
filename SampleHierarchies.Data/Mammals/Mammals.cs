using SampleHierarchies.Interfaces.Data.Animals;
using SampleHierarchies.Interfaces.Data.Animals.Mammals;

namespace SampleHierarchies.Data.Mammals;

/// <summary>
/// Mammals collection.
/// </summary>
public class Mammals : IMammals
{
    #region IMammals Implementation

    /// <inheritdoc/>
    public List<IDog> Dogs { get; set; }

    public List<IPolarBear> PolarBears { get; set; }

    public List<ILion> Lions { get; set; }

    public List<IBottlenoseWhale> BottlenoseWhales { get; set; }

    #endregion // IMammals Implementation

    #region Ctors

    /// <summary>
    /// Default ctor.
    /// </summary>
    public Mammals()
    {
        Dogs = new List<IDog>();
        PolarBears = new List<IPolarBear>();
        Lions = new List<ILion>();
        BottlenoseWhales = new List<IBottlenoseWhale>();
    }

    #endregion // Ctors
}
