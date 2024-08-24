using System.Collections;
using System.Collections.Generic;
using Script.Interface;
using UnityEngine;

namespace Script.Interactable
{
    public class OpenLemari : MonoBehaviour, Iinteractable
    {
        [SerializeField] Transform objekToLerp;
        [SerializeField] Vector3 targetLerp;
        [SerializeField] float _SpeedLerp = 0.5f;
        private bool _canInteract = true;

        public void Interact()
        {
            if (!_canInteract)
                return;

            _canInteract = false;
            StartCoroutine(LerpToObject());
        }

        private IEnumerator LerpToObject()
        {
            Vector3 startPosition = objekToLerp.localPosition; // Use localPosition here
            float timeElapsed = 0;

            while (timeElapsed < 1)
            {
                objekToLerp.localPosition = Vector3.Lerp(startPosition, targetLerp, timeElapsed);
                timeElapsed += Time.deltaTime * _SpeedLerp;
                yield return null;
            }

            objekToLerp.localPosition = targetLerp; // Ensure the object reaches the target position
        }
    }
}
