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
    public class BottlenoseWhaleScreen : Screen
    {
        #region Ctor and Properties
        private IDataService _dataService;
        private ISettings _settings;

        public BottlenoseWhaleScreen(IDataService dataService, ISettings settings)
        {
            _dataService = dataService;
            _settings = settings;
        }

        #endregion

        #region Public methods
        public override void Show()
        {
            while (true)
            {
                _settings = new Settings();
                _settings.ScreenColor = _settings.ReadValue("BottlenoseWhaleScreenColor", "White");
                Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), _settings.ScreenColor);

                Console.WriteLine();
                Console.WriteLine("Your available choices are:");
                Console.WriteLine("0. Exit");
                Console.WriteLine("1. List all bottlenose whales");
                Console.WriteLine("2. Create a new bottlenose whale");
                Console.WriteLine("3. Delete bottlenose whale");
                Console.WriteLine("4. Modify bottlenose whale");
                Console.Write("Please enter your choice: ");

                string? menuChoiseAsString = Console.ReadLine();
                try
                {
                    if (menuChoiseAsString is null)
                    {
                        throw new ArgumentNullException(nameof(menuChoiseAsString));
                    }

                    BottlenoseWhaleScreenChoices choice = (BottlenoseWhaleScreenChoices)Int32.Parse(menuChoiseAsString);
                    switch (choice)
                    {
                        case BottlenoseWhaleScreenChoices.List:
                            ListOfBottlenoseWhales();
                            break;
                        case BottlenoseWhaleScreenChoices.Create:
                            AddBottlenoseWhale();
                            break;
                        case BottlenoseWhaleScreenChoices.Delete:
                            DeleteBottlenoseWhale();
                            break;
                        case BottlenoseWhaleScreenChoices.Modify:
                            EditBottlenoseWhaleRecent();
                            break;
                        case BottlenoseWhaleScreenChoices.Exit:
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

        public BottlenoseWhale AddEditBottlenoseWhale()
        {
            bool isEcholocation = false;
            string? echolocationDescription = "Nothing";
            bool isToothedWhale = false;
            string? toothedWhaleDescription = "Nothing";
            bool isSociableBehavior = false;
            string? sociableBehaviorDescription = "Nothing";

            Console.Write("What name of the whale? ");
            string? name = Console.ReadLine();
            Console.Write("What is the whale's age? ");
            string? ageAsString = Console.ReadLine();
            Console.Write("Is this whale use echolocation? (Yes/No) ");
            string? isEcholocationAsString = Console.ReadLine();
            switch(isEcholocationAsString)
            {
                case "Yes":
                    isEcholocation = true;
                    Console.Write("Describe how he use echolocation ");
                    echolocationDescription = Console.ReadLine();
                    break;
                case "No":
                    isEcholocation = false;
                    break;
                default:
                    Console.Write("Invalid input");
                    break;
            }
            Console.Write("Is this whale toothed? (Yes/No) ");
            string? isToothedWhaleAsString = Console.ReadLine();
            switch (isToothedWhaleAsString)
            {
                case "Yes":
                    isToothedWhale = true;
                    Console.Write("Describe how his toothed ");
                    toothedWhaleDescription = Console.ReadLine();
                    break;
                case "No":
                    isToothedWhale = false;
                    break;
                default:
                    Console.Write("Invalid input");
                    break;
            }
            Console.Write("How long this whale can live? ");
            string? longlifespanAsString = Console.ReadLine();
            Console.Write("Is this whale social behavior? (Yes/No) ");
            string? isSociableBehaviorAsString = Console.ReadLine();
            switch (isSociableBehaviorAsString) 
            {
                case "Yes":
                    isSociableBehavior = true;
                    Console.Write("Describe his social behavior ");
                    sociableBehaviorDescription = Console.ReadLine();
                    break;
                case "No":
                    isSociableBehavior = false;
                    break;
                default:
                    Console.Write("Invalid input");
                    break;
            }
            Console.Write("How he feeds with squids? ");
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

        private void EditBottlenoseWhaleRecent()
        {
            try
            {
                Console.Write("What is the name of the bottlenose whale you want to edit? ");
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
                    Console.Write("Whale after edit: ");
                    whale.Display();
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

        private void DeleteBottlenoseWhale()
        {
            try
            {
                Console.Write("What is the name of the bottlenose whale you want to delete? ");
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
                    Console.WriteLine("Bottlenose whale not found.");
                }
            }
            catch
            {
                Console.WriteLine("Invalid input.");
            }
        }

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
                Console.WriteLine("Invalid input.");
            }
        }

        private void ListOfBottlenoseWhales()
        {
            Console.WriteLine();
            if (_dataService?.Animals?.Mammals?.BottlenoseWhales is not null &&
                _dataService.Animals.Mammals.BottlenoseWhales.Count > 0)
            {
                Console.WriteLine("Here's a list of bottlenose whales:");
                int i = 1;
                foreach (BottlenoseWhale whale in _dataService.Animals.Mammals.BottlenoseWhales)
                {
                    Console.Write($"Whale number {i}, ");
                    whale.Display();
                    i++;
                }
            }
            else
            {
                Console.WriteLine("The list of bottlenose whales is empty.");
            }
        }

        #endregion
    }
}
