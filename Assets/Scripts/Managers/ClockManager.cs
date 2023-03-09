using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClockManager : MonoBehaviour
{
    private Dictionary<int, List<IObserver>> _observers = new Dictionary<int, List<IObserver>>();
    private Dictionary<int, int> _clocks = new Dictionary<int, int>();
    public int timeFactor;


    public static ClockManager clockInstance { get; private set; }

    void Awake()
    {
        if (clockInstance != null && clockInstance != this)
        {
            Destroy(this);
        }
        else
        {
            clockInstance = this;
        }        

        // DontDestroyOnLoad(this.gameObject);
    }



    void Start()
    {
        if (timeFactor == 0)
        {
            timeFactor = 10;
        }
    }

    void FixedUpdate()
    {
        foreach (int mark in _observers.Keys)
        {
            _clocks[mark] += 1;
            if (_clocks[mark] == mark * timeFactor)
            {
                NotifyObservers(mark);
                _clocks[mark] = 0;
            }
        }
    }

    public void AddObserver(int mark, IObserver observer)
    {
        if (_observers.ContainsKey(mark))
        {
            _observers[mark].Add(observer);
        }
        else
        {
            _observers[mark] = new List<IObserver>() { observer };
            _clocks[mark] = 0;
        }
    }

    public void RemoveObserver(IObserver observer)
    {
        foreach (int mark in _observers.Keys)
        {
            if (_observers[mark].Contains(observer))
            {
                _observers[mark].Remove(observer);
                if (_observers[mark].Count == 0)
                {
                    _observers.Remove(mark);
                    _clocks.Remove(mark);
                }
                break;
            }
        }
    }

    protected void NotifyObservers(int mark)
    {
        foreach (IObserver observer in _observers[mark])
        {
            observer.OnClockTick();
        }
    }

}
