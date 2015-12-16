namespace GrandTheftApocalpyse.World
{
    using System.Collections.Generic;

    using GTA;

    public class WorldPeds
    {
        public WorldPeds()
        {
            // Values set by script when creating peds for world events, etc
            this.Survivors = new List<Ped>();
            this.Military = new List<Ped>();
            this.Bandits = new List<Ped>();
            this.Wildlife = new List<Ped>();
            this.Hoard = new List<Ped>();

            // Values set by ambient ped processor when converting regular peds into zombies
            this.Zeds = new List<Ped>();
        }

        public List<Ped> Zeds { get; set; }

        public List<Ped> Hoard { get; set; }

        public List<Ped> Survivors { get; set; }

        public List<Ped> Military { get; set; }

        public List<Ped> Bandits { get; set; }

        public List<Ped> Wildlife { get; set; }
    }
}
