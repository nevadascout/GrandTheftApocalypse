namespace GrandTheftApocalpyse
{
    using System;

    using GrandTheftApocalpyse.World;

    using GTA;

    public class Main : Script
    {
        public Main()
        {
        }

        private void OnTick(object sender, EventArgs e)
        {
            Ambient.DisableLife();
        }
    }
}
