using CardGame.Managers;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame.Engine
{
    public static class SoundLoader
    {
        public static void LoadSounds(SoundManager soundManager, ContentManager content)
        {
            soundManager.AddSoundEffect("heroAttack_1", content.Load<SoundEffect>("Sounds/Hero_Attack_001"));
            soundManager.AddSoundEffect("draw", content.Load<SoundEffect>("Sounds/cards/draw"));
            soundManager.AddSoundEffect("addToHand", content.Load<SoundEffect>("Sounds/cards/bookOpen"));
            soundManager.AddSoundEffect("summoned", content.Load<SoundEffect>("Sounds/cards/summoned"));
            soundManager.AddSoundEffect("summonedStart", content.Load<SoundEffect>("Sounds/cards/warp"));
            soundManager.AddSoundEffect("readyToAttack", content.Load<SoundEffect>("Sounds/cards/readyToAttack"));
            soundManager.AddSoundEffect("hitTarget", content.Load<SoundEffect>("Sounds/cards/hitTarget"));
            soundManager.AddSoundEffect("playSpell", content.Load<SoundEffect>("Sounds/cards/playSpell"));
            soundManager.AddSoundEffect("draggingCard", content.Load<SoundEffect>("Sounds/cards/draggingCard"));
            soundManager.AddSoundEffect("cardPlace4", content.Load<SoundEffect>("Sounds/cards/cardPlace4"));

            soundManager.AddSoundEffect("curse", content.Load<SoundEffect>("Sounds/cards/curse"));
            soundManager.AddSoundEffect("iceball", content.Load<SoundEffect>("Sounds/cards/iceball"));
            soundManager.AddSoundEffect("lava", content.Load<SoundEffect>("Sounds/cards/lava"));
            soundManager.AddSoundEffect("magicfail", content.Load<SoundEffect>("Sounds/cards/magicfail"));
            soundManager.AddSoundEffect("shootFireball", content.Load<SoundEffect>("Sounds/cards/shootFireball"));
            soundManager.AddSoundEffect("SpellHit", content.Load<SoundEffect>("Sounds/cards/SpellHit"));
            soundManager.AddSoundEffect("niceHit", content.Load<SoundEffect>("Sounds/cards/niceHit"));
            soundManager.AddSoundEffect("normalHit", content.Load<SoundEffect>("Sounds/cards/normalHit"));


            soundManager.AddSoundEffect("Fire impact", content.Load<SoundEffect>("Sounds/cards/Fire impact"));
            soundManager.AddSoundEffect("GrowEffect", content.Load<SoundEffect>("Sounds/cards/GrowEffect"));
            soundManager.AddSoundEffect("Healing Full", content.Load<SoundEffect>("Sounds/cards/Healing Full"));
            soundManager.AddSoundEffect("Healspell", content.Load<SoundEffect>("Sounds/cards/healspell"));
            soundManager.AddSoundEffect("Ice attack 2", content.Load<SoundEffect>("Sounds/cards/Ice attack 2"));
            soundManager.AddSoundEffect("pling", content.Load<SoundEffect>("Sounds/cards/Misc 02"));
            soundManager.AddSoundEffect("playSound", content.Load<SoundEffect>("Sounds/cards/magicfail"));




        }
    }
}
