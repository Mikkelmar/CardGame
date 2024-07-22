using CardGame.Cards.PanimaionSystem;
using CardGame.Objects;
using Engine;
using Microsoft.Xna.Framework;

namespace CardGame.PanimaionSystem.Animations
{
    public class TriggerEffectAnimation : ObjectAnimation
    {
        private CardBoard_Actor objectToTrigger;
        private float alphaValue = 0f;
        public TriggerEffectAnimation(CardBoard_Actor actor, float speed=2f) : base(speed)
        {
            objectToTrigger = actor;
            Width = actor.Width;
            Height = actor.Height;
            depth = actor.depth* 0.001f;
        }
        public override void Update(GameTime gt, Game1 g)
        {
            elapsedTime += (float)gt.ElapsedGameTime.TotalSeconds;

            float t = MathHelper.Clamp(elapsedTime / duration, 0f, 1f);

         
            alphaValue = MathHelper.Lerp(0f, 1f, t);
                
                
            base.Update(gt, g);
            
        }
        public override void Start(Game1 g)
        {
            g.gameBoard.actionHistoryManager.LogCardEffectTrigger(g, objectToTrigger.card);
            base.Start(g);
        }
        public override void Draw(Game1 g)
        {
            float resize = objectToTrigger.Width / 360;
            Drawing.FillRect(
                new Rectangle(
                   (int)(objectToTrigger.X + (100 * resize)), (int)(objectToTrigger.Y + (245 * resize)), (int)(160 * resize), (int)(160 * resize)),
                Color.Yellow,
                depth,
                g,
                alphaValue);
        }
        
    }
}
