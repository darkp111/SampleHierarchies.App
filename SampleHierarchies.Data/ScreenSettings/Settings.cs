using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SampleHierarchies.Interfaces.Services;

namespace SampleHierarchies.Data.ScreenSettings
{
    public class Settings : ISettings
    {
        #region Ctor and Properties
        public string Version { get => throw new NotImplementedException(); set => throw new NotImplementedException(); } 
        public string? ScreenColor { get; set; }

        public string? FilePath { get; set; }

        public Settings()
        {
            FilePath = "settings.json";
        }
        #endregion

        #region Public Methods

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
                    dynamic jsonObject = JObject.Parse(json);
                    if (jsonObject is not null && jsonObject[propertyName] is not null)
                    {
                        JToken propertyValue = jsonObject[propertyName];
                        if (propertyValue.Type != JTokenType.Null)
                        {
                            if (propertyValue.ToObject<T>() != null)
                            {
                                T? result = propertyValue.ToObject<T>();
                                if (result is not null)
                                {
                                    return result;
                                }
                            }
                        }
                    }
                    return defaultValue;
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
