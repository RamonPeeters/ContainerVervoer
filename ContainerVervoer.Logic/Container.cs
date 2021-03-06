﻿using System;

namespace ContainerVervoer.Logic
{
    public class Container
    {
        private const int MAX_WEIGHT_ON_TOP = 120000000;
        private const int EMPTY_CONTAINER_WEIGHT = 0;

        private readonly int _Weight;
        public int Weight { get { return EMPTY_CONTAINER_WEIGHT + _Weight; } }
        public ContainerType Type { get; }
        
        public Container(int weight, ContainerType type)
        {
            _Weight = weight;
            Type = type;
        }

        public static int GetMaxWeightOnTop()
        {
            return MAX_WEIGHT_ON_TOP;
        }

        public override string ToString()
        {
            string s = "'";

            s += Type.HasFlag(ContainerType.Coolable) ? "C" : "-";
            s += Type.HasFlag(ContainerType.Valuable) ? "V" : "-";

            return s + "'";
        }

        public override bool Equals(object obj)
        {
            return obj is Container container &&
                   Weight == container.Weight &&
                   Type == container.Type;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Weight, Type);
        }
    }
}
