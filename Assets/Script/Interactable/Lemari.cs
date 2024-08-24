using System.Collections;
using System.Collections.Generic;
using Script.Interface;
using UnityEngine;

public class Lemari : MonoBehaviour, Iinteractable
{
    public bool _canInteract = false;
    public Vector3 targetRotation1;
    public Vector3 targetRotation2;
    public BoxCollider boxCollider;

    // References to the two door GameObjects
    public GameObject door1;
    public GameObject door2;
    public BoxCollider _bajuItem;

    // The duration over which to rotate
    public float duration = 2.0f;

    // Coroutine to handle the rotation
    private IEnumerator RotateToTarget(Vector3 targetEulerAngles1, Vector3 targetEulerAngles2, float duration)
    {
        Quaternion startRotation1 = door1.transform.rotation;
        Quaternion endRotation1 = Quaternion.Euler(targetEulerAngles1);
        Quaternion startRotation2 = door2.transform.rotation;
        Quaternion endRotation2 = Quaternion.Euler(targetEulerAngles2);
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Calculate the fraction of time that has passed
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);

            // Perform the linear interpolation for both doors
            door1.transform.rotation = Quaternion.Lerp(startRotation1, endRotation1, t);
            door2.transform.rotation = Quaternion.Lerp(startRotation2, endRotation2, t);

            // Yield control back to Unity and wait for the next frame
            yield return null;
        }

        // Ensure the final rotation is set to the target rotation for both doors
        door1.transform.rotation = endRotation1;
        door2.transform.rotation = endRotation2;
    }

    public void Interact()
    {
        if (!_canInteract)
            return;

        boxCollider.enabled = false;
        _canInteract = false;
        StartCoroutine(RotateToTarget(targetRotation1, targetRotation2, duration));
        _bajuItem.enabled = true;
    }
}
