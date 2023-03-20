using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public enum KeyEvent
    {
        GetButton,
        GetButtonDown,
        GetButtonUp,
        GetMouseButton,
        GetMouseButtonDown,
        GetMouseButtonUp
    }

    public struct KeyAction
    {
        public KeyAction(KeyEvent keyEvent, KeyCode keyCode)
        {
            this.keyEvent = keyEvent;
            this.keyCode = keyCode;
        }

        public KeyEvent keyEvent {get; set;}
        public KeyCode keyCode {get; set;}

        public override string ToString() => $"{keyEvent.ToString()} : {keyCode.ToString()}";
    }

    private Dictionary<KeyCode, List<IObserver>> _observers;


    public static InputManager inputManagerInstance { get; private set; }

    void Awake()
    {
        if (inputManagerInstance != null && inputManagerInstance != this)
        {
            Destroy(this);
        }
        else
        {
            inputManagerInstance = this;
        }        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // HERE
        // Check inputs and notify observers

    }

    public void AddObserver(KeyCode mark, IObserver observer)
    {
        // TODO REDO to include KeyEvent
        if (_observers.ContainsKey(mark))
        {
            _observers[mark].Add(observer);
        }
        else
        {
            _observers[mark] = new List<IObserver>() { observer };
        }
    }

    public void RemoveObserver(IObserver observer)
    {
        // TODO REDO to include KeyEvent
        foreach (KeyCode mark in _observers.Keys)
        {
            if (_observers[mark].Contains(observer))
            {
                _observers[mark].Remove(observer);
                if (_observers[mark].Count == 0)
                {
                    _observers.Remove(mark);
                }
                break;
            }
        }
    }


}
