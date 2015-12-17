namespace GrandTheftApocalpyse.World
{
    using GrandTheftApocalpyse.Internal;

    using GTA;
    using GTA.Math;
    using GTA.Native;

    public class Npcs
    {
        public static void RunCompanion(Ped companion, int tickCount)
        {
            //if (tickCount % 1000 == 0)
            //{
            //    companion?.Task.GoTo(Game.Player.Character, new Vector3(1, 1, 0));
            //}

            // note - won't work as it's not a sequence
            // only thing that does work is this:
            // Function.Call(Hash.TASK_GO_STRAIGHT_TO_COORD, dog.Handle, playerPos.X, playerPos.Y, playerPos.Z, 2f, -1, 0f, 0f); // speed -> 1 walk, 2 jog, 3 sprint (note that drunk only allows jogging)
            if (companion?.TaskSequenceProgress == 100)
            {
                companion.Task.RunTo(Game.Player.Character.GetOffsetInWorldCoords(new Vector3(1, 1, 0)), false);
                //companion.Task.GoTo(Game.Player.Character, new Vector3(1, 1, 0));
            }

            // companion?.Task.RunTo(Game.Player.Character.GetOffsetInWorldCoords(new Vector3(1, 1, 0)), false);
        }

        public static Ped SpawnCompanion(PedHash pedHash, Vector3 location, int groupId)
        {
            var ped = GTA.World.CreatePed(pedHash, location);
            ped.IsPersistent = true;
            ped.RelationshipGroup = groupId;
            ped.NeverLeavesGroup = true;

            var blip = ped.AddBlip();
            blip.IsFriendly = true;
            blip.Scale = 0.5f;

            return ped;
        }
    }
}
