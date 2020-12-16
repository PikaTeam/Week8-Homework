using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarTest
{
    public interface IWeightedGraph<T>
    {
        IEnumerable<KeyValuePair<T,float>> Neighbors(T node);
    }
}
