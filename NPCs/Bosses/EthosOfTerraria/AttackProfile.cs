using System;

namespace DarknessUnbound.NPCs.Bosses.EthosOfTerraria
{
    public struct AttackProfile
    {
        public float CD;
        public Action Attack;

        public AttackProfile(float cd, Action attack)
        {
            CD = cd;
            Attack = attack;
        }
    }
}
