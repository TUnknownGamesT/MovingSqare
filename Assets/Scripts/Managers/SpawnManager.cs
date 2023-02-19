using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;
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


   [Header("Money Spawner Zone")] 
   public float maxX;
   public float minX;
   public float maxY;
   public float minY;
   
   public float timeBetweenSpawnEnemy;
   public float timeBetweenSpawnMoney;
   public float timeForExtraMoney;
   public GameObject averageMoney;
   public GameObject extraMoney;
   
   
   private Vector2 positionToSpawn;
   private float time;

   private void Awake()
   {
      SetTime();
   }

   private void SetTime()
   {
      if (PlayerPrefs.HasKey("Time"))
      {
         time = PlayerPrefs.GetFloat("Time");
      }
   }

   private void Start()
   {
      StartCoroutine(time >= timeForExtraMoney ? SpawnExtraMoney() : SpawnAverageMoney());
      StartCoroutine(SpawnObjects());
   }

   IEnumerator SpawnAverageMoney()
   {
      yield return new WaitForSeconds(timeBetweenSpawnMoney);
      time = Time.time;
      Instantiate(averageMoney, new Vector2(Random.Range(minX, maxX)
         , Random.Range(minY, maxY)), Quaternion.identity);
      StartCoroutine(time >= timeForExtraMoney ? SpawnExtraMoney() : SpawnAverageMoney());
   }


   IEnumerator SpawnExtraMoney()
   {
      yield return new WaitForSeconds(timeBetweenSpawnMoney);
      Instantiate(extraMoney, new Vector2(Random.Range(minX, maxX)
         , Random.Range(minY, maxY)), Quaternion.identity);
      StartCoroutine(SpawnExtraMoney());
   }

   IEnumerator SpawnObjects()
   {
      yield return new WaitForSeconds(timeBetweenSpawnEnemy);

      GameObject objectToSpawn = spawnableObjects[Random.Range(0, 3)];
      Transform spawnPoint = spawningPoints[Random.Range(0, 3)];
      EnemyDirection(spawnPoint.name);
      Instantiate(objectToSpawn, positionToSpawn, Quaternion.identity);

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
