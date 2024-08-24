using System.Collections;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Transform hinge; // Posisi engsel pintu
    public float openAngle = 90f; // Sudut pintu terbuka
    public float closeAngle = 0f; // Sudut pintu tertutup
    public float openSpeed = 2f; // Kecepatan buka pintu
    public float closeSpeed = 2f; // Kecepatan tutup pintu
    public float delayBeforeClose = 1f; // Delay sebelum pintu tertutup otomatis setelah buka
    public Collider doorCollider; // Collider pintu

    private bool isOpen = false; // Status pintu terbuka/tidak

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OpenDoor();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CloseDoorWithDelay();
        }
    }

    private void OpenDoor()
    {
        StopCoroutine("MoveDoor");
        StartCoroutine("MoveDoor", openAngle);
    }

    private void CloseDoor()
    {
        StopCoroutine("MoveDoor");
        StartCoroutine("MoveDoor", closeAngle);
    }

    private void CloseDoorWithDelay()
    {
        Invoke("CloseDoor", delayBeforeClose);
    }

    private IEnumerator MoveDoor(float targetAngle)
    {
        float currentAngle = hinge.localRotation.eulerAngles.y;
        while (Mathf.Abs(currentAngle - targetAngle) > 1f)
        {
            currentAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, (targetAngle == openAngle) ? openSpeed : closeSpeed * Time.deltaTime);
            hinge.localRotation = Quaternion.Euler(0f, currentAngle, 0f);
            yield return null;
        }
        isOpen = (targetAngle == openAngle);
    }
}
