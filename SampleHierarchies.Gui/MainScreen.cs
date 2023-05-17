using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Linq;
using SampleHierarchies.Data;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Services;

namespace SampleHierarchies.Gui;

/// <summary>
/// Application main screen.
/// </summary>
public sealed class MainScreen : Screen
{
    #region Properties And Ctor

    /// <summary>
    /// Data service.
    /// </summary>
    private IDataService _dataService;
    private ISettings _settings;


    /// <summary>
    /// Animals screen.
    /// </summary>
    private AnimalsScreen _animalsScreen;

    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="dataService">Data service reference</param>
    /// <param name="animalsScreen">Animals screen</param>
    public MainScreen(
        IDataService dataService,
        AnimalsScreen animalsScreen,
        ISettings settings)
    {
        _dataService = dataService;
        _animalsScreen = animalsScreen;
        _settings = settings;
    }

    #endregion Properties And Ctor

    #region Public Methods

    /// <inheritdoc/>
    public override void Show()
    {
        while (true)
        {
            _settings = new Settings();
            _settings.ScreenColor = _settings.ReadValue("MainScreenColor", "White");
            Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), _settings.ScreenColor);

            Console.WriteLine();
            Console.WriteLine("Your available choices are:");
            Console.WriteLine("0. Exit");
            Console.WriteLine("1. Animals");
            Console.WriteLine("2. Create a new settings");
            Console.Write("Please enter your choice: ");

            string? choiceAsString = Console.ReadLine();

            // Validate choice
            try
            {
                if (choiceAsString is null)
                {
                    throw new ArgumentNullException(nameof(choiceAsString));
                }

                MainScreenChoices choice = (MainScreenChoices)Int32.Parse(choiceAsString);
                switch (choice)
                {
                    case MainScreenChoices.Animals:
                        _animalsScreen.Show();
                        break;

                    case MainScreenChoices.Settings:
                        EditJsonFile(Settings.FilePath);                        // TODO: implement
                        break;

                    case MainScreenChoices.Exit:
                        Console.WriteLine("Goodbye.");
                        return;
                }
            }
            catch
            {
                Console.WriteLine("Invalid choice. Try again.");
            }
        }
    }

    #endregion // Public Methods

    #region Private Methods

    public void EditJsonFile(string filePath)
    {
        try
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                dynamic jsonObject = JObject.Parse(json);

                Console.WriteLine("Enter the name of the property to edit:");
                string? propertyName = Console.ReadLine();

                if (jsonObject[propertyName] != null)
                {
                    Console.WriteLine($"Current value of '{propertyName}': {jsonObject[propertyName]}");
                    Console.WriteLine("Enter the new value:");
                    string? newValue = Console.ReadLine();

                    // Обновляем значение свойства
                    jsonObject[propertyName] = JToken.FromObject(newValue);

                    // Сохраняем изменения в файл
                    File.WriteAllText(filePath, jsonObject.ToString());

                    Console.WriteLine($"Value of property '{propertyName}' updated successfully.");
                }
                else
                {
                    Console.WriteLine($"Property '{propertyName}' not found in the JSON file.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while editing the JSON file: {ex.Message}");
        }
    }

    #endregion
}