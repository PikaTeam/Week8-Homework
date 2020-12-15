using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts._1_tiles
{
    class WeightedAllowedTiles : MonoBehaviour
    {
        [SerializeField] WeightedTile[] allowedTiles;

        public bool Contains(WeightedTile tile)
        {
            return allowedTiles.Contains(tile);
        }

        public bool Contains(TileBase tile)
        {
            foreach(var aTile in allowedTiles)
            {
                if (aTile.tile.Equals(tile))
                    return true;
            }

            return false;
        }

        public float GetWeightOf(TileBase tile)
        {
            foreach (var aTile in allowedTiles)
            {
                if (aTile.tile.Equals(tile))
                    return aTile.weight;
            }

            throw new KeyNotFoundException("Tried to get the tile's weight but the tile wasn't found on the list");
        }

        public WeightedTile[] Get() { return allowedTiles; }
    }
}
