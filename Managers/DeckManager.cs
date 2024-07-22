using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.Json;

namespace CardGame.Managers
{
    public class DeckManager
    {
        private readonly string saveDirectory;

        public DeckManager()
        {
            saveDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MyCardGame", "Saves");
            try
            {
                EnsureDirectoryExists(saveDirectory);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed to create directory: " + ex.Message);
            }
        }

        private void EnsureDirectoryExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public void SaveDeck(string fileName, Deck deck)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string filePath = Path.Combine(saveDirectory, fileName);

            try
            {
                // Ensure the file exists
                if (!File.Exists(filePath))
                {
                    using (FileStream fs = File.Create(filePath))
                    {
                        fs.Close();
                    }
                }

                // Serialize the deck and write to the file
                string json = JsonSerializer.Serialize(deck, options);
                File.WriteAllText(filePath, json);

                Debug.WriteLine("Saving " + filePath);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed to save deck: " + ex.Message);
            }
        }

        public Deck LoadDeck(string fileName)
        {
            string filePath = Path.Combine(saveDirectory, fileName);

            try
            {
                if (File.Exists(filePath))
                {
                    Debug.WriteLine("Reading " + filePath);
                    string json = File.ReadAllText(filePath);

                    try
                    {
                        Deck deck = JsonSerializer.Deserialize<Deck>(json);
                        return deck;
                    }
                    catch (JsonException e)
                    {
                        Debug.WriteLine("Error deserializing: " + e.Message);
                        return createNewDeck();
                    }
                }
                else
                {
                    Debug.WriteLine("No file found: " + filePath);
                    return createNewDeck();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed to load deck: " + ex.Message);
                return createNewDeck();
            }
        }

        public string[] ListSaveFiles()
        {
            try
            {
                if (Directory.Exists(saveDirectory))
                {
                    return Directory.GetFiles(saveDirectory, "*.json");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed to list save files: " + ex.Message);
            }
            return Array.Empty<string>();
        }
        private Deck createNewDeck()
        {
            Deck _deck = new Deck();
            _deck.Name = "New deck";
            _deck.HeroPower = 0;
            _deck.DeckContents = "";
            return _deck;
        }
    }

    
    
    public class Deck
    {
        public string Name { get; set; }
        public int HeroPower { get; set; }
        public string DeckContents { get; set; }
    }
}
