using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName ="Tile Config")]
public class TileConfig : ScriptableObject
{
    public List<TileConfig> left;
    public List<TileConfig> right;
    public List<TileConfig> up;
    public List<TileConfig> down;

    public List<TileConfig> ru;
    public List<TileConfig> rd;
    public List<TileConfig> lu;
    public List<TileConfig> ld;

    public Tile tile;
}
