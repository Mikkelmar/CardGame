using CardGame.Graphics;
using CardGame.Managers;
using CardGame.Objects.Cards;
using Engine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CardGame.Objects.Collection
{
    public class DeckDisplay : GameObject
    {
        List<CardDeckDisplay_Actor> cardsActors = new List<CardDeckDisplay_Actor>();
        Dictionary<Card, int> deck;
        DeckManaCostsDisplay deckManaCostsDisplay;
        private Dictionary<Card.Faction, float> factionYCoordinates = new Dictionary<Card.Faction, float>();
        private static Sprite NatureIcon = new Sprite(Textures.NatureIcon);
        private static Sprite NecroticIcon = new Sprite(Textures.NecroticIcon);
        private static Sprite OutworldersIcon = new Sprite(Textures.OutworldersIcon); 
        private static Sprite OrderIcon = new Sprite(Textures.OrderIcon);
        private static Sprite WildIcon = new Sprite(Textures.WildIcon);
        private static Sprite DiscordIcon = new Sprite(Textures.DiscordIcon);
        private static Sprite ArcaneIcon = new Sprite(Textures.ArcaneIcon);

        private float scrollOffset = 0f;
        private float maxDisplayHeight = 2600f; // Set this to your desired max display height
        private float currentLength = 0f;
        public DeckDisplay()
        {
            X = 160;
            Y = 300;
            deckManaCostsDisplay = new DeckManaCostsDisplay();
        }

        public override void Draw(Game1 g)
        {
            
            Dictionary<Card.Faction, int> cardsHave = g.collectionPage.collectionManager.deckBuilder.GetCardCountByFaction();
            Dictionary<Card.Faction, int> cardsNeed = g.collectionPage.collectionManager.deckBuilder.GetRequiredCardCountByFaction();
            Vector2 position = new Vector2(100, 0);

            position.X += DrawRequirements(g, 30, g.collectionPage.collectionManager.deckBuilder.GetTotalCardCount(), "Deck",
                new Vector2(X, Y - 200 + scrollOffset));
            position.X += 20;

            Drawing.DrawText($"Page {g.collectionPage.collectionManager.PageNumber + 1}/{g.collectionPage.collectionManager.PageCount}",
                4400, 1320, scale: 3f, layerDepth: depth * 0.1f, color: Color.White, drawCenter: true, border: true);

            // Draw faction headers and requirements above each faction's card list
            foreach (var faction in factionYCoordinates.Keys.ToList())
            {
                float yCoordinate = factionYCoordinates[faction] + scrollOffset;
                if (yCoordinate + 160 < Y || yCoordinate > Y + maxDisplayHeight)
                {
                    continue; // Skip drawing factions outside the visible area
                }

                if (cardsHave.ContainsKey(faction) && cardsHave[faction] > 0)
                {
                    string factionName = faction.ToString();
                    int have = cardsHave.ContainsKey(faction) ? cardsHave[faction] : 0;
                    int need = cardsNeed.ContainsKey(faction) ? cardsNeed[faction] : 0;
                    DrawRequirements(g, need, have, factionName, new Vector2(X, yCoordinate - 110));
                    Sprite icon = null;
                    switch (faction)
                    {
                        case Card.Faction.Arcane:
                            icon = ArcaneIcon;
                            break;
                        case Card.Faction.Order:
                            icon = OrderIcon;
                            break;
                        case Card.Faction.Necrotic:
                            icon = NecroticIcon;
                            break;
                        case Card.Faction.Discord:
                            icon = DiscordIcon;
                            break;
                        case Card.Faction.Nature:
                            icon = NatureIcon;
                            break;
                        case Card.Faction.Wild:
                            icon = WildIcon;
                            break;
                        case Card.Faction.Outworlders:
                            icon = OutworldersIcon;
                            break;
                    }
                    if (icon != null)
                    {
                        icon.Draw(new Vector2(X - 140, yCoordinate - 120), 160, 160, layerDepth: depth * 0.002f);
                    }
                }
                else
                {
                    // Remove the faction from the dictionary if it has no cards
                    factionYCoordinates.Remove(faction);
                }
            }
        }

        private float DrawRequirements(Game1 g, int need, int have, string name, Vector2 pos)
        {
            float _xOffset = 0;
            Color textColor = Color.Green;
            if (have < need)
            {
                textColor = Color.Red;
            }
            string text = (have.ToString() + "/" + need.ToString() + "  " + name);
            Drawing.DrawText(text,
                pos.X + 10, pos.Y + 20, scale: 3f, layerDepth: depth * 0.01f, color: textColor, drawCenter: false, border: true);
            _xOffset += TextHandler.textLength(text) * 3f;
            _xOffset += 50;
            return _xOffset;
        }

        public void updateDeck(Game1 g, Dictionary<Card, int> newDeck)
        {
            // Remove all current card actors
            foreach (CardDeckDisplay_Actor ca in cardsActors)
            {
                g.collectionPage.objectManager.Remove(ca, g);
            }
            cardsActors.Clear();
            deckManaCostsDisplay.updateDeck(g, newDeck);
            deck = newDeck;

            float resize = 1f;
            float sizeX = 650 * resize;
            float ySpacing = sizeX / (512 / 100f); // Adjust the y spacing based on the card size

            // Initialize dictionary to hold lists of cards by faction
            var cardsByFaction = new Dictionary<Card.Faction, List<KeyValuePair<Card, int>>>();

            // Initialize lists for each faction
            foreach (Card.Faction faction in Enum.GetValues(typeof(Card.Faction)))
            {
                cardsByFaction[faction] = new List<KeyValuePair<Card, int>>();
            }

            // Group cards by faction
            foreach (var kvp in newDeck)
            {
                Card card = kvp.Key;
                cardsByFaction[card.faction].Add(kvp);
            }

            float currentY = Y;
            factionYCoordinates.Clear(); // Clear the previous coordinates

            foreach (var faction in cardsByFaction.Keys)
            {
                var factionGroup = cardsByFaction[faction];
                if (factionGroup.Count == 0)
                {
                    continue; // Skip factions with no cards
                }

                // Store the starting Y coordinate for this faction
                factionYCoordinates[faction] = currentY;

                int cardIndex = 0;
                foreach (var kvp in factionGroup)
                {
                    Card card = kvp.Key;
                    int count = kvp.Value;

                    CardDeckDisplay_Actor ca = new CardDeckDisplay_Actor(card, count)
                    {
                        X = X,
                        Y = currentY + (ySpacing * cardIndex) + scrollOffset,
                        Width = sizeX,
                        Height = sizeX / (512 / 100f)
                    };

                    // Add only if within the visible area
                    if (ca.Y + ca.Height >= Y && ca.Y <= Y + maxDisplayHeight)
                    {
                        cardsActors.Add(ca);
                        g.collectionPage.objectManager.Add(ca, g);
                    }

                    cardIndex++;
                }
                // Move the currentY down to start the next faction group
                currentY += (ySpacing * (cardIndex + 1)); // Add extra spacing between factions
            }
            currentLength = currentY;
        }

        public void Scroll(Game1 g, float amount)
        {
            scrollOffset += amount;
            scrollOffset = Math.Min(0, scrollOffset);
            scrollOffset = Math.Max(Math.Min(-currentLength + 300 + maxDisplayHeight, 0), scrollOffset);
            
            
            updateDeck(g, deck);
        }

        public override void Init(Game1 g)
        {
            deck = new Dictionary<Card, int>();
            DeckBuilder.DeckUpdate += updateDeck;
            g.collectionPage.objectManager.Add(deckManaCostsDisplay, g);
        }

        public override void Destroy(Game1 g)
        {
            g.collectionPage.objectManager.Remove(deckManaCostsDisplay, g);
            DeckBuilder.DeckUpdate -= updateDeck;
        }

        public override void Update(GameTime gt, Game1 g)
        {
        }
    }
}
