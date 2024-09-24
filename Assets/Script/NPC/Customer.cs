using UnityEngine;
using System.Collections.Generic;

public class Customer : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    public Transform cleanClothesArea;
    public Transform exitPoint; // New exit point
    public Transform waitingArea; // New waiting area
    private int payment; // Random payment value

    private LaundryBasket laundryBasket;
    private static Queue<Customer> laundryQueue = new Queue<Customer>();
    private static Queue<Customer> cleanClothesQueue = new Queue<Customer>();
    private Vector3 laundryQueuePosition;
    private Vector3 cleanQueuePosition;
    private bool hasDroppedClothes = false;
    private bool isInCleanQueue = false;
    private bool isDisabled = false;

    private enum State { MovingToLaundryBasket, DroppingClothes, MovingToCleanQueue, WaitingForCleanClothes, TakingCleanClothes, Leaving, MovingToWaitingArea }
    private State currentState = State.MovingToLaundryBasket;

    private void Start()
    {
        laundryBasket = FindObjectOfType<LaundryBasket>();
        laundryQueue.Enqueue(this);
        laundryQueuePosition = CustomerManager.GetNextLaundryQueuePosition();

        // Set a random payment between 25 and 250
        payment = Random.Range(25, 250);
        Debug.Log("Customer will pay: " + payment);
    }

    private void Update()
    {
        if (isDisabled) return;

        switch (currentState)
        {
            case State.MovingToLaundryBasket:
                MoveToLaundryBasket();
                break;
            case State.DroppingClothes:
                DropDirtyClothes();
                break;
            case State.MovingToCleanQueue:
                MoveToCleanQueue();
                break;
            case State.WaitingForCleanClothes:
                WaitInCleanQueue();
                break;
            case State.TakingCleanClothes:
                MoveToCleanClothesArea();
                break;
            case State.Leaving:
                MoveToExitPoint();
                break;
            case State.MovingToWaitingArea:
                MoveToWaitingArea();
                break;
        }
    }

    private void MoveToLaundryBasket()
    {
        if (laundryBasket != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, laundryQueuePosition, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, laundryQueuePosition) < 0.1f && laundryQueue.Peek() == this)
            {
                currentState = State.DroppingClothes;
            }
        }
    }

    private void DropDirtyClothes()
    {
        if (laundryBasket.AddDirtyClothes())
        {
            hasDroppedClothes = true;
            Debug.Log("Dropped dirty clothes in the basket.");
            laundryQueue.Dequeue();
            CustomerManager.ReleaseLaundryQueuePosition(laundryQueuePosition);
            currentState = State.MovingToCleanQueue;
        }
        else
        {
            Debug.Log("Basket is full, cannot drop dirty clothes.");
        }
    }

    private void MoveToCleanQueue()
    {
        if (!isInCleanQueue)
        {
            if (CustomerManager.IsCleanQueueFull())
            {
                Debug.Log("Clean queue is full, moving to waiting area.");
                currentState = State.MovingToWaitingArea;
                return;
            }
            cleanClothesQueue.Enqueue(this);
            isInCleanQueue = true;
            cleanQueuePosition = CustomerManager.GetNextCleanQueuePosition();
            Debug.Log("Assigned clean queue position: " + cleanQueuePosition);
        }
        transform.position = Vector3.MoveTowards(transform.position, cleanQueuePosition, moveSpeed * Time.deltaTime);
        Debug.Log("Moving to clean queue position: " + cleanQueuePosition);
        if (Vector3.Distance(transform.position, cleanQueuePosition) < 0.1f)
        {
            currentState = State.WaitingForCleanClothes;
        }
    }

    private void WaitInCleanQueue()
    {
        if (cleanClothesQueue.Peek() == this)
        {
            currentState = State.TakingCleanClothes;
        }
    }

    private void MoveToCleanClothesArea()
    {
        CleanClothesArea area = cleanClothesArea.GetComponent<CleanClothesArea>();
        if (area != null)
        {
            Debug.Log("Attempting to take clean clothes. Current clean clothes count: " + area.cleanClothesCount);
            if (area.TakeCleanClothes())
            {
                // Customer pays the random amount between 25 and 250
                MoneyManager.instance.AddCurrency(payment);
                Debug.Log("Took clean clothes and gave payment of: " + payment);
                cleanClothesQueue.Dequeue();
                CustomerManager.ReleaseCleanQueuePosition(cleanQueuePosition);
                currentState = State.Leaving;

                // Move the next customer in the clean clothes queue
                if (cleanClothesQueue.Count > 0)
                {
                    cleanClothesQueue.Peek().currentState = State.TakingCleanClothes;
                }
            }
            else
            {
                Debug.Log("No clean clothes available to take.");
                currentState = State.WaitingForCleanClothes;
            }
        }
        else
        {
            Debug.LogError("CleanClothesArea component not found on cleanClothesArea object.");
        }
    }

    private void MoveToExitPoint()
    {
        if (exitPoint != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, exitPoint.position, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, exitPoint.position) < 0.1f)
            {
                Destroy(gameObject); // Customer leaves and is destroyed
            }
        }
        else
        {
            Debug.LogError("Exit point is not assigned.");
            Destroy(gameObject);
        }
    }

    private void MoveToWaitingArea()
    {
        if (waitingArea != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, waitingArea.position, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, waitingArea.position) < 0.1f)
            {
                Debug.Log("Reached waiting area. Disabling customer.");
                isDisabled = true;
                gameObject.SetActive(false);
            }
        }
        else
        {
            Debug.LogError("Waiting area is not assigned.");
            isDisabled = true;
            gameObject.SetActive(false);
        }
    }

    public static int GetLaundryQueueLength()
    {
        return laundryQueue.Count;
    }

    public static int GetCleanQueueLength()
    {
        return cleanClothesQueue.Count;
    }

    public void EnableCustomer()
    {
        isDisabled = false;
        gameObject.SetActive(true);
        currentState = State.MovingToCleanQueue;
    }
}
