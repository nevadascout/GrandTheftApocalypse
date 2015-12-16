namespace GrandTheftApocalpyse
{
    using System;
    using System.Windows.Forms;

    using GrandTheftApocalpyse.Internal;
    using GrandTheftApocalpyse.World;

    using GTA;
    using GTA.Math;
    using GTA.Native;

    public class Main : Script
    {
        private readonly WorldPeds worldPeds = new WorldPeds();

        public Main()
        {
            this.Tick += this.OnTick;
            this.KeyUp += this.onKeyUp;

            Logger.Log("GrandTheftApocalypse Initalised");

            // Disable wanted levels
            Function.Call(Hash.SET_MAX_WANTED_LEVEL, 0);
            Function.Call(Hash.CLEAR_PLAYER_WANTED_LEVEL, Game.Player);
        }

        private void OnTick(object sender, EventArgs e)
        {
            Ambient.DisableLife();
            Ambient.ConvertPedsToZeds(this.worldPeds);
            Ambient.RunWorldEvents(this.worldPeds);
        }

        private void onKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.NumPad0)
            {
                // Debug
                UI.Notify($"ZedCount: {this.worldPeds.Zeds.Count}");
                UI.Notify($"Peds in world: { GTA.World.GetAllPeds().Length }");
            }

            if (e.KeyCode == Keys.NumPad1)
            {
                Function.Call(Hash.CLEAR_PED_BLOOD_DAMAGE, Game.Player.Character.Handle);
            }
        }
    }
}
