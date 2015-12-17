namespace GrandTheftApocalpyse
{
    using System;
    using System.Linq;
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
        }

        private void OnTick(object sender, EventArgs e)
        {
            Ambient.DisableLife(this.enableWorldZeds);
            Ambient.ConvertPedsToZeds(this.worldPeds, this.enableZedAttack);
            Ambient.RunWorldEvents(this.worldPeds);

            Npcs.RunCompanion(this.worldPeds.Companion, this.tickCount);


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
                // Debug
                UI.Notify($"ZedCount: {this.worldPeds.Zeds.Count}");
                UI.Notify($"Peds in world: {GTA.World.GetAllPeds().Length}");
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
                // Spawn aussie dog as companion
                var companionGroupId = GTA.World.AddRelationshipGroup("companion");
                Game.Player.Character.RelationshipGroup = companionGroupId;

                var companion = Npcs.SpawnCompanion(PedHash.Shepherd, Game.Player.Character.GetOffsetInWorldCoords(new Vector3(0, 5, 0)), companionGroupId);

                //companion.Task.RunTo(Game.Player.Character, new Vector3(1, 1, 0));

                companion.Task.RunTo(Game.Player.Character.GetOffsetInWorldCoords(new Vector3(1, 1, 0)), false);

                //Function.Call(Hash.TASK_FOLLOW_TO_OFFSET_OF_ENTITY, companion.Handle, Game.Player.Character, 1, 1, 0, 100, -1, 1, 1);

                //companion.Task.GoTo(Game.Player.Character, new Vector3(1, 1, 0));

                this.worldPeds.Companion = companion;
                Logger.Log("Creating companion");
            }


            if (e.KeyCode == Keys.F8)
            {
                // Make dog attack nearest zed
                var worldpeds = GTA.World.GetAllPeds().Where(p => p != Game.Player.Character);
                // Example:
                //Function.Call(Hash.TASK_COMBAT_PED, dogPed.Handle, zedPed.Handle, 0, 16);
            }
        }
    }
}
