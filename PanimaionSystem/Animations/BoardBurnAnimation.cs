using CardGame.Cards.PanimaionSystem;
using CardGame.Graphics;
using CardGame.Objects;
using CardGame.PanimaionSystem.GameObjectAnimation;
using Engine;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace CardGame.PanimaionSystem.Animations
{
    public class BoardBurnAnimation : ObjectAnimation
    {
        private List<FireBurningAnimation> animations;
        private Sprite fireSprite;
        private int numAnimations = 18;
        private string type;

        public BoardBurnAnimation(Sprite fireSprite, float duration = 2f, string type = "both") : base(duration)
        {
            this.fireSprite = fireSprite;
            animations = new List<FireBurningAnimation>();
            this.type = type;
        }

        public override void Start(Game1 g)
        {
            float minDuration = duration/2;
            int offset = 800;
            if(type.Equals("both") || type.Equals("top")){
                int _y = 600;
                for (int i = 0; i < numAnimations; i++)
                {
                    float normalizedPosition = (float)i / (numAnimations - 1);
                    float _duration = minDuration + (duration - minDuration) * 4 * normalizedPosition * (1 - normalizedPosition);

                    Vector2 randomPosition = new Vector2(offset + i*200, _y)+ GetRandomBoardPosition(g);
                    FireBurningAnimation fireAnimation = new FireBurningAnimation(randomPosition, fireSprite, duration: _duration);
                    fireAnimation.Start(g);
                    animations.Add(fireAnimation);
                }
            }
            if (type.Equals("both") || type.Equals("bottom"))
            {
                int _y = 1300;
                for (int i = 0; i < numAnimations; i++)
                {
                    float normalizedPosition = (float)i / (numAnimations - 1);
                    float _duration = minDuration + (duration - minDuration) * 4 * normalizedPosition * (1 - normalizedPosition);

                    Vector2 randomPosition = new Vector2(offset - 100 + i * 200, _y) + GetRandomBoardPosition(g);
                    FireBurningAnimation fireAnimation = new FireBurningAnimation(randomPosition, fireSprite, duration: _duration);
                    fireAnimation.Start(g);
                    animations.Add(fireAnimation);
                }
            }
            base.Start(g);
        }

        public override void Update(GameTime gt, Game1 g)
        {
            foreach (var animation in animations)
            {
                animation.Update(gt, g);
            }
            base.Update(gt, g);
        }

        public override void Draw(Game1 g)
        {
            foreach (var animation in animations)
            {
                animation.Draw(g);
            }
        }

        public override void Destroy(Game1 g)
        {
            // Cleanup logic if needed
        }

        private Vector2 GetRandomBoardPosition(Game1 g)
        {
            // Replace with your logic to get random positions on the board
            //int x = new Random().Next(0, 80);
            //int y = new Random().Next(0, 80);
            return new Vector2(0, 0);
        }
    }
}
