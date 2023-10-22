using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public Animator animator;
    public Transform camPos;
    public Transform moveTo;
    public float moveDuration = 0.4f;

    private void OnMouseEnter()
    {
        if (animator != null)
        {
            animator.Play("hover");
        }
    }
    private void OnMouseExit()
    {
        if (animator != null)
        {
            animator.Play("hover reverse");
        }
    }

    private void OnMouseDown()
    {
        //camPos.position = levelSelect.position;
        StartCoroutine(Tween(camPos.gameObject, moveTo.position));
    }

    /// <summary>
    /// Tween this <see cref="GameObject"/> to <paramref name="targetPosition"/>
    /// </summary>
    /// <param name="targetPosition">The target position of the tween</param>
    /// <returns></returns>
    IEnumerator Tween(Vector3 targetPosition)
    {
        //Obtain the previous position (original position) of the gameobject this script is attached to
        Vector3 previousPosition = gameObject.transform.position;
        //Create a time variable
        float time = 0.0f;
        do
        {
            //Add the deltaTime to the time variable
            time += Time.deltaTime;
            //Lerp the gameobject's position that this script is attached to. Lerp takes in the original position, target position and the time to execute it in
            gameObject.transform.position = Vector3.Lerp(previousPosition, targetPosition, time / moveDuration);
            yield return 0;
            //Do the Lerp function while to time is less than the move duration.
        } while (time < moveDuration);
    }

    /// <summary>
    /// Tween <paramref name="toTween"/> to <paramref name="targetPosition"/>
    /// </summary>
    /// <param name="toTween">The <see cref="GameObject"/> to tween</param>
    /// <param name="targetPosition">The target position of the tween</param>
    /// <returns></returns>
    IEnumerator Tween(GameObject toTween, Vector3 targetPosition)
    {
        //Obtain the previous position (original position) of the gameobject this script is attached to
        Vector3 previousPosition = toTween.transform.position;
        //Create a time variable
        float time = 0.0f;
        do
        {
            //Add the deltaTime to the time variable
            time += Time.deltaTime;
            //Lerp the gameobject's position that this script is attached to. Lerp takes in the original position, target position and the time to execute it in
            toTween.transform.position = Vector3.Lerp(previousPosition, targetPosition, time / moveDuration);
            yield return 0;
            //Do the Lerp function while to time is less than the move duration.
        } while (time < moveDuration);
    }
}
