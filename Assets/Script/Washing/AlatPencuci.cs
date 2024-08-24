using System;
using Script.SO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Script.Washing
{
    public class AlatPencuci : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public PencuciSO PencuciSo;

        [SerializeField] private Image _spriteModel;

        private bool _isDrag = false;
        private Vector2 _StartPos;

        private bool _isOnBaju = false;

        private void Awake()
        {
            if (PencuciSo == null)
            {
                Debug.LogError("PencuciSo is null");
                return;
            }

            InitAlatCuci();
        }

        private void InitAlatCuci()
        {
            _spriteModel.sprite = PencuciSo._modelSprite;
            _StartPos = transform.position;
        }

        private void Update()
        {
            if (_isDrag)
            {
                transform.position = Input.mousePosition;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDragItem();
        }

        private void OnDragItem()
        {
            _isDrag = true;
            _spriteModel.raycastTarget = false;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            
            OnDropItem(eventData.pointerCurrentRaycast.gameObject);
        }

        void OnDropItem(GameObject obj)
        {
            _isDrag = false;
            transform.position = _StartPos;
            _spriteModel.raycastTarget = true;

            if (obj.CompareTag("Clothes"))
            {
                obj.GetComponent<BajuDragAble>().CuciBaju(PencuciSo._LangkahCuci, PencuciSo._SuaraAlatCuci);
            }

        }


       
    }
}