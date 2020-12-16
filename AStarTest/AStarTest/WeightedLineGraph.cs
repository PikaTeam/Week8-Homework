using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.Numerics;

namespace AStarTest
{ 

    class WeightedLineGraph : IWeightedGraph<Vector3>
    {
        static Vector3[] directions = {
            new Vector3(-1, 0, 0),
            new Vector3(1, 0, 0),
        };

        public IEnumerable<KeyValuePair<Vector3, float>> Neighbors(Vector3 node)
        {
            foreach(var direction in directions)
            {
                yield return new KeyValuePair<Vector3, float>(
                    node + direction,
                    1 + Math.Abs(node.X)/10);
            }

        }
    }


}
