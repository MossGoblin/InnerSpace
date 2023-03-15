using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Serializer
{
    public static string AddressToString(int[] address)
    {
        string result = string.Join(",", address);

        return result;
    }

    public static int[] AddressToArray(string address)
    {
        string[] addressList = address.Split(",");
        int[] result = addressList.Select(c => int.Parse(c)).ToArray();

        return result;
    }
}
