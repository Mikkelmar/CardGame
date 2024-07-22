using CardGame.Graphics;
using CardGame.Managers.GameManagers;
using CardGame.Objects;
using CardGame.Objects.Cards;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame.PanimaionSystem.Animations
{
    public static class AnimationList
    {
        private static Sprite bullet = new Sprite(Textures.bullet);
        private static Sprite frostBolt = new Sprite(Textures.frostbolt);
        private static Sprite MissileAnimation = new Sprite(Textures.MissileAnimation);
        private static Sprite FireAnimation = new Sprite(Textures.FireAnimation);
        private static Sprite GreenCastAnimation = new Sprite(Textures.GreenCastAnimation);
        private static Sprite BlueCastAnimation = new Sprite(Textures.BlueCastAnimation);
        private static Sprite BiteAnimation = new Sprite(Textures.BiteAnimation);
        private static Sprite SlashAnimation = new Sprite(Textures.SlashAnimation);
        private static Sprite PentragramAnimation = new Sprite(Textures.PentragramAnimation);
        private static Sprite HealAnimation = new Sprite(Textures.HealAnimation);
        private static Sprite BlessAnimation = new Sprite(Textures.BlessAnimation);
        private static Sprite IceBoltAnimation = new Sprite(Textures.IceBoltAnimation);
        private static Sprite CrystalHitAnimation = new Sprite(Textures.CrystalHitAnimation);
        private static Sprite SunAnimation = new Sprite(Textures.SunAnimation);
        private static Sprite ExplotionAnimation = new Sprite(Textures.ExplotionAnimation);

        private static Sprite flame_fireAnimation = new Sprite(Textures.flame_fireAnimation);
        private static Sprite flame_fireAnimationBlue = new Sprite(Textures.flame_fireAnimationBlue);
        private static Sprite flame_fireAnimationPurple = new Sprite(Textures.flame_fireAnimationPurple);
        private static Sprite flame_fireAnimationWhite = new Sprite(Textures.flame_fireAnimationWhite);

        public static MagicMissileAnimation createMagicMissileAnimation(Game1 g, Card fromCard, Card TargetCard)
        {
            MagicMissileAnimation ani = new MagicMissileAnimation(
                fromCard,
                TargetCard,
                MissileAnimation,
                1.8f);
            return ani;
        }
        

        public static FireballAnimation createFireballAnimation(Game1 g, Card fromCard, Card TargetCard)
        {
            FireballAnimation ani = new FireballAnimation(
                fromCard,
                TargetCard,
                FireAnimation,
                2f);
            return ani;
        }
        public static BoardBurnAnimation createRedFireBoardAnimation(
            Game1 g, float duration = 2f, Player onPlayer=null, string color = "red")
        {
            Sprite _sprite = flame_fireAnimation;
            switch (color)
            {
                case "blue":
                    _sprite = flame_fireAnimationBlue;
                    break;
                case "white":
                    _sprite = flame_fireAnimationWhite;
                    break;
                case "purple":
                    _sprite = flame_fireAnimationPurple;
                    break;
            }
            string type = "both";
            if(g.isClient && onPlayer!= null)
            {
                if(g.gameBoard.isPlayer.id == onPlayer.id)
                {
                    type = "bottom";
                }
                else
                {
                    type = "top";
                }
            }
            BoardBurnAnimation ani = new BoardBurnAnimation(
                _sprite,
                duration,
                type);
            return ani;
        }
        
        public static PyroBallAnimation createPyroBallAnimation(Game1 g, Card fromCard, Card TargetCard, float duration=3f)
        {
            PyroBallAnimation ani = new PyroBallAnimation(
                fromCard,
                TargetCard,
                SunAnimation,
                duration);
            return ani;
        }
        public static SpellCastAnimation createCastAnimation(Game1 g, Card fromCard, float duration=2f)
        {
            SpellCastAnimation ani = new SpellCastAnimation(
                fromCard,
                GreenCastAnimation,
                duration);
            return ani;
        }
        public static SpellCastAnimation createPentagramAnimation(Game1 g, Card fromCard, float duration = 0.7f)
        {
            SpellCastAnimation ani = new SpellCastAnimation(
                fromCard,
                PentragramAnimation,
                duration);
            return ani;
        }
        public static SpellCastAnimation createArcaneCastAnimation(Game1 g, Card fromCard, float duration=1f)
        {
            SpellCastAnimation ani = new SpellCastAnimation(
                fromCard,
                BlueCastAnimation,
                duration);
            return ani;
        }
        public static SpellTargetAnimation createBiteAnimation(Game1 g, Card fromCard, float duration = 1f)
        {
            SpellTargetAnimation ani = new SpellTargetAnimation(
                fromCard,
                BiteAnimation,
                duration);
            return ani;
        }
        public static SpellTargetAnimation createExplotionAnimation(Game1 g, Card fromCard, float duration = 1f)
        {
            SpellTargetAnimation ani = new SpellTargetAnimation(
                fromCard,
                ExplotionAnimation,
                duration);
            return ani;
        }
        
        public static SpellTargetAnimation createCrystalHitAnimation(Game1 g, Card fromCard, float duration = 1f)
        {
            SpellTargetAnimation ani = new SpellTargetAnimation(
                fromCard,
                CrystalHitAnimation,
                duration,
                scale: 0.5f);
            return ani;
        }
        
        public static SpellTargetAnimation createHealAnimation(Game1 g, Card fromCard, float duration = 1f)
        {
            SpellTargetAnimation ani = new SpellTargetAnimation(
                fromCard,
                HealAnimation,
                duration);
            return ani;
        }
        public static SpellTargetAnimation createBlessAnimation(Game1 g, Card fromCard, float duration = 1f)
        {
            SpellTargetAnimation ani = new SpellTargetAnimation(
                fromCard,
                BlessAnimation,
                duration);
            return ani;
        }
        public static SpellTargetAnimation createSlashAnimation(Game1 g, Card fromCard, float duration = 1f)
        {
            SpellTargetAnimation ani = new SpellTargetAnimation(
                fromCard,
                SlashAnimation,
                duration);
            return ani;
        }

        public static ShootObjectAnimation createBulletAnimation(Game1 g, Card fromCard, Card TargetCard)
        {
            ShootObjectAnimation ani = new ShootObjectAnimation(
                fromCard,
                TargetCard,
                bullet,
                0.5f);
            return ani;
        }
        public static FrostBoltAnimation createFrostBoltAnimation(Game1 g, Card fromCard, Card TargetCard, float duration = 1.5f)
        {
            FrostBoltAnimation ani = new FrostBoltAnimation(
                fromCard,
                TargetCard,
                IceBoltAnimation,
                duration);
            return ani;
        }




        
    }
}
