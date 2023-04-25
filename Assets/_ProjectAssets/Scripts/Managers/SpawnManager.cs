using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json.Converters;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public List<Transform> spawningPoints;
    public List<GameObject> spawnableObjects;
    public List<GameObject> powerUps;
    public GameObject enemyAlertSignPrefab;
    public GameObject coinPrefab;
   
    [Header("Status")]
    public float spawnPowerUps;
    public float spawnEnemy;
    public float spawnMoney;

    [HideInInspector]
    public float squareStage2;
    [HideInInspector]
    public float circleStage2;
    [HideInInspector]
    public float hexagonStage2;

    [Header("V Values")] public float maxV;
    public float minV;
    [Header("W Values")] public float maxW;
    public float minW;
    [Header("E Values")] public float maxE;
    public float minE;

    [Header("Money Spawner Zone")] public float maxX;
    public float minX;
    public float maxY;
    public float minY;
    
    
    
    private Vector2 positionToSpawn;
    private AttentionSignBehaviour attentionSignBehaviour;


    private void OnEnable()
    {
        BossGameplay.OnBossAppear += StopSpawning;
        BossGameplay.OnBossDisappear += StartSpawning;
    }

    private void OnDisable()
    {
        BossGameplay.OnBossAppear -= StopSpawning;
        BossGameplay.OnBossDisappear -= StartSpawning;
    }


    public void StopSpawning()
    {
        StopAllCoroutines();
        Debug.Log("Stop Spawning");
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnMoney());
        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnPowerUps());
        
        Debug.Log("Start Spawning");
        
    }

    #region Spawn Money

    private IEnumerator SpawnMoney()
    {
        yield return new WaitForSeconds(spawnMoney);
        Instantiate(coinPrefab, new Vector2(Random.Range(minX, maxX)
            , Random.Range(minY, maxY)), Quaternion.identity);

        StartCoroutine(SpawnMoney());
    }

    #endregion

    #region Spawn Enemy

    private IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(spawnEnemy);

        GameObject objectToSpawn = spawnableObjects[Random.Range(0, 3)];
        Transform spawnPoint = spawningPoints[Random.Range(0, 3)];

        objectToSpawn.GetComponent<EnemyBehaviour>().speed = Random.Range(2, 4);
        
        //Set direction, Target and Stage
        SetStage(objectToSpawn);
        attentionSignBehaviour = Instantiate(enemyAlertSignPrefab, spawnPoint.position, spawnPoint.rotation)
            .GetComponent<AttentionSignBehaviour>();
        SetEnemyDirection(spawnPoint);
        attentionSignBehaviour.target =
            Instantiate(objectToSpawn, positionToSpawn, Quaternion.identity).GetComponent<Transform>();

        StartCoroutine(SpawnEnemy());
    }

    // Set the stage of enemies
    private void SetStage(GameObject objectToSpawn)
    {
        switch (objectToSpawn.name)
        {
            case "Circle":
            {
                objectToSpawn.GetComponent<EnemyBehaviour>().stage =
                    Random.value < circleStage2 ? Stages.second : Stages.first;
                break;
            }

            case "Hexagon":
            {
                objectToSpawn.GetComponent<EnemyBehaviour>().stage =
                    Random.value < hexagonStage2 ? Stages.second : Stages.first;
                break;
            }

            case "Square":
            {
                objectToSpawn.GetComponent<EnemyBehaviour>().stage =
                    Random.value < squareStage2 ? Stages.second : Stages.first;
                break;
            }
        }
    }
    
    
    private void SetEnemyDirection(Transform spawnedPoint)
    {
        switch (spawnedPoint.name)
        {
            case "E":
                positionToSpawn = new Vector2(5.99f, Random.Range(minE, maxE));
                attentionSignBehaviour.enemyDirection = Constants.Directions.E;
                break;
            case "V":
                positionToSpawn = new Vector2(-4.9f, Random.Range(minV, maxV));
                attentionSignBehaviour.enemyDirection = Constants.Directions.V;
                break;
            case "W":
                positionToSpawn = new Vector2(Random.Range(minW, maxW), 6.14f);
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
        yield return new WaitForSeconds(spawnPowerUps);
        Instantiate(powerUps[Random.Range(0, 3)], new Vector2(Random.Range(minX, maxX)
            , Random.Range(minY, maxY)), Quaternion.identity);
        StartCoroutine(SpawnPowerUps());
    }

    #endregion
}