using CardGame.Cards;
using CardGame.Cards.PanimaionSystem;
using CardGame.Graphics;
using CardGame.Managers;
using CardGame.Objects.Cards;
using CardGame.Pages;
using CardGame.PanimaionSystem;
using Engine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using static CardGame.Objects.Cards.Card;
using static CardGame.Objects.Cards.MinionCard;

namespace CardGame.Objects
{
    public abstract class Card_Actor : GameObject, Clickable, Targeter, HoverLisner
    {
        public Card card;
        private static Sprite commonIcon = new Sprite(Textures.common);
        private static Sprite rareIcon = new Sprite(Textures.rare);
        private static Sprite epicIcon = new Sprite(Textures.epic);
        private static Sprite legendaryIcon = new Sprite(Textures.legendary);
        private static Sprite legendaryBoarder = new Sprite(Textures.legendaryBoarder);
        protected static Sprite armourIcon = new Sprite(Textures.armourIcon);


        private static Sprite powerCard = new Sprite(Textures.powerCard);
        private static Sprite manaIcon = new Sprite(Textures.Mana);
        private static Sprite tribeIcon = new Sprite(Textures.tribeTag);
        public float scale = 1f;
        protected bool drawManaCost = false;
        protected static Sprite blood = new Sprite(Textures.Health), attack = new Sprite(Textures.Attack);
        protected static Sprite OnBoard_art = new Sprite(Textures.Minion_Board_Icon), cardback = new Sprite(Textures.cardback);
        protected bool hideCardOnOpponentTurn = true;
        protected Dictionary<string, string> ValuesToDraw = new Dictionary<string, string>();
        public float baseX, baseY, baseWidth, baseHeight;

        protected int cardsCost, cardsBaseCost;
        public Card_Actor(Card card) : base(0, 0, 100, 100)
        {
            this.card = card;
            card.StatsChange += TriggerChangeCardValues;
            ChangeCardValues(card.getStats(), null);
            X = 50;
            Y = 50;
            Width = 540 * 1.5f;
            Height = 840 * 1.5f;
            scale = 1f;
            depth = 0.001f;
        }
        public override void Destroy(Game1 g)
        {
            g.game.getMouseManager().Remove(this);
            g.game.getMouseManager().RemoveHover(this);
            card.StatsChange -= TriggerChangeCardValues;
        }
        public void setNewBaseLocation(int x, int y)
        {
            baseX = x;
            baseY = y;
        }
        private void TriggerChangeCardValues(Game1 g, Dictionary<string, int> dict)
        {
            QueueManager.Enqueue(new GameAction((game) => {
                ChangeCardValues(dict, g);
            }));
        }
        protected virtual void ChangeCardValues(Dictionary<string, int> dict, Game1 g)
        {
            cardsCost = dict["cost"];
            cardsBaseCost = dict["baseCost"];
        }

        protected abstract void Activated(Game1 g);
        public abstract void GiveTarget(Game1 g, Card_Actor targetActor);
        public virtual void Clicked(float x, float y, Game1 g)
        {
            if (GetHitbox().Contains(x, y))
            {
                if (g.gameBoard.CanPlay())
                {
                    Activated(g);
                }
            }
        }

        public override void Init(Game1 g)
        {
            g.game.getMouseManager().Add(this);
            g.game.getMouseManager().AddHover(this);

            baseWidth = Width;
            baseHeight = Height;
        }

        public override void Update(GameTime gt, Game1 g){
            //X = baseX;
            //Y = baseY;
            //Width = baseWidth;
            //Height = baseHeight;
        }
        private void DrawCardBack(Game1 g)
        {
            cardback.Draw(new Vector2(X, Y), Width, Height, scale: scale, layerDepth: depth);
        }
        public static void drawACardBack(Game1 g, float X, float Y, float depth, int size = 540)
        {
            float Width = size;
            float Height = (840 * size) / 540;
            cardback.Draw(new Vector2(X, Y), Width, Height, layerDepth: depth);
        }

