// using System;
// using System.Collections;
// using System.Collections.Generic;
// using Script.SO;
// using UnityEngine;
// using UnityEngine.SceneManagement;
// using UnityEngine.UI;

// namespace Script.Jemuran
// {
//     public class JemuranManager : MonoBehaviour
//     {
//         public List<BajuJemuranItem> BajuJemuranItems;
//         public JemuranSpawner JemuranSpawner;
//         public JemuranAnimation JemuranAnimation;
//         [SerializeField] private Image _spriteModel;
//         public List<Image> ListJemuranSpawn;
//         public int CurrentJemuranIndex ;
//         public List<Image> ListHangerSpawn;
//         [SerializeField] private Image _ImageDisable;
//         public bool _canShowJemuran = false;
//         public bool _canPickup = true;
//         private void OnEnable()
//         {
//             InitJemuran();
//         }

//         private void InitJemuran()
//         {
//             CurrentJemuranIndex = BajuJemuranItems.Count - 1;
//             foreach (BajuJemuranItem jemuranItem in BajuJemuranItems)
//             {
//                 Image newImage = Instantiate(_spriteModel, JemuranSpawner.transform);
//                 newImage.sprite = jemuranItem.BajuSprite;
//                 newImage.SetNativeSize();
//                 ListJemuranSpawn.Add(newImage);
//             }
//         }

//         public void HideCurrectModelImage()
//         {
//             if(!_canPickup)
//                 return;
//             ListJemuranSpawn[CurrentJemuranIndex].enabled = false;
//         }

//         public void ShowCurrectModelImage()
//         {
//             if (!_canPickup)
//                 return;
//             ListJemuranSpawn[CurrentJemuranIndex].enabled = true;
//         }

//         public void StartPeresAnimation()
//         {
//             _ImageDisable.raycastTarget = true;
//             JemuranAnimation.PlayPeresAnimation(BajuJemuranItems[CurrentJemuranIndex].BajuAnimation);
//         }

//         public void PlaceHanger()
//         {
//             if (!_canPickup)
//                 return;
            
//             ListHangerSpawn[CurrentJemuranIndex].sprite = BajuJemuranItems[CurrentJemuranIndex].BajuHangerSprite;
//             ListHangerSpawn[CurrentJemuranIndex].enabled = true;
            
//             CurrentJemuranIndex--;

//             if (CurrentJemuranIndex < 0)
//             {
//                 _canPickup = false;
//                 StartCoroutine(wait());
//             }

//             _ImageDisable.raycastTarget = false;
//         }


//     private IEnumerator wait()
//     {
//         yield return new WaitForSeconds(5);
//         SceneManager.LoadScene("GoodEndingScene");
//     }
//     }
// }

using System;
using System.Collections;
using System.Collections.Generic;
using Script.SO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Script.Jemuran
{
    public class JemuranManager : MonoBehaviour
    {
        public List<BajuJemuranItem> BajuJemuranItems;
        public JemuranSpawner JemuranSpawner;
        public JemuranAnimation JemuranAnimation;
        [SerializeField] private Image _spriteModel;
        public List<Image> ListJemuranSpawn;
        public int CurrentJemuranIndex;
        public List<Image> ListHangerSpawn;
        [SerializeField] private Image _ImageDisable;
        public bool _canShowJemuran = false;
        public bool _canPickup = true;

        private void OnEnable()
        {
            InitJemuran();
        }

        private void InitJemuran()
        {
            CurrentJemuranIndex = BajuJemuranItems.Count - 1;
            foreach (BajuJemuranItem jemuranItem in BajuJemuranItems)
            {
                Image newImage = Instantiate(_spriteModel, JemuranSpawner.transform);
                newImage.sprite = jemuranItem.BajuSprite;
                newImage.SetNativeSize();
                ListJemuranSpawn.Add(newImage);
            }
        }

        public void HideCurrectModelImage()
        {
            if (!_canPickup)
                return;
            ListJemuranSpawn[CurrentJemuranIndex].enabled = false;
        }

        public void ShowCurrectModelImage()
        {
            if (!_canPickup)
                return;
            ListJemuranSpawn[CurrentJemuranIndex].enabled = true;
        }

        public void StartPeresAnimation()
        {
            _ImageDisable.raycastTarget = true;
            JemuranAnimation.PlayPeresAnimation(BajuJemuranItems[CurrentJemuranIndex].BajuAnimation);
        }

        public void StartHeavyTypeAnimation()
        {
            _ImageDisable.raycastTarget = true;
            // Add the animation or actions for heavy items
            JemuranAnimation.PlayPeresAnimation(BajuJemuranItems[CurrentJemuranIndex].BajuAnimation);
        }

        public void PlaceHanger()
        {
            if (!_canPickup)
                return;

            ListHangerSpawn[CurrentJemuranIndex].sprite = BajuJemuranItems[CurrentJemuranIndex].BajuHangerSprite;
            ListHangerSpawn[CurrentJemuranIndex].enabled = true;

            CurrentJemuranIndex--;

            if (CurrentJemuranIndex < 0)
            {
                _canPickup = false;
                StartCoroutine(wait());
            }

            _ImageDisable.raycastTarget = false;
        }

        private IEnumerator wait()
        {
            yield return new WaitForSeconds(5);
            SceneManager.LoadScene("GoodEndingScene");
        }
    }
}
