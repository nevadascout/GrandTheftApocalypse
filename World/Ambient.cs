namespace GrandTheftApocalpyse.World
{
    using System.Collections.Generic;
    using System.Linq;

    using GTA;
    using GTA.Native;

    public class Ambient
    {
        public static void DisableLife()
        {
            // TODO -- NOTE: some random events still occur -> eg. cars arriving at roadside market stands
            Function.Call(Hash.SET_PARKED_VEHICLE_DENSITY_MULTIPLIER_THIS_FRAME, 0f);
            Function.Call(Hash.SET_RANDOM_VEHICLE_DENSITY_MULTIPLIER_THIS_FRAME, 0f);
            //Function.Call(Hash.SET_PED_DENSITY_MULTIPLIER_THIS_FRAME, 0f);
            Function.Call(Hash.SET_SCENARIO_PED_DENSITY_MULTIPLIER_THIS_FRAME, 0f);
            Function.Call(Hash.SET_VEHICLE_DENSITY_MULTIPLIER_THIS_FRAME, 0f);
        }

        public static void ConvertPedsToZeds(WorldPeds worldPeds)
        {
            var pedsInWorld = World.GetAllPeds();

            // Don't apply zombie effects to any NPCs spawned by scripts, or previously processed peds (or the player)
            var existingPeds =
                worldPeds.Bandits.Concat(worldPeds.Hoard)
                    .Concat(worldPeds.Military)
                    .Concat(worldPeds.Survivors)
                    .Concat(worldPeds.Wildlife)
                    .Concat(worldPeds.Zeds)
                    .Select(p => p.Handle)
                    .ToList();

            existingPeds.Add(Game.Player.Character.Handle);

            var processedPeds = new List<Ped>();
            
            foreach (var ped in pedsInWorld)
            {
                if (existingPeds.Contains(ped.Handle)) { continue; }

                Function.Call(Hash.APPLY_PED_DAMAGE_PACK, ped.Handle, "BigHitByVehicle", 0, 1);
                Function.Call(Hash.APPLY_PED_DAMAGE_PACK, ped.Handle, "HOSPITAL_8", 0, 1);
                Function.Call(Hash.APPLY_PED_DAMAGE_PACK, ped.Handle, "HOSPITAL_9", 0, 1);
                Function.Call(Hash.APPLY_PED_DAMAGE_PACK, ped.Handle, "Explosion_Med", 0, 1);

                if (!Function.Call<bool>(Hash.HAS_ANIM_SET_LOADED, "move_m@drunk@verydrunk"))
                {
                    Function.Call(Hash.REQUEST_ANIM_SET, "move_m@drunk@verydrunk");
                }

                Function.Call(Hash.SET_PED_MOVEMENT_CLIPSET, ped.Handle, "move_m@drunk@verydrunk", 1f);

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
