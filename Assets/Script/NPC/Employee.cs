// using UnityEngine;
// using UnityEngine.AI;

// public class Employee : MonoBehaviour
// {
//     public int dirtyClothesHolding = 0;
//     public int maxDirtyClothes = 5;
//     public float interactionRange = 0.5f;
//     public float walkSpeed = 2.5f;
//     private NavMeshAgent agent;
//     private bool isCarryingClothes = false;
//     public float detectionRadius = 5.0f; // Radius untuk mendeteksi karyawan lain
//     public LayerMask employeeLayer; // Layer mask untuk mendeteksi karyawan lain
// private Vector3 lastPosition;
// private float stuckTime = 0.0f;
// private float maxStuckTime = 1.0f;

//     void Start()
//     {
//         agent = GetComponent<NavMeshAgent>();
//         agent.speed = walkSpeed;
//         GoToLaundryBasket();
//     }

//     void Update()
//     {

//     if (Vector3.Distance(transform.position, lastPosition) < 0.1f) // If the agent hasn't moved much
//     {
//         stuckTime += Time.deltaTime;
//         if (stuckTime > maxStuckTime)
//         {
//             agent.SetDestination(agent.destination); // Force path recalculation
//             stuckTime = 0.0f; // Reset the stuck timer
//         }
//     }
//     else
//     {
//         stuckTime = 0.0f; // Reset if the agent is moving
//     }

//     lastPosition = transform.position;

//         Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactionRange);
//         foreach (var hitCollider in hitColliders)
//         {
//             if (hitCollider.CompareTag("LaundryBasket"))
//             {
//                 LaundryBasket basket = hitCollider.GetComponent<LaundryBasket>();
//                 if (basket != null && !isCarryingClothes)
//                 {
//                     TakeDirtyClothes(basket);
//                 }
//             }
//             else if (hitCollider.CompareTag("WashingMachine"))
//             {
//                 WashingMachine machine = hitCollider.GetComponent<WashingMachine>();
//                 if (machine != null && isCarryingClothes)
//                 {
//                     PutClothesInWashingMachine(machine);
//                 }
//             }
            
//         }

//         // Check for obstacles (other employees) in the path
//         CheckForObstacles();
//     }

//     private void CheckForObstacles()
//     {
//         // Cek apakah ada karyawan lain dalam radius tertentu
//         Collider[] employeesNearby = Physics.OverlapSphere(transform.position, detectionRadius, employeeLayer);
        
//         foreach (var employee in employeesNearby)
//         {
//             if (employee.gameObject != this.gameObject)
//             {
//                 // Jika ada karyawan lain yang terdeteksi, buat ulang jalur (re-route)
//                 agent.SetDestination(agent.destination); // Recalculate path
//                 break;
//             }
//         }
//     }

//     private void GoToLaundryBasket()
//     {
//         Transform laundryBasket = GameObject.FindWithTag("LaundryBasket").transform;
//         agent.SetDestination(laundryBasket.position);
//     }

//     private void GoToWashingMachine()
//     {
//         WashingMachine nearestMachine = FindNearestAvailableWashingMachine();
//         if (nearestMachine != null)
//         {
//             agent.SetDestination(nearestMachine.transform.position);
//         }
//     }

//     private WashingMachine FindNearestAvailableWashingMachine()
//     {
//         WashingMachine[] allMachines = FindObjectsOfType<WashingMachine>();
//         WashingMachine nearestMachine = null;
//         float shortestDistance = Mathf.Infinity;

//         foreach (WashingMachine machine in allMachines)
//         {
//             if (!machine.isWashing)
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

//     private void TakeDirtyClothes(LaundryBasket basket)
// {
//     int clothesToTake = Mathf.Min(maxDirtyClothes - dirtyClothesHolding, basket.TakeDirtyClothes(maxDirtyClothes - dirtyClothesHolding));
//     if (clothesToTake > 0)
//     {
//         dirtyClothesHolding += clothesToTake;
//         isCarryingClothes = true;
//         GoToWashingMachine();
//         Debug.Log("Karyawan mengambil " + clothesToTake + " baju kotor. Sekarang membawa: " + dirtyClothesHolding);
//     }
// }

//     private void PutClothesInWashingMachine(WashingMachine machine)
//     {
//         if (dirtyClothesHolding > 0 && machine.AddDirtyClothes(dirtyClothesHolding))
//         {
//             dirtyClothesHolding = 0;
//             isCarryingClothes = false;
//             GoToLaundryBasket();
//         }
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
using UnityEngine;
using UnityEngine.AI;

public class Employee : MonoBehaviour
{
    public int dirtyClothesHolding = 0;
    public int maxDirtyClothes = 5;
    public float interactionRange = 3.0f; // Make sure it matches NavMeshAgent stopping distance
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

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = walkSpeed;
        agent.radius = 0.1f;  // Ensures radius matches your prefab setting
        agent.stoppingDistance = interactionRange;
        GoToLaundryBasket();
    }

    void Update()
    {
        CheckMovement();

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
        if (Vector3.Distance(transform.position, lastPosition) < 0.1f) // Detect if stuck
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
        agent.SetDestination(agent.destination); // Recalculate path
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
    }

    private WashingMachine FindNearestAvailableWashingMachine()
    {
        WashingMachine[] allMachines = FindObjectsOfType<WashingMachine>();
        WashingMachine nearestMachine = null;
        float shortestDistance = Mathf.Infinity;

        foreach (WashingMachine machine in allMachines)
        {
            if (!machine.isWashing)
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
            // Interaction with washing machine
            PutClothesInWashingMachine(targetWashingMachine);
        }
        else if (!isCarryingClothes && laundryBasket != null)
        {
            // Interaction with laundry basket
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
            Debug.Log("Karyawan mengambil " + clothesToTake + " baju kotor. Sekarang membawa: " + dirtyClothesHolding);
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


