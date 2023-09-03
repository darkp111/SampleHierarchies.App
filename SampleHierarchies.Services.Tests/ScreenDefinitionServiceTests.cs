using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using SampleHierarchies.Data.ScreenSettings;
using SampleHierarchies.Services;
using System;
using System.IO;

namespace SampleHierarchies.Tests
{
    [TestClass]
    public class ScreenDefinitionServiceTests
    {
        private const string TestJsonFileName = "test.json";

        [TestMethod]
        public void Load_ValidJsonFile_ReturnsScreenDefinition()
        {
            // Arrange
            var service = new ScreenDefinitionService();
            var screenDefinition = new ScreenDefinition
            {
                LineEntries = new List<ScreenLineEntry>
                {
                    new ScreenLineEntry { BackgroundColor = "Black", ForegroundColor = "White", Text = "Sample Text" }
                }
            };
            string jsonContent = JsonConvert.SerializeObject(screenDefinition);
            File.WriteAllText(TestJsonFileName, jsonContent);

            try
            {
                // Act
                var result = service.Load(TestJsonFileName);

                // Assert
                Assert.IsNotNull(result);
                // Add more assertions to verify the loaded screenDefinition object
            }
            finally
            {
                // Cleanup
                File.Delete(TestJsonFileName);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Load_InvalidJsonFile_ThrowsException()
        {
            // Arrange
            var service = new ScreenDefinitionService();
            File.WriteAllText(TestJsonFileName, "invalid json content");

            try
            {
                // Act
                service.Load(TestJsonFileName);
            }
            finally
            {
                // Cleanup
                File.Delete(TestJsonFileName);
            }
        }

        [TestMethod]
        public void Save_ValidScreenDefinition_ReturnsTrue()
        {
            // Arrange
            var service = new ScreenDefinitionService();
            var screenDefinition = new ScreenDefinition
            {
                LineEntries = new List<ScreenLineEntry>
                {
                    new ScreenLineEntry { BackgroundColor = "Black", ForegroundColor = "White", Text = "Sample Text" }
                }
            };

            // Act
            var result = service.Save(screenDefinition, TestJsonFileName);

            // Assert
            Assert.IsTrue(result);
            // Add more assertions to verify the file content, if needed
        }

        [TestMethod]
        public void PrintScreen_ContentFound_PrintsContent()
        {
            // Arrange
            var service = new ScreenDefinitionService();
            var screenDefinition = new ScreenDefinition
            {
                LineEntries = new List<ScreenLineEntry>
                {
                    new ScreenLineEntry { BackgroundColor = "Black", ForegroundColor = "White", Text = "Sample Text" }
                }
            };
            File.WriteAllText(TestJsonFileName, JsonConvert.SerializeObject(screenDefinition));

            try
            {
                // Act
                using (StringWriter sw = new StringWriter())
                {
                    Console.SetOut(sw);

                    service.PrintScreen(screenDefinition, 0, TestJsonFileName);

                    string expectedOutput = "Sample Text" + Environment.NewLine;
                    string actualOutput = sw.ToString();

                    // Assert
                    Assert.AreEqual(expectedOutput, actualOutput);
                }
            }
            finally
            {
                // Cleanup
                File.Delete(TestJsonFileName);
            }
        }

        [TestMethod]
        public void PrintScreen_ContentNotFound_PrintsErrorMessage()
        {
            // Arrange
            var service = new ScreenDefinitionService();
            var screenDefinition = new ScreenDefinition
            {
                LineEntries = new List<ScreenLineEntry>
                {
                    new ScreenLineEntry { BackgroundColor = null, ForegroundColor = null, Text = null }
                }
            };
            File.WriteAllText(TestJsonFileName, JsonConvert.SerializeObject(screenDefinition));

            try
            {
                // Act
                using (StringWriter sw = new StringWriter())
                {
                    Console.SetOut(sw);

                    service.PrintScreen(screenDefinition, 0, TestJsonFileName);

                    string expectedOutput = "Content not found." + Environment.NewLine;
                    string actualOutput = sw.ToString();

                    // Assert
                    Assert.AreEqual(expectedOutput, actualOutput);
                }
            }
            finally
            {
                // Cleanup
                File.Delete(TestJsonFileName);
            }
        }
    }
}
