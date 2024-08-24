using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Script.SO
{
    public class BajuDragAble : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Image _spriteModel;
        [SerializeField] private List<LangkahCuci> _curretLangkahCuci;
        [SerializeField] private float _timeToFinish = 3;
        public BajuSo _bajuSo;
        [SerializeField] private WashManager _washManager;

        [SerializeField] private int _currectCurrentLangkahIndex = 0;

        [Header("Sound Properties")]
        [SerializeField]
        private AudioClip _bilasSound;

        private bool _isDrag = false;
        [SerializeField] private Image _image;
        private Vector2 _startPos;
        private Transform _camPos;
        [SerializeField] GameObject bubbleeffect;
        [SerializeField] private AudioClip salah;

        private void Start()
        {
            _camPos = Camera.main.transform;
            _startPos = transform.position;
        }

        public void InitBaju(BajuSo bajuSo)
        {
            _bajuSo = bajuSo;
            _spriteModel.sprite = bajuSo.BajuKotorSprite;
            _curretLangkahCuci = bajuSo.langkahCuci;
            _image.raycastTarget = true;
            _currectCurrentLangkahIndex = 0;
            bubbleeffect.SetActive(false);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDragItem();
        }

        private void Update()
        {
            if (_isDrag)
            {
                transform.position = Input.mousePosition;
                ;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnDropItem(eventData.pointerCurrentRaycast.gameObject);
        }

        void OnDropItem(GameObject pointerCurrentRaycastGameObject)
        {
            _isDrag = false;
            _image.raycastTarget = true;
            transform.position = _startPos;

            if (pointerCurrentRaycastGameObject.name == "Bilas")
            {
                CuciBaju(LangkahCuci.Bilas);
            }
        }

        void OnDragItem()
        {
            _isDrag = true;
            _image.raycastTarget = false;
        }

        public void CuciBaju(LangkahCuci langkahCuci, AudioClip audioClip = null)
        {
            if (langkahCuci == _curretLangkahCuci[_currectCurrentLangkahIndex])
            {
                GantiSpriteBaju();
                _currectCurrentLangkahIndex++;
                if (audioClip != null)
                {
                    AudioSource.PlayClipAtPoint(audioClip, _camPos.position);

                }
                Debug.Log("Langkah Cuci Benar");

                if (_curretLangkahCuci.Count == _currectCurrentLangkahIndex)
                {
                    StartCoroutine(FinishNyuciBaju());
                }

            }
            else
            {   
                AudioSource.PlayClipAtPoint(salah, Camera.main.transform.position);
                GameManager.Instance.DialogueManager.StartDialogue("Ooops... sepertinya ada yang salah", true);

            }
        }

        private void GantiSpriteBaju()
        {
            if (_curretLangkahCuci[_currectCurrentLangkahIndex] == LangkahCuci.Detergen ||
                _curretLangkahCuci[_currectCurrentLangkahIndex] == LangkahCuci.Pemutih)
            {
                _spriteModel.sprite = _bajuSo.BajuBasahSprite;
                print("Baju Basah");
            }
            else if (_curretLangkahCuci[_currectCurrentLangkahIndex] == LangkahCuci.Bilas)
            {
                bubbleeffect.SetActive(false);
                AudioSource.PlayClipAtPoint(_bilasSound, Camera.main.transform.position);
                print("bajur bersih");
                _spriteModel.sprite = _bajuSo.BajuBersihSpritel;
            }
            else if (_curretLangkahCuci[_currectCurrentLangkahIndex] == LangkahCuci.Sikat)
            {
                bubbleeffect.SetActive(true);
            }

            print("Ganti Sprite Baju");
        }

        private IEnumerator FinishNyuciBaju()
        {
            Debug.LogError("Start Finish Nyuci Baju");
            _image.raycastTarget = false;
            yield return new WaitForSeconds(_timeToFinish);
            _washManager.NextBaju();
        }
        
        public AudioClip GetSalahClip()
        {
            return salah;
        }
    }

}