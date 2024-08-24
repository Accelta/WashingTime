// using System;
// using UnityEngine;
// using UnityEngine.EventSystems;
// using UnityEngine.UI;

// namespace Script.Jemuran
// {
//     public class JemuranSpawner : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
//     {
//         public JemuranManager JemuranManager;
//         public Image _spriteModel;

//         private bool _isDrag = false;
//         private Vector2 _spriteStartPos;

//         private void Start()
//         {
//             _spriteStartPos = _spriteModel.transform.position;
//         }

//         public void OnPointerDown(PointerEventData eventData)
//         {
//             if (!JemuranManager._canPickup)
//                 return;

//             _spriteModel.sprite = JemuranManager.BajuJemuranItems[JemuranManager.CurrentJemuranIndex].BajuSprite;
//             JemuranManager.HideCurrectModelImage();

//             _spriteModel.SetNativeSize();
//             _isDrag = true;
//             _spriteModel.enabled = true;
//         }


//         public void OnPointerUp(PointerEventData eventData)
//         {
//             if (!JemuranManager._canPickup)
//                 return;
//             _isDrag = false;
//             _spriteModel.transform.position = _spriteStartPos;
//             _spriteModel.enabled = false;

//             CheckCondition(eventData);
//         }

//         void CheckCondition(PointerEventData eventData)
//         {
//             if (eventData.pointerCurrentRaycast.gameObject == null )
//             {
//                 JemuranManager.ShowCurrectModelImage();
//                 return;
//             }


//             if (eventData.pointerCurrentRaycast.gameObject.name == "Trigger")
//             {
//                 JemuranManager.StartPeresAnimation();
//                 return;
//             }
//             if (eventData.pointerCurrentRaycast.gameObject.name != "Trigger")
//             {
//                 JemuranManager.ShowCurrectModelImage();
//             }
//         }

//         private void Update()
//         {
//             if (_isDrag)
//             {
//                 _spriteModel.transform.position = Input.mousePosition;
//             }
//         }
//     }
// }

using System;
using Script.SO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Script.Jemuran
{
    public class JemuranSpawner : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public JemuranManager JemuranManager;
        public Image _spriteModel;

        private bool _isDrag = false;
        private Vector2 _spriteStartPos;
        public AudioClip salah;


        private void Start()
        {
            _spriteStartPos = _spriteModel.transform.position;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!JemuranManager._canPickup)
                return;

            _spriteModel.sprite = JemuranManager.BajuJemuranItems[JemuranManager.CurrentJemuranIndex].BajuSprite;
            JemuranManager.HideCurrectModelImage();

            _spriteModel.SetNativeSize();
            _isDrag = true;
            _spriteModel.enabled = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!JemuranManager._canPickup)
                return;
            _isDrag = false;
            _spriteModel.transform.position = _spriteStartPos;
            _spriteModel.enabled = false;

            CheckCondition(eventData);
        }

        void CheckCondition(PointerEventData eventData)
        {
            if (eventData.pointerCurrentRaycast.gameObject == null)
            {
                JemuranManager.ShowCurrectModelImage();
                return;
            }

            var currentItem = JemuranManager.BajuJemuranItems[JemuranManager.CurrentJemuranIndex];

            if (eventData.pointerCurrentRaycast.gameObject.name == "Trigger" && currentItem.BajuType == tipe.ringan)
            {
                JemuranManager.StartPeresAnimation();
                return;
            }
            else if (eventData.pointerCurrentRaycast.gameObject.name == "Trigger2" && currentItem.BajuType == tipe.berat )
            {
                JemuranManager.StartHeavyTypeAnimation();
                return;
            }
            else
            {
                JemuranManager.ShowCurrectModelImage();
                AudioSource.PlayClipAtPoint(salah, Camera.main.transform.position);
                GameManager.Instance.DialogueManager.StartDialogue("Ooops... sepertinya ada yang salah", true);

            }
        }

        private void Update()
        {
            if (_isDrag)
            {
                _spriteModel.transform.position = Input.mousePosition;
            }
        }
    }
}
