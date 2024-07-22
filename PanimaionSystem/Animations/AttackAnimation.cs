using CardGame.Cards;
using CardGame.Cards.PanimaionSystem;
using CardGame.Graphics;
using CardGame.Objects;
using CardGame.Objects.Cards;
using Engine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using static CardGame.Objects.Cards.MinionCard;

namespace CardGame.PanimaionSystem.Animations
{
    public class AttackAnimation : ObjectAnimation
    {
        private bool IsAnimating = true;
        private CardBoard_Actor attacker, target;
        private Vector2 startPos, endPos;
        public AttackAnimation(CardBoard_Actor attacker, CardBoard_Actor target, float speed=0.6f) : base(speed)
        {
            this.attacker = attacker;
            this.target = target;
            Width = 400;
            Height = 400;
            depth = 0.00000000001f;
            //SetPosition(actor.X, actor.Y);
        }
        public override void Update(GameTime gt, Game1 g)
        {
            if (IsAnimating)
            {
                elapsedTime += (float)gt.ElapsedGameTime.TotalSeconds;

                // Calculate the normalized time (0 to 1)
                double t = MathHelper.Clamp(elapsedTime / duration, 0f, 1f);

                t = 1 - Math.Sqrt(1 - Math.Pow(t, 2));


                // Interpolate position and scale
                attacker.X = MathHelper.Lerp(startPos.X, endPos.X, (float)t);
                attacker.Y = MathHelper.Lerp(startPos.Y, endPos.Y, (float)t);
                
                // Check if the animation is complete
                if (t >= 1f)
                {
                    IsAnimating = false;
                }
            }
            base.Update(gt, g);
        }
        protected override void AnimationFinished(Game1 g)
        {

            g.soundManager.PlaySound("niceHit");
            attacker.isAttacking = false;
            if(!(attacker is HeroActor))
            {
                Vector2 newPos = g.gameBoard.gameInterface.getPlayer(attacker.card.belongToPlayer).visualBoard.getBasePos(attacker);
                attacker.updateBaseposision(newPos.X, newPos.Y);
            }
            else
            {
                attacker.updateBaseposision(startPos.X, startPos.Y);
            }
        }
        public override void Start(Game1 g)
        {
            startPos = attacker.position;
            endPos = target.position;
            attacker.isAttacking = true;
            base.Start(g);
        }
        
    }
}
