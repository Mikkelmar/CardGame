using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame.Graphics
{
    class Textures
    {
        //CA == Card art
        public static Texture2D CA, CA_minion, CA_rune_golem2, CA_runeGolem, Minion_Board_Icon, Health, Mana, Attack,
            CA_Themzam, CA_magic_missiles, cardback, CA_flameMage, arrowPointer, CA_gonlinrider, CA_bot, CA_goblincool, CA_knight,
            CA_goblinwarrior, CA_mageGoblin, CA_spellWizard, CA_summon, CA_summonMage, CA_summonPurple, CA_conuring, CA_ritual,
            CA_undead, CA_magic_skeleton, CA_skeleton, CA_icebolt, powerShield, poison, taunt, triggerEffect, skull, frostbolt, bullet,
            summonCard, CA_manawyrm, aura, DieCard, CA_dragonSmith, CA_wisdom, common, rare, epic, legendary, necrotic_card, arcane_card,
            wild_card, nature_card, order_card, legendaryBoarder, temp_art, usedHeroPower, largeSpider, flamestrike, spellSpammer, lightBlast,
            sanctumHealing, fieldmagic, recruit2, gateGuard, Vasnji, bonecolector, newmanagement, goblinHunter,
                dreadlitch, darkAprentice, falseLife, angryMage, arcaneDrake, armourIcon, spellCastinGoblin, powerCard,
            gigaDrill, warrior, shieldslam, blessed, shield, ghast, cryptStalker, giantSkeleton, dragonSpellSmith,
            dragonSpellCaster, eggDragonGirl, eggDragonGirl2, dragonRockCollector, chillGemDragon, Discord_card, ordercard2,
            frozenMinion, nestGuard, frostDrake, pristess2, lockIcon, commander2, witherSkeletons, assassinBow, smuggler, crockDefender,
            spy1, assassinToken2, infoBroker2, crazedCultist, ragingFanatic, inTheFlesh2, painDraw, agentofWar, temple, painBringer,
            painBuffer, syabsyabsyab, prep, freshCut, nestGuard2, commander, redbear, fleshwalker, panther, spiderEgg, bloodCultist, blizzard, frostNova2,
            giantMossy, meril, protetiveMinion, necroGirl, bladeOfTheTemple, cruul2, undeadArmy2, bloodCopy,
            goblinSmall3, hillshaman, greatsword, moltenBlast, fireball2, sappling, hyena, manaBurst, ManaGrowth, StrongGobling, hunter_arrow,
            goblinSpellCaster, ForestDefender, forestCall, shootImageArrow, animalFriend, SwapTurtle, ForbiddenKnowledge, epic_knight, necroLizzard,
            bloodPerson, drums, deathPotion, hellfire, smithArmour, cannon, buzzard, wolf, spirits, drums2, FrostSkeleton,
            forestGiant, rockGolem, worldEnd, whitlwind, dragon, forestFather, tundraStag, returnThing, atgus, healingAura,
            GlowTree, JungleGirl, forestCreature, SerpentKey, ShadyPotion, goblinPack2, spiderEggNew, lizzardWhip, goblinTrader,
            elderLitch, arcaneBlastGem, underWorldGate, knowledgeDrake, anvil, darkMage, discoverMage, soulSuck, assassin,
            animalHealer, deathparrot,
            scorpion, ArrowHP, healing, savageRoar, sacredSword, ritualDagger, mace, woodenPuppet, fieldMech, FacelessSurpent,
            ClockWorkUnit, clockworkMachine, ProtectiveCompanion2, puppetMaster, mapWriter, magicEgg, doubleBlades, holySpell,
            snakPoison, ChargeRider, pogRouge, rougeGirl, emeraldDrake, beastForm, whisp, puppet, copyFiend, walkingRocks, CamptainOrc, 
            WoodWarrior, captainFist, smallBlast, weirdGuy, necroDancer, poisonBottle, glowFly, weaponCrusher, elementalWeapon, weapon, 
            cruuulWorshiper, templeGuardian, handBuffer, spellmoon, soulForest, robberKnife, twinDragon, bloodlust2, Terror, tauntToken, 
            weaponMaster, queen, goblinInArmour, assassinGoblin, paladinGoblin3, paladinGoblin2, paladinGoblin1, canonFire, bloodlust,
            holyOrbs, holyKnowledge, warlockGiant, wrathOfSpirits, hillshaman2, CargoExplorer, callOfTheWild, deathBuffer, timespell, 
            smite, spider2, forestMana, RockGiant, RedKnight, BeastMaster, snakePlayingInstruments, EpicSoldierGuy, 
            LegendaryFrostMage, Merchant, cuteDarkMage, EyeSpell, summonMinionsOrder, thief, necroSkeletonCaster, greedSpell,
            bindingDemon, coolNecromancer, wingedDemon, SpiderFlower, beastParasytte, DarkArcher, CuteLittleGuy, FrostRatBear, 
            blastSpell, damageSpell, woodFrog, MoonMaiden, SmithLady, TreeSapling, ForestTeacher, ForestKnight, ManaGem, 
            NecroticPotion, ForestTomb, NatureIcon, DiscordIcon, WildIcon, ArcaneIcon, OrderIcon, NecroticIcon, EmptyMana, 
            FullDeck, ShieldPower, Corruption, largeSnake, sheep, MissileAnimation, FireAnimation, GreenCastAnimation,
            BlueCastAnimation, BiteAnimation, SlashAnimation, PentragramAnimation, HealAnimation, BlessAnimation, 
            IceBoltAnimation, CrystalHitAnimation, goblinarm2, goblinArmy1, epicDruid, TheWanderer, lurkinDragon, 
            orangeWhelp, SmallDragon, UnstableMana, SpinMirror, DarkMageOrb, mageSpell, BlastingDragon, GreenDragon, 
            smollDragon, GloriousRhaino, SpellDamageAnimation, PurpleFlameAnimation, SunAnimation, ExplotionAnimation,
            auraBless, blastMech, ScreamSoul, fanOfKnives, battleSmith, lavaForge, fallenTears, HolyAura, StoneStatue, 
            OatkKeeper, RedUndeadLord, skeletonNecromancer, inventer, manaRiver, spiritDragon, amarillo, rat, bearRider2, 
            witherSkeletons2, prince, cleric, hungryDragon2, hungryDragon, whiteDragon, DragonQueen, craftSpell, purpleDragon,
            lootHoarder, rider, ironForge2, blueMech, battleSmith3, darkBlast, lonlyKnight, gateKeeper, spellBlast, 
            hungryDemon, ratSwarm, polyMorph, lifesteal, discordHero, mageHero2, NecroHero, MagerHero1, NatureHero, 
            warlockHero, rougeHero, Souldrain, ArcherHero, orderHero3,flame_fireAnimation, fatuigeCard, X, flame_fireAnimationWhite, 
            flame_fireAnimationPurple, flame_fireAnimationBlue, Flaem_of_Frenzy, dragonRider, annoying_guard, DrainLife, 
            GhastSmall, Skelomancer, RedEye, darkShwirl, Maroth, gemCreature, soldier, treeSpirit, cub2, cub, statsSwapper, 
            fridaCard, Priest_Card, tribeTag, FlameSplitter, OutworldersIcon, outworlders_card, flameImp, impOrb, impFlame, 
            imp, fireImp, femaleImp, DemonicPowers, EvilFairy, IronClaw, WeaveConsumer, soulSplitter, Legion, contractBinder,
            AmberDemon, Necroticcreature, BoneGrower, BlessingBeast, DevilInABox, DemonicMirror, DemonSummoner, DoomWarrior, 
            potionMaster, SpellGiver, lifesuckingHorror;


        public static SpriteFont font, font_cardo, font_robortoBold;
        public static void Load(Game1 g)
        {
            font = g.Content.Load<SpriteFont>("fonts/boldGame");
            font_cardo = g.Content.Load<SpriteFont>("fonts/Cardo");
            font_robortoBold = g.Content.Load<SpriteFont>("fonts/RobortoBold");


            arcaneBlastGem = g.Content.Load<Texture2D>("Art/arcaneBlastGem");
            underWorldGate = g.Content.Load<Texture2D>("Art/underWorldGate");
            knowledgeDrake = g.Content.Load<Texture2D>("Art/knowledgeDrake");
            anvil = g.Content.Load<Texture2D>("Art/anvil");
            darkMage = g.Content.Load<Texture2D>("Art/darkMage");
            discoverMage = g.Content.Load<Texture2D>("Art/discoverMage");
            soulSuck = g.Content.Load<Texture2D>("Art/soulSuck");
            assassin = g.Content.Load<Texture2D>("Art/assassin");
            animalHealer = g.Content.Load<Texture2D>("Art/animalHealer");
            deathparrot = g.Content.Load<Texture2D>("Art/deathparrot");
            scorpion = g.Content.Load<Texture2D>("Art/scorpion");
            ArrowHP = g.Content.Load<Texture2D>("Art/ArrowHP");
            healing = g.Content.Load<Texture2D>("Art/healing");
            savageRoar = g.Content.Load<Texture2D>("Art/savageRoar");
            sacredSword = g.Content.Load<Texture2D>("Art/sacredSword");
            ritualDagger = g.Content.Load<Texture2D>("Art/ritualDagger");
            woodenPuppet = g.Content.Load<Texture2D>("Art/woodenPuppet");
            fieldMech = g.Content.Load<Texture2D>("Art/fieldMech");
            FacelessSurpent = g.Content.Load<Texture2D>("Art/FacelessSurpent");
            ClockWorkUnit = g.Content.Load<Texture2D>("Art/ClockWorkUnit");
            clockworkMachine = g.Content.Load<Texture2D>("Art/clockworkMachine");
            ProtectiveCompanion2 = g.Content.Load<Texture2D>("Art/ProtectiveCompanion");
            mace = g.Content.Load<Texture2D>("Art/mace");
            EmptyMana = g.Content.Load<Texture2D>("Art/EmptyMana");
            FullDeck = g.Content.Load<Texture2D>("Art/FullDeck");
            MissileAnimation = g.Content.Load<Texture2D>("Art/MissileAnimation");
            FireAnimation = g.Content.Load<Texture2D>("Art/FireAnimation");
            GreenCastAnimation = g.Content.Load<Texture2D>("Art/GreenCastAnimation");
            BlueCastAnimation = g.Content.Load<Texture2D>("Art/BlueCastAnimation");
            SlashAnimation = g.Content.Load<Texture2D>("Art/SlashAnimation");
            BiteAnimation = g.Content.Load<Texture2D>("Art/BiteAnimation");
            PentragramAnimation = g.Content.Load<Texture2D>("Art/PentragramAnimation");
            BlessAnimation = g.Content.Load<Texture2D>("Art/BlessAnimation");
            HealAnimation = g.Content.Load<Texture2D>("Art/HealAnimation");
            IceBoltAnimation = g.Content.Load<Texture2D>("Art/IceBoltAnimation");
            CrystalHitAnimation = g.Content.Load<Texture2D>("Art/CrystalHitAnimation");
            SpellDamageAnimation = g.Content.Load<Texture2D>("Art/SpellDamageAnimation");
            PurpleFlameAnimation = g.Content.Load<Texture2D>("Art/PurpleFlameAnimation");
            SunAnimation = g.Content.Load<Texture2D>("Art/SunAnimation");
            ExplotionAnimation = g.Content.Load<Texture2D>("Art/ExplotionAnimation");
            lifesteal = g.Content.Load<Texture2D>("Art/lifesteal");
            flame_fireAnimation = g.Content.Load<Texture2D>("Art/flame_fireAnimation");
            fatuigeCard = g.Content.Load<Texture2D>("Art/fatuigeCard");
            X = g.Content.Load<Texture2D>("Art/X");
            flame_fireAnimationWhite = g.Content.Load<Texture2D>("Art/flame_fireAnimationWhite");
            flame_fireAnimationBlue = g.Content.Load<Texture2D>("Art/flame_fireAnimationBlue");
            flame_fireAnimationPurple = g.Content.Load<Texture2D>("Art/flame_fireAnimationPurple");
            outworlders_card = g.Content.Load<Texture2D>("Art/outworlders_card");


            NatureIcon = g.Content.Load<Texture2D>("Art/NatureIcon");
            DiscordIcon = g.Content.Load<Texture2D>("Art/DiscordIcon");
            WildIcon = g.Content.Load<Texture2D>("Art/WildIcon");
            ArcaneIcon = g.Content.Load<Texture2D>("Art/ArcaneIcon");
            OrderIcon = g.Content.Load<Texture2D>("Art/OrderIcon");
            NecroticIcon = g.Content.Load<Texture2D>("Art/NecroticIcon");
            OutworldersIcon = g.Content.Load<Texture2D>("Art/outworldersIcon");


            CA_runeGolem = g.Content.Load<Texture2D>("Art/rune golem");
            CA_Themzam = g.Content.Load<Texture2D>("Art/Themzam");
            CA_magic_missiles = g.Content.Load<Texture2D>("Art/magic missiles");
            CA_rune_golem2 = g.Content.Load<Texture2D>("Art/rune golem2");
            CA = g.Content.Load<Texture2D>("Art/card format");
            CA_minion = g.Content.Load<Texture2D>("Art/card format_minion");
            necrotic_card = g.Content.Load<Texture2D>("Art/necrotic_card");
            arcane_card = g.Content.Load<Texture2D>("Art/arcane_card");
            nature_card = g.Content.Load<Texture2D>("Art/Nature_card");
            order_card = g.Content.Load<Texture2D>("Art/Order_card");
            wild_card = g.Content.Load<Texture2D>("Art/wild_card");
            legendaryBoarder = g.Content.Load<Texture2D>("Art/legendaryBoarder");
            temp_art = g.Content.Load<Texture2D>("Art/temp_art");
            usedHeroPower = g.Content.Load<Texture2D>("Art/usedHeroPower");
            Discord_card = g.Content.Load<Texture2D>("Art/Discord_card");
            ordercard2 = g.Content.Load<Texture2D>("Art/ordercard2");
            frozenMinion = g.Content.Load<Texture2D>("Art/frozenMinion");
            frostDrake = g.Content.Load<Texture2D>("Art/frostDrake");
            nestGuard = g.Content.Load<Texture2D>("Art/protector");
            pristess2 = g.Content.Load<Texture2D>("Art/pristess2");
            lockIcon = g.Content.Load<Texture2D>("Art/lock");
            weaponCrusher = g.Content.Load<Texture2D>("Art/weaponCrusher");
            elementalWeapon = g.Content.Load<Texture2D>("Art/elementalWeapon");
            weapon = g.Content.Load<Texture2D>("Art/weapon");
            cruuulWorshiper = g.Content.Load<Texture2D>("Art/cruuulWorshiper");
            templeGuardian = g.Content.Load<Texture2D>("Art/templeGuardian");
            handBuffer = g.Content.Load<Texture2D>("Art/handBuffer");
            spellmoon = g.Content.Load<Texture2D>("Art/spellmoon");
            soulForest = g.Content.Load<Texture2D>("Art/forestSoul");
            robberKnife = g.Content.Load<Texture2D>("Art/robberKnife");
            twinDragon = g.Content.Load<Texture2D>("Art/twinDragon");
            bloodlust2 = g.Content.Load<Texture2D>("Art/bloodlust2");
            Terror = g.Content.Load<Texture2D>("Art/Terror");
            tauntToken = g.Content.Load<Texture2D>("Art/tauntToken");
            weaponMaster = g.Content.Load<Texture2D>("Art/weaponMaster");
            queen = g.Content.Load<Texture2D>("Art/queen");
            goblinInArmour = g.Content.Load<Texture2D>("Art/goblinInArmour");
            assassinGoblin = g.Content.Load<Texture2D>("Art/assassinGoblin");
            paladinGoblin3 = g.Content.Load<Texture2D>("Art/paladinGoblin3");
            paladinGoblin2 = g.Content.Load<Texture2D>("Art/paladinGoblin2");
            paladinGoblin1 = g.Content.Load<Texture2D>("Art/paladinGoblin1");
            canonFire = g.Content.Load<Texture2D>("Art/canonFire");
            bloodlust = g.Content.Load<Texture2D>("Art/bloodlust");
            holyOrbs = g.Content.Load<Texture2D>("Art/holyOrbs");
            holyKnowledge = g.Content.Load<Texture2D>("Art/holyKnowledge");
            warlockGiant = g.Content.Load<Texture2D>("Art/warlockGiant");
            wrathOfSpirits = g.Content.Load<Texture2D>("Art/wrathOfSpirirts");
            hillshaman2 = g.Content.Load<Texture2D>("Art/hillshaman2");
            CargoExplorer = g.Content.Load<Texture2D>("Art/CargoExplorer");
            callOfTheWild = g.Content.Load<Texture2D>("Art/callOfTheWild");
            deathBuffer = g.Content.Load<Texture2D>("Art/deathBuffer");
            timespell = g.Content.Load<Texture2D>("Art/timespell");
            smite = g.Content.Load<Texture2D>("Art/smite");
            spider2 = g.Content.Load<Texture2D>("Art/spider2");
            forestMana = g.Content.Load<Texture2D>("Art/forestMana");
            SpiderFlower = g.Content.Load<Texture2D>("Art/SpiderFlower");
            beastParasytte = g.Content.Load<Texture2D>("Art/beastParasyte");
            DarkArcher = g.Content.Load<Texture2D>("Art/DarkArcher");
            CuteLittleGuy = g.Content.Load<Texture2D>("Art/CuteLittleGuy");
            FrostRatBear = g.Content.Load<Texture2D>("Art/FrostRatBear");
            blastSpell = g.Content.Load<Texture2D>("Art/blastSpell");
            damageSpell = g.Content.Load<Texture2D>("Art/damageSpell");
            woodFrog = g.Content.Load<Texture2D>("Art/woodFrog");
            MoonMaiden = g.Content.Load<Texture2D>("Art/MoonMaiden");
            SmithLady = g.Content.Load<Texture2D>("Art/SmithLady");
            TreeSapling = g.Content.Load<Texture2D>("Art/TreeSapling");
            ForestTeacher = g.Content.Load<Texture2D>("Art/ForestTeacher");
            ForestKnight = g.Content.Load<Texture2D>("Art/ForestKnight");
            ManaGem = g.Content.Load<Texture2D>("Art/ManaGem");
            NecroticPotion = g.Content.Load<Texture2D>("Art/NecroticPotion");
            ForestTomb = g.Content.Load<Texture2D>("Art/ForestTomb");
            ShieldPower = g.Content.Load<Texture2D>("Art/ShieldPower");
            Corruption = g.Content.Load<Texture2D>("Art/Corruption");
            largeSnake = g.Content.Load<Texture2D>("Art/largeSnake");
            sheep = g.Content.Load<Texture2D>("Art/sheep");
            goblinarm2 = g.Content.Load<Texture2D>("Art/goblinarm2");
            goblinArmy1 = g.Content.Load<Texture2D>("Art/goblinArmy1");
            epicDruid = g.Content.Load<Texture2D>("Art/epicDruid");
            TheWanderer = g.Content.Load<Texture2D>("Art/TheWandrer");
            lurkinDragon = g.Content.Load<Texture2D>("Art/lurkinDragon");
            orangeWhelp = g.Content.Load<Texture2D>("Art/orangeWhelp");
            SmallDragon = g.Content.Load<Texture2D>("Art/SmallDragon");
            UnstableMana = g.Content.Load<Texture2D>("Art/UnstableMana");
            SpinMirror = g.Content.Load<Texture2D>("Art/SpinMirror");
            DarkMageOrb = g.Content.Load<Texture2D>("Art/DarkMageOrb");
            mageSpell = g.Content.Load<Texture2D>("Art/mageSpell");
            BlastingDragon = g.Content.Load<Texture2D>("Art/BlastingDragon");
            GreenDragon = g.Content.Load<Texture2D>("Art/GreenDragon");
            smollDragon = g.Content.Load<Texture2D>("Art/smollDragon");
            GloriousRhaino = g.Content.Load<Texture2D>("Art/GloriousRhaino");
            auraBless = g.Content.Load<Texture2D>("Art/auraBless");
            blastMech = g.Content.Load<Texture2D>("Art/blastMech");
            ScreamSoul = g.Content.Load<Texture2D>("Art/ScreamSoul");
            fanOfKnives = g.Content.Load<Texture2D>("Art/fanOfKnives");
            battleSmith = g.Content.Load<Texture2D>("Art/battleSmith");
            lavaForge = g.Content.Load<Texture2D>("Art/lavaForge");
            fallenTears = g.Content.Load<Texture2D>("Art/fallenTears");
            HolyAura = g.Content.Load<Texture2D>("Art/HolyAura");
            StoneStatue = g.Content.Load<Texture2D>("Art/StoneStatue");
            OatkKeeper = g.Content.Load<Texture2D>("Art/OatkKeeper");
            RedUndeadLord = g.Content.Load<Texture2D>("Art/RedUndeadLord");
            skeletonNecromancer = g.Content.Load<Texture2D>("Art/skeletonNecromancer");
            inventer = g.Content.Load<Texture2D>("Art/inventer");
            manaRiver = g.Content.Load<Texture2D>("Art/manaRiver");
            spiritDragon = g.Content.Load<Texture2D>("Art/spiritDragon");
            amarillo = g.Content.Load<Texture2D>("Art/amarillo");
            rat = g.Content.Load<Texture2D>("Art/rat");
            bearRider2 = g.Content.Load<Texture2D>("Art/bearRider2");
            witherSkeletons2 = g.Content.Load<Texture2D>("Art/witherSkeletons2");
            prince = g.Content.Load<Texture2D>("Art/prince");
            cleric = g.Content.Load<Texture2D>("Art/cleric");
            hungryDragon2 = g.Content.Load<Texture2D>("Art/hungryDragon2");
            hungryDragon = g.Content.Load<Texture2D>("Art/hungryDragon");
            whiteDragon = g.Content.Load<Texture2D>("Art/whiteDragon");
            DragonQueen = g.Content.Load<Texture2D>("Art/DragonQueen");
            craftSpell = g.Content.Load<Texture2D>("Art/craftSpell");
            purpleDragon = g.Content.Load<Texture2D>("Art/purpleDragon");
            lootHoarder = g.Content.Load<Texture2D>("Art/lootHoarder");
            rider = g.Content.Load<Texture2D>("Art/rider");
            ironForge2 = g.Content.Load<Texture2D>("Art/ironForge2");
            blueMech = g.Content.Load<Texture2D>("Art/blueMech");
            battleSmith3 = g.Content.Load<Texture2D>("Art/battleSmith3");
            darkBlast = g.Content.Load<Texture2D>("Art/darkBlast");
            lonlyKnight = g.Content.Load<Texture2D>("Art/lonlyKnight");
            gateKeeper = g.Content.Load<Texture2D>("Art/gateKeeper");
            spellBlast = g.Content.Load<Texture2D>("Art/spellBlast");
            hungryDemon = g.Content.Load<Texture2D>("Art/hungryDemon");
            ratSwarm = g.Content.Load<Texture2D>("Art/ratSwarm");
            polyMorph = g.Content.Load<Texture2D>("Art/polymorph");
            discordHero = g.Content.Load<Texture2D>("Art/discordHero");
            mageHero2 = g.Content.Load<Texture2D>("Art/mageHero2");
            NecroHero = g.Content.Load<Texture2D>("Art/NecroHero");
            MagerHero1 = g.Content.Load<Texture2D>("Art/MagerHero1");
            NatureHero = g.Content.Load<Texture2D>("Art/NatureHero");
            warlockHero = g.Content.Load<Texture2D>("Art/warlockHero");
            rougeHero = g.Content.Load<Texture2D>("Art/rougeHero");
            ArcherHero = g.Content.Load<Texture2D>("Art/ArcherHero"); 
            orderHero3 = g.Content.Load<Texture2D>("Art/orderHero3");
            Souldrain = g.Content.Load<Texture2D>("Art/Souldrain");
            Flaem_of_Frenzy = g.Content.Load<Texture2D>("Art/Flaem_of_Frenzy");
            dragonRider = g.Content.Load<Texture2D>("Art/dragonRider");
            annoying_guard = g.Content.Load<Texture2D>("Art/annoying guard");
            DrainLife = g.Content.Load<Texture2D>("Art/DrainLife");
            GhastSmall = g.Content.Load<Texture2D>("Art/GhastSmall");
            Skelomancer = g.Content.Load<Texture2D>("Art/Skelomancer");
            RedEye = g.Content.Load<Texture2D>("Art/RedEye");
            darkShwirl = g.Content.Load<Texture2D>("Art/darkShwirl");
            Maroth = g.Content.Load<Texture2D>("Art/Maroth");
            gemCreature = g.Content.Load<Texture2D>("Art/gemCreature");
            soldier = g.Content.Load<Texture2D>("Art/soldier");
            treeSpirit = g.Content.Load<Texture2D>("Art/treeSpirit");
            cub2 = g.Content.Load<Texture2D>("Art/cub2");
            cub = g.Content.Load<Texture2D>("Art/cub");
            statsSwapper = g.Content.Load<Texture2D>("Art/statsSwapper");
            fridaCard = g.Content.Load<Texture2D>("Art/fridaCard");
            Priest_Card = g.Content.Load<Texture2D>("Art/Priest_Card");
            tribeTag = g.Content.Load<Texture2D>("Art/tribeTag");
            FlameSplitter = g.Content.Load<Texture2D>("Art/FlameSplitter");
            flameImp = g.Content.Load<Texture2D>("Art/flameImp");
            impOrb = g.Content.Load<Texture2D>("Art/impOrb");
            impFlame = g.Content.Load<Texture2D>("Art/impFlame");
            imp = g.Content.Load<Texture2D>("Art/imp");
            fireImp = g.Content.Load<Texture2D>("Art/fireImp");
            femaleImp = g.Content.Load<Texture2D>("Art/femaleImp");
            DemonicPowers = g.Content.Load<Texture2D>("Art/DemonicPowers");
            EvilFairy = g.Content.Load<Texture2D>("Art/EvilFairy");
            IronClaw = g.Content.Load<Texture2D>("Art/IronClaw");
            WeaveConsumer = g.Content.Load<Texture2D>("Art/WeaveConsumer");
            soulSplitter = g.Content.Load<Texture2D>("Art/soulSplitter");
            Legion = g.Content.Load<Texture2D>("Art/Legion");
            contractBinder = g.Content.Load<Texture2D>("Art/contractBinder");
            AmberDemon = g.Content.Load<Texture2D>("Art/AmberDemon");
            Necroticcreature = g.Content.Load<Texture2D>("Art/Necroticcreature");
            BoneGrower = g.Content.Load<Texture2D>("Art/BoneGrower");
            BlessingBeast = g.Content.Load<Texture2D>("Art/BlessingBeast");
            DevilInABox = g.Content.Load<Texture2D>("Art/DevilInABox");
            DemonicMirror = g.Content.Load<Texture2D>("Art/DemonicMirror");
            DemonSummoner = g.Content.Load<Texture2D>("Art/DemonSummoner");
            DoomWarrior = g.Content.Load<Texture2D>("Art/DoomWarrior");
            potionMaster = g.Content.Load<Texture2D>("Art/potionMaster");
            SpellGiver = g.Content.Load<Texture2D>("Art/SpellGiver");
            lifesuckingHorror = g.Content.Load<Texture2D>("Art/lifesuckingHorror");

            



                        Minion_Board_Icon = g.Content.Load<Texture2D>("Art/Minion_format");
            powerShield = g.Content.Load<Texture2D>("Art/powerShield");
            taunt = g.Content.Load<Texture2D>("Art/taunt");
            poison = g.Content.Load<Texture2D>("Art/poison");
            Health = g.Content.Load<Texture2D>("Art/blood");
            Attack = g.Content.Load<Texture2D>("Art/attack");
            Mana = g.Content.Load<Texture2D>("Art/mana");
            arrowPointer = g.Content.Load<Texture2D>("Art/arrowPointer");
            cardback = g.Content.Load<Texture2D>("Art/cardback");
            triggerEffect = g.Content.Load<Texture2D>("Art/triggerEffect");
            skull = g.Content.Load<Texture2D>("Art/skull");
            frostbolt = g.Content.Load<Texture2D>("Art/frostbolt");
            bullet = g.Content.Load<Texture2D>("Art/bullet");
            summonCard = g.Content.Load<Texture2D>("Art/summonCard");
            aura = g.Content.Load<Texture2D>("Art/aura");
            DieCard = g.Content.Load<Texture2D>("Art/DieCard");
            common = g.Content.Load<Texture2D>("Art/common"); 
            rare = g.Content.Load<Texture2D>("Art/rare");
            epic = g.Content.Load<Texture2D>("Art/epic");
            legendary = g.Content.Load<Texture2D>("Art/legendary");
            armourIcon = g.Content.Load<Texture2D>("Art/armourIcon");
            powerCard = g.Content.Load<Texture2D>("Art/powerCard");
            puppetMaster = g.Content.Load<Texture2D>("Art/puppetMaster");
            mapWriter = g.Content.Load<Texture2D>("Art/mapWriter");
            magicEgg = g.Content.Load<Texture2D>("Art/magicEgg");
            doubleBlades = g.Content.Load<Texture2D>("Art/doubleBlades");
            holySpell = g.Content.Load<Texture2D>("Art/holySpell");
            snakPoison = g.Content.Load<Texture2D>("Art/snakPoison");
            ChargeRider = g.Content.Load<Texture2D>("Art/ChargeRider");
            pogRouge = g.Content.Load<Texture2D>("Art/pogRouge");
            rougeGirl = g.Content.Load<Texture2D>("Art/rougeGirl");
            emeraldDrake = g.Content.Load<Texture2D>("Art/emeraldDrake");
            beastForm = g.Content.Load<Texture2D>("Art/beastForm");
            whisp = g.Content.Load<Texture2D>("Art/whisp");
            puppet = g.Content.Load<Texture2D>("Art/puppet");
            copyFiend = g.Content.Load<Texture2D>("Art/copyFiend");
            walkingRocks = g.Content.Load<Texture2D>("Art/walkingRocks");
            CamptainOrc = g.Content.Load<Texture2D>("Art/CamptainOrc");
            WoodWarrior = g.Content.Load<Texture2D>("Art/WoodWarrior");
            captainFist = g.Content.Load<Texture2D>("Art/captainFist");
            smallBlast = g.Content.Load<Texture2D>("Art/smallBlast");
            weirdGuy = g.Content.Load<Texture2D>("Art/weirdGuy");
            necroDancer = g.Content.Load<Texture2D>("Art/necroDancer");
            poisonBottle = g.Content.Load<Texture2D>("Art/poisonBottle");
            glowFly = g.Content.Load<Texture2D>("Art/glowFly");

            




            ForestDefender = g.Content.Load<Texture2D>("Art/ForestDefender");
            forestCall = g.Content.Load<Texture2D>("Art/forestCall");
            shootImageArrow = g.Content.Load<Texture2D>("Art/shootImageArrow");
            animalFriend = g.Content.Load<Texture2D>("Art/animalFriend");
            SwapTurtle = g.Content.Load<Texture2D>("Art/SwapTurtle");
            ForbiddenKnowledge = g.Content.Load<Texture2D>("Art/ForbiddenKnowledge");
            epic_knight = g.Content.Load<Texture2D>("Art/epic knight");
            necroLizzard = g.Content.Load<Texture2D>("Art/necroLizzard");
            bloodPerson = g.Content.Load<Texture2D>("Art/bloodPerson");
            drums = g.Content.Load<Texture2D>("Art/drums");
            deathPotion = g.Content.Load<Texture2D>("Art/deathPotion");
            hellfire = g.Content.Load<Texture2D>("Art/hellfire");
            smithArmour = g.Content.Load<Texture2D>("Art/smithArmour");
            cannon = g.Content.Load<Texture2D>("Art/cannon");
            buzzard = g.Content.Load<Texture2D>("Art/buzzard");
            wolf = g.Content.Load<Texture2D>("Art/wolf");
            spirits = g.Content.Load<Texture2D>("Art/spirits");
            drums2 = g.Content.Load<Texture2D>("Art/drums2");
            FrostSkeleton = g.Content.Load<Texture2D>("Art/FrostSkeleton");
            forestGiant = g.Content.Load<Texture2D>("Art/forestGiant");
            rockGolem = g.Content.Load<Texture2D>("Art/rockGolem");
            worldEnd = g.Content.Load<Texture2D>("Art/worldEnd");
            whitlwind = g.Content.Load<Texture2D>("Art/whitlwind");
            dragon = g.Content.Load<Texture2D>("Art/dragon");
            forestFather = g.Content.Load<Texture2D>("Art/forestFather");
            tundraStag = g.Content.Load<Texture2D>("Art/tundraStag");
            returnThing = g.Content.Load<Texture2D>("Art/returnThing");
            atgus = g.Content.Load<Texture2D>("Art/atgus");
            healingAura = g.Content.Load<Texture2D>("Art/GlowTree");
            GlowTree = g.Content.Load<Texture2D>("Art/GlowTree");
            JungleGirl = g.Content.Load<Texture2D>("Art/JungleGirl");
            forestCreature = g.Content.Load<Texture2D>("Art/forestCreature");
            RockGiant = g.Content.Load<Texture2D>("Art/RockGiant");
            RedKnight = g.Content.Load<Texture2D>("Art/RedKnight");
            BeastMaster = g.Content.Load<Texture2D>("Art/BeastMaster");
            snakePlayingInstruments = g.Content.Load<Texture2D>("Art/snakePlayingInstruments");
            EpicSoldierGuy = g.Content.Load<Texture2D>("Art/EpicSoldierGuy");
            LegendaryFrostMage = g.Content.Load<Texture2D>("Art/LegendaryFrostMage");
            Merchant = g.Content.Load<Texture2D>("Art/Merchant");
            cuteDarkMage = g.Content.Load<Texture2D>("Art/cuteDarkMage");
            EyeSpell = g.Content.Load<Texture2D>("Art/EyeSpell");
            summonMinionsOrder = g.Content.Load<Texture2D>("Art/summonMinionsOrder");
            thief = g.Content.Load<Texture2D>("Art/thief");
            necroSkeletonCaster = g.Content.Load<Texture2D>("Art/necroSkeletonCaster");
            greedSpell = g.Content.Load<Texture2D>("Art/greedSpell");
            bindingDemon = g.Content.Load<Texture2D>("Art/bindingDemon");
            coolNecromancer = g.Content.Load<Texture2D>("Art/coolNevcromancer");
            wingedDemon = g.Content.Load<Texture2D>("Art/wingedDemon");

            



            SerpentKey = g.Content.Load<Texture2D>("Art/SerpentKey");
            ShadyPotion = g.Content.Load<Texture2D>("Art/ShadyPotion");
            goblinPack2 = g.Content.Load<Texture2D>("Art/goblinPack2");
            spiderEggNew = g.Content.Load<Texture2D>("Art/spiderEggNew");
            lizzardWhip = g.Content.Load<Texture2D>("Art/lizzardWhip");
            goblinTrader = g.Content.Load<Texture2D>("Art/goblinTrader");
            elderLitch = g.Content.Load<Texture2D>("Art/elderLitch");

            meril = g.Content.Load<Texture2D>("Art/meril");
            protetiveMinion = g.Content.Load<Texture2D>("Art/protetiveMinion");
            necroGirl = g.Content.Load<Texture2D>("Art/necroGirl");
            bladeOfTheTemple = g.Content.Load<Texture2D>("Art/bladeOfTheTemple");
            cruul2 = g.Content.Load<Texture2D>("Art/cruul2");
            undeadArmy2 = g.Content.Load<Texture2D>("Art/undeadArmy2");
            bloodCopy = g.Content.Load<Texture2D>("Art/bloodCopy");


            
            goblinSmall3 = g.Content.Load<Texture2D>("Art/goblinSmall3");
            hillshaman = g.Content.Load<Texture2D>("Art/hillshaman");
            greatsword = g.Content.Load<Texture2D>("Art/greatsword");
            moltenBlast = g.Content.Load<Texture2D>("Art/moltenBlast");
            fireball2 = g.Content.Load<Texture2D>("Art/fireball2");
            sappling = g.Content.Load<Texture2D>("Art/sappling");
            hyena = g.Content.Load<Texture2D>("Art/hyena");
            manaBurst = g.Content.Load<Texture2D>("Art/manaBurst");
            ManaGrowth = g.Content.Load<Texture2D>("Art/ManaGrowth");
            StrongGobling = g.Content.Load<Texture2D>("Art/StrongGobling warrior");
            hunter_arrow = g.Content.Load<Texture2D>("Art/hunter arrow");
            goblinSpellCaster = g.Content.Load<Texture2D>("Art/goblinSpellCaster");


            commander2 = g.Content.Load<Texture2D>("Art/commander2");
            witherSkeletons = g.Content.Load<Texture2D>("Art/witherSkeletons");
            assassinBow = g.Content.Load<Texture2D>("Art/assassinBow");
            smuggler = g.Content.Load<Texture2D>("Art/smuggler");
            crockDefender = g.Content.Load<Texture2D>("Art/crockDefender");
            spy1 = g.Content.Load<Texture2D>("Art/spy1");
            assassinToken2 = g.Content.Load<Texture2D>("Art/assassinToken2");
            infoBroker2 = g.Content.Load<Texture2D>("Art/infoBroker2");
            crazedCultist = g.Content.Load<Texture2D>("Art/crazedCultist");
            giantMossy = g.Content.Load<Texture2D>("Art/giantMossy");
            ragingFanatic = g.Content.Load<Texture2D>("Art/ragingFanatic");
            inTheFlesh2 = g.Content.Load<Texture2D>("Art/inTheFlesh2");
            painDraw = g.Content.Load<Texture2D>("Art/painDraw");
            agentofWar = g.Content.Load<Texture2D>("Art/agentofWar");
            temple = g.Content.Load<Texture2D>("Art/temple");
            painBringer = g.Content.Load<Texture2D>("Art/painBringer");
            painBuffer = g.Content.Load<Texture2D>("Art/painBuffer");
            syabsyabsyab = g.Content.Load<Texture2D>("Art/syabsyabsyab");
            prep = g.Content.Load<Texture2D>("Art/prep");
            freshCut = g.Content.Load<Texture2D>("Art/freshCut");
            nestGuard2 = g.Content.Load<Texture2D>("Art/nestGuard");
            commander = g.Content.Load<Texture2D>("Art/commander");
            redbear = g.Content.Load<Texture2D>("Art/redbear");
            fleshwalker = g.Content.Load<Texture2D>("Art/fleshwalker");
            panther = g.Content.Load<Texture2D>("Art/panther");
            spiderEgg = g.Content.Load<Texture2D>("Art/spiderEgg");
            bloodCultist = g.Content.Load<Texture2D>("Art/bloodCultist");
            blizzard = g.Content.Load<Texture2D>("Art/blizzard");
            frostNova2 = g.Content.Load<Texture2D>("Art/frostNova2");

            largeSpider = g.Content.Load<Texture2D>("Art/largeSpider");
            flamestrike = g.Content.Load<Texture2D>("Art/flamestrike");
            spellSpammer = g.Content.Load<Texture2D>("Art/spellSpammer");
            lightBlast = g.Content.Load<Texture2D>("Art/lightBlast");
            sanctumHealing = g.Content.Load<Texture2D>("Art/sanctumHealing");
            fieldmagic = g.Content.Load<Texture2D>("Art/fieldmagic");
            recruit2 = g.Content.Load<Texture2D>("Art/recruit2");
            gateGuard = g.Content.Load<Texture2D>("Art/gateGuard");
            Vasnji = g.Content.Load<Texture2D>("Art/Vasnji");
            bonecolector = g.Content.Load<Texture2D>("Art/bonecolector");
            newmanagement = g.Content.Load<Texture2D>("Art/newmanagement");
            dreadlitch = g.Content.Load<Texture2D>("Art/dreadlitch");
            darkAprentice = g.Content.Load<Texture2D>("Art/darkAprentice");
            falseLife = g.Content.Load<Texture2D>("Art/falseLife");
            angryMage = g.Content.Load<Texture2D>("Art/angryMage");
            arcaneDrake = g.Content.Load<Texture2D>("Art/arcaneDrake");
            dragonSpellCaster = g.Content.Load<Texture2D>("Art/dragonSpellCaster");
            dragonSpellSmith = g.Content.Load<Texture2D>("Art/dragonSpellSmith");
            eggDragonGirl = g.Content.Load<Texture2D>("Art/eggDragonGirl");
            eggDragonGirl2 = g.Content.Load<Texture2D>("Art/eggDragonGirl2");
            dragonRockCollector = g.Content.Load<Texture2D>("Art/dragonRockCollector");
            chillGemDragon = g.Content.Load<Texture2D>("Art/chillGemDragon");


            goblinHunter = g.Content.Load<Texture2D>("Art/goblinHunter");
            gigaDrill = g.Content.Load<Texture2D>("Art/gigaDrill");
            warrior = g.Content.Load<Texture2D>("Art/warrior");
            shieldslam = g.Content.Load<Texture2D>("Art/shieldslam");
            blessed = g.Content.Load<Texture2D>("Art/blessed");
            shield = g.Content.Load<Texture2D>("Art/shield");
            ghast = g.Content.Load<Texture2D>("Art/ghast");
            cryptStalker = g.Content.Load<Texture2D>("Art/cryptStalker");
            giantSkeleton = g.Content.Load<Texture2D>("Art/giantSkeleton");
            spellCastinGoblin = g.Content.Load<Texture2D>("Art/spellcastingGoblin");

            CA_gonlinrider = g.Content.Load<Texture2D>("Art/gonlinrider");
            CA_bot = g.Content.Load<Texture2D>("Art/bot");
            CA_goblincool = g.Content.Load<Texture2D>("Art/goblincool");
            CA_knight = g.Content.Load<Texture2D>("Art/knight");
            CA_goblinwarrior = g.Content.Load<Texture2D>("Art/goblinwarrior");
            CA_mageGoblin = g.Content.Load<Texture2D>("Art/mage goblin");
            CA_flameMage = g.Content.Load<Texture2D>("Art/flameMage");
            CA_skeleton = g.Content.Load<Texture2D>("Art/skeleton");
            CA_magic_skeleton = g.Content.Load<Texture2D>("Art/magic skeleton");
            CA_undead = g.Content.Load<Texture2D>("Art/undead");
            CA_ritual = g.Content.Load<Texture2D>("Art/ritual");
            CA_conuring = g.Content.Load<Texture2D>("Art/conuring");
            CA_summonPurple = g.Content.Load<Texture2D>("Art/summonPurple");
            CA_summonMage = g.Content.Load<Texture2D>("Art/summonMage");
            CA_summon = g.Content.Load<Texture2D>("Art/summon");
            CA_spellWizard = g.Content.Load<Texture2D>("Art/spellWizard");
            CA_icebolt = g.Content.Load<Texture2D>("Art/icebolt");
            CA_manawyrm = g.Content.Load<Texture2D>("Art/manawyrm");
            CA_dragonSmith = g.Content.Load<Texture2D>("Art/dragonSmith");
            CA_wisdom = g.Content.Load<Texture2D>("Art/wisdom");

        }
    }
}
