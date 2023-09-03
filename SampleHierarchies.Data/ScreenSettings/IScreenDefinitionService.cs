using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleHierarchies.Data.ScreenSettings
{
    public interface IScreenDefinitionService
    {
        ScreenDefinition Load(string jsonFileName);
        bool Save(ScreenDefinition screenDefinition, string jsonFileName);
        void PrintScreen(ScreenDefinition content, int lineNumber, string jsonPath);
        string GetText(ScreenDefinition content, int lineNumber, string jsonPath);
        ConsoleColor GetForegroundColor(ScreenDefinition content, int lineNumber, string jsonPath);
        ConsoleColor GetBackgroundColor(ScreenDefinition content, int lineNumber, string jsonPath);
        string[] SplitStringByNewLine(string input);
    }
}
