﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts._0_bfs
{
    public interface IWeightedGraph<T>
    {
        // returns KeyValuePair
        // where Key = VERTEX ID (position)
        // and   value = WEIGHT.
        IEnumerable<KeyValuePair<T,float>> Neighbors(T node);
    }
}
