using UnityEngine;
using System.Collections.Generic;

public class CustomerManager : MonoBehaviour
{
    public GameObject customerPrefab;
    public Transform[] spawnPoints;
    public Transform[] laundryQueuePositions;
    public Transform[] cleanQueuePositions;
    public Transform exitPoint; // New exit point
    public Transform waitingArea; // New waiting area
    public float spawnInterval = 5.0f;
    private float timer = 0.0f;
    public int maxLaundryQueueLength = 10;
    public int maxCleanQueueLength = 10;

    private static Queue<Vector3> availableLaundryQueuePositions = new Queue<Vector3>();
    private static Queue<Vector3> availableCleanQueuePositions = new Queue<Vector3>();
    private List<Customer> disabledCustomers = new List<Customer>();

    private void Start()
    {
        foreach (Transform pos in laundryQueuePositions)
        {
            availableLaundryQueuePositions.Enqueue(pos.position);
        }

        foreach (Transform pos in cleanQueuePositions)
        {
            availableCleanQueuePositions.Enqueue(pos.position);
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval && Customer.GetLaundryQueueLength() < maxLaundryQueueLength && availableLaundryQueuePositions.Count > 0)
        {
            SpawnCustomer();
            timer = 0.0f;
        }

        if (disabledCustomers.Count > 0 && !IsCleanQueueFull())
        {
            EnableNextDisabledCustomer();
        }
    }

    private void SpawnCustomer()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        GameObject customer = Instantiate(customerPrefab, spawnPoints[randomIndex].position, Quaternion.identity);
        customer.GetComponent<Customer>().exitPoint = exitPoint; // Set the exit point
        customer.GetComponent<Customer>().waitingArea = waitingArea; // Set the waiting area
        customer.SetActive(true);
        Debug.Log("Spawned customer at: " + spawnPoints[randomIndex].position);
    }

    public static Vector3 GetNextLaundryQueuePosition()
    {
        if (availableLaundryQueuePositions.Count > 0)
        {
            Vector3 position = availableLaundryQueuePositions.Dequeue();
            Debug.Log("Assigned laundry queue position: " + position);
            return position;
        }
        return Vector3.zero;
    }

    public static void ReleaseLaundryQueuePosition(Vector3 position)
    {
        availableLaundryQueuePositions.Enqueue(position);
        Debug.Log("Released laundry queue position: " + position);
    }

    public static Vector3 GetNextCleanQueuePosition()
    {
        if (availableCleanQueuePositions.Count > 0)
        {
            Vector3 position = availableCleanQueuePositions.Dequeue();
            Debug.Log("Assigned clean queue position: " + position);
            return position;
        }
        return Vector3.zero;
    }

    public static void ReleaseCleanQueuePosition(Vector3 position)
    {
        availableCleanQueuePositions.Enqueue(position);
        Debug.Log("Released clean queue position: " + position);
    }

    public static bool IsCleanQueueFull()
    {
        return availableCleanQueuePositions.Count == 0;
    }

    private void EnableNextDisabledCustomer()
    {
        Customer customer = disabledCustomers[0];
        disabledCustomers.RemoveAt(0);
        customer.EnableCustomer();
    }
}
