using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Timeline;
// using Open.Numeric.Primes;

public class Ticker : MonoBehaviour
{
    [SerializeField]
    [Range(1, 100)]
    private int timeFactor;
    [SerializeField]
    private int cicleLength;
    [SerializeField]
    private int time;
    [SerializeField]
    private List<int> tickTriggers;
    [SerializeField]
    private List<int> tickCounts;
    [SerializeField]
    private List<int> tickRemainders;


    // Start is called before the first frame update
    void Start()
    {
        cicleLength = 23;

        time = 0;


        // value safety
        if (timeFactor == 0)
        {
            timeFactor = 100;
        }

        if (tickTriggers.Count == 0)
        {
            tickTriggers = new List<int>();
            Debug.Log("NO MARKS; adding 2, 3, 5, 7");
            tickTriggers.Add(2);
            tickTriggers.Add(3);
            tickTriggers.Add(5);
            tickTriggers.Add(7);
        }

        tickCounts = new List<int>();
        tickRemainders = new List<int>();

        foreach (int mark in tickTriggers)
        {
            tickCounts.Add(0);
            tickRemainders.Add(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += 1;
        for (int index = 0; index < tickCounts.Count; index ++)
        {
            if ((time + tickRemainders[index]) % (tickTriggers[index] * timeFactor) == 0)
            {
                tickCounts[index]++;
                tickRemainders[index] = 0;
                Debug.Log($"{index}");
            }
            else
            {
                tickRemainders[index]++;
            }
        }

        if (time >= cicleLength)
        {
            time = 0;
        }
    }

    public void RegisterTick(int newTrigger)
    {
        
    }
}
