using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SpriteLoader : MonoBehaviour
{
    void Start()
    {
        string root = @"D:\[ CODEE ]\[ GITTED ]\InnerSpace\Assets\Tilesets\dev02\TestField";
        // Get a list of all subdirectories

        // var dirs = Directory.EnumerateDirectories(root);

        // foreach (var subdir in dirs)
        // {
        //     Debug.Log(subdir);
        // }

        var files = Directory.EnumerateFiles(root);
        foreach (var file in files)
        {
            if (file.EndsWith(".png"))
            {
                Debug.Log(file);
            }
        }


        /*
        var dirs = from dir in
            Directory.EnumerateDirectories(root)
                     select dir;
        Debug.Log("Subdirectories: {0}", dirs.Count<string>().ToString());
        Debug.Log("List of Subdirectories");
        foreach (var dir in dirs)
        {
            Debug.Log("{0}", dir.Substring(dir.LastIndexOf("\\") + 1));
        }

        */
    }
}
