using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetManager : MonoBehaviour
{
    public List<Sprite> tileSpriteSet00;
    public List<Sprite> tileSpriteSet01;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public struct TileSprite
    {
        public TileSprite(Sprite tileSprite, int tileIndex)
        {
            this.tileSprite = tileSprite;
            this.tileSpriteIndex = tileIndex;
        }
        public Sprite tileSprite { get; set; }
        public int tileSpriteIndex { get; set; }
    }
    public TileSprite GetSprite(int tileSetID)
    {
        int rndID = 0;
        switch (tileSetID)
        {
            case 0:
                rndID = Random.Range(0, tileSpriteSet00.Count);
                return new TileSprite(tileSpriteSet00[rndID], rndID);
            case 1:
                rndID = Random.Range(0, tileSpriteSet01.Count);
                return new TileSprite(tileSpriteSet01[rndID], rndID);
        }

        return new TileSprite(null, 0);
    }
}
