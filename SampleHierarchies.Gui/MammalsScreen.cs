using Newtonsoft.Json;
using SampleHierarchies.Data;
using SampleHierarchies.Data.ScreenSettings;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Services;
using SampleHierarchies.Services;

namespace SampleHierarchies.Gui;

/// <summary>
/// Mammals main screen.
/// </summary>
public sealed class MammalsScreen : Screen
{
    #region Properties And Ctor

    /// <summary>
    /// Animals screen.
    /// </summary>
    private DogsScreen _dogsScreen;
    private PolarBearScreen _polarBearScreen;
    private LionScreen _lionScreen;
    private BottlenoseWhaleScreen _bottlenoseWhaleScreen;
    private ISettings _settings;
    private IScreenDefinitionService _screenDefinitionService;
    private readonly ScreenDefinition? _currentScreenDefinition;
    private MenuManager menuManager;
    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="dataService">Data service reference</param>
    /// <param name="dogsScreen">Dogs screen</param>
    public MammalsScreen(DogsScreen dogsScreen, PolarBearScreen polarBearScreen, LionScreen lionScreen, BottlenoseWhaleScreen bottlenoseWhaleScreen, ISettings settings, IScreenDefinitionService screenDefinitionService, MenuManager menuManager)
    {
        _dogsScreen = dogsScreen;
        _polarBearScreen = polarBearScreen;
        _lionScreen = lionScreen;
        _bottlenoseWhaleScreen = bottlenoseWhaleScreen;
        _settings = settings;
        _screenDefinitionService = screenDefinitionService;
        _screenDefinitionJson = "MammalsMenu.json";
        this.menuManager = menuManager;
    }

    #endregion Properties And Ctor

    #region Public Methods

    /// <inheritdoc/>
    public override void Show()
    {
        while (true)
        {
            menuManager.AddToMenuPath("Mammals Screen");
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

            //    MammalsScreenChoices choice = (MammalsScreenChoices)Int32.Parse(choiceAsString);
            //    switch (choice)
            //    {
            //        case MammalsScreenChoices.Dogs:
            //            Console.Clear();
            //            _dogsScreen.Show();
            //            break;
            //        case MammalsScreenChoices.PolarBears:
            //            Console.Clear();
            //            _polarBearScreen.Show();
            //            break;
            //        case MammalsScreenChoices.Lions:
            //            Console.Clear();
            //            _lionScreen.Show();
            //            break;
            //        case MammalsScreenChoices.Whales:
            //            Console.Clear();
            //            _bottlenoseWhaleScreen.Show();
            //            break;
            //        case MammalsScreenChoices.Exit:
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

            while(true)
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
                                MammalsScreenChoices choice = (MammalsScreenChoices)selectedItemIndex - 1;
                                switch (choice)
                                {
                                    case MammalsScreenChoices.Dogs:
                                        Console.Clear();
                                        _dogsScreen.Show();
                                        break;
                                    case MammalsScreenChoices.PolarBears:
                                        Console.Clear();
                                        _polarBearScreen.Show();
                                        break;
                                    case MammalsScreenChoices.Lions:
                                        Console.Clear();
                                        _lionScreen.Show();
                                        break;
                                    case MammalsScreenChoices.Whales:
                                        Console.Clear();
                                        _bottlenoseWhaleScreen.Show();
                                        break;
                                    case MammalsScreenChoices.Exit:
                                        // _screenDefinitionService.PrintScreen(_currentScreenDefinition, 1, _screenDefinitionJson);
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
            }
        }
    }

    #endregion // Public Methods

    #region Private methods

    #endregion
}
