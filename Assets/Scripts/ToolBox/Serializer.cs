using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Serializer
{
    public static string SerializeAddress(int[] address)
    {
        string result = string.Join(",", address);

        return result;
    }
}


// HERE remove the comma after the last opject in each section