        public override void Draw(Game1 g)
        {
            
            if (g.game is GameBoard && g.gameBoard.isPlayer != card.belongToPlayer)
            {
                DrawCardBack(g);
                return;
            }
            drawCard(g, X, Y, card, depth, drawManaCost: drawManaCost, drawBaseStats: false, size: (int)Width);
        }
        public virtual Vector2 GetSourcePos(Game1 g)
        {
            Vector2 sourcePos;
            if (card.currentState == CardState.Hand)
            {
                //Hmm kanskje ha denne logiken i targeter.GetSourcePos(g) for å gi den mer kontroll
                
                sourcePos = g.gameBoard.gameInterface.getPlayer(card.belongToPlayer).heroActor.GetSourcePos(g);
            }
            else
            {
                sourcePos = GetPosCenter();
            }
            return sourcePos;
        }

        public virtual string GetTargetText(Game1 g)
        {
            return card.getText(g);
        }
        protected bool isHovering = false;
        public virtual void Hover(float x, float y, Game1 g)
        {
            if (GetHitbox().Contains(x, y))
            {
                onHover(g);
                isHovering = true;
            }
            else
            {
                offHover(g);
                isHovering = false;
            }
        }
        protected virtual void offHover(Game1 g)
        {
            if (isHovering)
            {
                TriggerOffHovered(g);
            }
        }
        protected virtual void onHover(Game1 g)
        {
            if (!isHovering)
            {
                TriggerHovered(g);
            }
        }
        protected virtual void TriggerHovered(Game1 g)
        {

        }
        protected virtual void TriggerOffHovered(Game1 g)
        {

        }

