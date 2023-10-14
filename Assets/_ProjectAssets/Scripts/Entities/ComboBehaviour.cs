using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComboBehaviour : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI comboTXT;
   
   [Header("Fill Effect")]
   [SerializeField] private RectTransform fillImage;
   [ColorUsage(true,true)]
   public Color fillColorActive;
   [ColorUsage(true,false)]
   public Color fillColorInactive;

   private RawImage _fillRawImage;
   
   
   private RectTransform _rectTransform;
   

   private int comboAmount;
   private int leanTweenID;

   private void Awake()
   {
      _rectTransform = GetComponent<RectTransform>();
      _fillRawImage = fillImage.GetComponent<RawImage>();
   }


   private void OnEnable()
   {
      CoinsBehaviour.onCoinDestroy += ComboStatus;
   }

   private void OnDisable()
   {
      CoinsBehaviour.onCoinDestroy -= ComboStatus;
   }

   private void Start()
   {
      UpdateComboText();
   }


   private void ComboStatus(bool touchedByPlayer)
   {
      if (touchedByPlayer)
      {
         UpdateComboText();
         FillAnimation();
      }
      else
      {
         LoseCombo();
         ResetFillAnimation();
      }
   }

   private void UpdateComboText()
   {
     // BackgroundParticlesManager.instance.InitAnimation();
      comboAmount++;
      comboTXT.text = $"X{comboAmount}";
      
      CoinsBehaviour.amount = comboAmount;
      ScaleAnimation();

   }
   
   private void LoseCombo()
   {
      comboAmount=1;
      comboTXT.text = $"X{comboAmount}";
      
      CoinsBehaviour.amount = comboAmount;
      ScaleAnimation();
   }
   
   private void ScaleAnimation()
   {
      LeanTween.scale(_rectTransform, Vector3.zero, 0.1f).setEasePunch()
         .setOnComplete(() =>
         {
            LeanTween.scale(_rectTransform, new Vector3(1f,1f,1f), 1f)
               .setEaseInElastic();
         });
   }

   private void FillAnimation()
   {
      ResetFillAnimation();
      _fillRawImage.color = fillColorActive;
      leanTweenID = LeanTween.moveY(fillImage, -427f, 7.46f).setEaseLinear().id;
   }

   private void ResetFillAnimation()
   {
      LeanTween.cancel(leanTweenID);
      Vector3 initialPosition = fillImage.localPosition;
      initialPosition.y = -5.72f;
      fillImage.localPosition = initialPosition;
      _fillRawImage.color = fillColorInactive;
   }
   
}
