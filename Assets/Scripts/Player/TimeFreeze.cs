using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeFreeze : MonoBehaviour
{
    public CharacterController Controller;

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = 0;
        
        float hMov = Input.GetAxis("Mouse X");
        float vMov = Input.GetAxis("Mouse Y");

        if (Input.anyKey || hMov != 0 || vMov != 0)
            Time.timeScale = 1;
    }
}
