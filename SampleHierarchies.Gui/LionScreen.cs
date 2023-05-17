using SampleHierarchies.Data;
using SampleHierarchies.Data.Mammals;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Data;
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

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dataService"></param>
        public LionScreen(IDataService dataService, ISettings settings)
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
                _settings.ScreenColor = _settings.ReadValue("LionScreenColor", "White");
                Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), _settings.ScreenColor);

                Console.WriteLine();
                Console.WriteLine("Your available choices are:");
                Console.WriteLine("0. Exit");
                Console.WriteLine("1. List all lions");
                Console.WriteLine("2. Create a new lion");
                Console.WriteLine("3. Delete existing lion");
                Console.WriteLine("4. Modify existing lion");
                Console.Write("Please enter your choice: ");

                string? menuChoiseAsString = Console.ReadLine();

                try
                {
                    if (menuChoiseAsString is null)
                    {
                        throw new ArgumentNullException(nameof(menuChoiseAsString));
                    }

                    LionScreenChoices choice = (LionScreenChoices)Int32.Parse(menuChoiseAsString);

                    switch(choice)
                    {
                        case LionScreenChoices.List:
                            ListOfLions();
                            break;
                        case LionScreenChoices.Create:
                            AddLion();
                            break;
                        case LionScreenChoices.Delete:
                            DeleteLion();
                            break;
                        case LionScreenChoices.Modify:
                            EditRecentLion();
                            break;
                        case LionScreenChoices.Exit:
                            Console.WriteLine("Returning to previous menu");
                            return ;
                    }
                }
                catch
                {
                    Console.WriteLine("Invalid choice. Try again.");
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
                Console.WriteLine("Here's a list of lions:");
                int i = 1;
                foreach (Lion lion in _dataService.Animals.Mammals.Lions)
                {
                    Console.Write($"Lion number {i}, ");
                    lion.Display();
                    i++;
                }
            }
            else
            {
                Console.WriteLine("The list of lions is empty.");
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
                Console.WriteLine("Invalid input.");
            }
        }

        /// <summary>
        /// Delete lion
        /// </summary>
        private void DeleteLion()
        {
            try
            {
                Console.Write("What is the name of the lion you want to delete? ");
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
                    Console.WriteLine("Lion not found.");
                }
            }
            catch
            {
                Console.WriteLine("Invalid input.");
            }
        }

        /// <summary>
        /// Editing recent lion
        /// </summary>
        private void EditRecentLion()
        {
            try
            {
                Console.Write("What is the name of the lion you want to edit? ");
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
                    Console.Write("Lion after edit:");
                    lion.Display();
                }
                else
                {
                    Console.WriteLine("Lion not found.");
                }
            }
            catch
            {
                Console.WriteLine("Invalid input. Try again.");
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
            Console.Write("What is the name of lion? ");
            string? name = Console.ReadLine();
            Console.Write("What is the age of lion? ");
            string? ageAsString = Console.ReadLine();
            Console.Write("Is the lion apex predator? (Yes/No) ");
            string? IsApexPredatorAsString = Console.ReadLine();
            switch(IsApexPredatorAsString)
            {
                case "Yes":
                    IsApexPredator = true;
                    Console.Write("Please, describe the apex predator ");
                    ApexPredatorDescription = Console.ReadLine();
                    break;
                case "No":
                    IsApexPredator = false;
                    break;
                default:
                    Console.WriteLine("Invalid input");
                    break;
            }
            Console.Write("Does your lion hunt in packs? (Yes/No) ");
            string? IsPuckHunterAsString = Console.ReadLine();
            switch(IsPuckHunterAsString) 
            {
                case "Yes":
                    IsPuckHunter = true;
                    Console.Write("Please, describe the puck hunting of this lion ");
                    PuckHuntingDescription = Console.ReadLine();
                    break;
                case "No":
                    IsPuckHunter = false;
                    break;
                default:
                    Console.WriteLine("Invalid input");
                    break;
            }
            Console.Write("What mane dose your lion have? ");
            string? mane = Console.ReadLine();
            Console.Write("Does your lion communicate with roar? (Yes/No) ");
            string? IsRoaringCommunicateAsString = Console.ReadLine();
            switch(IsRoaringCommunicateAsString) 
            {
                case "Yes":
                    IsRoarCommunication = true;
                    Console.Write("Please, describe the communication of your lion ");
                    RoaringCommunicationDescription = Console.ReadLine();
                    break;
                case "No":
                    IsRoarCommunication = false;
                    break;
                default:
                    Console.WriteLine("Invalid input");
                    break;
            }
            Console.Write("Does your lion defense his territory? (Yes/No) ");
            string? IsTerritoryDefenseAsString = Console.ReadLine();
            switch (IsTerritoryDefenseAsString)
            {
                case "Yes":
                    IsTerritoryDefense = true;
                    Console.Write("Please, describe how your lion defense his territory ");
                    TerritoryDefenseDescription = Console.ReadLine();
                    break;
                case "No":
                    IsTerritoryDefense = false;
                    break;
                default:
                    Console.WriteLine("Invalid input");
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
