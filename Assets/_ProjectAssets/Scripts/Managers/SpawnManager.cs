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
   public List<GameObject> powerUps;
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

   public float timeBetweenSpawnPowerUps;
   public float timeBetweenSpawnEnemy;
   public float timeBetweenSpawnMoney;
   public GameObject coinPrefab;


   private Vector2 positionToSpawn;
   private AttentionSignBehaviour attentionSignBehaviour;
   
   
   private void Start()
   {
      StartCoroutine(SpawnMoney());
      StartCoroutine(SpawnEnemy());
      StartCoroutine(SpawnPowerUps());
   }

   #region Spawn Money

   private IEnumerator SpawnMoney()
   {
      yield return new WaitForSeconds(timeBetweenSpawnMoney);
      Instantiate(coinPrefab, new Vector2(Random.Range(minX, maxX)
         , Random.Range(minY, maxY)), Quaternion.identity);

      StartCoroutine(SpawnMoney());
   }

   #endregion

   #region Spawn Enemy
   private IEnumerator SpawnEnemy()
   {
      yield return new WaitForSeconds(timeBetweenSpawnEnemy);

      GameObject objectToSpawn = spawnableObjects[Random.Range(0, 3)];
      Transform spawnPoint = spawningPoints[Random.Range(0, 3)];
     
      attentionSignBehaviour = Instantiate(enemyAlertSignPrefab, spawnPoint.position, spawnPoint.rotation).GetComponent<AttentionSignBehaviour>();
      
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
   

   #endregion

   #region Spawn Power Ups

   IEnumerator SpawnPowerUps()
   {
      yield return new WaitForSeconds(timeBetweenSpawnPowerUps);
      Instantiate(powerUps[Random.Range(0,3)], new Vector2(Random.Range(minX, maxX)
         , Random.Range(minY, maxY)), Quaternion.identity);
      StartCoroutine(SpawnPowerUps());
   }
   

   #endregion
  
   
}
