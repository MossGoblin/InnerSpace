using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserverTest : MonoBehaviour, IObserver
{
    [SerializeField]
    Clocker _clocker;

    [SerializeField]
    Logger _logger;

    [SerializeField]
    private int mark; // in 1/50 of a sec; 50 for 1 tick per second
    private bool subscribed_to_clocker;
    public int max_ticks;
    private int clocks_ticked;

    public void OnEnable()
    {
    }

    void Start()
    {


        subscribed_to_clocker = false;
        clocks_ticked = 0;
        if (mark == 5)
        {
            mark = 5;
        }
        if (max_ticks == 0)
        {
            max_ticks = 10;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if (!subscribed_to_clocker)
            {
                _clocker.AddObserver(mark, this);
                _logger.LogDebug($"Subscribed for Clocker at {mark}", true);
                subscribed_to_clocker = true;
            }
            else
            {
                _clocker.RemoveObserver(this);
                _logger.LogDebug($"Unsubscribed from Clocker at input", true);
                subscribed_to_clocker = false;
            }
        }

        if (clocks_ticked == max_ticks)
        {
            clocks_ticked = 0;
            _clocker.RemoveObserver(this);
            _logger.LogDebug($"Unsubscribed from Clocker after {max_ticks} ticks", true);
            subscribed_to_clocker = false;
        }
    }

    public void OnClockTick()
    {
        clocks_ticked++;
        // _logger.LogDebug($"Clock received {Time.time}", true);
    }
}