using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDungeonGenerator : MonoBehaviour
{
    public int numberOfChunks;
    public int biomeCount;
    public int tileSetCount_00;
    public int tileSetCount_01;
    public int tileSetCount_02;
    public int tileSetCount_03;
    private List<Address> addressesInWaiting;
    private List<Address> addressesDone;
    private Dictionary<Address, Chunk> chunkList;
    public Chunk chunkPrefab;
    private string dungeonFilename;
    private Address activeChunkAddress;


    void Start()
    {
        addressesInWaiting = new List<Address>();
        addressesDone = new List<Address>();
        chunkList = new Dictionary<Address, Chunk>();
        ToolBox tb = new ToolBox();
        tb.Init(123456789);
        // start chunk is always 0, 0
        Address startAdress = new Address(0, 0);
        addressesInWaiting.Add(startAdress);
        int chunkCount = 1;

        while ((chunkCount <= numberOfChunks) && (addressesInWaiting.Count > 0))
        {
            int randomAddressInWaitingIndex = tb.RandomInt(0, addressesInWaiting.Count);
            Address currentAddress = addressesInWaiting[randomAddressInWaitingIndex];
            addressesInWaiting.RemoveAt(randomAddressInWaitingIndex);
            List<Address> nbrs = GetNbrs(currentAddress);
            foreach (Address nbrAddress in nbrs)
            {
                if (addressesInWaiting.Contains(nbrAddress) || addressesDone.Contains(nbrAddress))
                {
                }
                else
                {
                    addressesInWaiting.Add(nbrAddress);
                }
            }
            Chunk newChunk = Instantiate(chunkPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            newChunk.biomeID = tb.RandomInt(0, biomeCount);
            if (currentAddress.Equals(startAdress))
            {
                newChunk.isActivated = true;
            }
            else
            {
                newChunk.isActivated = false;
            }
            newChunk.GenerateRandomTiles();
            chunkList.Add(currentAddress, newChunk);
            addressesDone.Add(currentAddress);
            chunkCount ++;
        }
        Debug.Log(chunkList);
        activeChunkAddress = new Address(0, 0);
        dungeonFilename = "NewGeneratedDungeonData.txt";
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            Debug.Log($"Saving {chunkList.Count} chunks");
            SaveDungeonData();
        }
    }

    private List<Address> GetNbrs(Address address)
    {
        List<Address> nbrs = new List<Address>();

        nbrs.Add(new Address(address.addrP + 1, address.addrQ - 1));
        nbrs.Add(new Address(address.addrP + 1, address.addrQ));
        nbrs.Add(new Address(address.addrP, address.addrQ - 1));
        nbrs.Add(new Address(address.addrP, address.addrQ + 1));
        nbrs.Add(new Address(address.addrP - 1, address.addrQ));
        nbrs.Add(new Address(address.addrP - 1, address.addrQ + 1));

        return nbrs;
    }

    private void SaveDungeonData()
    {
        string dungeonString = CompileDungeonData();

        if (System.IO.File.Exists(dungeonFilename) == true)
        {
            System.IO.File.Delete(dungeonFilename);
        }

        System.IO.File.WriteAllText(dungeonFilename, dungeonString);
    }

    private string CompileDungeonData()
    {
        string result = "";
        result += $"{{activeChunkAddress:{activeChunkAddress.addrP},{activeChunkAddress.addrQ}}}\r";
        foreach (Address address in chunkList.Keys)
        {
            result += $"{{chunkaddr:{address.ToString()},chunkBiomeID:{chunkList[address].biomeID}}}";
            result += chunkList[address].GetData();
            result += "\r";
        }

        return result;
    }
}
