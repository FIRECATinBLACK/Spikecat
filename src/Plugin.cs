﻿using System;
using BepInEx;
using UnityEngine;
using SlugBase.Features;
using static SlugBase.Features.FeatureTypes;

namespace spikecat
{
    [BepInPlugin(MOD_ID, "spikecat", "0.1.0")]
    class Plugin : BaseUnityPlugin
    {
        private const string MOD_ID = "firecatinblack.spikecat";

        public static readonly PlayerFeature<float> SuperJump = PlayerFloat("spikecat/super_jump");
        public static readonly PlayerFeature<bool> ExplodeOnDeath = PlayerBool("spikecat/explode_on_death");
        public static readonly GameFeature<float> MeanLizards = GameFloat("spikecat/mean_lizards");
        public static readonly PlayerFeature<float[]> LungIAir = PlayerFloats("spikecat/infAir");




        // Add hooks
        public void OnEnable()
        {
            On.RainWorld.OnModsInit += Extras.WrapInit(LoadResources);

            // Put your custom hooks here!
            On.Player.Jump += Player_Jump;
            On.Player.Die += Player_Die;
            On.Lizard.ctor += Lizard_ctor;
            On.Player.LungUpdate += LungAir;
        }
        
        // Load any resources, such as sprites or sounds
        private void LoadResources(RainWorld rainWorld)
        {
        }

        // Implement MeanLizards
        private void Lizard_ctor(On.Lizard.orig_ctor orig, Lizard self, AbstractCreature abstractCreature, World world)
        {
            orig(self, abstractCreature, world);

            if(MeanLizards.TryGet(world.game, out float meanness))
            {
                self.spawnDataEvil = Mathf.Min(self.spawnDataEvil, meanness);
            }
        }


        // Implement SuperJump
        private void Player_Jump(On.Player.orig_Jump orig, Player self)
        {
            orig(self);

            if (SuperJump.TryGet(self, out var power))
            {
                self.jumpBoost *= 1f + power;
            }
        }

        // Implement ExlodeOnDeath
        private void Player_Die(On.Player.orig_Die orig, Player self)
        {
            bool wasDead = self.dead;

            orig(self);

            if(!wasDead && self.dead
                && ExplodeOnDeath.TryGet(self, out bool explode)
                && explode)
            {
                // Adapted from ScavengerBomb.Explode
                var room = self.room;
                var pos = self.mainBodyChunk.pos;
                var color = self.ShortCutColor();
                room.AddObject(new Explosion(room, self, pos, 7, 250f, 6.2f, 2f, 280f, 0.25f, self, 0.7f, 160f, 1f));
                room.AddObject(new Explosion.ExplosionLight(pos, 280f, 1f, 7, color));
                room.AddObject(new Explosion.ExplosionLight(pos, 230f, 1f, 3, new Color(1f, 1f, 1f)));
                room.AddObject(new ExplosionSpikes(room, pos, 14, 30f, 9f, 7f, 170f, color));
                room.AddObject(new ShockWave(pos, 330f, 0.045f, 5, false));

                room.ScreenMovement(pos, default, 1.3f);
                room.PlaySound(SoundID.Bomb_Explode, pos);
                room.InGameNoise(new Noise.InGameNoise(pos, 9000f, self, 1f));
            }
        }

        //reverse breath
        private void LungAir(On.Player.orig_LungUpdate orig, Player self)
        {
            orig(self);

            if (LungIAir.TryGet(self, out float[] inf))
            {
                //bugs to fix
                //#breath bar doesnt completely fill#//

                //there could be more bugs


                //changing LungUpdate()

                if (self.firstChunk.submersion > 0.9f && !self.room.game.setupValues.invincibility && !self.chatlog && self.submerged)
                {
                    if (self.airInLungs >= 0.89f)
                    {
                        self.airInLungs = 0.89f;
                        self.lungsExhausted = false;
                    }
                    else
                    {


                        self.airInLungs += (1f / (!self.lungsExhausted ? 240 : 60)) * inf[1];
                    }

                }
                else //not underwater
                {

                    self.airInLungs -= (1f / (!self.lungsExhausted ? 240 : 60)) * Mathf.Lerp(4, 4.5f, inf[0]);

                    if (self.airInLungs <= 0)
                    {
                        self.airInLungs = 0f;
                        self.Stun(10);
                        self.drown += 0.008333334f;
                        if (self.drown >= 1f)
                        {
                            self.Die();
                        }
                    }

                    if (self.airInLungs >= 0.89f)
                    {
                        self.airInLungs = 0.89f;
                        self.lungsExhausted = false;
                    }
                }


            }
        }



    }
}