// using System.Collections;
// using UnityEngine;
// using UnityEngine.AI;

// public class Employee : MonoBehaviour
// {
//     public int dirtyClothesHolding = 0;
//     public int maxDirtyClothes = 5;
//     public float interactionRange = 3.0f; // Make sure it matches NavMeshAgent stopping distance
//     public float walkSpeed = 2.5f;
//     private NavMeshAgent agent;
//     private bool isCarryingClothes = false;
//     public float detectionRadius = 5.0f;
//     public LayerMask employeeLayer;
//     private Vector3 lastPosition;
//     private float stuckTime = 0.0f;
//     private float maxStuckTime = 1.0f;

//     private Transform laundryBasket;
//     private WashingMachine targetWashingMachine;

//     public float checkInterval = 2.0f; // Time to wait before rechecking for an available washing machine
//     private Animator animator;

//     void Start()
//     {
//         agent = GetComponent<NavMeshAgent>();
//         agent.speed = walkSpeed;
//         agent.radius = 0.1f;  // Ensures radius matches your prefab setting
//         agent.stoppingDistance = interactionRange;
//         GoToLaundryBasket();
//          animator = GetComponent<Animator>();
//     }

//     void Update()
//     {
//         CheckMovement();

//         if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
//         {
//             if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
//             {
//                 InteractWithCurrentTarget();
//                 animator.SetBool("IsWalking", false);
//             }
//         } else {
//             animator.SetBool("IsWalking", true);
//         }
//     }

//     private void CheckMovement()
//     {
//         if (Vector3.Distance(transform.position, lastPosition) < 0.1f) // Detect if stuck
//         {
//             stuckTime += Time.deltaTime;
//             if (stuckTime > maxStuckTime)
//             {
//                 RecalculatePath();
//                 stuckTime = 0.0f;
//             }
//         }
//         else
//         {
//             stuckTime = 0.0f;
//         }

//         lastPosition = transform.position;
//     }

//     private void RecalculatePath()
//     {
//         agent.SetDestination(agent.destination); // Recalculate path
//     }

//     private void GoToLaundryBasket()
//     {
//         laundryBasket = GameObject.FindWithTag("LaundryBasket").transform;
//         agent.SetDestination(laundryBasket.position);
//     }

//     private void GoToWashingMachine()
//     {
//         targetWashingMachine = FindNearestAvailableWashingMachine();

//         if (targetWashingMachine != null)
//         {
//             agent.SetDestination(targetWashingMachine.transform.position);
//         }
//         else
//         {
//             // Start rechecking for available washing machines if none are found
//             StartCoroutine(RecheckForAvailableMachine());
//         }
//     }

//     private WashingMachine FindNearestAvailableWashingMachine()
//     {
//         WashingMachine[] allMachines = FindObjectsOfType<WashingMachine>();
//         WashingMachine nearestMachine = null;
//         float shortestDistance = Mathf.Infinity;

//         foreach (WashingMachine machine in allMachines)
//         {
//             if (machine.IsUnlocked && !machine.isWashing)
//             {
//                 float distanceToMachine = Vector3.Distance(transform.position, machine.transform.position);
//                 if (distanceToMachine < shortestDistance)
//                 {
//                     shortestDistance = distanceToMachine;
//                     nearestMachine = machine;
//                 }
//             }
//         }

//         return nearestMachine;
//     }

//     private void InteractWithCurrentTarget()
//     {
//         if (isCarryingClothes && targetWashingMachine != null)
//         {
//             // Interaction with washing machine
//             PutClothesInWashingMachine(targetWashingMachine);
//         }
//         else if (!isCarryingClothes && laundryBasket != null)
//         {
//             // Interaction with laundry basket
//             LaundryBasket basket = laundryBasket.GetComponent<LaundryBasket>();
//             if (basket != null)
//             {
//                 TakeDirtyClothes(basket);
//             }
//         }
//     }

//     private void TakeDirtyClothes(LaundryBasket basket)
//     {
//         int clothesToTake = Mathf.Min(maxDirtyClothes - dirtyClothesHolding, basket.TakeDirtyClothes(maxDirtyClothes - dirtyClothesHolding));
//         if (clothesToTake > 0)
//         {
//             dirtyClothesHolding += clothesToTake;
//             isCarryingClothes = true;
//             GoToWashingMachine();
//             Debug.Log("Employee took " + clothesToTake + " dirty clothes. Now holding: " + dirtyClothesHolding);
//         }
//     }

//     private void PutClothesInWashingMachine(WashingMachine machine)
//     {
//         if (dirtyClothesHolding > 0 && machine.AddDirtyClothes(dirtyClothesHolding))
//         {
//             dirtyClothesHolding = 0;
//             isCarryingClothes = false;
//             GoToLaundryBasket();
//         }
//     }

//     private IEnumerator RecheckForAvailableMachine()
//     {
//         Debug.Log("No available washing machines. Rechecking...");

//         // Wait for a few seconds and then recheck for an available machine
//         yield return new WaitForSeconds(checkInterval);

//         // After waiting, recheck for available washing machines
//         GoToWashingMachine();
//     }

//     public void UpgradeSpeed(float additionalSpeed)
//     {
//         walkSpeed += additionalSpeed;
//         agent.speed = walkSpeed;
//     }

