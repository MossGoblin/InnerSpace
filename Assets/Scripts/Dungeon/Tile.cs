using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    public int addr_p { get; set; }
    public int addr_q { get; set; }
    public int addr_r { get; set; }

    private int pos_p;
    private int pos_q;
    private int pos_r;

    public int biomeID { get; set; }
    public int tileTypeID  { get; set; }
    public int tileID  { get; set; }


    // public Tile(int p, int q, int r, int biomeID)
    // {
    //     this.addr_p = p;
    //     this.addr_q = q;
    //     this.addr_r = r;
    //     this.biomeID = biomeID;
    // }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public List<int[]> GetNbrsAddrs()
    {
        var nbrs_list = new List<int[]>
        {
            new int[] { addr_p, addr_q + 1, addr_r - 1 },
            new int[] { addr_p, addr_q - 1, addr_r + 1 },
            new int[] { addr_p + 1, addr_q - 1, addr_r },
            new int[] { addr_p - 1, addr_q + 1, addr_r },
            new int[] { addr_p + 1, addr_q, addr_r - 1 },
            new int[] { addr_p - 1, addr_q, addr_r + 1 }
        };

        return nbrs_list;
    }
}
