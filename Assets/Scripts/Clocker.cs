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
    private List<int> remainderList; // remainders
    [SerializeField]
    private int timeFactor;
    // Start is called before the first frame update
    void Start()
    {
        listenerRecorder = new Dictionary<string, int>();
        listenerList = new List<int>();
        clockList = new List<int>();
        timersList = new List<int>();
        remainderList = new List<int>();
        timeFactor = 10;


        // debug
        listenerRecorder.Add("five", 5);
        listenerList.Add(5);
        clockList.Add(0);
        timersList.Add(0);
        remainderList.Add(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (listenerRecorder.Count > 0)
        {
            for (int index = 0; index < listenerRecorder.Count; index++)
            {
                timersList[index] += 1;
                // if (timersList[index] + remainderList[index] % (listenerList[index] * timeFactor) == 0)
                if (timersList[index] % (listenerList[index] * timeFactor) == 0)
                {
                    clockList[index] += 1;
                    // remainderList[index] = 0;
                    Debug.Log(timersList[index]);
                }
                //else
                //{
                //    remainderList[index]++;
                //}

            }
        }
    }
}
