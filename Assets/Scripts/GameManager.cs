using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public static GameObject instance;
    static List<TileDataForUsed> tileList = new List<TileDataForUsed>();
    public BoundsInt area;
    public GameObject scarWall;
    public static Tilemap tilemap;
    public int RamNumTile;
    public Sprite[] scarSprites;
    public Sprite fixSpriteIns;
    public static Sprite fixSprite;
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
        tilemap = scarWall.GetComponent<Tilemap>();
        fixSprite = fixSpriteIns;
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
                    tempTile.pos = new Vector3Int(i, j, 0);
                    tempTile.isScar = false;
                    tileList.Add(tempTile);
                }
            }
        }
        int temp = RamNumTile;
        while (temp != 0)
        {
            int randamTile = Random.Range(0, scarSprites.Length);
            int randPos = Random.Range(0, tileList.Count);
            if (tileList[randPos].isScar)
            {
                continue;
            }
            temp--;
            Tile tempTile = ScriptableObject.CreateInstance<Tile>();
            tempTile.sprite = scarSprites[randamTile];
            tilemap.SetTile(tileList[randPos].pos, tempTile);
            tileList[randPos].isScar = true;
        }
    }

    public static bool getTile(Vector3Int temp)
    {
        if (tilemap.HasTile(temp))
        {
            foreach (var item in tileList)
            {
                if (item.pos == temp)
                    return item.isScar;
            }
        }
        return false;
    }

    public static void fixTile(Vector3Int pos)
    {
        Tile tempTile = ScriptableObject.CreateInstance<Tile>();
        tempTile.sprite = fixSprite;
        tilemap.SetTile(pos, tempTile);
    }
}
public class TileDataForUsed
{
    public Vector3Int pos;
    public bool isScar;
}
