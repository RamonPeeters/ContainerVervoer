using System;

namespace ContainerVervoer.Logic
{
    [Flags]
    public enum ContainerType
    {
        Normal = 0,
        Valuable = 1,
        Coolable = 2,
        ValuableAndCoolable = 3
    }
}
