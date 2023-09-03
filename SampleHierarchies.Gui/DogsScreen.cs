using SampleHierarchies.Data;
using SampleHierarchies.Data.Mammals;
using SampleHierarchies.Data.ScreenSettings;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Services;

namespace SampleHierarchies.Gui;

/// <summary>
/// Mammals main screen.
/// </summary>
public sealed class DogsScreen : Screen
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
    /// Ctor.
    /// </summary>
    /// <param name="dataService">Data service reference</param>
    public DogsScreen(IDataService dataService, ISettings settings, IScreenDefinitionService screenDefinitionService, MenuManager menuManager)
    {
        _dataService = dataService;
        _settings = settings;
        _screenDefinitionService = screenDefinitionService;
        _screenDefinitionJson = "DogsMenu.json";
        this.menuManager = menuManager;

    }

    #endregion Properties And Ctor

    #region Public Methods

    /// <inheritdoc/>
    public override void Show()
    {
        while (true)
        {
            menuManager.AddToMenuPath("Dogs Screen");
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

            //    DogsScreenChoices choice = (DogsScreenChoices)Int32.Parse(choiceAsString);
            //    switch (choice)
            //    {
            //        case DogsScreenChoices.List:
            //            ListDogs();
            //            break;

            //        case DogsScreenChoices.Create:
            //            AddDog(); break;

            //        case DogsScreenChoices.Delete: 
            //            DeleteDog();
            //            break;

            //        case DogsScreenChoices.Modify:
            //            EditDogMain();
            //            break;

            //        case DogsScreenChoices.Exit:
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
                                DogsScreenChoices choice = (DogsScreenChoices)selectedItemIndex - 1;
                                switch (choice)
                                {
                                    case DogsScreenChoices.List:
                                        ListDogs();
                                        Console.ReadKey();
                                        break;

                                    case DogsScreenChoices.Create:
                                        AddDog();
                                        Console.ReadKey();
                                        break;

                                    case DogsScreenChoices.Delete:
                                        DeleteDog();
                                        Console.ReadKey();
                                        break;

                                    case DogsScreenChoices.Modify:
                                        EditDogMain();
                                        Console.ReadKey();
                                        break;

                                    case DogsScreenChoices.Exit:
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
            }
        }
    }

    #endregion // Public Methods

    #region Private Methods

    /// <summary>
    /// List all dogs.
    /// </summary>
    private void ListDogs()
    {
        Console.WriteLine();
        if (_dataService?.Animals?.Mammals?.Dogs is not null &&
            _dataService.Animals.Mammals.Dogs.Count > 0)
        {
            _screenDefinitionService.PrintScreen(_currentScreenDefinition, 3, _screenDefinitionJson);
            int i = 1;
            foreach (Dog dog in _dataService.Animals.Mammals.Dogs)
            {
                Console.Write($"Dog number {i}, ");
                dog.Display();
                i++;
            }
        }
        else
        {
            _screenDefinitionService.PrintScreen(_currentScreenDefinition, 4, _screenDefinitionJson);
        }
    }

    /// <summary>
    /// Add a dog.
    /// </summary>
    private void AddDog()
    {
        try
        {
            Dog dog = AddEditDog();
            _dataService?.Animals?.Mammals?.Dogs?.Add(dog);
            Console.WriteLine("Dog with name: {0} has been added to a list of dogs", dog.Name);
        }
        catch
        {
            _screenDefinitionService.PrintScreen(_currentScreenDefinition, 5, _screenDefinitionJson);
        }
    }

    /// <summary>
    /// Deletes a dog.
    /// </summary>
    private void DeleteDog()
    {
        try
        {
            _screenDefinitionService.PrintScreen(_currentScreenDefinition, 6, _screenDefinitionJson);
            string? name = Console.ReadLine();
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            Dog? dog = (Dog?)(_dataService?.Animals?.Mammals?.Dogs
                ?.FirstOrDefault(d => d is not null && string.Equals(d.Name, name)));
            if (dog is not null)
            {
                _dataService?.Animals?.Mammals?.Dogs?.Remove(dog);
                Console.WriteLine("Dog with name: {0} has been deleted from a list of dogs", dog.Name);
            }
            else
            {
                _screenDefinitionService.PrintScreen(_currentScreenDefinition, 7, _screenDefinitionJson);
            }
        }
        catch
        {
            _screenDefinitionService.PrintScreen(_currentScreenDefinition, 8, _screenDefinitionJson);
        }
    }

    /// <summary>
    /// Edits an existing dog after choice made.
    /// </summary>
    private void EditDogMain()
    {
        try
        {
            _screenDefinitionService.PrintScreen(_currentScreenDefinition, 9, _screenDefinitionJson);
            string? name = Console.ReadLine();
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            Dog? dog = (Dog?)(_dataService?.Animals?.Mammals?.Dogs
                ?.FirstOrDefault(d => d is not null && string.Equals(d.Name, name)));
            if (dog is not null)
            {
                Dog dogEdited = AddEditDog();
                dog.Copy(dogEdited);
                _screenDefinitionService.PrintScreen(_currentScreenDefinition, 10, _screenDefinitionJson);
                dog.Display();
            }
            else
            {
                _screenDefinitionService.PrintScreen(_currentScreenDefinition, 7, _screenDefinitionJson);
            }
        }
        catch
        {
            _screenDefinitionService.PrintScreen(_currentScreenDefinition, 11, _screenDefinitionJson);
        }
    }

    /// <summary>
    /// Adds/edit specific dog.
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    private Dog AddEditDog()
    {
        _screenDefinitionService.PrintScreen(_currentScreenDefinition, 12, _screenDefinitionJson);
        string? name = Console.ReadLine();
        _screenDefinitionService.PrintScreen(_currentScreenDefinition, 13, _screenDefinitionJson);
        string? ageAsString = Console.ReadLine();
        _screenDefinitionService.PrintScreen(_currentScreenDefinition, 14, _screenDefinitionJson);
        string? breed = Console.ReadLine();

        if (name is null)
        {
            throw new ArgumentNullException(nameof(name));
        }
        if (ageAsString is null)
        {
            throw new ArgumentNullException(nameof(ageAsString));
        }
        if (breed is null)
        {
            throw new ArgumentNullException(nameof(breed));
        }
        int age = Int32.Parse(ageAsString);
        Dog dog = new Dog(name, age, breed);

        return dog;
    }
    #endregion // Private Methods
}
