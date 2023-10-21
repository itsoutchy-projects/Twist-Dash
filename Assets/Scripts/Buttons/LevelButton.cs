using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class LevelButton : MonoBehaviour
{
    public string level;

    private void OnMouseDown()
    {
        SceneManager.LoadScene(level);
    }
}
