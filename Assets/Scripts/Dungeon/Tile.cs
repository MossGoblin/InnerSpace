using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    private int addr_p;
    private int addr_q;
    private int addr_r;

    private int pos_p;
    private int pos_q;
    private int pos_r;

    private int biomeID;
    private int tileTypeID;
    private int tileID;


    public Tile(int p, int q, int r, int biomeID)
    {
        this.addr_p = p;
        this.addr_q = q;
        this.addr_r = r;
        this.biomeID = biomeID;
    }

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
