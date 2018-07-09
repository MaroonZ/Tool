using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
[Serializable]
public class KeyEvent{

    public KeyCode inputKey;

    public UnityEvent Down;
    public UnityEvent Up;
    public UnityEvent Press;
}
