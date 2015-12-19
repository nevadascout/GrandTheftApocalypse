namespace GrandTheftApocalpyse.World
{
    using System.Collections.Generic;
    using System.Linq;

    using GTA;
    using GTA.Math;
    using GTA.Native;
    using GTA.NaturalMotion;

    public class Ambient
    {
        public static void DisableLife(bool enablePeds)
        {
            if (!enablePeds)
            {
                Function.Call(Hash.SET_PED_DENSITY_MULTIPLIER_THIS_FRAME, 0f);
            }

            // TODO -- NOTE: some random events still occur -> eg. cars arriving at roadside market stands
            Function.Call(Hash.SET_PARKED_VEHICLE_DENSITY_MULTIPLIER_THIS_FRAME, 0f);
            Function.Call(Hash.SET_RANDOM_VEHICLE_DENSITY_MULTIPLIER_THIS_FRAME, 0f);
            Function.Call(Hash.SET_SCENARIO_PED_DENSITY_MULTIPLIER_THIS_FRAME, 0f);
            Function.Call(Hash.SET_VEHICLE_DENSITY_MULTIPLIER_THIS_FRAME, 0f);
        }

        public static void ConvertPedsToZeds(WorldPeds worldPeds, bool zedsAttackPlayer)
        {
            var pedsInWorld = World.GetAllPeds();

            // Don't apply zombie effects to any NPCs spawned by scripts, or previously processed peds (or the player)
            var existingPeds =
                worldPeds.Bandits//.Concat(worldPeds.Hoard)
                    .Concat(worldPeds.Military)
                    .Concat(worldPeds.Survivors)
                    .Concat(worldPeds.Wildlife)
                    .Concat(worldPeds.Zeds)
                    .Select(p => p.Handle)
                    .ToList();

            existingPeds.Add(Game.Player.Character.Handle);

            if (worldPeds.Companion != null)
            {
                existingPeds.Add(worldPeds.Companion.Handle);
            }

            var processedPeds = new List<Ped>();
            
            foreach (var ped in pedsInWorld)
            {
                // Don't process any existing peds, aliens or non-humans
                if (existingPeds.Contains(ped.Handle)) { continue; }
                //if (ped.Model.Hash == (uint)PedHash.MovAlien01) { continue; }
                if (!ped.IsHuman) { continue; }

                Function.Call(Hash.APPLY_PED_DAMAGE_PACK, ped.Handle, "BigHitByVehicle", 0, 1);
                Function.Call(Hash.APPLY_PED_DAMAGE_PACK, ped.Handle, "HOSPITAL_8", 0, 1);
                Function.Call(Hash.APPLY_PED_DAMAGE_PACK, ped.Handle, "HOSPITAL_9", 0, 1);
                Function.Call(Hash.APPLY_PED_DAMAGE_PACK, ped.Handle, "Explosion_Med", 0, 1);

                if (Function.Call<bool>(Hash.IS_AMBIENT_SPEECH_PLAYING, ped.Handle))
                {
                    Function.Call(Hash.STOP_CURRENT_PLAYING_AMBIENT_SPEECH, ped.Handle);
                }

                //Function.Call(Hash.STOP_PED_SPEAKING, ped.Handle, 0);

                Function.Call(Hash.SET_PED_FLEE_ATTRIBUTES, ped.Handle, 0, 0);
                
                if (!Function.Call<bool>(Hash.HAS_ANIM_SET_LOADED, "move_m@drunk@verydrunk"))
                {
                    Function.Call(Hash.REQUEST_ANIM_SET, "move_m@drunk@verydrunk");
                }

                Function.Call(Hash.SET_PED_MOVEMENT_CLIPSET, ped.Handle, "move_m@drunk@verydrunk", 1f);

                if (zedsAttackPlayer)
                {
                    ped.Task.GoTo(Game.Player.Character);
                }

                //if (ped.IsTouching(Game.Player.Character))
                //{
                //    Function.Call(Hash.APPLY_DAMAGE_TO_PED, Game.Player.Character.Handle, 10, 0);
                //}

                processedPeds.Add(ped);
            }

            var pedsDespawned = worldPeds.Zeds.Except(pedsInWorld);
            worldPeds.Zeds = worldPeds.Zeds.Except(pedsDespawned).ToList();
            worldPeds.Zeds.AddRange(processedPeds);

            // TODO
            //  / Add all peds in world to list
            //  / Remove player + peds in worldPeds
            //  / Set zeds to be drunk
            //  / Apply blood decals
            //  / Add ped to worldPeds.Zeds to prevent processing ped multiple times
            //  / Remove peds that have been despawned from worldPeds.Zeds
            //  - Stop peds using phones
            //  - Stop peds talking
            //  - Test that spawned npcs (bandits, wildlife, etc) don't get decals or drunk applied to them

            // Damage decal packs:
            // BigHitByVehicle, HOSPITAL_8, HOSPITAL_9, Explosion_Med

            // Example:
            // Function.Call(Hash.APPLY_PED_DAMAGE_PACK, ped, "BigHitByVehicle", 0, 1);

            // To make a ped drunk (zombie-like)
            // Function.Call(Hash.SET_PED_MOVEMENT_CLIPSET, ped.Handle, "move_m@drunk@verydrunk", 1f);
            //switch@trevor@drunk_howling
            //http://gtaforums.com/topic/817563-v-euphorianaturalmotion-messages/
        }


        public static void CreateNest(List<Ped> aliensInUpperNest, List<Ped> aliensInLowerNest)
        {
            // Place ~15 aliens in the nest

            // Upper level
            aliensInUpperNest.Add(World.CreatePed(PedHash.MovAlien01, new Vector3(-817.4495f, -128.2314f, 28.17533f)));
            aliensInUpperNest.Add(World.CreatePed(PedHash.MovAlien01, new Vector3(-818.6939f, -125.7161f, 28.17533f)));
            aliensInUpperNest.Add(World.CreatePed(PedHash.MovAlien01, new Vector3(-821.9654f, -128.6299f, 28.17533f)));
            aliensInUpperNest.Add(World.CreatePed(PedHash.MovAlien01, new Vector3(-819.9349f, -133.7624f, 28.17533f)));
            aliensInUpperNest.Add(World.CreatePed(PedHash.MovAlien01, new Vector3(-813.5521f, -133.4342f, 28.17536f)));
            aliensInUpperNest.Add(World.CreatePed(PedHash.MovAlien01, new Vector3(-810.6312f, -136.2794f, 28.17535f)));
            aliensInUpperNest.Add(World.CreatePed(PedHash.MovAlien01, new Vector3(-814.5298f, -144.5878f, 28.17535f)));

            foreach (var ped in aliensInUpperNest)
            {
                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped.Handle, 0, 0, 0, 2);
                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped.Handle, 3, 0, 0, 2);
                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped.Handle, 4, 0, 0, 2);
                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped.Handle, 5, 0, 0, 2);

                ped.Armor = 100;
                ped.MaxHealth = 500;
                ped.Health = 500;
            }

            // Lower level
            aliensInLowerNest.Add(World.CreatePed(PedHash.MovAlien01, new Vector3(-836.904f, -147.4867f, 19.95036f)));
            aliensInLowerNest.Add(World.CreatePed(PedHash.MovAlien01, new Vector3(-840.624f, -147.9867f, 19.95036f)));
            aliensInLowerNest.Add(World.CreatePed(PedHash.MovAlien01, new Vector3(-844.9889f, -151.647f, 19.95036f)));
            aliensInLowerNest.Add(World.CreatePed(PedHash.MovAlien01, new Vector3(-851.1503f, -155.384f, 19.95036f)));
            aliensInLowerNest.Add(World.CreatePed(PedHash.MovAlien01, new Vector3(-839.4078f, -150.9815f, 19.95036f)));
            aliensInLowerNest.Add(World.CreatePed(PedHash.MovAlien01, new Vector3(-845.4167f, -155.0815f, 19.95036f)));

            foreach (var ped in aliensInLowerNest)
            {
                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped.Handle, 0, 0, 0, 2);
                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped.Handle, 3, 0, 0, 2);
                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped.Handle, 4, 0, 0, 2);
                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped.Handle, 5, 0, 0, 2);

                ped.Armor = 100;
                ped.MaxHealth = 500;
                ped.Health = 500;
            }

            // Nest higher level -818.0561, -131.0791, 28.17533

            // Alien placement (higher level)
            // -817.4495, -128.2314, 28.17533
            // -818.6939, -125.7161, 28.17533
            // -821.9654, -128.6299, 28.17533
            // -819.9349, -133.7624, 28.17533
            // -813.5521, -133.4342, 28.17536
            // -810.6312, -136.2794, 28.17535
            // -814.5298, -144.5878, 28.17535

            // Nest lower level -832.9576, -145.6825, 19.94775

            // Alient placement (lower level)
            // -836.904, -147.4867, 19.95036
            // -840.624, -147.9867, 19.95036
            // -844.9889, -151.647, 19.95036
            // -851.1503, -155.384, 19.95036
            // -839.4078, -150.9815, 19.95036
            // -845.4167, -155.0815, 19.95036

            UI.Notify("Alien nest created");
        }

        public static void RunNest(List<Ped> aliensInUpperNest, List<Ped> aliensInLowerNest)
        {
            var upperNestCoord = new Vector3(-818.0561f, -131.0791f, 28.17533f);
            var lowerNestCoord = new Vector3(-832.9576f, -145.6825f, 19.94775f);

            // Upper nest
            if (Game.Player.Character.Position.DistanceTo(upperNestCoord) > 20)
            {
                // Make aliens attack player
                foreach (var ped in aliensInUpperNest)
                {
                    ped.Task.RunTo(Game.Player.Character.Position);
                }
            }

            // Lower nest
            if (Game.Player.Character.Position.DistanceTo(lowerNestCoord) > 15)
            {
                // Make aliens attack player
                foreach (var ped in aliensInLowerNest)
                {
                    ped.Task.RunTo(Game.Player.Character.Position);
                }
            }
        }



        public static void CreateHoard(List<Ped> pedsInHoard)
        {
            var hoardPosition = Game.Player.Character.GetOffsetInWorldCoords(new Vector3(-10f, 0, 0));

            for (int i = 0; i < 15; i++)
            {
                pedsInHoard.Add(GTA.World.CreateRandomPed(hoardPosition.Around(5)));
            }
        }

        public static void RunHoard(List<Ped> pedsInHoard)
        {
            var playerPos = Game.Player.Character.Position;
            var speed = 2f;

            foreach (var ped in pedsInHoard)
            {
                Function.Call(Hash.TASK_GO_STRAIGHT_TO_COORD, ped.Handle, playerPos.X, playerPos.Y, playerPos.Z, speed, -1, 0f, 0f);
            }
        }



        public static void RunWorldEvents(WorldPeds worldPeds)
        {
            RunSurvivors(worldPeds);
            RunWildlife(worldPeds);
            RunMilitary(worldPeds);
            RunBandits(worldPeds);
            RunHoard(worldPeds);
        }


        private static void RunSurvivors(WorldPeds worldPeds)
        {
        }

        private static void RunWildlife(WorldPeds worldPeds)
        {
        }

        private static void RunMilitary(WorldPeds worldPeds)
        {
        }

        private static void RunBandits(WorldPeds worldPeds)
        {
        }

        private static void RunHoard(WorldPeds worldPeds)
        {
        }
    }
}
