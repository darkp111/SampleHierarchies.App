using SampleHierarchies.Data;
using SampleHierarchies.Data.Mammals;
using SampleHierarchies.Data.ScreenSettings;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SampleHierarchies.Gui
{
    public class PolarBearScreen : Screen
    {
        #region Ctor and Properties
        /// <summary>
        /// Properties
        /// </summary>
        private readonly IDataService _dataService;
        private readonly IScreenDefinitionService _screenDefinitionService;
        private readonly ScreenDefinition? _currentScreenDefinition;
        private MenuManager menuManager;
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="dataService"></param>
        public PolarBearScreen(IDataService dataService, IScreenDefinitionService screenDefinitionService, MenuManager menuManager)
        {
            _dataService = dataService;
            _screenDefinitionService = screenDefinitionService;
            _screenDefinitionJson = "PolarBearsMenu.json";
            this.menuManager = menuManager;
        }

        #endregion

        #region Public Methods
        public override void Show()
        {
            while (true) 
            {
                menuManager.AddToMenuPath("Polar Bears Screen");
                Console.WriteLine("Current Menu Path: " + menuManager.GetCurrentMenuPath());
                _screenDefinitionService.PrintScreen(_currentScreenDefinition, 0, _screenDefinitionJson);
                string menuAsString = _screenDefinitionService.GetText(_currentScreenDefinition, 0, _screenDefinitionJson);
                string[] menuItems = _screenDefinitionService.SplitStringByNewLine(menuAsString);
                int selectedItemIndex = 0;

                //string? menuChoiseAsString = Console.ReadLine();
                //try
                //{
                //    if(menuChoiseAsString is null)
                //    {
                //        throw new ArgumentNullException(nameof(menuChoiseAsString));
                //    }

                //    PolarBearScreenChoices choice = (PolarBearScreenChoices)Int32.Parse(menuChoiseAsString);
                //    switch (choice) 
                //    {
                //        case PolarBearScreenChoices.List:
                //            ListOfPolarBears();
                //            break;
                //        case PolarBearScreenChoices.Create:
                //            AddPolarBear();
                //            break;
                //        case PolarBearScreenChoices.Delete:
                //            DeletePolarBear();
                //            break;
                //        case PolarBearScreenChoices.Modify:
                //            EditPolarBearRecent();
                //            break;
                //        case PolarBearScreenChoices.Exit:
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
                    Console.ReadKey();
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
                                    PolarBearScreenChoices choice = (PolarBearScreenChoices)selectedItemIndex - 1;
                                    switch (choice)
                                    {
                                        case PolarBearScreenChoices.List:
                                            ListOfPolarBears();
                                            Console.ReadKey();
                                            break;
                                        case PolarBearScreenChoices.Create:
                                            AddPolarBear();
                                            Console.ReadKey();
                                            break;
                                        case PolarBearScreenChoices.Delete:
                                            DeletePolarBear();
                                            Console.ReadKey();
                                            break;
                                        case PolarBearScreenChoices.Modify:
                                            EditPolarBearRecent();
                                            Console.ReadKey();
                                            break;
                                        case PolarBearScreenChoices.Exit:
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

        #endregion

        #region Private Methods
        /// <summary>
        /// Shows list of all polar bears
        /// </summary>
        private void ListOfPolarBears()
        {
            Console.WriteLine();
            if (_dataService?.Animals?.Mammals?.PolarBears is not null &&
                _dataService.Animals.Mammals.PolarBears.Count > 0)
            {
                _screenDefinitionService.PrintScreen(_currentScreenDefinition, 3, _screenDefinitionJson);
                int i = 1;
                foreach (PolarBear bear in _dataService.Animals.Mammals.PolarBears)
                {
                    Console.Write($"Bear number {i}, ");
                    bear.Display();
                    i++;
                }
            }
            else
            {
                _screenDefinitionService.PrintScreen(_currentScreenDefinition, 4, _screenDefinitionJson);
            }
        }

        private void DeletePolarBear()
        {
            try
            {
                _screenDefinitionService.PrintScreen(_currentScreenDefinition, 5, _screenDefinitionJson);
                string? name = Console.ReadLine();
                if (name is null)
                {
                    throw new ArgumentNullException(nameof(name));
                }
                PolarBear? bear = (PolarBear?)(_dataService?.Animals?.Mammals?.PolarBears
                    ?.FirstOrDefault(pb => pb is not null && string.Equals(pb.Name, name)));
                if (bear is not null)
                {
                    _dataService?.Animals?.Mammals?.PolarBears?.Remove(bear);
                    Console.WriteLine("Polar bear with name: {0} has been deleted from a list of polar bears", bear.Name);
                }
                else
                {
                    _screenDefinitionService.PrintScreen(_currentScreenDefinition, 6, _screenDefinitionJson);
                }
            }
            catch
            {
                _screenDefinitionService.PrintScreen(_currentScreenDefinition, 7, _screenDefinitionJson);
            }
        }

        /// <summary>
        /// Edit a recent bear that we create
        /// </summary>
        private void EditPolarBearRecent()
        {
            try
            {
                _screenDefinitionService.PrintScreen(_currentScreenDefinition, 8, _screenDefinitionJson);
                string? name = Console.ReadLine();
                if (name is null)
                {
                    throw new ArgumentNullException(nameof(name));
                }
                PolarBear? bear = (PolarBear?)(_dataService?.Animals?.Mammals?.PolarBears
                    ?.FirstOrDefault(pb => pb is not null && string.Equals(pb.Name, name)));
                if (bear is not null)
                {
                    PolarBear editedPolarBear = AddEditPolarBear();
                    bear.Copy(editedPolarBear);
                    _screenDefinitionService.PrintScreen(_currentScreenDefinition, 9, _screenDefinitionJson);
                    bear.Display();
                }
                else
                {
                    _screenDefinitionService.PrintScreen(_currentScreenDefinition, 10, _screenDefinitionJson);
                }
            }
            catch
            {
                _screenDefinitionService.PrintScreen(_currentScreenDefinition, 11, _screenDefinitionJson);
            }
        }

        /// <summary>
        /// Add a polar bear to the list of bears
        /// </summary>
        private void AddPolarBear()
        {
            try
            {
                PolarBear bear = AddEditPolarBear();
                _dataService?.Animals?.Mammals?.PolarBears?.Add(bear);
                Console.WriteLine("Polar bear with name: {0} has been added to a list of polar bears", bear.Name);
            }
            catch
            {
                _screenDefinitionService.PrintScreen(_currentScreenDefinition, 11, _screenDefinitionJson);
            }
        }

        /// <summary>
        /// Add and editing of existing polar bears
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>

        private PolarBear AddEditPolarBear()
        {
            bool isSemiAquatic = false;
            string? semiAquaticDescription = "Nothing";
            _screenDefinitionService.PrintScreen(_currentScreenDefinition, 12, _screenDefinitionJson);
            string? name = Console.ReadLine();
            _screenDefinitionService.PrintScreen(_currentScreenDefinition, 13, _screenDefinitionJson);
            string? ageAsString = Console.ReadLine();
            _screenDefinitionService.PrintScreen(_currentScreenDefinition, 14, _screenDefinitionJson);
            string? kindOf = Console.ReadLine();
            _screenDefinitionService.PrintScreen(_currentScreenDefinition, 15, _screenDefinitionJson);
            string? typeOfFur = Console.ReadLine();
            _screenDefinitionService.PrintScreen(_currentScreenDefinition, 16, _screenDefinitionJson);
            string? largeOfPaws = Console.ReadLine();
            _screenDefinitionService.PrintScreen(_currentScreenDefinition, 17, _screenDefinitionJson);
            string? typeOfDiet = Console.ReadLine();
            _screenDefinitionService.PrintScreen(_currentScreenDefinition, 18, _screenDefinitionJson);
            string? IsSemiAquaticAsString = Console.ReadLine();
            switch(IsSemiAquaticAsString)
            {
                case "Yes":
                    isSemiAquatic = true;
                    _screenDefinitionService.PrintScreen(_currentScreenDefinition, 19, _screenDefinitionJson);
                    semiAquaticDescription = Console.ReadLine();
                    break;
                case "No":
                     isSemiAquatic = false;
                    break;
                default:
                    _screenDefinitionService.PrintScreen(_currentScreenDefinition, 11, _screenDefinitionJson);
                    break;
            }
            _screenDefinitionService.PrintScreen(_currentScreenDefinition, 20, _screenDefinitionJson);
            string? excellentSenseOfSmell = Console.ReadLine();

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (ageAsString is null)
            {
                throw new ArgumentNullException(nameof(ageAsString));
            }
            if (kindOf is null)
            {
                throw new ArgumentNullException(nameof(kindOf));
            }
            if (typeOfFur is null)
            {
                throw new ArgumentNullException(nameof(typeOfFur));
            }
            if(largeOfPaws is null)
            {
                throw new ArgumentNullException(nameof(largeOfPaws));
            }
            if (typeOfDiet is null)
            {
                throw new ArgumentNullException(nameof(typeOfDiet));
            }
            if (IsSemiAquaticAsString is null)
            {
                throw new ArgumentNullException(nameof(IsSemiAquaticAsString));
            }
            if (semiAquaticDescription is null)
            {
                throw new ArgumentNullException(nameof(semiAquaticDescription));
            }
            if (excellentSenseOfSmell is null)
            {
                throw new ArgumentNullException(nameof(excellentSenseOfSmell));
            }
            int age = Int32.Parse(ageAsString);
            PolarBear bear = new PolarBear(name, age, kindOf,typeOfFur,largeOfPaws,typeOfDiet,isSemiAquatic,semiAquaticDescription,excellentSenseOfSmell);

            return bear;
        }
        #endregion
    }
}
