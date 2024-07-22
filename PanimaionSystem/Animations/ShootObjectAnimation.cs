
using CardGame.Cards.PanimaionSystem;
using CardGame.Graphics;
using CardGame.Objects.Cards;
using Microsoft.Xna.Framework;

namespace CardGame.PanimaionSystem.Animations
{
    public class ShootObjectAnimation : ObjectAnimation
    {
        protected Sprite bullet;
        private bool IsAnimating = true;
        private Card source, target;
        private Vector2 startPos, endPos;
        public ShootObjectAnimation(Card source, Card target, Sprite texture, float speed=2f) : base(speed)
        {
            this.source = source;
            this.target = target;
            Width = 100;
            Height = 100;
            depth = 0.00000000001f;
            this.bullet = texture;
        }
        public override void Update(GameTime gt, Game1 g)
        {
            if (IsAnimating)
            {
                elapsedTime += (float)gt.ElapsedGameTime.TotalSeconds;

                // Calculate the normalized time (0 to 1)
                float t = MathHelper.Clamp(elapsedTime / duration, 0f, 1f);

         

                // Interpolate position and scale
                X = MathHelper.Lerp(startPos.X, endPos.X, t);
                Y = MathHelper.Lerp(startPos.Y, endPos.Y, t);

                // Check if the animation is complete
                if (t >= 1f)
                {
                    IsAnimating = false;
                }
            }
            base.Update(gt, g);
        }
        public override void Start(Game1 g)
        {
            //idk fix this later tired rn
            if(AnimationUtils.getCardActor(g, source) == null)
            {
                startPos = g.gameBoard.gameInterface.getPlayer(source.belongToPlayer).heroActor.GetPosCenter();
            }
            else {
                startPos = AnimationUtils.getCardActor(g, source).GetPosCenter();
            }
            
            endPos = AnimationUtils.getCardActor(g, target).GetPosCenter();
            base.Start(g);
        }
        public override void Draw(Game1 g)
        {
            bullet.Draw(position, Width, Height, layerDepth: depth);
        }
        
    }
}
