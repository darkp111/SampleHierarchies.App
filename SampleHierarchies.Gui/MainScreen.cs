using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Linq;
using SampleHierarchies.Data;
using SampleHierarchies.Data.ScreenSettings;
using SampleHierarchies.Enums;
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
    private IScreenDefinitionService _screenDefinitionService;
    private readonly ScreenDefinition? _currentScreenDefinition;
    private MenuManager menuManager;
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
        ISettings settings,
        IScreenDefinitionService screenDefinitionService,
        MenuManager menuManager)
    {
        _dataService = dataService;
        _animalsScreen = animalsScreen;
        _settings = settings;
        _screenDefinitionService = screenDefinitionService;
        _screenDefinitionJson = "MainMenu.json";
        this.menuManager = menuManager;
    }

    #endregion Properties And Ctor

    #region Public Methods

    /// <inheritdoc/>
    public override void Show()
    {
        while (true)
        {
            menuManager.AddToMenuPath("Main Screen");
            Console.WriteLine("Current Menu Path: " + menuManager.GetCurrentMenuPath());
            _screenDefinitionService.PrintScreen(_currentScreenDefinition, 0, _screenDefinitionJson);
            string menuAsString = _screenDefinitionService.GetText(_currentScreenDefinition, 0, _screenDefinitionJson);
            string[] menuItems = _screenDefinitionService.SplitStringByNewLine(menuAsString);
            int selectedItemIndex = 0;


            



            //// Validate choice
            //try
            //{
            //    if (choiceAsString is null)
            //    {
            //        throw new ArgumentNullException(nameof(choiceAsString));
            //    }

            //    MainScreenChoices choice = (MainScreenChoices)Int32.Parse(choiceAsString);
            //    switch (choice)
            //    {
            //        case MainScreenChoices.Animals:
            //            Console.Clear();
            //            _animalsScreen.Show();
            //            break;

            //        case MainScreenChoices.Settings:
            //            EditJsonFile("settings.json");                       
            //            break;

            //        case MainScreenChoices.Exit:
            //            _screenDefinitionService.PrintScreen(_currentScreenDefinition, 1, _screenDefinitionJson);
            //            return;
            //    }
            //}
            //catch
            //{
            //    _screenDefinitionService.PrintScreen(_currentScreenDefinition, 2, _screenDefinitionJson);
            //}

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Current Menu Path: " + menuManager.GetCurrentMenuPath());
                for (int i = 0; i < menuItems.Length; i++)
                {
                    if (i == selectedItemIndex)
                    {
                        Console.ForegroundColor = _screenDefinitionService.GetForegroundColor(_currentScreenDefinition, 0, _screenDefinitionJson);
                        Console.BackgroundColor = _screenDefinitionService.GetBackgroundColor(_currentScreenDefinition, 0, _screenDefinitionJson);
                        Console.Write(menuItems[i]);
                    }
                    else
                    {
                        Console.ForegroundColor = _screenDefinitionService.GetBackgroundColor(_currentScreenDefinition, 0, _screenDefinitionJson);
                        Console.BackgroundColor = _screenDefinitionService.GetForegroundColor(_currentScreenDefinition, 0, _screenDefinitionJson);
                        Console.Write(menuItems[i]);
                    }

                    Console.WriteLine();
                }

                ConsoleKeyInfo keyInfo = Console.ReadKey();
                //string? choiceAsString = Console.ReadLine();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        selectedItemIndex = (selectedItemIndex - 1 + menuItems.Length) % menuItems.Length;
                        Thread.Sleep(150);
                        break;

                    case ConsoleKey.DownArrow:
                        selectedItemIndex = (selectedItemIndex + 1) % menuItems.Length;
                        Thread.Sleep(150);
                        break;

                    case ConsoleKey.Enter:
                        if (selectedItemIndex == menuItems.Length - 1)
                        {
                            return; // Вихід з програми
                        }
                        else
                        {
                            Console.WriteLine("\nYou choose: " + menuItems[selectedItemIndex]);
                            try
                            {
                                MainScreenChoices choice = (MainScreenChoices)selectedItemIndex - 1;
                                switch (choice)
                                {
                                    case MainScreenChoices.Animals:
                                        Console.Clear();
                                        _animalsScreen.Show();
                                        break;

                                    case MainScreenChoices.Settings:
                                        EditJsonFile("settings.json");
                                        break;

                                    case MainScreenChoices.Exit:
                                        _screenDefinitionService.PrintScreen(_currentScreenDefinition, 1, _screenDefinitionJson);
                                        return;
                                }
                            }
                            catch
                            {
                                Console.WriteLine("Invalid choice. Try again.");
                            }
                        }
                        break;
                }
                // Validate choice
            }
        }
    }



    #endregion // Public Methods

    #region Private Methods

    private void EditJsonFile(string filePath)
    {
        try
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                dynamic jsonObject = JObject.Parse(json);

                _screenDefinitionService.PrintScreen(_currentScreenDefinition, 3, _screenDefinitionJson);
                string? propertyName = Console.ReadLine();

                if (jsonObject[propertyName] != null)
                {
                    Console.WriteLine($"Current value of '{propertyName}': {jsonObject[propertyName]}");
                    _screenDefinitionService.PrintScreen(_currentScreenDefinition, 4, _screenDefinitionJson);
                    string? newValue = Console.ReadLine();

                   
                    jsonObject[propertyName] = JToken.FromObject(newValue);


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