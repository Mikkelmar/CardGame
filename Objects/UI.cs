using CardGame.Graphics;
using CardGame.Managers;
using CardGame.Managers.GameManagers;
using CardGame.Objects.Cards;
using CardGame.PanimaionSystem;
using CardGame.PanimaionSystem.Animations;
using Engine;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using System.Linq;

namespace CardGame.Objects
{
    public class UI : GameObject, HoverLisner
    {

        private static Sprite NatureIcon = new Sprite(Textures.NatureIcon);
        private static Sprite NecroticIcon = new Sprite(Textures.NecroticIcon);
        private static Sprite OrderIcon = new Sprite(Textures.OrderIcon);
        private static Sprite WildIcon = new Sprite(Textures.WildIcon);
        private static Sprite DiscordIcon = new Sprite(Textures.DiscordIcon);
        private static Sprite ArcaneIcon = new Sprite(Textures.ArcaneIcon);
        private static Sprite FullDeck = new Sprite(Textures.FullDeck);
        private static Sprite CardBack = new Sprite(Textures.cardback);
        private static Sprite EmptyMana = new Sprite(Textures.EmptyMana);
        private static Sprite ManaCrystal = new Sprite(Textures.Mana);

        private bool player1DeckHover = false, player2DeckHover = false;
        public override void Destroy(Game1 g)
        {

            g.gameBoard.mouseManager.RemoveHover(this);
        }

        public override void Draw(Game1 g)
        {
            Player bottomPlayer = g.gameBoard.isPlayer;
            Player topPlayer = g.gameBoard.gameHandler.getOpponent(g.gameBoard.isPlayer);
            drawBloodRunesPlayer(g, bottomPlayer, 1900);
            drawManaPlayer(g, bottomPlayer, 2000);
            //drawCardPlayer(g, bottomPlayer, 2050);
            drawCardDeckPlayer(g, bottomPlayer, 1400);
            drawDeckIconsPlayer(g, bottomPlayer, 2110);
            DrawPlayerName(g, bottomPlayer, 2310);

            drawBloodRunesPlayer(g, topPlayer, 400);
            drawManaPlayer(g, topPlayer, 500);
            //drawCardPlayer(g, topPlayer, 750);
            drawCardDeckPlayer(g, topPlayer, 620);
            drawDeckIconsPlayer(g, topPlayer, 260);
            DrawPlayerName(g, topPlayer, 180);

            //TODO move this elsewhere
            if (g.gameBoard.gameHandler.SelectingTarget)
            {
                Vector2 sourcePos = g.gameBoard.gameHandler.targeter.GetSourcePos(g);
                Vector2 targetPos = g.gameBoard.mouseManager.GetMousePos(g);
                Drawing.DrawLine(Textures.arrowPointer, sourcePos, targetPos);
                
                Drawing.DrawText(g.gameBoard.gameHandler.targeter.GetTargetText(g), targetPos.X - 20, targetPos.Y + 50, layerDepth: 0.0000001f, scale: 3f);
            }
        }

