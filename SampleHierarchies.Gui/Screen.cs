using SampleHierarchies.Data.ScreenSettings;
using SampleHierarchies.Services;

namespace SampleHierarchies.Gui;

/// <summary>
/// Abstract base class for a screen.
/// </summary>
public abstract class Screen
{
    #region Public Methods

    /// <summary>
    /// Show the screen.
    /// </summary>
    /// 
    public SettingsService? _settingsService;

    public string? _screenDefinitionJson;

    public virtual void Show()
    {
        Console.WriteLine("Showing screen");
    }

    #endregion // Public Methods
}
