using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
   public List<Transform> spawningPoints;
   public List<GameObject> spawnableObjects;
   public GameObject enemyAlertSignPrefab;
   
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
   private AttentionSignBehaviour attentionSignBehaviour;

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
      StartCoroutine(SpawnEnemy());
   }

   #region Money Spawning

   private IEnumerator SpawnAverageMoney()
   {
      yield return new WaitForSeconds(timeBetweenSpawnMoney);
      time = Time.time;
      Instantiate(averageMoney, new Vector2(Random.Range(minX, maxX)
         , Random.Range(minY, maxY)), Quaternion.identity);
      StartCoroutine(time >= timeForExtraMoney ? SpawnExtraMoney() : SpawnAverageMoney());
   }


   private IEnumerator SpawnExtraMoney()
   {
      yield return new WaitForSeconds(timeBetweenSpawnMoney);
      Instantiate(extraMoney, new Vector2(Random.Range(minX, maxX)
         , Random.Range(minY, maxY)), Quaternion.identity);
      StartCoroutine(SpawnExtraMoney());
   }

   #endregion
  
   
   private IEnumerator SpawnEnemy()
   {
      yield return new WaitForSeconds(timeBetweenSpawnEnemy);

      GameObject objectToSpawn = spawnableObjects[Random.Range(0, 3)];
      Transform spawnPoint = spawningPoints[Random.Range(0, 3)];
     
      attentionSignBehaviour = Instantiate(enemyAlertSignPrefab, spawnPoint.position, spawnPoint.rotation)
         .GetComponent<AttentionSignBehaviour>();
      
      EnemyDirection(spawnPoint);
      attentionSignBehaviour.target = Instantiate(objectToSpawn, positionToSpawn, Quaternion.identity).GetComponent<Transform>();

      StartCoroutine(SpawnEnemy());
   }


   private void EnemyDirection(Transform spawnedPoint)
   {
      switch (spawnedPoint.name)
      {
         case "E" :
            positionToSpawn = new Vector2(5.99f,Random.Range(minE, maxE));
            attentionSignBehaviour.enemyDirection = Constants.Directions.E;
            break;
         case "V":
            positionToSpawn = new Vector2(-4.9f,Random.Range(minV,maxV));
            attentionSignBehaviour.enemyDirection = Constants.Directions.V;
            break;
         case "W":
            positionToSpawn = new Vector2(Random.Range(minW, maxW),6.14f);
            attentionSignBehaviour.enemyDirection = Constants.Directions.W;
            break;
         default:
            Debug.LogError("Error in Enemy Direction");
            break;
      }
   }
   
}
