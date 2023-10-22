using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkButton : MonoBehaviour
{
    public string URL;

    private void OnMouseDown()
    {
        Application.OpenURL(URL);
    }
}
