using SampleHierarchies.Data;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Services;

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
    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="dataService">Data service reference</param>
    /// <param name="dogsScreen">Dogs screen</param>
    public MammalsScreen(DogsScreen dogsScreen, PolarBearScreen polarBearScreen, LionScreen lionScreen,BottlenoseWhaleScreen bottlenoseWhaleScreen, ISettings settings)
    {
        _dogsScreen = dogsScreen;
        _polarBearScreen = polarBearScreen;
        _lionScreen = lionScreen; 
        _bottlenoseWhaleScreen = bottlenoseWhaleScreen;
        _settings = settings;
    }

    #endregion Properties And Ctor

    #region Public Methods

    /// <inheritdoc/>
    public override void Show()
    {
        while (true)
        {
            _settings = new Settings();
            _settings.ScreenColor = _settings.ReadValue("MammalsScreenColor", "White");
            Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), _settings.ScreenColor);

            Console.WriteLine();
            Console.WriteLine("Your available choices are:");
            Console.WriteLine("0. Exit");
            Console.WriteLine("1. Dogs");
            Console.WriteLine("2. Polar Bears");
            Console.WriteLine("3. Lions");
            Console.WriteLine("4. Bottlenose Whales");
            Console.Write("Please enter your choice: ");

            string? choiceAsString = Console.ReadLine();

            // Validate choice
            try
            {
                if (choiceAsString is null)
                {
                    throw new ArgumentNullException(nameof(choiceAsString));
                }

                MammalsScreenChoices choice = (MammalsScreenChoices)Int32.Parse(choiceAsString);
                switch (choice)
                {
                    case MammalsScreenChoices.Dogs:
                        _dogsScreen.Show();
                        break;
                    case MammalsScreenChoices.PolarBears:
                        _polarBearScreen.Show(); 
                        break;
                    case MammalsScreenChoices.Lions:
                        _lionScreen.Show(); 
                        break;
                    case MammalsScreenChoices.Whales:
                        _bottlenoseWhaleScreen.Show();
                        break;
                    case MammalsScreenChoices.Exit:
                        Console.WriteLine("Going back to parent menu.");
                        return;
                }
            }
            catch
            {
                Console.WriteLine("Invalid choice. Try again.");
            }
        }
    }

    #endregion // Public Methods
}
