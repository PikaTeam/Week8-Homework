using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace AStarTest
{
    class Weighted2DGraph : IWeightedGraph<Vector3>
    {
        static Vector3[] directions = {
            new Vector3(-1, 0, 0),
            new Vector3(1, 0, 0),
            new Vector3(0, -1, 0),
            new Vector3(0, 1, 0)
        };


        public IEnumerable<KeyValuePair<Vector3, float>> Neighbors(Vector3 node)
        {
            foreach (var direction in directions)
            {
                yield return new KeyValuePair<Vector3, float>(
                    node + direction,
                    Weight(node, node+direction));
            }

        }

        private float Weight(Vector3 v1, Vector3 v2)
        {
            if (v1.X == 2 && v1.Y == 2)
            {
                return 5;
            }

            return 1;
        }
    }
}