        public static void drawCard(Game1 g, float X, float Y, Card card, float depth, bool drawManaCost=true, bool drawBaseStats=false, int size=540)
        {
            
            string _mana="", _attack="", _health="";
            if (drawBaseStats)
            {
                _mana = card.BaseCost.ToString();
                if (card is MinionCard)
                {
                    _attack = ((MinionCard)card).BaseAttack.ToString();
                    _health = ((MinionCard)card).BaseHealth.ToString();
                }
                if (card is WeaponCard)
                {
                    _attack = ((WeaponCard)card).BaseAttack.ToString();
                    _health = ((WeaponCard)card).BaseHealth.ToString();
                }
            }
            else
            {
                _mana = Math.Max(card.Cost, 0).ToString();
                if(card is MinionCard)
                {
                    _attack = ((MinionCard)card).Attack.ToString();
                    _health = ((MinionCard)card).Health.ToString();
                }
                if (card is WeaponCard)
                {
                    _attack = ((WeaponCard)card).Attack.ToString();
                    _health = ((WeaponCard)card).Health.ToString();
                }

            }
            Sprite cardBase;
            if (card is SpellCard)
            {
                cardBase = new Sprite(Textures.CA);
            }
            else
            {
                cardBase = new Sprite(Textures.CA_minion);
            }
            //FACTION LOGIC
            if(card.faction == Card.Faction.Necrotic)
            {
                cardBase = new Sprite(Textures.necrotic_card);
            }
            if (card.faction == Card.Faction.Arcane)
            {
                cardBase = new Sprite(Textures.arcane_card);
            }
            if (card.faction == Card.Faction.Nature)
            {
                cardBase = new Sprite(Textures.nature_card);
            }
            if (card.faction == Card.Faction.Wild)
            {
                cardBase = new Sprite(Textures.order_card);
            }
            if (card.faction == Card.Faction.Order)
            {
                cardBase = new Sprite(Textures.ordercard2);
            }
            if (card.faction == Card.Faction.Discord)
            {
                cardBase = new Sprite(Textures.Discord_card);
            }
            if (card.faction == Card.Faction.Outworlders)
            {
                cardBase = new Sprite(Textures.outworlders_card);
            }
            

            float Width = size;
            float Height = (840 * size) / 540;
            float resize = Width / 540;
            Color manaColor = Color.White;
            if (card.Cost < card.BaseCost && !drawBaseStats)
            {
                manaColor = Color.Green;
            }
            else if (card.Cost > card.BaseCost && !drawBaseStats)
            {
                manaColor = Color.Red;
            }
            if (drawManaCost)
            {
                manaIcon.Draw(new Vector2(X - 80 * resize, Y - 50 * resize), 200 * resize, 200 * resize, layerDepth: depth * 0.2f);
                Drawing.DrawText(_mana, X+15, Y - 28 * resize, scale: 4 * resize, layerDepth: depth * 0.1f, border: true, color: manaColor, drawCenter: true);
            }
            if (card.PowerCard)
            {
                powerCard.Draw(new Vector2(X - 30 * resize, Y + 250 * resize), 140 * resize, 140 * resize, layerDepth: depth * 0.2f);
            }


            //Drawing.FillRect(GetHitbox(), Color.Blue, 0.000000001f, g);
            cardBase.Draw(new Vector2(X, Y), Width, Height, layerDepth: depth);
            float margin = 36*resize;
            card.cardArt.Draw(new Vector2(X + margin, Y+ margin), Width-(margin*2), Width - (margin*2), layerDepth: depth * 1.1f);

            //name
            float nameTextSize = Math.Min(2.4f * resize, TextHandler.GetFitScale(card.Name, Width * 0.89f));
            Drawing.DrawText(card.Name, X + (Width / 2), Y + 380 * resize+30*(((2.4f * resize)-nameTextSize)/(2.4f * resize)), scale: nameTextSize, layerDepth: depth * 0.1f, color: Color.Black, drawCenter: true);
            //Drawing.FillRect(new Rectangle((int)(X + (Width / 2)-5), (int)(Y + 400 * resize), 10, 200), Color.Blue, 0.000000000001f, g);
            //card text
            int _y = 0;
            int _yoffset = 35;
            float miniFont = 1.2f;
            int spacingY = 55;
            if (TextHandler.textLength(card.getText(g)) * 1.4f < 600)
            {
                miniFont = 1.2f;
                _yoffset = 40;
            }
            else if (TextHandler.textLength(card.getText(g)) * 1.4f >= 1600)
            {
                miniFont = 1f;
                spacingY = 45;
                _yoffset = 0;
            }
            else if (TextHandler.textLength(card.getText(g)) * 1.4f >= 1200)
            {
                miniFont = 1f;
                spacingY = 47;
                _yoffset = 0;
            }
            else if (TextHandler.textLength(card.getText(g)) * 1.4f >= 900)
            {
                miniFont = 1f;
                _yoffset = 20;
            }
            else if (TextHandler.textLength(card.getText(g)) * 1.4f >= 800)
            {
                miniFont = 1.2f;
                _yoffset = 10;
            }
            else if (TextHandler.textLength(card.getText(g)) * 1.4f >= 700)
            {
                miniFont = 1.2f;
                _yoffset = 10;
                _yoffset = 10;
            }
            foreach (string text in TextHandler.FitText(card.getText(g), 410 * resize, 1.4f * resize * miniFont))
            {
                Drawing.DrawFormattedText(text, new Vector2(X + Width / 2, Y + ((490 + _yoffset + _y * spacingY * miniFont) * resize)), scale: 1.4f * resize * miniFont, layerDepth: depth * 0.1f, color: Color.Black, drawCenter: true);
                _y++;
            }

            if (card is MinionCard)
            {
                //Attack
                Color attackColor = Color.White;
                if (((MinionCard)card).Attack > ((MinionCard)card).BaseAttack && !drawBaseStats)
                {
                    attackColor = Color.Green;
                }
                attack.Draw(new Vector2(X - (60 * resize), Y + (660 * resize)), 190 * resize, 190 * resize, layerDepth: depth * 0.2f);
                Drawing.DrawText(_attack, X + 35 * resize, Y + (700 * resize), scale: 4f * resize, color: attackColor, layerDepth: depth * 0.1f, border: true, drawCenter: true);

                //Health
                Color healthColor = Color.White;
                if (((MinionCard)card).Health < ((MinionCard)card).BaseHealth + ((MinionCard)card).ExtraHealth && !drawBaseStats)
                {
                    healthColor = Color.Red;
                }
                else if (((MinionCard)card).Health > ((MinionCard)card).BaseHealth && !drawBaseStats)
                {
                    healthColor = Color.Green;
                }
                blood.Draw(new Vector2(X + (390 * resize), Y + (620 * resize)), 230 * resize, 230 * resize, layerDepth: depth * 0.2f);
                Drawing.DrawText(_health, X + (510 * resize), Y + (700 * resize), scale: 4f * resize, layerDepth: depth * 0.1f, border: true, color: healthColor, drawCenter: true);


                //tribe
                if (((MinionCard)card).tribe != Tribe.None)
                {
                    Drawing.DrawText(((MinionCard)card).tribe.ToString(), X + Width / 2, Y + (728 * resize), scale: 2.5f * resize, layerDepth: depth * 0.1f, color: Color.Black, drawCenter: true);
       
                    tribeIcon.Draw(new Vector2(X + 60 * resize, Y + (718 * resize)), 500* resize  , 71 * resize*1.4f, layerDepth: depth * 0.9f);
                }
            }
            if (card is WeaponCard)
            {
                //Attack
                Color attackColor = Color.White;
                if (((WeaponCard)card).Attack > ((WeaponCard)card).BaseAttack && !drawBaseStats)
                {
                    attackColor = Color.Green;
                }
                attack.Draw(new Vector2(X - (60 * resize), Y + (660 * resize)), 190 * resize, 190 * resize, layerDepth: depth * 0.2f);
                Drawing.DrawText(_attack, X + (10 * resize), Y + (700 * resize), scale: 4f * resize, color: attackColor, layerDepth: depth * 0.1f, border: true);

                //Health
                Color healthColor = Color.White;
                if (((WeaponCard)card).Health < ((WeaponCard)card).BaseHealth + ((WeaponCard)card).ExtraHealth && !drawBaseStats)
                {
                    healthColor = Color.Red;
                }
                else if (((WeaponCard)card).Health > ((WeaponCard)card).BaseHealth && !drawBaseStats)
                {
                    healthColor = Color.Green;
                }
                armourIcon.Draw(new Vector2(X + (393 * resize), Y + (652 * resize)), 230 * resize, 230 * resize, layerDepth: depth * 0.2f);
                Drawing.DrawText(_health, X + (510 * resize), Y + (700 * resize), scale: 4f * resize, layerDepth: depth * 0.1f, border: true, color: healthColor, drawCenter: true);

            }
            //Draw rarity
            Sprite gemIcon = commonIcon; //todo fix default no gem
            if (card.grade == Card.Grade.Legendary)
            {
                gemIcon = legendaryIcon;
                legendaryBoarder.Draw(new Vector2(X - (74 * resize), Y - (150 * resize)), 325 *2.2f  * resize, 204 * 2.2f * resize, layerDepth: depth * 0.99f);
                gemIcon.Draw(new Vector2(X + 195 * resize, Y - 120 * resize), 150 * resize, 150 * resize, layerDepth: depth * 0.2f);
            }
            else if (card.grade == Card.Grade.Epic)
            {
                gemIcon = epicIcon;
            }
            else if (card.grade == Card.Grade.Rare)
            {
                gemIcon = rareIcon;
            }
            else if (card.grade == Card.Grade.Common)
            {
                gemIcon = commonIcon;
            }
            if(card.grade != Card.Grade.Legendary)
            {
                gemIcon.Draw(new Vector2(X - 120 * resize + Width, Y - 10 * resize), 140 * resize, 140 * resize, layerDepth: depth * 0.2f);
            }
            
        }
    }
}
