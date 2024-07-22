using CardGame.Graphics;
using CardGame.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame.PanimaionSystem.GameObjectAnimation
{
    public static class GameObjectAnimationList
    {

        private static Sprite SpellDamageAnimation = new Sprite(Textures.SpellDamageAnimation);
        private static Sprite PurpleFlameAnimation = new Sprite(Textures.PurpleFlameAnimation);
        
        public static GameObjectAnimation getSpellDamageAnimation(Card_Actor actor)
        {
            GameObjectAnimation animation = new GameObjectAnimation(actor, SpellDamageAnimation, frameWidth: 128, frameHeight: 128, frameCount: 64, frameTime: 0.1f, 8);
            animation.ID = "SPELLDAMAGE";
            return animation;
        }
        public static GameObjectAnimation getCursedAnimation(Card_Actor actor)
        {
            GameObjectAnimation animation = new GameObjectAnimation(actor, PurpleFlameAnimation, frameWidth: 100, frameHeight: 100, frameCount: 61, frameTime: 0.1f, 8);
            animation.ID = "CURSE";
            return animation;
        }

    }
}
