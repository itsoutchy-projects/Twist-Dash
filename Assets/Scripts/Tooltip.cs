using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tooltip : MonoBehaviour
{
    [Tooltip("Shows if this Tooltip is touching this collider")]public BoxCollider2D For;

    public new SpriteRenderer renderer;
    public TextMeshPro text;

    // Update is called once per frame
    void Update()
    {
        transform.position = MousePos2D();

        if (Overlapping(transform, For.transform))
        {
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 255);
            text.color = new Color(text.color.r, text.color.g, text.color.b, 255);
        } else
        {
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 0);
            text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        }
    }

    public static Vector3 MousePos2D()
    {
        Vector3 temp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        temp = new Vector3(temp.x, temp.y);
        return temp;
    }

    /// <summary>
    /// Checks if <paramref name="first"/> and <paramref name="second"/> are overlapping
    /// </summary>
    /// <param name="first">One <see cref="Transform"/></param>
    /// <param name="second">Another <see cref="Transform"/></param>
    /// <returns>Whether <paramref name="first"/> and <paramref name="second"/> are overlapping</returns>
    public bool Overlapping(Transform first, Transform second)
    {
        Vector2 point1First = new Vector2(first.position.x - (first.localScale.x / 2), first.position.y + (first.localScale.y / 2));
        Vector2 point2First = new Vector2(first.position.x + (first.localScale.x / 2), first.position.y - (first.localScale.y / 2));

        Vector2 point1Second = new Vector2(second.position.x - (second.localScale.x / 2), second.position.y + (second.localScale.y / 2));
        Vector2 point2Second = new Vector2(second.position.x + (second.localScale.x / 2), second.position.y - (second.localScale.y / 2));

        Collider2D overlap1First = Physics2D.OverlapPoint(point1First);
        Collider2D overlap1Second = Physics2D.OverlapPoint(point1Second);
        bool overlapping1s = overlap1First == overlap1Second;

        Collider2D overlap2First = Physics2D.OverlapPoint(point2First);
        Collider2D overlap2Second = Physics2D.OverlapPoint(point2Second);
        bool overlapping2s = overlap2First == overlap2Second;

        return overlapping1s == true && overlapping2s == true;
    }

    /// <summary>
    /// Checks if <paramref name="first"/> and <paramref name="second"/> are overlapping
    /// </summary>
    /// <param name="first">One <see cref="BoxCollider2D"/></param>
    /// <param name="second">Another <see cref="BoxCollider2D"/></param>
    /// <returns>Whether <paramref name="first"/> and <paramref name="second"/> are overlapping</returns>
    public bool Overlapping(BoxCollider2D first, BoxCollider2D second)
    {
        Vector2 point1First = new Vector2(first.transform.position.x - (first.size.x / 2), first.transform.position.y + (first.size.y / 2));
        Vector2 point2First = new Vector2(first.transform.position.x + (first.size.x / 2), first.transform.position.y - (first.size.y / 2));

        Vector2 point1Second = new Vector2(second.transform.position.x - (second.size.x / 2), second.transform.position.y + (second.size.y / 2));
        Vector2 point2Second = new Vector2(second.transform.position.x + (second.size.x / 2), second.transform.position.y - (second.size.y / 2));

        Collider2D overlap1First = Physics2D.OverlapPoint(point1First);
        Collider2D overlap1Second = Physics2D.OverlapPoint(point1Second);
        bool overlapping1s = overlap1First == overlap1Second;

        Collider2D overlap2First = Physics2D.OverlapPoint(point2First);
        Collider2D overlap2Second = Physics2D.OverlapPoint(point2Second);
        bool overlapping2s = overlap2First == overlap2Second;

        return overlapping1s == false && overlapping2s == false;
    }
}
