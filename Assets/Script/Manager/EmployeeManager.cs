using UnityEngine;
using System.Collections.Generic;

public class EmployeeManager : MonoBehaviour
{
    public GameObject employeePrefab; // Prefab karyawan
    public Transform[] spawnPoints; // Titik spawn karyawan
    private List<GameObject> employees = new List<GameObject>();
    private int currentlvl = 0;
    private int maxEmployees = 3; // Jumlah maksimum karyawan
    public static EmployeeManager instance;

        private void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddEmployee()
{
    if (currentlvl < maxEmployees)
    {
        Transform spawnPoint = spawnPoints[employees.Count]; // Gunakan employees.Count sebagai index
        GameObject newEmployee = Instantiate(employeePrefab, spawnPoint.position, spawnPoint.rotation);
        employees.Add(newEmployee);
        newEmployee.SetActive(true);

        currentlvl++; // Tingkatkan level saat karyawan baru ditambahkan
        Debug.Log("New employee added! Total employees: " + employees.Count + ". Current Level: " + currentlvl);
    }
    else
    {
        Debug.Log("Maximum number of employees reached!");
    }
}

    public void UpgradeAllEmployeesSpeed(float additionalSpeed)
    {
        foreach (GameObject employee in employees)
        {
            Employee employeeScript = employee.GetComponent<Employee>(); // Dapatkan komponen EmployeeScript dari karyawan
            if (employeeScript != null)
            {
                employeeScript.UpgradeSpeed(additionalSpeed); // Panggil metode UpgradeSpeed pada instance employeeScript
            }
        }
    }

    public void UpgradeAllEmployeesCapacity(int additionalCapacity)
    {
        foreach (GameObject employee in employees)
        {
            Employee employeeScript = employee.GetComponent<Employee>(); // Dapatkan komponen EmployeeScript dari karyawan
            if (employeeScript != null)
            {
                employeeScript.UpgradeCapacity(additionalCapacity); // Panggil metode UpgradeCapacity pada instance employeeScript
            }
        }
    }
    public int GetCurrentLevel()
{
    return currentlvl; // Assuming you have a currentLevel variable
}

public void SetCurrentLevel(int level)
{
    currentlvl = level;
    // Rebuild employees based on the saved level if necessary
}
}
