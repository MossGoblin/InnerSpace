using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clocker : MonoBehaviour
{
    [SerializeField]
    private Dictionary<string, int> listenerRecorder;
    [SerializeField]
    private List<int> listenerList; // clock stamps that need to be met
    [SerializeField]
    private List<int> clockList; // accumulated clocks
    [SerializeField]
    private List<int> timersList; // actual timers
    [SerializeField]
    [Range(1, 100)]
    private int timeFactor;
    // Start is called before the first frame update
    void Start()
    {
        listenerRecorder = new Dictionary<string, int>();
        listenerList = new List<int>();
        clockList = new List<int>();
        timersList = new List<int>();
        timeFactor = 10;


        // debug values
        Debug.Log(this.RegisterListener("Test Seven", 7));
        Debug.Log(this.RegisterListener("Test Seven Two", 7));
        Debug.Log(this.RegisterListener("Test Thirteen", 13));
    }

    // Update is called once per frame
    void Update()
    {
        if (listenerList.Count > 0)
        {
            for (int index = 0; index < listenerList.Count; index++)
            {
                timersList[index] += 1;
                if (timersList[index] % (listenerList[index] * timeFactor) == 0)
                {
                    clockList[index] += 1;
                    // Debug.Log(timersList[index]);
                    timersList[index] = 0;
                }
            }
        }
    }

    public string RegisterListener(string listenerName, int listenerClock)
    {
        if (listenerRecorder.ContainsKey(listenerName))
        {
            throw new ArgumentException($"Listener {listenerName} is already registered");
        }

        listenerRecorder.Add(listenerName, listenerClock);
        if (listenerList.Contains(listenerClock))
        {
            return "Listener Clock already in list";
        }
        else
        {
            listenerList.Add(listenerClock);
            clockList.Add(0);
            timersList.Add(0);
            return $"Listener Clock registered: {listenerName} -- {listenerClock}";
        }
    }
}
