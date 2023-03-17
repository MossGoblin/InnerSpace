using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Serializer
{
    public static string AddressToString(int[] address)
    {
        // TODO check if address is valid
        string result = string.Join(",", address);

        return result;
    }

    public static int[] AddressToArray(string address)
    {
        // TODO check if address is valid
        string[] addressList = address.Split(",");
        int[] result = addressList.Select(c => int.Parse(c)).ToArray();

        return result;
    }
}
