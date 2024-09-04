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
private Vector3 lastPosition;
private float stuckTime = 0.0f;
private float maxStuckTime = 1.0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = walkSpeed;
        GoToLaundryBasket();
    }

    void Update()
    {

    if (Vector3.Distance(transform.position, lastPosition) < 0.1f) // If the agent hasn't moved much
    {
        stuckTime += Time.deltaTime;
        if (stuckTime > maxStuckTime)
        {
            agent.SetDestination(agent.destination); // Force path recalculation
            stuckTime = 0.0f; // Reset the stuck timer
        }
    }
    else
    {
        stuckTime = 0.0f; // Reset if the agent is moving
    }

    lastPosition = transform.position;

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
