using SampleHierarchies.Enums;
using Newtonsoft.Json;

namespace SampleHierarchies.Interfaces.Services;

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

    /// <summary>
    /// Color of display
    /// </summary>
    public string? ScreenColor { get; set; }

    /// <summary>
    /// Method that helps us to read display color settings from json file 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="propertyName"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    T ReadValue<T>(string propertyName, T defaultValue);

    #endregion // Interface Members
}

