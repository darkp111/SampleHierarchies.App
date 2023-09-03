using Newtonsoft.Json;
using SampleHierarchies.Data.ScreenSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SampleHierarchies.Services
{
    public class ScreenDefinitionService : IScreenDefinitionService
    {
        public ScreenDefinition Load(string jsonFileName)
        {
            string jsonContent = File.ReadAllText(jsonFileName);
            ScreenDefinition? screenDefinition;
            try
            {
                screenDefinition = JsonConvert.DeserializeObject<ScreenDefinition>(jsonContent);
            }
            catch (JsonException ex)
            {
                throw new Exception("Error deserializing JSON: " + ex.Message);
            }

            if (screenDefinition == null)
                throw new Exception("Deserialization resulted in null object.");

            return screenDefinition;
        }

        public bool Save(ScreenDefinition screenDefinition, string jsonFileName)
        {
            try
            {
                string jsonContent = JsonConvert.SerializeObject(screenDefinition);
                File.WriteAllText(jsonFileName, jsonContent);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving screen definition: " + ex.Message);
                return false;
            }
        }

        public void PrintScreen(ScreenDefinition content, int lineNumber, string jsonPath)
        {
            content = Load(jsonPath);
            if(content.LineEntries == null)
            {
                Console.WriteLine("Invalid content download");
            }
            else
            {
                if (content.LineEntries[lineNumber].BackgroundColor == null & content.LineEntries[lineNumber].ForegroundColor == null)
                {
                    Console.WriteLine("Content not found.");
                }
                else
                {
                    Console.BackgroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), content.LineEntries[lineNumber].BackgroundColor, true);
                    Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), content.LineEntries[lineNumber].ForegroundColor, true);
                    Console.WriteLine(content.LineEntries[lineNumber].Text);
                    Console.ResetColor();
                }
            }
        }

        public string GetText(ScreenDefinition content, int lineNumber, string jsonPath)
        {
            try
            {
                content = Load(jsonPath);
                if (content == null)
                {
                    return "Content not found";
                }
                else
                {
                    return content.LineEntries[lineNumber].Text;
                }
            }
            catch
            {
                return "Text not found";
            }
        }

        public ConsoleColor GetForegroundColor(ScreenDefinition content, int lineNumber, string jsonPath)
        {
            content = Load(jsonPath);
            if (content == null)
            {
                Console.WriteLine("Invalid content download");
                return ConsoleColor.White;
            }
            else
            {
                return (ConsoleColor)Enum.Parse(typeof(ConsoleColor), content.LineEntries[lineNumber].ForegroundColor, true);
            }
        }

        public ConsoleColor GetBackgroundColor(ScreenDefinition content, int lineNumber, string jsonPath)
        {
            content = Load(jsonPath);
            if (content == null)
            {
                Console.WriteLine("Invalid content download");
                return ConsoleColor.Black;
            }
            else
            {
                return (ConsoleColor)Enum.Parse(typeof(ConsoleColor), content.LineEntries[lineNumber].BackgroundColor, true);
            }
        }

        public string[] SplitStringByNewLine(string input)
        {
            string[] lines = input.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            return lines;
        }
    }
}
