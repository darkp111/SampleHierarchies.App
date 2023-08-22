using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SampleHierarchies.Interfaces.Data;

namespace SampleHierarchies.Data
{
    public class Settings : ISettings
    {
        #region Ctor and Properties
        public string Version { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string? ScreenColor { get; set; }

        public static string? FilePath { get; set;}

        static Settings()
        {
            FilePath = "settings.json"; 
        }
        #endregion

        #region Public Methods
        /* Mistake
        public void SetFilePath(string filePath)
        {
           FilePath = filePath;
        }
        */

        /// <summary>
        /// The method that allows to take a color from settings.json file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public T ReadValue<T>(string propertyName, T defaultValue)
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    string json = File.ReadAllText(FilePath);
                    dynamic jsonObject = JsonConvert.DeserializeObject(json);
                    if (jsonObject != null && jsonObject[propertyName] != null)
                    {
                        return jsonObject[propertyName].ToObject<T>();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while reading the JSON file: {ex.Message}");
            }

            return defaultValue;
        }
        #endregion
    }
}
