using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
        // debugText.text = string.Join(" / ", new List<double>{addrP, addrQ, addrR});
        // debugText.text = "QWEASDZXC";
        debugText.enabled = false;
    }

    // Update is called once per frame
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
}
