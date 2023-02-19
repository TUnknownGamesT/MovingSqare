using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
   public List<Transform> spawningPoints;
   public List<GameObject> spawnableObjects;
   
   [Header("V Values")] 
   public float maxV;
   public float minV;
   [Header("W Values")] 
   public float maxW;
   public float minW;
   [Header("E Values")] 
   public float maxE;
   public float minE;
   
   public float timeBetweenSpawn;

   
   private float time;
   private Vector2 positionToSpawn;

   private void Awake()
   {
      time = Time.time;
   }

   private void Start()
   {
      StartCoroutine(SpawnObjects());
   }

   IEnumerator SpawnObjects()
   {
      if (time + timeBetweenSpawn <= Time.time)
      {
         time += timeBetweenSpawn;
         GameObject objectToSpawn = spawnableObjects[Random.Range(0,3)];
         Transform spawnPoint = spawningPoints[Random.Range(0, 3)];
         EnemyDirection(spawnPoint.name);
         Instantiate(objectToSpawn, positionToSpawn, Quaternion.identity);
      }
      
      yield return null;
      
      StartCoroutine(SpawnObjects());
      
   }


   private void EnemyDirection(string spawnedPoint)
   {
      switch (spawnedPoint)
      {
         case "E" :
            positionToSpawn = new Vector2(5.99f,Random.Range(minE, maxE));
            break;
           // return Vector2.left;
         case "V":
            positionToSpawn = new Vector2(-4.9f,Random.Range(minV,maxV));
            break;
           // return Vector2.right;
         case "W":
            positionToSpawn = new Vector2(Random.Range(minW, maxW),6.14f);
            break;
           // return Vector2.down;
         default:
            Debug.LogError("Error in Enemy Direction");
            break;
           // return Vector2.zero;
      }
   }
   
}
