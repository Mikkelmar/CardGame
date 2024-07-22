using CardGame.Graphics;
using CardGame.Managers;
using CardGame.Objects.Cards;
using Engine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame.Objects
{
    public class SetPageButton : GameObject, Clickable
    {

        private static Sprite NatureIcon = new Sprite(Textures.NatureIcon);
        private static Sprite NecroticIcon = new Sprite(Textures.NecroticIcon);
        private static Sprite OrderIcon = new Sprite(Textures.OrderIcon);
        private static Sprite WildIcon = new Sprite(Textures.WildIcon);
        private static Sprite DiscordIcon = new Sprite(Textures.DiscordIcon);
        private static Sprite ArcaneIcon = new Sprite(Textures.ArcaneIcon);
        private int page, lastPage;
        private Card.Faction factionButton;
        public SetPageButton(int value, int lastPage, Card.Faction faction)
        {
            page = value;
            this.lastPage = lastPage;
            this.factionButton = faction;
        }
        public void Clicked(float x, float y, Game1 g)
        {
            if (this.GetHitbox().Contains(x, y))
            {
                g.collectionPage.collectionManager.setPage(g, page);
            }
        }

        public override void Destroy(Game1 g)
        {
            g.collectionPage.mouseManager.Remove(this);
        }

        public override void Draw(Game1 g)
        {
            Sprite factionIcon;
            switch (factionButton){
                case Card.Faction.Arcane:
                    factionIcon = ArcaneIcon;
                    break;
                case Card.Faction.Order:
                    factionIcon = OrderIcon;
                    break;
                case Card.Faction.Discord:
                    factionIcon = DiscordIcon;
                    break;
                case Card.Faction.Wild:
                    factionIcon = WildIcon;
                    break;
                case Card.Faction.Nature:
                    factionIcon = NatureIcon;
                    break;
                case Card.Faction.Necrotic:
                    factionIcon = NecroticIcon;
                    break;

                default:
                    factionIcon = ArcaneIcon;
                    break;
            }
            factionIcon.Draw(position, Width, Height, layerDepth: depth);

            if (g.collectionPage.collectionManager.PageNumber >= page && g.collectionPage.collectionManager.PageNumber <= lastPage) {
                Drawing.FillRect(GetHitbox(), Color.Green, depth+ depth, g);
                Drawing.DrawText(factionButton.ToString(), 2340, Y, layerDepth: depth, color: Color.White, border: true, drawCenter: true, scale: 4f);
            }
            else{
                Drawing.FillRect(GetHitbox(), Color.Yellow, depth+ depth, g);
            }

            
        }

        public override void Init(Game1 g)
        {
            g.collectionPage.mouseManager.Add(this);
        }

        public override void Update(GameTime gt, Game1 g)
        {
        }
    }
}
