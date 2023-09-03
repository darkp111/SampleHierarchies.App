using SampleHierarchies.Enums;
using Newtonsoft.Json;

namespace SampleHierarchies.Interfaces.Data;

/// <summary>
/// Settings interface.
/// </summary>
public interface ISettings
{
    #region Interface Members

    /// <summary>
    /// Version of settings.
    /// </summary>
    public string Version { get; set; }

    public string? ScreenColor { get; set; }

    T ReadValue<T>(string propertyName, T defaultValue);
    // void SetFilePath(string filePath);

    #endregion // Interface Members
}

