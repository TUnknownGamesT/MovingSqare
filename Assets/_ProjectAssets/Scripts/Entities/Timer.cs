using System;
using System.Collections;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Timer : MonoBehaviour
{
   #region Singleton  
   public static Timer instance;
   
   private void Awake()
   {
      instance = FindObjectOfType<Timer>();

      if (instance == null)
         instance = this;
   }
   #endregion

   public static Action onCounterEnd;
   
   private static float lvlDuration;
   private  CancellationTokenSource _ct= new();
   

   public static float Duration
   {
      set => lvlDuration = value;
   }


   private void OnEnable()
   {
      PlayerLife.onPlayerDie+=PauseTimer;
      AdsManager.onReviveADFinish += ResumeTimer;
   }

   private void OnDisable()
   {
      AdsManager.onReviveADFinish -= ResumeTimer;
      PlayerLife.onPlayerDie-=PauseTimer;
   }

   public  void StartCounter()
   {
      Counter();
   }


   private void PauseTimer()
   {
      Debug.LogWarning("PauseTimer");
      _ct.Cancel();
      _ct.Dispose();
      _ct = new();
   }

   private void ResumeTimer()
   {
      Debug.LogWarning("StartTimer");
      Counter();
   }
   
   private async UniTask Counter()
   {
      await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: _ct.Token);

      lvlDuration--;
      if (lvlDuration==0)
      {
         onCounterEnd?.Invoke();
      }
      else
      {
         Counter();
      }
   }
}
