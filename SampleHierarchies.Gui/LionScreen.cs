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
    public class LionScreen : Screen
    {
        #region Ctor and Properties
        /// <summary>
        /// Data service of lions
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
        public LionScreen(IDataService dataService, ISettings settings,IScreenDefinitionService screenDefinitionService, MenuManager menuManager)
        {
            _dataService = dataService;
            _settings = settings;
            _screenDefinitionService = screenDefinitionService;
            _screenDefinitionJson = "LionsMenu.json";
            this.menuManager = menuManager;
        }

        #endregion

        #region Public Methods
        public override void Show()
        {
            while (true) 
            {
                menuManager.AddToMenuPath("Lions Screen");
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

                //    LionScreenChoices choice = (LionScreenChoices)Int32.Parse(menuChoiseAsString);

                //    switch(choice)
                //    {
                //        case LionScreenChoices.List:
                //            ListOfLions();
                //            break;
                //        case LionScreenChoices.Create:
                //            AddLion();
                //            break;
                //        case LionScreenChoices.Delete:
                //            DeleteLion();
                //            break;
                //        case LionScreenChoices.Modify:
                //            EditRecentLion();
                //            break;
                //        case LionScreenChoices.Exit:
                //            // _screenDefinitionService.PrintScreen(_currentScreenDefinition, 1, _screenDefinitionJson);
                //            menuManager.RemoveLastFromMenuPath();
                //            Console.WriteLine("Returning to " + menuManager.GetCurrentMenuPath());
                //            Console.Clear();
                //            return ;
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
                                    LionScreenChoices choice = (LionScreenChoices)selectedItemIndex - 1;
                                    switch (choice)
                                    {
                                        case LionScreenChoices.List:
                                            ListOfLions();
                                            Console.ReadKey();
                                            break;
                                        case LionScreenChoices.Create:
                                            AddLion();
                                            Console.ReadKey();
                                            break;
                                        case LionScreenChoices.Delete:
                                            DeleteLion();
                                            Console.ReadKey();
                                            break;
                                        case LionScreenChoices.Modify:
                                            EditRecentLion();
                                            Console.ReadKey();
                                            break;
                                        case LionScreenChoices.Exit:
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
        /// List of all lions
        /// </summary>
        private void ListOfLions()
        {
            Console.WriteLine();
            if (_dataService?.Animals?.Mammals?.Lions is not null &&
                _dataService.Animals.Mammals.Lions.Count > 0)
            {
                _screenDefinitionService.PrintScreen(_currentScreenDefinition, 4, _screenDefinitionJson);
                int i = 1;
                foreach (Lion lion in _dataService.Animals.Mammals.Lions)
                {
                    Console.Write($"Lion number {i}, ");
                    lion.Display();
                    i++;
                }
                Console.ReadKey();
            }
            else
            {
                _screenDefinitionService.PrintScreen(_currentScreenDefinition, 5, _screenDefinitionJson);
            }
        }

        /// <summary>
        /// Add new lion
        /// </summary>
        private void AddLion()
        {
            try
            {
                Lion lion = AddEditLion();
                _dataService?.Animals?.Mammals?.Lions?.Add(lion);
                Console.WriteLine("Lion with name: {0} has been added to a list of lions", lion.Name);
            }
            catch
            {
                _screenDefinitionService.PrintScreen(_currentScreenDefinition, 3, _screenDefinitionJson);
            }
        }

        /// <summary>
        /// Delete lion
        /// </summary>
        private void DeleteLion()
        {
            try
            {
                _screenDefinitionService.PrintScreen(_currentScreenDefinition, 6, _screenDefinitionJson);
                string? name = Console.ReadLine();
                if (name is null)
                {
                    throw new ArgumentNullException(nameof(name));
                }
                Lion? lion = (Lion?)(_dataService?.Animals?.Mammals?.Lions
                    ?.FirstOrDefault(l => l is not null && string.Equals(l.Name, name)));
                if (lion is not null)
                {
                    _dataService?.Animals?.Mammals?.Lions?.Remove(lion);
                    Console.WriteLine("Lion with name: {0} has been deleted from a list of lions", lion.Name);
                }
                else
                {
                    _screenDefinitionService.PrintScreen(_currentScreenDefinition, 7, _screenDefinitionJson);
                }
            }
            catch
            {
                _screenDefinitionService.PrintScreen(_currentScreenDefinition, 3, _screenDefinitionJson);
            }
        }

        /// <summary>
        /// Editing recent lion
        /// </summary>
        private void EditRecentLion()
        {
            try
            {
                _screenDefinitionService.PrintScreen(_currentScreenDefinition, 8, _screenDefinitionJson);
                string? name = Console.ReadLine();
                if (name is null)
                {
                    throw new ArgumentNullException(nameof(name));
                }
                Lion? lion = (Lion?)(_dataService?.Animals?.Mammals?.Lions
                    ?.FirstOrDefault(l => l is not null && string.Equals(l.Name, name)));
                if (lion is not null)
                {
                    Lion dogEdited = AddEditLion();
                    lion.Copy(dogEdited);
                    _screenDefinitionService.PrintScreen(_currentScreenDefinition, 9, _screenDefinitionJson);
                    lion.Display();
                }
                else
                {
                    _screenDefinitionService.PrintScreen(_currentScreenDefinition, 10, _screenDefinitionJson);
                }
            }
            catch
            {
                _screenDefinitionService.PrintScreen(_currentScreenDefinition, 2, _screenDefinitionJson);
            }
        }

        /// <summary>
        /// Add and edit lion from all lions (not current)
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        private Lion AddEditLion()
        {
            bool IsApexPredator = false;
            bool IsPuckHunter = false;
            bool IsRoarCommunication = false;
            bool IsTerritoryDefense = false;
            string? ApexPredatorDescription = "Nothing";
            string? PuckHuntingDescription = "Nothing";
            string? RoaringCommunicationDescription = "Nothing";
            string? TerritoryDefenseDescription = "Nothing";
            _screenDefinitionService.PrintScreen(_currentScreenDefinition, 11, _screenDefinitionJson);
            string? name = Console.ReadLine();
            _screenDefinitionService.PrintScreen(_currentScreenDefinition, 12, _screenDefinitionJson);
            string? ageAsString = Console.ReadLine();
            _screenDefinitionService.PrintScreen(_currentScreenDefinition, 13, _screenDefinitionJson);
            string? IsApexPredatorAsString = Console.ReadLine();
            switch(IsApexPredatorAsString)
            {
                case "Yes":
                    IsApexPredator = true;
                    _screenDefinitionService.PrintScreen(_currentScreenDefinition, 14, _screenDefinitionJson);
                    ApexPredatorDescription = Console.ReadLine();
                    break;
                case "No":
                    IsApexPredator = false;
                    break;
                default:
                    _screenDefinitionService.PrintScreen(_currentScreenDefinition, 3, _screenDefinitionJson);
                    break;
            }
           
            _screenDefinitionService.PrintScreen(_currentScreenDefinition, 15, _screenDefinitionJson);
            string? IsPuckHunterAsString = Console.ReadLine();
            switch(IsPuckHunterAsString) 
            {
                case "Yes":
                    IsPuckHunter = true;
                    _screenDefinitionService.PrintScreen(_currentScreenDefinition, 16, _screenDefinitionJson);
                    PuckHuntingDescription = Console.ReadLine();
                    break;
                case "No":
                    IsPuckHunter = false;
                    break;
                default:
                    _screenDefinitionService.PrintScreen(_currentScreenDefinition, 3, _screenDefinitionJson);
                    break;
            }
            _screenDefinitionService.PrintScreen(_currentScreenDefinition, 17, _screenDefinitionJson);
            string? mane = Console.ReadLine();
            _screenDefinitionService.PrintScreen(_currentScreenDefinition, 18, _screenDefinitionJson);
            string? IsRoaringCommunicateAsString = Console.ReadLine();
            switch(IsRoaringCommunicateAsString) 
            {
                case "Yes":
                    IsRoarCommunication = true;
                    _screenDefinitionService.PrintScreen(_currentScreenDefinition, 19, _screenDefinitionJson);
                    RoaringCommunicationDescription = Console.ReadLine();
                    break;
                case "No":
                    IsRoarCommunication = false;
                    break;
                default:
                    _screenDefinitionService.PrintScreen(_currentScreenDefinition, 3, _screenDefinitionJson);
                    break;
            }
            _screenDefinitionService.PrintScreen(_currentScreenDefinition, 20, _screenDefinitionJson);
            string? IsTerritoryDefenseAsString = Console.ReadLine();
            switch (IsTerritoryDefenseAsString)
            {
                case "Yes":
                    IsTerritoryDefense = true;
                    _screenDefinitionService.PrintScreen(_currentScreenDefinition, 21, _screenDefinitionJson);
                    TerritoryDefenseDescription = Console.ReadLine();
                    break;
                case "No":
                    IsTerritoryDefense = false;
                    break;
                default:
                    _screenDefinitionService.PrintScreen(_currentScreenDefinition, 3, _screenDefinitionJson);
                    break;
            }

            int age = Int32.Parse(ageAsString);
            Lion lion = new Lion(name,age,IsApexPredator,ApexPredatorDescription,IsPuckHunter,
                PuckHuntingDescription,mane,IsRoarCommunication,RoaringCommunicationDescription,IsTerritoryDefense,TerritoryDefenseDescription);

            return lion;
        }
        #endregion
    }
}
