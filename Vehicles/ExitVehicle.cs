namespace GrandTheftApocalpyse.Vehicles
{
    using System;
    using System.Windows.Forms;

    using GTA;
    using GTA.Native;

    public class ExitVehicle : Script
    {
        private DateTime vehicleLastExit;

        public ExitVehicle()
        {
            this.KeyDown += this.onKeyDown;
        }

        private void onKeyDown(object sender, KeyEventArgs e)
        {
            var player = Game.Player.Character;

            if (e.KeyCode == Keys.F && DateTime.Now > this.vehicleLastExit && player.IsInVehicle())
            {
                Script.Wait(500);

                var vehicle = player.CurrentVehicle;
                var isDriver = Function.Call<Ped>(Hash.GET_PED_IN_VEHICLE_SEAT, vehicle, (int)VehicleSeat.Driver) == player;

                if (Game.IsKeyPressed(Keys.F))
                {
                    // If the player is still holding the key after half a second, turn off the engine
                    player.Task.LeaveVehicle(vehicle, true);
                }
                else
                {
                    // Otherwise, leave it running and don't close the door
                    player.Task.LeaveVehicle(vehicle, false);

                    Script.Wait(0);

                    if (isDriver)
                    {
                        vehicle.EngineRunning = true;
                    }
                }

                this.vehicleLastExit = DateTime.Now + TimeSpan.FromMilliseconds(2000);
            }
        }
    }
}
