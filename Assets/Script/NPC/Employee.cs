// using UnityEngine;
// using UnityEngine.AI;

// public class Employee : MonoBehaviour
// {
//     public int dirtyClothesHolding = 0;
//     public int maxDirtyClothes = 5;
//     public float interactionRange = 2.0f; // Jarak untuk interaksi otomatis
//     public float walkSpeed = 0;
//     private NavMeshAgent agent;
//     private bool isCarryingClothes = false;

//     void Start()
//     {
//         agent = GetComponent<NavMeshAgent>();
//         agent.speed = walkSpeed;
//         Debug.Log(agent.destination);
//         GoToLaundryBasket(); // Awalnya pergi ke keranjang baju
//     }

//     void Update()
//     {
//         // Debug.Log("pathfinding : "+ agent.pathPending+"--jarak : "+agent.remainingDistance+"--stop : "+agent.stoppingDistance);
//         // Periksa apakah karyawan sudah sampai tujuan
//         if (agent.remainingDistance <= agent.stoppingDistance)
//         {
//             Debug.Log("onposition");
//             if (!isCarryingClothes)
//             {
//                 Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactionRange);
//                 // Gizmos.DrawSphere(transform.position, interactionRange);
//                 foreach (var hitCollider in hitColliders)
//                 { Debug.Log(hitCollider.name);
//                     if (hitCollider.CompareTag("LaundryBasket"))
//                     {
//                         LaundryBasket basket = hitCollider.GetComponent<LaundryBasket>();
//                         if (basket != null)
//                         {
//                             TakeDirtyClothes(basket);
//                             return; // Hentikan loop setelah mengambil baju
//                         }
//                     }
//                 }
//             }
//             else
//             {
//                 Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactionRange);
//                 foreach (var hitCollider in hitColliders)
//                 {
//                     if (hitCollider.CompareTag("WashingMachine"))
//                     {
//                         WashingMachine machine = hitCollider.GetComponent<WashingMachine>();
//                         if (machine != null)
//                         {
//                             PutClothesInWashingMachine(machine);
//                             return; // Hentikan loop setelah meletakkan baju
//                         }
//                     }
//                 }
//             }
//         }
        
//     }

//     private void GoToLaundryBasket()
//     {
//         Transform laundryBasket = GameObject.FindWithTag("LaundryBasket").transform;
//         if (laundryBasket != null)
//         {
//             Debug.Log("found laundry basket");
            
//             agent.destination = laundryBasket.position;
//             Debug.Log(agent.destination + " " + laundryBasket.name);
//         }
//     }

//     private void GoToWashingMachine()
// {
//     WashingMachine nearestMachine = FindNearestAvailableWashingMachine();
//     if (nearestMachine != null)
//     {
//         agent.SetDestination(nearestMachine.transform.position);
//     }
//     else
//     {
//         Debug.Log("No available washing machine found!");
//     }
// }

// private WashingMachine FindNearestAvailableWashingMachine()
// {
//     WashingMachine[] allMachines = FindObjectsOfType<WashingMachine>();
//     WashingMachine nearestMachine = null;
//     float shortestDistance = Mathf.Infinity;

//     foreach (WashingMachine machine in allMachines)
//     {
//         // Check if the machine is available (e.g., not full)
//         if (!machine.isWashing)
//         {
//             float distanceToMachine = Vector3.Distance(transform.position, machine.transform.position);
//             if (distanceToMachine < shortestDistance)
//             {
//                 shortestDistance = distanceToMachine;
//                 nearestMachine = machine;
//             }
//         }
//     }

//     return nearestMachine;
// }

//     private void TakeDirtyClothes(LaundryBasket basket)
//     {
//         int clothesToTake = Mathf.Min(maxDirtyClothes - dirtyClothesHolding, basket.GetDirtyClothes());
//         if (clothesToTake > 0)
//         {
//             dirtyClothesHolding += clothesToTake;
//             isCarryingClothes = true;
//             Debug.Log("Karyawan mengambil " + clothesToTake + " baju kotor. Sekarang membawa: " + dirtyClothesHolding);
//             GoToWashingMachine(); // Setelah mengambil baju, pergi ke mesin cuci
//         }
//     }

//     private void PutClothesInWashingMachine(WashingMachine machine)
//     {
//         if (dirtyClothesHolding > 0 && machine.AddDirtyClothes(dirtyClothesHolding))
//         {
//             dirtyClothesHolding = 0;
//             isCarryingClothes = false;
//             Debug.Log("Karyawan memasukkan semua baju kotor ke dalam mesin cuci.");
//             GoToLaundryBasket(); // Setelah meletakkan baju, kembali ke keranjang baju
//         }
//     }

//     public void UpgradeSpeed(float additionalSpeed)
//     {
//         walkSpeed += additionalSpeed;
//         agent.speed = walkSpeed; // Perbarui kecepatan pada NavMeshAgent
//         Debug.Log("Employee speed upgraded! New speed: " + walkSpeed);
//     }

//     public void UpgradeCapacity(int additionalCapacity)
//     {
//         maxDirtyClothes += additionalCapacity;
//         Debug.Log("Employee capacity upgraded! New capacity: " + maxDirtyClothes);
//     }
// }
using UnityEngine;
using UnityEngine.AI;

public class Employee : MonoBehaviour
{
    public int dirtyClothesHolding = 0;
    public int maxDirtyClothes = 5;
    public float interactionRange = 0.5f;
    public float walkSpeed = 2.5f;
    private NavMeshAgent agent;
    private bool isCarryingClothes = false;
    public float detectionRadius = 5.0f; // Radius untuk mendeteksi karyawan lain
    public LayerMask employeeLayer; // Layer mask untuk mendeteksi karyawan lain

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = walkSpeed;
        GoToLaundryBasket();
    }

    void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactionRange);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("LaundryBasket"))
            {
                LaundryBasket basket = hitCollider.GetComponent<LaundryBasket>();
                if (basket != null && !isCarryingClothes)
                {
                    TakeDirtyClothes(basket);
                }
            }
            else if (hitCollider.CompareTag("WashingMachine"))
            {
                WashingMachine machine = hitCollider.GetComponent<WashingMachine>();
                if (machine != null && isCarryingClothes)
                {
                    PutClothesInWashingMachine(machine);
                }
            }
        }

        // Check for obstacles (other employees) in the path
        CheckForObstacles();
    }

    private void CheckForObstacles()
    {
        // Cek apakah ada karyawan lain dalam radius tertentu
        Collider[] employeesNearby = Physics.OverlapSphere(transform.position, detectionRadius, employeeLayer);
        
        foreach (var employee in employeesNearby)
        {
            if (employee.gameObject != this.gameObject)
            {
                // Jika ada karyawan lain yang terdeteksi, buat ulang jalur (re-route)
                agent.SetDestination(agent.destination); // Recalculate path
                break;
            }
        }
    }

    private void GoToLaundryBasket()
    {
        Transform laundryBasket = GameObject.FindWithTag("LaundryBasket").transform;
        agent.SetDestination(laundryBasket.position);
    }

    private void GoToWashingMachine()
    {
        WashingMachine nearestMachine = FindNearestAvailableWashingMachine();
        if (nearestMachine != null)
        {
            agent.SetDestination(nearestMachine.transform.position);
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
