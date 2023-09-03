using SampleHierarchies.Data;
using SampleHierarchies.Data.ScreenSettings;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Services;
using SampleHierarchies.Services;

namespace SampleHierarchies.Gui;

/// <summary>
/// Animals main screen.
/// </summary>
public sealed class AnimalsScreen : Screen
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
    private MammalsScreen _mammalsScreen;

    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="dataService">Data service reference</param>
    /// <param name="animalsScreen">Animals screen</param>
    public AnimalsScreen(
        IDataService dataService,
        MammalsScreen mammalsScreen,
        ISettings settings,
        IScreenDefinitionService screenDefinitionService,
        MenuManager menuManager)
    {
        _dataService = dataService;
        _mammalsScreen = mammalsScreen;
        _settings = settings;
        _screenDefinitionService = screenDefinitionService;
        _screenDefinitionJson = "AnimalsMenu.json";
        this.menuManager = menuManager;

    }

    #endregion Properties And Ctor

    #region Public Methods

    /// <inheritdoc/>
    public override void Show()
    { 
        while (true)
        {
            menuManager.AddToMenuPath("Animals Screen");
            Console.WriteLine("Current Menu Path: " + menuManager.GetCurrentMenuPath());
            _screenDefinitionService.PrintScreen(_currentScreenDefinition, 0, _screenDefinitionJson);
            string menuAsString = _screenDefinitionService.GetText(_currentScreenDefinition, 0, _screenDefinitionJson);
            string[] menuItems = _screenDefinitionService.SplitStringByNewLine(menuAsString);
            int selectedItemIndex = 0;

            //string? choiceAsString = Console.ReadLine();

            //// Validate choice
            //try
            //{
            //    if (choiceAsString is null)
            //    {
            //        throw new ArgumentNullException(nameof(choiceAsString));
            //    }

            //    AnimalsScreenChoices choice = (AnimalsScreenChoices)Int32.Parse(choiceAsString);
            //    switch (choice)
            //    {
            //        case AnimalsScreenChoices.Mammals:
            //            Console.Clear();
            //            _mammalsScreen.Show();
            //            break;

            //        case AnimalsScreenChoices.Read:
            //            ReadFromFile();
            //            break;

            //        case AnimalsScreenChoices.Save:
            //            SaveToFile();
            //            break;

            //        case AnimalsScreenChoices.Exit:
            //            // _screenDefinitionService.PrintScreen(_currentScreenDefinition, 1, _screenDefinitionJson);
            //            menuManager.RemoveLastFromMenuPath();
            //            Console.WriteLine("Returning to " + menuManager.GetCurrentMenuPath());
            //            Console.Clear();
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
                                AnimalsScreenChoices choice = (AnimalsScreenChoices)selectedItemIndex - 1;
                                switch (choice)
                                {
                                    case AnimalsScreenChoices.Mammals:
                                        Console.Clear();
                                        _mammalsScreen.Show();
                                        break;

                                    case AnimalsScreenChoices.Read:
                                        ReadFromFile();
                                        break;

                                    case AnimalsScreenChoices.Save:
                                        SaveToFile();
                                        break;

                                    case AnimalsScreenChoices.Exit:
                                        _screenDefinitionService.PrintScreen(_currentScreenDefinition, 1, _screenDefinitionJson);
                                        menuManager.RemoveLastFromMenuPath();
                                        Console.WriteLine("Returning to " + menuManager.GetCurrentMenuPath());
                                        Console.Clear();
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

    /// <summary>
    /// Save to file.
    /// </summary>
    private void SaveToFile()
    {
        try
        {
            _screenDefinitionService.PrintScreen(_currentScreenDefinition, 3, _screenDefinitionJson);
            var fileName = Console.ReadLine();
            if (fileName is null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }
            _dataService.Write(fileName);
            Console.WriteLine("Data saving to: '{0}' was successful.", fileName);
        }
        catch
        {
            _screenDefinitionService.PrintScreen(_currentScreenDefinition, 4, _screenDefinitionJson);
        }
    }

    /// <summary>
    /// Read data from file.
    /// </summary>
    private void ReadFromFile()
    {
        try
        {
            _screenDefinitionService.PrintScreen(_currentScreenDefinition, 5, _screenDefinitionJson);
            var fileName = Console.ReadLine();
            if (fileName is null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }
            _dataService.Write(fileName);
            Console.WriteLine("Data reading from: '{0}' was successful.", fileName);
        }
        catch
        {
            _screenDefinitionService.PrintScreen(_currentScreenDefinition, 6, _screenDefinitionJson);
        }
    }
    #endregion // Private Methods
}
