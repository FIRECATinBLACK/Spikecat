using System;
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


        public static readonly GameFeature<float> BubbleGrassTime = GameFloat("spikecat/bubblegrasslastingtime");

        // Add hooks
        public void OnEnable()
        {
            On.RainWorld.OnModsInit += Extras.WrapInit(LoadResources);

            // Put your custom hooks here!
            On.Player.Jump += Player_Jump;
            On.Player.Die += Player_Die;
            On.Lizard.ctor += Lizard_ctor;
            On.Player.LungUpdate += LungAir;
            On.BubbleGrass.Update += CustomBubbleGrass;
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
                //#none, breathmeter can be full underwater now#//

                
                //speed dependent on amount of air
                self.slugcatStats.runspeedFac = Mathf.Lerp(inf[2], inf[3], Mathf.InverseLerp(0, 0.9f, self.airInLungs));
                self.slugcatStats.poleClimbSpeedFac = (Mathf.Lerp(inf[2], inf[3], Mathf.InverseLerp(0, 0.9f, self.airInLungs))) * 0.75f;
                self.slugcatStats.corridorClimbSpeedFac = (Mathf.Lerp(inf[2], inf[3], Mathf.InverseLerp(0, 0.9f, self.airInLungs))) * 0.75f;


                //changing LungUpdate()

                if (self.firstChunk.submersion > 0.9f && self.submerged)
                {
                    if (self.airInLungs >= 0.95f)
                    {
                        self.airInLungs = 0.95f;
                        self.lungsExhausted = false;
                    }
                    else
                    {


                        self.airInLungs += (1f / (!self.lungsExhausted ? 240 : 60)) * inf[1];

                    }

                    if (self.airInLungs >= 0.1f)
                    {
                        self.waterJumpDelay = Mathf.Clamp(self.waterJumpDelay, self.waterJumpDelay, Mathf.RoundToInt(inf[4]));
                    }
                    else
                    {

                        self.waterJumpDelay++;
                    }

                }
                else //not underwater
                {



                    if (self.room.abstractRoom.shelter)
                    {
                        Room room = self.room;

                        for (int i = 0; i < room.roomSettings.placedObjects.Count; i++)
                        {
                            if (room.roomSettings.placedObjects[i].type == PlacedObject.Type.BrokenShelterWaterLevel)
                            {

                                room.AddWater();
                                room.waterObject.fWaterLevel = room.roomSettings.placedObjects[i].pos.y;
                                room.waterObject.originalWaterLevel = room.roomSettings.placedObjects[i].pos.y;
                            }

                        }

                    }
                    else
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
                        else
                        {
                            if (self.airInLungs < 0.15f)
                            {

                                self.Blink(5);
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


        public void CustomBubbleGrass(On.BubbleGrass.orig_Update orig, BubbleGrass self, bool eu)
        {

            orig(self, eu);



            if (BubbleGrassTime.TryGet(self.room.game, out float t))
            {
                if (self.grabbedBy.Count > 0 && self.grabbedBy[0].grabber is Player && (self.grabbedBy[0].grabber as Player).airInLungs > 0 && !(self.grabbedBy[0].grabber as Player).submerged)
                {
                    Player player = self.grabbedBy[0].grabber as Player;

                    if (self.AbstrBubbleGrass.oxygenLeft > 0)
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            if (player.grasps[i] != null && player.grasps[i].grabbed is BubbleGrass)
                            {
                                (player.graphicsModule as PlayerGraphics).BiteFly(i);

                            }
                        }


                        self.AbstrBubbleGrass.oxygenLeft = Mathf.Max(0f, self.AbstrBubbleGrass.oxygenLeft - 0.0009090909f * t);

                        for (int i = 0; i < self.room.abstractRoom.creatures.Count; i++)
                        {


                            if (self.room.abstractRoom.creatures[i].realizedCreature is Player)
                            {
                                player.airInLungs = 1f;
                            }

                        }
                    }

                }
            }







        }



    }
}