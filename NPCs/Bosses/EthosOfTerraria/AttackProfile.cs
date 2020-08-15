using System;

namespace DarknessUnbound.NPCs.Bosses.EthosOfTerraria
{
    public struct AttackProfile
    {
        public const int Attack_DartCircle = 1;
        public const int Attack_Wavy = 2;

        public float CD;
        public Action Attack;

        public AttackProfile(float cd, Action attack)
        {
            CD = cd;
            Attack = attack;
        }
    }
}
