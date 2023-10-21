using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elapsed : MonoBehaviour
{
    public static DateTime openedDateTime = DateTime.Now;
    public static TimeSpan elapsed;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        elapsed = DateTime.Now - openedDateTime;
    }
}
