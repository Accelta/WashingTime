using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Script.Interactable;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.XInput;


public class QuestManager : MonoBehaviour
{
   [SerializeField] private float _tutorialWait = 3;
   public TextMeshProUGUI _questText;
   public List<string> Quests = new List<string>();

   [SerializeField] private HingeJoint _pintuLuar;
   [SerializeField] private HingeJoint _pintuMandi;
   [SerializeField] private HingeJoint _pintuKamar;
   [SerializeField] private GameObject _questPanel;
   [SerializeField] private Book bookKamar;
   [SerializeField] private GameObject _spawnPakian;
   [SerializeField] private Lemari lemari;
   private JointLimits j_limitKamar;
   private JointLimits j_limitLuar;
   private JointLimits j_limitMandi;
   [SerializeField] private WashManager washmanager;


   private void Start()
   {
      j_limitKamar = _pintuKamar.limits;
      j_limitLuar = _pintuLuar.limits;
      j_limitMandi = _pintuMandi.limits;


      ChangeQuest(0);
   }

   public void ChangeQuest(int id)
   {
      if (!washmanager)
      {
         if (id == 0)
         {
            StartCoroutine(StartTutorial());
         }

         if (id == 1)
         {
            StartCheckNoteKamar();
         }

         if (id == 2)
         {
            _spawnPakian.SetActive(true);
            lemari._canInteract = true;
            _questText.text = Quests[3];
         }

         if (id == 3)
         {
            _pintuMandi.limits = j_limitKamar;
            _questText.text = Quests[4];
         }

         if (id == 4)
         {
            _pintuLuar.limits = j_limitLuar;
            _questText.text = Quests[5];
         }
      }
      else
      {
         if (id == 2 && washmanager.lvlid == 2)
         {
            lemari._canInteract = true;
            _questText.text = Quests[3];
            _spawnPakian.SetActive(false);

         }
      }

   }

   IEnumerator StartTutorial()
   {
      JointLimits jointLimits = new JointLimits();
      jointLimits.min = 0;
      jointLimits.max = 0;

      _pintuKamar.limits = jointLimits;
      _pintuLuar.limits = jointLimits;
      _pintuMandi.limits = jointLimits;
      bookKamar._canInteract = false;
      yield return new WaitForSeconds(_tutorialWait);
      bookKamar._canInteract = true;
      _questText.text = Quests[1];

   }

   void StartCheckNoteKamar()
   {
      _questText.text = Quests[2];
      _pintuKamar.limits = j_limitKamar;
   }

   public void ShowQuestPanel()
   {
      _questPanel.SetActive(true);
   }

   public void HideQuestPanel()
   {
      _questPanel.SetActive(false);
   }



}
