using System;
using Script.Interface;
using UnityEngine;

namespace Script
{
    public class InteractAbility : MonoBehaviour
    {
        public Iinteractable currectInteractObj = null;


        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                TryInteract();
            }
        }

        private void TryInteract()
        {
            if (currectInteractObj != null)
            {
                currectInteractObj.Interact();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Interact"))
            {
                Iinteractable otherObj = other.GetComponent<Iinteractable>();
                currectInteractObj = otherObj;

            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Interact"))
            {
                currectInteractObj = null;
            }
        }
    }
}