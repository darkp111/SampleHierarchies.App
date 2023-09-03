using SampleHierarchies.Data;
using SampleHierarchies.Data.Mammals;
using SampleHierarchies.Data.ScreenSettings;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleHierarchies.Gui
{
    public class BottlenoseWhaleScreen : Screen
    {
        #region Ctor and Properties
        /// <summary>
        /// Properties
        /// </summary>
        private IDataService _dataService;
        private ISettings _settings;
        private IScreenDefinitionService _screenDefinitionService;
        private readonly ScreenDefinition? _currentScreenDefinition;
        private MenuManager menuManager;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dataService"></param>
        /// <param name="settings"></param>
        public BottlenoseWhaleScreen(IDataService dataService, ISettings settings, IScreenDefinitionService screenDefinitionService, MenuManager menuManager)
        {
            _dataService = dataService;
            _settings = settings;
            _screenDefinitionService = screenDefinitionService;
            _screenDefinitionJson = "BottlenoseWhalesMenu.json";
            this.menuManager = menuManager;
        }

        #endregion

        #region Public methods
        public override void Show()
        {
            while (true)
            {
                menuManager.AddToMenuPath("Bottlenose Whales Screen");
                Console.WriteLine("Current Menu Path: " + menuManager.GetCurrentMenuPath());
                _screenDefinitionService.PrintScreen(_currentScreenDefinition, 0, _screenDefinitionJson);
                string menuAsString = _screenDefinitionService.GetText(_currentScreenDefinition, 0, _screenDefinitionJson);
                string[] menuItems = _screenDefinitionService.SplitStringByNewLine(menuAsString);
                int selectedItemIndex = 0;

                //string? menuChoiseAsString = Console.ReadLine();
                //try
                //{
                //    if (menuChoiseAsString is null)
                //    {
                //        throw new ArgumentNullException(nameof(menuChoiseAsString));
                //    }

                //    BottlenoseWhaleScreenChoices choice = (BottlenoseWhaleScreenChoices)Int32.Parse(menuChoiseAsString);
                //    switch (choice)
                //    {
                //        case BottlenoseWhaleScreenChoices.List:
                //            ListOfBottlenoseWhales();
                //            break;
                //        case BottlenoseWhaleScreenChoices.Create:
                //            AddBottlenoseWhale();
                //            break;
                //        case BottlenoseWhaleScreenChoices.Delete:
                //            DeleteBottlenoseWhale();
                //            break;
                //        case BottlenoseWhaleScreenChoices.Modify:
                //            EditBottlenoseWhaleRecent();
                //            break;
                //        case BottlenoseWhaleScreenChoices.Exit:
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
                                    BottlenoseWhaleScreenChoices choice = (BottlenoseWhaleScreenChoices)selectedItemIndex - 1;
                                    switch (choice)
                                    {
                                        case BottlenoseWhaleScreenChoices.List:
                                            ListOfBottlenoseWhales();
                                            Console.ReadKey();
                                            break;
                                        case BottlenoseWhaleScreenChoices.Create:
                                            AddBottlenoseWhale();
                                            Console.ReadKey();
                                            break;
                                        case BottlenoseWhaleScreenChoices.Delete:
                                            DeleteBottlenoseWhale();
                                            Console.ReadKey();
                                            break;
                                        case BottlenoseWhaleScreenChoices.Modify:
                                            EditBottlenoseWhaleRecent();
                                            Console.ReadKey();
                                            break;
                                        case BottlenoseWhaleScreenChoices.Exit:
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

        /// <summary>
        /// Main function of Add and Edit Bottlenose whale
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public BottlenoseWhale AddEditBottlenoseWhale()
        {
            bool isEcholocation = false;
            string? echolocationDescription = "Nothing";
            bool isToothedWhale = false;
            string? toothedWhaleDescription = "Nothing";
            bool isSociableBehavior = false;
            string? sociableBehaviorDescription = "Nothing";

            _screenDefinitionService.PrintScreen(_currentScreenDefinition, 3, _screenDefinitionJson);
            string? name = Console.ReadLine();

            _screenDefinitionService.PrintScreen(_currentScreenDefinition, 4, _screenDefinitionJson);
            string? ageAsString = Console.ReadLine();

            _screenDefinitionService.PrintScreen(_currentScreenDefinition, 5, _screenDefinitionJson);
            string? isEcholocationAsString = Console.ReadLine();

            switch(isEcholocationAsString)
            {
                case "Yes":
                    isEcholocation = true;
                    _screenDefinitionService.PrintScreen(_currentScreenDefinition, 6, _screenDefinitionJson);
                    echolocationDescription = Console.ReadLine();
                    break;
                case "No":
                    isEcholocation = false;
                    break;
                default:
                    _screenDefinitionService.PrintScreen(_currentScreenDefinition, 7, _screenDefinitionJson);
                    break;
            }
            _screenDefinitionService.PrintScreen(_currentScreenDefinition, 8, _screenDefinitionJson);
            string? isToothedWhaleAsString = Console.ReadLine();
            switch (isToothedWhaleAsString)
            {
                case "Yes":
                    isToothedWhale = true;
                    Console.Write("Describe how his toothed ");
                    _screenDefinitionService.PrintScreen(_currentScreenDefinition, 9, _screenDefinitionJson);
                    toothedWhaleDescription = Console.ReadLine();
                    break;
                case "No":
                    isToothedWhale = false;
                    break;
                default:
                    _screenDefinitionService.PrintScreen(_currentScreenDefinition, 7, _screenDefinitionJson);
                    break;
            }
            _screenDefinitionService.PrintScreen(_currentScreenDefinition, 10, _screenDefinitionJson);
            string? longlifespanAsString = Console.ReadLine();
            _screenDefinitionService.PrintScreen(_currentScreenDefinition, 11, _screenDefinitionJson);
            string? isSociableBehaviorAsString = Console.ReadLine();
            switch (isSociableBehaviorAsString) 
            {
                case "Yes":
                    isSociableBehavior = true;
                    _screenDefinitionService.PrintScreen(_currentScreenDefinition, 12, _screenDefinitionJson);
                    sociableBehaviorDescription = Console.ReadLine();
                    break;
                case "No":
                    isSociableBehavior = false;
                    break;
                default:
                    _screenDefinitionService.PrintScreen(_currentScreenDefinition, 7, _screenDefinitionJson);
                    break;
            }
            _screenDefinitionService.PrintScreen(_currentScreenDefinition, 13, _screenDefinitionJson);

            string? feedsOnSquid = Console.ReadLine();

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (ageAsString is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (isEcholocationAsString is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (echolocationDescription is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (isToothedWhaleAsString is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (toothedWhaleDescription is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (longlifespanAsString is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (isSociableBehaviorAsString is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (sociableBehaviorDescription is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (feedsOnSquid is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            int age = Int32.Parse(ageAsString);
            int longLifeSpan = Int32.Parse(longlifespanAsString);

            BottlenoseWhale whale = new BottlenoseWhale(name,age,isEcholocation,echolocationDescription,isToothedWhale,toothedWhaleDescription,longLifeSpan,isSociableBehavior,sociableBehaviorDescription,feedsOnSquid);

            return whale;
        }

        /// <summary>
        /// Editing recent Bottlenose whale that was created by user
        /// </summary>
        private void EditBottlenoseWhaleRecent()
        {
            try
            {
                _screenDefinitionService.PrintScreen(_currentScreenDefinition, 14, _screenDefinitionJson);
                string? name = Console.ReadLine();
                if (name is null)
                {
                    throw new ArgumentNullException(nameof(name));
                }
                BottlenoseWhale? whale = (BottlenoseWhale?)(_dataService?.Animals?.Mammals?.BottlenoseWhales
                    ?.FirstOrDefault(wh => wh is not null && string.Equals(wh.Name, name)));
                if (whale is not null)
                {
                    BottlenoseWhale editedWhale = AddEditBottlenoseWhale();
                    whale.Copy(editedWhale);
                    _screenDefinitionService.PrintScreen(_currentScreenDefinition, 15, _screenDefinitionJson);
                    whale.Display();
                }
                else
                {
                    _screenDefinitionService.PrintScreen(_currentScreenDefinition, 16, _screenDefinitionJson);
                }
            }
            catch
            {
                _screenDefinitionService.PrintScreen(_currentScreenDefinition, 2, _screenDefinitionJson);
            }
        }

        /// <summary>
        /// Delete an existing Bottlenose whale
        /// </summary>
        private void DeleteBottlenoseWhale()
        {
            try
            {
                _screenDefinitionService.PrintScreen(_currentScreenDefinition, 17, _screenDefinitionJson);
                string? name = Console.ReadLine();
                if (name is null)
                {
                    throw new ArgumentNullException(nameof(name));
                }
                BottlenoseWhale? whale = (BottlenoseWhale?)(_dataService?.Animals?.Mammals?.BottlenoseWhales
                    ?.FirstOrDefault(wh => wh is not null && string.Equals(wh.Name, name)));
                if (whale is not null)
                {
                    _dataService?.Animals?.Mammals?.BottlenoseWhales?.Remove(whale);
                    Console.WriteLine("Bottlenose whale with name: {0} has been deleted from a list of bottlenose whales", whale.Name);
                }
                else
                {
                    _screenDefinitionService.PrintScreen(_currentScreenDefinition, 16, _screenDefinitionJson);
                }
            }
            catch
            {
                _screenDefinitionService.PrintScreen(_currentScreenDefinition, 7, _screenDefinitionJson);
            }
        }
        /// <summary>
        /// Add new Bottlenose whale
        /// </summary>
        private void AddBottlenoseWhale()
        {
            try
            {
                BottlenoseWhale whale = AddEditBottlenoseWhale();
                _dataService?.Animals?.Mammals?.BottlenoseWhales?.Add(whale);
                Console.WriteLine("Bottlenose whale with name: {0} has been added to a list of bottlenose whales ", whale.Name);
            }
            catch
            {
                _screenDefinitionService.PrintScreen(_currentScreenDefinition, 7, _screenDefinitionJson);
            }
        }

        /// <summary>
        /// Shows list of all Bottlenose whales
        /// </summary>
        private void ListOfBottlenoseWhales()
        {
            Console.WriteLine();
            if (_dataService?.Animals?.Mammals?.BottlenoseWhales is not null &&
                _dataService.Animals.Mammals.BottlenoseWhales.Count > 0)
            {
                _screenDefinitionService.PrintScreen(_currentScreenDefinition, 18, _screenDefinitionJson);
                int i = 1;
                foreach (BottlenoseWhale whale in _dataService.Animals.Mammals.BottlenoseWhales)
                {
                    Console.Write($"Whale number {i}, ");
                    whale.Display();
                    i++;
                }
                Console.ReadKey();
            }
            else
            {
                _screenDefinitionService.PrintScreen(_currentScreenDefinition, 19, _screenDefinitionJson);
            }
        }
        #endregion
    }
}
