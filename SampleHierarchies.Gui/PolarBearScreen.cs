using SampleHierarchies.Data;
using SampleHierarchies.Data.Mammals;
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
        private IDataService _dataService;
        private ISettings _settings;
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="dataService"></param>
        public PolarBearScreen(IDataService dataService, ISettings settings)
        {
            _dataService = dataService;
            _settings = settings;
        }

        #endregion

        #region Public Methods
        public override void Show()
        {  
            while (true) 
            {
                _settings = new Settings();
                _settings.ScreenColor = _settings.ReadValue("PolarBearScreenColor", "White");
                Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), _settings.ScreenColor);

                Console.WriteLine();
                Console.WriteLine("Your available choices are:");
                Console.WriteLine("0. Exit");
                Console.WriteLine("1. List all polar bears");
                Console.WriteLine("2. Create a new polar bear");
                Console.WriteLine("3. Delete existing polar bear");
                Console.WriteLine("4. Modify existing polar bear");
                Console.Write("Please enter your choice: ");

                string? menuChoiseAsString = Console.ReadLine();
                try
                {
                    if(menuChoiseAsString is null)
                    {
                        throw new ArgumentNullException(nameof(menuChoiseAsString));
                    }

                    PolarBearScreenChoices choice = (PolarBearScreenChoices)Int32.Parse(menuChoiseAsString);
                    switch (choice) 
                    {
                        case PolarBearScreenChoices.List:
                            ListOfPolarBears();
                            break;
                        case PolarBearScreenChoices.Create:
                            AddPolarBear();
                            break;
                        case PolarBearScreenChoices.Delete:
                            DeletePolarBear();
                            break;
                        case PolarBearScreenChoices.Modify:
                            EditPolarBearRecent();
                            break;
                        case PolarBearScreenChoices.Exit:
                            Console.WriteLine("Returning to previous menu");
                            return;

                    }
                }
                catch
                {
                    Console.WriteLine("Invalid choice. Try again.");
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
                Console.WriteLine("Here's a list of polar bears:");
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
                Console.WriteLine("The list of polar bears is empty.");
            }
        }

        private void DeletePolarBear()
        {
            try
            {
                Console.Write("What is the name of the bear you want to delete? ");
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
                    Console.WriteLine("Polar bear not found.");
                }
            }
            catch
            {
                Console.WriteLine("Invalid input.");
            }
        }

        /// <summary>
        /// Edit a recent bear that we create
        /// </summary>
        private void EditPolarBearRecent()
        {
            try
            {
                Console.Write("What is the name of the polar bear you want to edit? ");
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
                    Console.Write("Bear after edit: ");
                    bear.Display();
                }
                else
                {
                    Console.WriteLine("Dog not found.");
                }
            }
            catch
            {
                Console.WriteLine("Invalid input. Try again.");
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
                Console.WriteLine("Invalid input.");
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
            Console.Write("What name of the bear? ");
            string? name = Console.ReadLine();
            Console.Write("What is the bear's age? ");
            string? ageAsString = Console.ReadLine();
            Console.Write("What is the bear's kind of? ");
            string? kindOf = Console.ReadLine();
            Console.Write("What is the type of bear's fur? ");
            string? typeOfFur = Console.ReadLine();
            Console.Write("What is the size of bear's paws? ");
            string? largeOfPaws = Console.ReadLine();
            Console.Write("What is the type of bear's diet? ");
            string? typeOfDiet = Console.ReadLine();
            Console.Write("Is the bear semi-aquatic? ");
            string? IsSemiAquaticAsString = Console.ReadLine();
            switch(IsSemiAquaticAsString)
            {
                case "Yes":
                    isSemiAquatic = true;
                    Console.Write("What is the bear description of swimming? ");
                    semiAquaticDescription = Console.ReadLine();
                    break;
                case "No":
                     isSemiAquatic = false;
                    break;
                default:
                    Console.WriteLine("Invalid input");
                    break;
            }
            Console.Write("How good is his scent? ");
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
