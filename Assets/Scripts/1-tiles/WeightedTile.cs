using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts._1_tiles
{
    public class WeightedTile : MonoBehaviour
    {
        [SerializeField] public float weight;
        [SerializeField] public TileBase tile;

    }
}
