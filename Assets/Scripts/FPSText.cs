using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class FPSText : MonoBehaviour
{
    private TextMeshPro tmp;
    private float fps = 30f;

    private void Awake()
    {
        tmp = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        fps = 1.0f / Time.unscaledDeltaTime;
        tmp.text = "FPS: " + math.round(fps);
    }
}
