using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WFS : MonoBehaviour
{
    public int w, h;
    BoxInfor[,] boxes;
    public List<TileConfig> tileConfigs;
    public float deltaT = .05f;
    int count;
    public Tilemap tileMap;
    public Tile defaultTile, markTile;
    public TileConfig emptyTile;
    
    struct BoxInfor
    {
        public bool collapsed;
        public List<TileConfig> possibleTiles;

        public BoxInfor(bool collapsed, List<TileConfig> possibleTiles)
        {
            this.collapsed = collapsed;
            this.possibleTiles = possibleTiles;
        }
    }

    private void Start()
    {
        count = 1;
        boxes = new BoxInfor[w, h];
        for(int i=0; i<w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                boxes[i, j] = new BoxInfor(false, tileConfigs);
                tileMap.SetTile(new Vector3Int(i, j, 0), defaultTile);
            }
        }
        SetTile(w/2, h/2);
        StartCoroutine(SetTileCo());
    }

    void SetEmptyTile(int i, int j)
    {
        Debug.Log("Here");
        if (i < 0 || j < 0 || i >= w || j >= h) return;
        if (boxes[i, j].collapsed) return;

        boxes[i, j].collapsed = true;
        tileMap.SetTile(new Vector3Int(i, j, 0), emptyTile.tile);
        count++;


        SetPossibles(i-1, j + 1, emptyTile.lu);
        SetPossibles(i, j + 1, emptyTile.up);
        SetPossibles(i+1, j + 1, emptyTile.ru);
         SetPossibles(i + 1, j, emptyTile.right);
        SetPossibles(i+1, j - 1, emptyTile.rd);
         SetPossibles(i, j - 1, emptyTile.down);
        SetPossibles(i-1, j - 1, emptyTile.ld);     
         SetPossibles(i - 1, j, emptyTile.left);
    }

    void Progress()
    {
        if (count > w * h) return;
        int tmpMin = 1000;
        Vector2Int res = new Vector2Int(0, 0);
        int tmp;
        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                if (boxes[i, j].collapsed) continue;
                tmp = boxes[i, j].possibleTiles.Count;
                if (tmp < tmpMin)
                {
                    tmpMin = tmp;
                    res.x = i;
                    res.y = j;
                }
            }
        }

        SetTile(res.x, res.y);
    }

    void SetTile(int i, int j)
    {
        if (i < 0 || j < 0 || i >= w || j >= h) return;
        if (boxes[i, j].collapsed) return;

        boxes[i, j].collapsed = true;
        int size = boxes[i, j].possibleTiles.Count;
        int rd = Random.Range(0, size);
        tileMap.SetTile(new Vector3Int(i, j, 0), boxes[i, j].possibleTiles[rd].tile);
        count++;


        SetPossibles(i-1, j + 1, boxes[i, j].possibleTiles[rd].lu);
        SetPossibles(i, j + 1, boxes[i, j].possibleTiles[rd].up);
        SetPossibles(i+1, j + 1, boxes[i, j].possibleTiles[rd].ru);
        SetPossibles(i + 1, j, boxes[i, j].possibleTiles[rd].right);
        SetPossibles(i+1, j - 1, boxes[i, j].possibleTiles[rd].rd);
        SetPossibles(i, j-1, boxes[i, j].possibleTiles[rd].down);
        SetPossibles(i-1, j - 1, boxes[i, j].possibleTiles[rd].ld);     
        SetPossibles(i - 1, j, boxes[i, j].possibleTiles[rd].left);
    }

    void SetPossibles(int i, int j, List<TileConfig> possibles)
    {
        if (i < 0 || j < 0 || i >= w || j >= h) return;
        if (boxes[i, j].collapsed) return;
        if (possibles == null || possibles.Count == 0 || boxes[i, j].possibleTiles.Count == 0)
        {
            Debug.Log($"({i},{j}) {possibles == null} {possibles.Count == 0} {boxes[i, j].possibleTiles.Count == 0}");
            SetEmptyTile(i, j);
            return;
        }

        List<TileConfig> res = new List<TileConfig>();
        int n = possibles.Count;
        for(int k=0; k<n; k++)
        {
            if (boxes[i, j].possibleTiles.Contains(possibles[k])){
                res.Add(possibles[k]);
            }
        }

        tileMap.SetTile(new Vector3Int(i, j, 0), markTile);

        // Debug.Log(res.Count);

        if (res.Count == 0)
        {
            SetEmptyTile(i, j);
            return;
        }
        boxes[i, j].possibleTiles = res;
        // Debug.Log(res.Count);
    }

    IEnumerator SetTileCo()
    {
        Progress();
        yield return new WaitForSeconds(deltaT);
        if(count <= w*h)
        {
            StartCoroutine(SetTileCo());
        }
    }
}
