using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

public class Tile : MonoBehaviour
{

    public Address address;

    // OBS Wil those be needed
    private double posP;
    private double posQ;
    private double posR;

    public int biomeID { get; set; }
    public int tileTypeID  { get; set; }
    public int tileID  { get; set; }

        
    public TMP_Text debugText;

    void Start()
    {
        debugText.text = $"{address.addrP} / {address.addrQ} / {address.addrR}";
    }

    void Update()
    {

    }

    public List<double[]> GetNbrsAddrs()
    {
        var nbrs_list = new List<double[]>
        {
            new double[] { address.addrP, address.addrQ + 1, address.addrR - 1 },
            new double[] { address.addrP, address.addrQ - 1, address.addrR + 1 },
            new double[] { address.addrP + 1, address.addrQ - 1, address.addrR },
            new double[] { address.addrP - 1, address.addrQ + 1, address.addrR },
            new double[] { address.addrP + 1, address.addrQ, address.addrR - 1 },
            new double[] { address.addrP - 1, address.addrQ, address.addrR + 1 }
        };

        return nbrs_list;
    }

    public void SetData(Address newAddr, int chunkBiomeID, string tileData)
    {
        address = newAddr;

        // HERE Deserialize Tile Data
        biomeID = chunkBiomeID;

        string tileTypeID_pattern = "tileTypeID:(\\d)";
        Regex rg = new Regex(tileTypeID_pattern);
        MatchCollection matches = rg.Matches(tileData);
        tileTypeID = int.Parse(matches[0].Groups[1].Value);

        string tileID_pattern = "tileID:(\\d)";
        rg = new Regex(tileID_pattern);
        matches = rg.Matches(tileData);
        tileID = int.Parse(matches[0].Groups[1].Value);
    }

    public string GetData()
    {
        // {tileTypeID:0,tileID:0}
        string result = "";
        result += "{";
        result += $"tileTypeID:{tileTypeID},";
        result += $"tileID:{tileID}";
        result += "}";

        return result;
    }
}
