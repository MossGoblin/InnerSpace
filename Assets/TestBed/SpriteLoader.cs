using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SpriteLoader : MonoBehaviour
{

    // public List<Sprite> spritesAll;
    public List<Sprite> chunkSprites;
    public List<Sprite> hexField;
    public List<Sprite> hexDesert;
    public List<Sprite> hexVolcanic;
    public List<Sprite> hexSnow;
    void Start()
    {
        // spritesAll = new List<Sprite>();
        chunkSprites = new List<Sprite>();
        hexField = new List<Sprite>();
        hexDesert = new List<Sprite>();
        hexVolcanic = new List<Sprite>();
        hexSnow = new List<Sprite>();
        string[] prefixes = {"chunk", "hex_fields", "hex_desert", "hex_volcanic", "hex_snow"};

        string root = @"D:\[ CODEE ]\[ GITTED ]\InnerSpace\Assets\Tilesets\dev02\TestField\Resources\TestSprites";
        string resourcesRoot = "TestSprites";
        var files = Directory.EnumerateFiles(root);

        foreach (var filePath in files)
        {
            if (filePath.EndsWith(".png"))
            {
                string[] filenameSplit = filePath.Split("\\");
                int lastIndex = filenameSplit.Length - 1;
                string fileName = filenameSplit[lastIndex];
                string fullFileName = resourcesRoot + "/" + fileName.Replace(".png", "");
                Sprite loadedSprite = Resources.Load<Sprite>(fullFileName);
                Debug.Log($"file loaded: {fileName}");
                // Distribute sprites
                if (fileName.StartsWith(prefixes[0]))
                {
                    chunkSprites.Add(loadedSprite);
                }
                else if (fileName.StartsWith(prefixes[1]))
                {
                    hexField.Add(loadedSprite);
                }
                else if (fileName.StartsWith(prefixes[2]))
                {
                    hexDesert.Add(loadedSprite);
                }
                else if (fileName.StartsWith(prefixes[3]))
                {
                    hexVolcanic.Add(loadedSprite);
                }
                else if (fileName.StartsWith(prefixes[4]))
                {
                    hexSnow.Add(loadedSprite);
                }
            }
        }
        Debug.Log("DONE");
    }
}
