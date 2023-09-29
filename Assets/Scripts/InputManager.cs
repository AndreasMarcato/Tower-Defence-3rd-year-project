using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    public event EventHandler OnScreenTap;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            OnScreenTap?.Invoke(this, EventArgs.Empty);
    }

}