//     public void UpgradeCapacity(int additionalCapacity)
//     {
//         maxDirtyClothes += additionalCapacity;
//     }
// }
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Employee : MonoBehaviour
{
    public int dirtyClothesHolding = 0;
    public int maxDirtyClothes = 5;
    public float interactionRange = 3.0f; 
    public float walkSpeed = 2.5f;
    private NavMeshAgent agent;
    private bool isCarryingClothes = false;
    public float detectionRadius = 5.0f;
    public LayerMask employeeLayer;
    private Vector3 lastPosition;
    private float stuckTime = 0.0f;
    private float maxStuckTime = 1.0f;
    
    private Transform laundryBasket;
    private WashingMachine targetWashingMachine;
    private Animator animator; // Animator reference

    public float checkInterval = 2.0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = walkSpeed;
        agent.radius = 0.1f; 
        agent.stoppingDistance = interactionRange;

        // Get the Animator component
        animator = GetComponent<Animator>();
        
        GoToLaundryBasket();
    }

void Update()
    {
        CheckMovement();

        // If the employee is moving, play the walk animation
        if (agent.velocity.sqrMagnitude > 0.1f)
        {
            PlayWalkAnimation();
        }
        else
        {
            // If not moving, play idle animation
            PlayIdleAnimation();
        }

        // Check for interaction when employee reaches destination
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                InteractWithCurrentTarget();
            }
        }
    }

    private void CheckMovement()
    {
        if (Vector3.Distance(transform.position, lastPosition) < 0.1f) 
        {
            stuckTime += Time.deltaTime;
            if (stuckTime > maxStuckTime)
            {
                RecalculatePath();
                stuckTime = 0.0f;
            }
        }
        else
        {
            stuckTime = 0.0f;
        }

        lastPosition = transform.position;
    }

    private void RecalculatePath()
    {
        agent.SetDestination(agent.destination);
    }

    private void GoToLaundryBasket()
    {
        laundryBasket = GameObject.FindWithTag("LaundryBasket").transform;
        agent.SetDestination(laundryBasket.position);
    }

    private void GoToWashingMachine()
    {
        targetWashingMachine = FindNearestAvailableWashingMachine();

        if (targetWashingMachine != null)
        {
            agent.SetDestination(targetWashingMachine.transform.position);
        }
        else
        {
            StartCoroutine(RecheckForAvailableMachine());
        }
    }

    private WashingMachine FindNearestAvailableWashingMachine()
    {
        WashingMachine[] allMachines = FindObjectsOfType<WashingMachine>();
        WashingMachine nearestMachine = null;
        float shortestDistance = Mathf.Infinity;

        foreach (WashingMachine machine in allMachines)
        {
            if (machine.IsUnlocked && !machine.isWashing)
            {
                float distanceToMachine = Vector3.Distance(transform.position, machine.transform.position);
                if (distanceToMachine < shortestDistance)
                {
                    shortestDistance = distanceToMachine;
                    nearestMachine = machine;
                }
            }
        }

        return nearestMachine;
    }

    private void InteractWithCurrentTarget()
    {
        if (isCarryingClothes && targetWashingMachine != null)
        {
            PutClothesInWashingMachine(targetWashingMachine);
        }
        else if (!isCarryingClothes && laundryBasket != null)
        {
            LaundryBasket basket = laundryBasket.GetComponent<LaundryBasket>();
            if (basket != null)
            {
                TakeDirtyClothes(basket);
            }
        }
    }

    private void TakeDirtyClothes(LaundryBasket basket)
    {
        int clothesToTake = Mathf.Min(maxDirtyClothes - dirtyClothesHolding, basket.TakeDirtyClothes(maxDirtyClothes - dirtyClothesHolding));
        if (clothesToTake > 0)
        {
            dirtyClothesHolding += clothesToTake;
            isCarryingClothes = true;
            GoToWashingMachine();
            Debug.Log("Employee took " + clothesToTake + " dirty clothes. Now holding: " + dirtyClothesHolding);
        }
    }

    private void PutClothesInWashingMachine(WashingMachine machine)
    {
        if (dirtyClothesHolding > 0 && machine.AddDirtyClothes(dirtyClothesHolding))
        {
            dirtyClothesHolding = 0;
            isCarryingClothes = false;
            GoToLaundryBasket();
        }
    }

    private IEnumerator RecheckForAvailableMachine()
    {
        Debug.Log("No available washing machines. Rechecking...");

        yield return new WaitForSeconds(checkInterval);

        GoToWashingMachine();
    }

    // Animation functions
    private void PlayWalkAnimation()
{
    if (animator != null && !animator.GetBool("isWalking"))
    {
        animator.SetBool("isWalking", true);
        animator.SetBool("isIdle", false);
    }
}

private void PlayIdleAnimation()
{
    if (animator != null && !animator.GetBool("isIdle"))
    {
        animator.SetBool("isWalking", false);
        animator.SetBool("isIdle", true);
    }
}


    public void UpgradeSpeed(float additionalSpeed)
    {
        walkSpeed += additionalSpeed;
        agent.speed = walkSpeed;
    }

    public void UpgradeCapacity(int additionalCapacity)
    {
        maxDirtyClothes += additionalCapacity;
    }
}
