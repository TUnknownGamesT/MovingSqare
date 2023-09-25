using System;
using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
   
   public static Timer instance;
   
   private void Awake()
   {
      instance = FindObjectOfType<Timer>();

      if (instance == null)
         instance = this;
   }
   

   public static Action onCounterEnd;

   private static float lvlDuration;

   public static float Duration
   {
      set => lvlDuration = value;
   }
   
   public  void StartCounter()
   {
      StartCoroutine(Counter());
   }

   private IEnumerator Counter()
   {
      yield return new WaitForSeconds(1f);

      lvlDuration--;
      if (lvlDuration==0)
      {
         onCounterEnd?.Invoke();
      }
      else
      {
         StartCoroutine(Counter());
      }
   }
}