        private void DrawPlayerName(Game1 g, Player player, int y)
        {
            Drawing.DrawText(player.Hero.Name, 120, y, color: Color.DarkBlue, scale: 3f);
        }
        private void drawDeckIconsPlayer(Game1 g, Player player, int y)
        {
            try { 
                var sortedList = player.originalDeck
                    .Where(entry => entry.Value > 0)
                    .OrderBy(entry => entry.Value)
                    .Select(entry => entry.Key)
                    .ToList();
                int space = 0;
                foreach (var entry in sortedList)
                {

                    Sprite icon = null;
                    switch (entry)
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
                    }
                    if (icon != null)
                    {
                        int size = 200;
                        icon.Draw(new Vector2(100+ space, y), 200, 200, layerDepth: 0.002f);
                        space += 200;
                    }

                }
            }
            catch
            {
                //todo remove try catch when action q system is in place
            }
        }
        private void drawManaPlayer(Game1 g, Player player, int y)
        {
            int size = 100;
            int xPos = 3600;
            for(int i=0;i< player.Mana; i++)
            {
                if(i >= player.CurrentMana)
                {
                    EmptyMana.Draw(new Vector2(xPos + size*i, y), size, size, layerDepth: 0.002f);
                }
                else
                {
                    ManaCrystal.Draw(new Vector2(xPos + size * i, y), size, size, layerDepth: 0.002f);
                }
            }
            string manaText = player.CurrentMana + "/" + player.Mana;
            Drawing.DrawText(manaText, xPos-6-TextHandler.textLength(manaText)*3f, y, color: Color.DarkBlue, scale: 3f);
        }
        private void drawCardPlayer(Game1 g, Player player, int y)
        {
            Drawing.DrawText(player.Hand.getCards().Count+ " hand", 4400, y, color: Color.Orange, scale: 4.3f);
        }
        private void drawBloodRunesPlayer(Game1 g, Player player, int y)
        {
            Drawing.DrawText(player.BloodRunes.ToString() + " Blood runes", 3500, y, color: Color.DarkRed, scale: 3f);
        }
        private void drawCardDeckPlayer(Game1 g, Player player, int y)
        {
            //Drawing.DrawText(player.Deck.getCards().Count + " deck", 4400, y, color: Color.Gray, scale: 3.5f);
            if(player1DeckHover && player.id == 1)
            {
                Drawing.DrawText(player.Deck.getCards().Count + " deck", 4400, y, color: Color.Gray, scale: 3.5f, layerDepth: 0.00000001f);
                Drawing.DrawText(player.Hand.getCards().Count + " hand", 4400, y+100, color: Color.Orange, scale: 4.3f, layerDepth: 0.00000001f);
            }
            if (player2DeckHover && player.id == 2)
            {
                Drawing.DrawText(player.Deck.getCards().Count + " deck", 4400, y, color: Color.Gray, scale: 3.5f, layerDepth: 0.00000001f);
                Drawing.DrawText(player.Hand.getCards().Count + " hand", 4400, y + 100, color: Color.Orange, scale: 4.3f, layerDepth: 0.00000001f);
            }

            if (player.Deck.Cards.Count > 0)
            {
                float fullpercent = ((float)player.Deck.Cards.Count) / 30;
                CardBack.Draw(new Vector2(4285+100* (1-fullpercent), y), 400, 560, layerDepth: 0.002f);
                FullDeck.Draw(new Vector2(4300, y), 500, 560, layerDepth: 0.003f);
            }
            else
            {
                //out of cards
            }
            
        }

        public override void Init(Game1 g)
        {
            g.gameBoard.mouseManager.AddHover(this);
            g.gameBoard.gameHandler.player1.CardDiscarded += createDiscardCardAnimation;
            g.gameBoard.gameHandler.player2.CardDiscarded += createDiscardCardAnimation;

        }
        private void createDiscardCardAnimation(Game1 g, Card card)
        {
            CardHand_Actor actor = g.gameBoard.gameInterface.getPlayer(card.belongToPlayer).visualHand.getCardActor(card);
            if (actor == null)
            {
                return;
            }
            Vector2 startPos = actor.position;
            Vector2 endpos = startPos + new Vector2(0, 900);
            if (card.belongToPlayer.id == g.gameBoard.isPlayer.id)
            {
                endpos = startPos - new Vector2(0, 900);
            }
            QueueManager.Enqueue(new DiscardCardAnimation(card, 3.1f, startPos, endpos));
        }

        public override void Update(GameTime gt, Game1 g)
        {
        }

        public void Hover(float x, float y, Game1 g)
        {

            if(new Rectangle(4300, 620, 500, 560).Contains(x, y))
            {
                setPlayerHover(true, 2, g);
            }
            else
            {
                setPlayerHover(false, 2, g);
            }

            if (new Rectangle(4300, 1400, 500, 560).Contains(x, y))
            {
                setPlayerHover(true, 1, g);
            }
            else
            {
                setPlayerHover(false, 1, g);
            }
        }
        private void setPlayerHover(bool hover, int player, Game1 g)
        {
            if(g.gameBoard.isPlayer.id == player)
            {
                player1DeckHover = hover;
            }
            else
            {
                player2DeckHover = hover;
            }
        }
    }
}
