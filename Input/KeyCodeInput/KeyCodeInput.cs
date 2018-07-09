using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class KeyCodeInput : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
    public void Update()
    {
        KeyInput();
    }
    public KeyEvent[] events;

    void KeyInput()
    {
        for (int i = 0; i < events.Length; i++)
        {
            if (Input.GetKeyDown(events[i].inputKey))
            {
                events[i].Down.Invoke();
            }
            else if (Input.GetKeyUp(events[i].inputKey))
            {
                events[i].Up.Invoke();
            }
            if (Input.GetKey(events[i].inputKey))
            {
                events[i].Press.Invoke();
            }
        }
    }
}

