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

    public Sprite GetSprite(int tileSetID)
    {
        int rndID = 0;
        switch (tileSetID)
        {
            case 0:
                rndID = Random.Range(0, tileSpriteSet00.Count);
                return tileSpriteSet00[rndID];
            case 1:
                rndID = Random.Range(0, tileSpriteSet01.Count);
                return tileSpriteSet01[rndID];
        }

        return null;
    }
}
