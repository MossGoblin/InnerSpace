using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clocker : MonoBehaviour
{
    [SerializeField]
    private Dictionary<int, List<string>> subscribers;
    [SerializeField]
    private Dictionary<int, List<int>> subscriptions;
    [SerializeField]
    [Range(1, 100)]
    private int timeFactor;


    void Start()
    {
        subscribers = new Dictionary<int, List<string>>();
        subscriptions= new Dictionary<int, List<int>>();
        timeFactor = 10;


        // debug values
        RegisterSubscriber("Test Seven", 7);
        RegisterSubscriber("Test Seven Two", 7);
        UnregisterSubscriber("Test Seven");
        RegisterSubscriber("Test Five", 5);
        //Debug.Log(this.RegisterListener("Test Seven Two", 7));
        //Debug.Log(this.RegisterListener("Test Thirteen", 13));
    }

    // Update is called once per frame
    void Update()
    {
        if (subscribers.Count > 0)
        {
            foreach (int step in subscriptions.Keys)
            {
                subscriptions[step][1] += 1; // increase timer
                if (subscriptions[step][1] > (step * timeFactor)) // if the step is reached
                {
                    // increase counter and rollover timer
                    subscriptions[step][0] += 1;
                    subscriptions[step][1] = 0;
                    Debug.Log($"Subscription {step} triggered");
                }
            }
        }
    }

    public void RegisterSubscriber(string subscriberName, int subscriberValue)
    {
        if (!subscribers.ContainsKey(subscriberValue)) // no such value
        {
            // register subscriber
            subscribers[subscriberValue] = new List<string> { subscriberName };

            // register subscription
            subscriptions.Add(subscriberValue, new List<int> { 0, 0 });
        }
        else /// already a subscription with that value
        {
            if (!subscribers[subscriberValue].Contains(subscriberName))
            {
                subscribers[subscriberValue].Add(subscriberName);
            }
        }
        Debug.Log($"Listener {subscriberName} unregistered");
    }

    public void UnregisterSubscriber(string subscriberName)
    {
        // removelistener from the dictionary
        foreach (int subsctiptionValue in subscribers.Keys)
        {
            if (subscribers[subsctiptionValue].Contains(subscriberName))
            {
                subscribers[subsctiptionValue].Remove(subscriberName);
                // if there is no other listener with the same clock, remove the clock from the clocker
                if (subscribers[subsctiptionValue].Count == 0)
                {
                    subscribers.Remove(subsctiptionValue);
                    subscriptions.Remove(subsctiptionValue);
                }
            }
        }
        Debug.Log($"Listener {subscriberName} unregistered");
    }
}
