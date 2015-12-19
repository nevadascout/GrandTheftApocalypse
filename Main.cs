namespace GrandTheftApocalpyse
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    using GrandTheftApocalpyse.Internal;
    using GrandTheftApocalpyse.World;

    using GTA;
    using GTA.Math;
    using GTA.Native;

    public class Main : Script
    {
        private readonly WorldPeds worldPeds = new WorldPeds();

        private bool enableWorldZeds = true;
        private bool enableZedAttack = false;

        private int tickCount = 1;

        public Main()
        {
            this.Tick += this.OnTick;
            this.KeyUp += this.onKeyUp;

            Logger.Log("GrandTheftApocalypse Initalised");

            // Disable wanted levels
            Function.Call(Hash.SET_MAX_WANTED_LEVEL, 0);
            Function.Call(Hash.CLEAR_PLAYER_WANTED_LEVEL, Game.Player);

            //Ambient.CreateNest(this.worldPeds.UpperNest, this.worldPeds.LowerNest);
        }

        private void OnTick(object sender, EventArgs e)
        {
            Ambient.DisableLife(this.enableWorldZeds);
            Ambient.ConvertPedsToZeds(this.worldPeds, this.enableZedAttack);
            Ambient.RunWorldEvents(this.worldPeds);

            Npcs.RunCompanion(this.worldPeds.Companion, this.tickCount);

            Ambient.RunHoard(this.worldPeds.Hoard);
            //Ambient.RunNest(this.worldPeds.UpperNest, this.worldPeds.LowerNest);


            this.tickCount++;

            if (this.tickCount >= 5000)
            {
                this.tickCount = 1;
            }
        }

        private void onKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F9)
            {
                // Spawn military world event

                var chopper = GTA.World.CreateVehicle(VehicleHash.Valkyrie, new Vector3(1331.345f, 3368.439f, 60f));
                chopper.CreateRandomPedOnSeat(VehicleSeat.Driver);
                chopper.CreateRandomPedOnSeat(VehicleSeat.Passenger);
                chopper.Speed = 100;
                chopper.IsPersistent = false;

                var blip = chopper.AddBlip();
                blip.Color = BlipColor.Red;
                blip.Scale = 0.5f;

                var driver = chopper.GetPedOnSeat(VehicleSeat.Driver);
                Function.Call(Hash.TASK_VEHICLE_DRIVE_TO_COORD, driver.Handle, chopper.Handle, 1198.114f, 4066.201f, 10f, 200f, 1, (uint)VehicleHash.Valkyrie, 16777216, 0, -1);
                //Function.Call(Hash.TASK_GO_STRAIGHT_TO_COORD, driver.Handle, 1198.114f, 4066.201f, 30f, 5f, -1, 0f, 0f);
                //driver.Task.DriveTo(chopper, new Vector3(1234.517f, 3661.473f, 30f), 0f, 5f);


                var chopper2 = GTA.World.CreateVehicle(VehicleHash.Valkyrie, new Vector3(1331.345f, 3338.439f, 60f));
                chopper2.CreateRandomPedOnSeat(VehicleSeat.Driver);
                chopper2.CreateRandomPedOnSeat(VehicleSeat.Passenger);
                chopper2.Speed = 100;
                chopper2.IsPersistent = false;

                var blip2 = chopper2.AddBlip();
                blip2.Color = BlipColor.Red;
                blip2.Scale = 0.5f;

                var driver2 = chopper2.GetPedOnSeat(VehicleSeat.Driver);
                Function.Call(Hash.TASK_VEHICLE_DRIVE_TO_COORD, driver2.Handle, chopper2.Handle, 1198.114f, 4066.201f, 10f, 200f, 1, (uint)VehicleHash.Valkyrie, 16777216, 0, -1);

                //16777216

                UI.Notify("Triggering military event");
            }

            if (e.KeyCode == Keys.F10)
            {
                this.enableWorldZeds = !this.enableWorldZeds;
                UI.Notify($"Zeds spawn: {this.enableWorldZeds}");
            }

            if (e.KeyCode == Keys.F11)
            {
                this.enableZedAttack = !this.enableZedAttack;
                UI.Notify($"Zeds attack: {this.enableWorldZeds}");
            }

            if (e.KeyCode == Keys.F8)
            {
                this.worldPeds.Companion?.Delete();

                // Spawn aussie dog as companion
                var companionGroupId = GTA.World.AddRelationshipGroup("companion");
                Game.Player.Character.RelationshipGroup = companionGroupId;

                var companion = Npcs.SpawnCompanion(PedHash.Shepherd, Game.Player.Character.GetOffsetInWorldCoords(new Vector3(0, 5, 0)), companionGroupId);

                this.worldPeds.Companion = companion;
                Logger.Log("Creating companion");
            }

            if (e.KeyCode == Keys.F7)
            {
                // Delete companion
                this.worldPeds.Companion.Delete();
                this.worldPeds.Companion = null;
            }

            if (e.KeyCode == Keys.Add)
            {
                // Get location
                UI.Notify("Location saved to log");

                var p = Game.Player.Character.Position;

                Logger.Log($"Player coords: {p.X}, {p.Y}, {p.Z}");
            }

            if (e.KeyCode == Keys.Subtract)
            {
                // Clean up alien nest
                UI.Notify("Cleaning up nest");

                foreach (var ped in this.worldPeds.UpperNest)
                {
                    ped.Delete();
                }

                foreach (var ped in this.worldPeds.LowerNest)
                {
                    ped.Delete();
                }

                this.worldPeds.UpperNest = new List<Ped>();
                this.worldPeds.LowerNest = new List<Ped>();
            }

            if (e.KeyCode == Keys.Multiply)
            {
                //UI.Notify("Creating hoard");
                //Ambient.CreateHoard(this.worldPeds.Hoard);

                UI.Notify("Creating military");

                // Marine model - PedHash.Marine03SMY
                // Marine component variation: hat (3) is value 1

                // FIB Model - PedHash.FibSec01

                // Vehicle positions
                // Tank (center):   647.5102f, -1020.344f, 36.56903f
                // Tank (right):    644.8627f, -1011.547f, 36.52551f
                // FIB car (left):  640.3108f, -1029.581f, 36.30553f

                // TODO - Get coords for each vehicle to drive to
                // TODO - Get coords to spawn each ped
                // TODO - Get coords for each ped to run to
            }

            if (e.KeyCode == Keys.Divide)
            {
                //UI.Notify("Cleaning up hoard");

                //foreach (var ped in this.worldPeds.Hoard)
                //{
                //    ped.Delete();
                //}

                //this.worldPeds.Hoard = new List<Ped>();

                UI.Notify("Cleaning up military");

                foreach (var ped in this.worldPeds.Military)
                {
                    ped.Delete();
                }

                this.worldPeds.Military = new List<Ped>();
            }
        }
    }
}
