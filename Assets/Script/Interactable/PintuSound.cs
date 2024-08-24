// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class PintuSound : MonoBehaviour
// {
//     [SerializeField] private AudioClip audioClip;

//     Vector3 campos= Vector3.zero;

//     private void Start() {
//         campos = Camera.main.transform.position;    
//     }
//     private void OnTriggerEnter(Collider other) {
//         AudioSource.PlayClipAtPoint(audioClip, other.transform.position,0.5f) ;
//     }
// }
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PintuSound : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;

    private void OnTriggerEnter(Collider other)
    {
        // Ensure the trigger works only when the player enters
        if (other.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(audioClip, other.transform.position,2.5f);
        }
    }
}
