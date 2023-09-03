using SampleHierarchies.Interfaces.Services;

namespace SampleHierarchies.Services;

/// <summary>
/// Settings service.
/// </summary>
/// 


public class SettingsService : ISettingsService
{
    #region ISettings Implementation

    /// <inheritdoc/>
    /// 
    private readonly ISettings? settings;
    public ISettings? Read(string jsonPath)
    {
        ISettings? result = null;

        return result;
    }

    /// <inheritdoc/>
    public void Write(ISettings settings, string jsonPath)
    {
        
    }

    public ISettings? GetSettings()
    {
        return settings;
    }

    #endregion // ISettings Implementation
}