using System;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Jemuran
{
    public class JemuranAnimation : MonoBehaviour
    {
        public Animator _animation;
        public JemuranManager JemuranManager;
        public AudioClip suaraperes;
        public void PlayPeresAnimation(string animationTriggername)
        {
            
            _animation.SetTrigger(animationTriggername);
                AudioSource.PlayClipAtPoint(suaraperes, Camera.main.transform.position);
                

        }

        public void TaroHanger()
        {

            JemuranManager.PlaceHanger();
        }
    }
}