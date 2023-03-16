using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

public class Tile : MonoBehaviour
{

    public double addrP { get; set; }
    public double addrQ { get; set; }
    public double addrR { get; set; }

    private double posP;
    private double posQ;
    private double posR;

    public int biomeID { get; set; }
    public int tileTypeID  { get; set; }
    public int tileID  { get; set; }

        
    public TMP_Text debugText;

    void Start()
    {
        debugText.text = string.Join(" / ", new List<double>{addrP, addrQ, addrR});
    }

    void Update()
    {

    }

    public List<double[]> GetNbrsAddrs()
    {
        var nbrs_list = new List<double[]>
        {
            new double[] { addrP, addrQ + 1, addrR - 1 },
            new double[] { addrP, addrQ - 1, addrR + 1 },
            new double[] { addrP + 1, addrQ - 1, addrR },
            new double[] { addrP - 1, addrQ + 1, addrR },
            new double[] { addrP + 1, addrQ, addrR - 1 },
            new double[] { addrP - 1, addrQ, addrR + 1 }
        };

        return nbrs_list;
    }

    public void SetData(string addressStr, string tileData)
    {
        // int[] address = Serializer.AddressToArray(addressStr);
        // addrP = address[0];
        // addrQ = address[1];
        // addrR = address[2];

        // HERE Deserialize Tile Data
        string biomeID_pattern = "biomeID\": (\\d)";
        Regex rg = new Regex(biomeID_pattern);
        MatchCollection matches = rg.Matches(tileData);
        biomeID = int.Parse(matches[0].Groups[1].Value);

        string tileTypeID_pattern = "tileTypeID\": (\\d)";
        rg = new Regex(tileTypeID_pattern);
        matches = rg.Matches(tileData);
        tileTypeID = int.Parse(matches[0].Groups[1].Value);

        string tileID_pattern = "tileID\": (\\d)";
        rg = new Regex(tileID_pattern);
        matches = rg.Matches(tileData);
        tileID = int.Parse(matches[0].Groups[1].Value);
    }

    public string GetData()
    {
        string result = "";
        result += "{";
        result += $"\"biomeID\" : {biomeID},";
        result += $"\"tileTypeID\" : {tileTypeID},";
        result += $"\"tileID\" : {tileID}";
        result += "},";

        return result;
    }
}
