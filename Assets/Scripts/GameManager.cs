using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public static GameObject instance;
    List<TileDataForUsed> tileList = new List<TileDataForUsed>();
    public BoundsInt area;
    public Tilemap tilemap;
    public int RamNumTile;
    public Sprite[] scarSprites;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this.gameObject;
        }
        else
        {
            if (instance != null)
                Destroy(this.gameObject);
        }
    }
    private void Start()
    {
        initTile();
    }
    private void initTile()
    {
        for (int i = area.xMin; i < area.xMax; i++)
        {
            for (int j = area.yMin; j < area.yMax; j++)
            {
                if (tilemap.HasTile(new Vector3Int(i, j, 0)))
                {
                    TileDataForUsed tempTile = new TileDataForUsed();
                    tempTile.pos=new Vector3Int(i, j, 0);
                    tempTile.isScar = false;
                    tileList.Add(tempTile);
                }
            }
        }
        int temp = RamNumTile;
        while(temp!=0)
        {
            int randamTile = Random.Range(0, scarSprites.Length);
            int randPos = Random.Range(0, tileList.Count);
            if (tileList[randPos].isScar)
            {
                continue;
            }
            temp--;
            Tile tempTile=ScriptableObject.CreateInstance<Tile>();
            tempTile.sprite = scarSprites[randamTile];
            tilemap.SetTile(tileList[randPos].pos, tempTile);
            tileList[randPos].isScar = true;
        }
    }
}

public class TileDataForUsed
{
    public Vector3Int pos;
    public bool isScar;
}
