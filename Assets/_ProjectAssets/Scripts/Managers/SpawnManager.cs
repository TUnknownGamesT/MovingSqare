using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;


public enum PowerUps
{
    heal,
    speed,
    size
}

public class SpawnManager : MonoBehaviour
{
     [Range(0f, 10f)]
    public float currentLvl = 1f;
    public List<Transform> spawningPoints;
    public List<GameObject> spawnableObjects;
    public List<GameObject> powerUps;
    public GameObject enemyAlertSignPrefab;
    public GameObject coinPrefab;
    public GameObject fullScreenLine;
    public GameStats gameStats;
    
    public float spawnEnemyTimeDelay;
    public float spawnPowerUpsTimeDelay;
    public float spawnMoney;
    
    [Header("Spawn lasers")]
    public float spawnLinesTimeDelay;
    public int linesSimultaneusly;
    public float linesLifeTime;
    
    [Header("Enemies Range")]
    public Vector2 enemySizeRange;
    public Vector2 enemySpeedRange;
    public float spawnObstacleTime;


    [Header("Obstacles")] 
    public List<GameObject> obstacles;
    public Transform obstaclePoint;

    [HideInInspector]
    public float squareStage2;
    [HideInInspector]
    public float circleStage2;
    [HideInInspector]
    public float hexagonStage2;

    [Header("V Values")]
    public float maxV;
    public float minV;
    [Header("W Values")] 
    public float maxW;
    public float minW;
    [Header("E Values")]
    public float maxE;
    public float minE;
    [Header("S Values")]
    public float maxS;
    public float minS;

    [Header("Money Spawner Zone")]
    public float maxX;
    public float minX;
    public float maxY;
    public float minY;
    
    
    
    private Vector2 positionToSpawn;
    private AttentionSignBehaviour attentionSignBehaviour;
    private List<GameObject> availablePowerUps;
    List<FullScreenLine> fullScreenLineObjects = new();


    private void OnEnable()
    {
        BossGameplay.OnBossAppear += StopSpawning;
        BossGameplay.OnBossDisappear += StartSpawning;
        GameManager.onGameOver += StopSpawning;
        AdsManager.onAdFinish += StartSpawning;
        StartCoroutine(LvlUp()); //Pls make it look better
    }

    private void OnDisable()
    {
        BossGameplay.OnBossAppear -= StopSpawning;
        BossGameplay.OnBossDisappear -= StartSpawning;
        GameManager.onGameOver -= StopSpawning;
        AdsManager.onAdFinish -= StartSpawning;
    }

    private void Start()
    {
        InitStats();
        InitPowerUps();
    }

    private void InitStats()
    {
        spawnPowerUpsTimeDelay = gameStats.spawnPowerUpsTime;
        spawnMoney = gameStats.spawnMoneyTime;
        spawnEnemyTimeDelay = gameStats.spawnEnemyTime;

        enemySizeRange = gameStats.enemySizeRange;
        enemySpeedRange = gameStats.enemySpeedRange;
    }

    private void InitPowerUps()
    {
        availablePowerUps = new();
        
        if (PlayerPrefs.HasKey(PowerUps.heal.ToString()))
        {
            availablePowerUps.Add(powerUps[0]);
        }
        
        if (PlayerPrefs.HasKey(PowerUps.size.ToString()))
        {
            availablePowerUps.Add(powerUps[1]);
        }
        
        if (PlayerPrefs.HasKey(PowerUps.speed.ToString()))
        {
            availablePowerUps.Add(powerUps[2]);
        }
        
    }

    public void StopSpawning()
    {
        StopAllCoroutines();

        foreach (var fullScreenLine in fullScreenLineObjects)
        {
            fullScreenLine.DestroyLaser();
        }
        fullScreenLineObjects.Clear();
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnMoney());
        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnLines());
        StartCoroutine(SpawnObstacle());

        if (availablePowerUps.Count != 0)
        {
            StartCoroutine(SpawnPowerUps());
        }
        
        Debug.Log("Start Spawning");
        
    }
    private IEnumerator LvlUp(){
        yield return new WaitForSeconds(5f);
        currentLvl+=0.5f;
    }


    #region SpawnObstacle

    IEnumerator SpawnObstacle()
    {
        yield return new WaitForSeconds(spawnObstacleTime);
        
        Instantiate(obstacles[Random.Range(0, obstacles.Count)], obstaclePoint.position, Quaternion.identity);
        StopSpawning();
    }

    #endregion
    
    #region Spawn fullScreenLines

    private IEnumerator SpawnLines()
    {
        yield return new WaitForSeconds(spawnLinesTimeDelay-currentLvl);

        for (int i = 0; i < linesSimultaneusly; i++)
        {
          fullScreenLineObjects.Add(Instantiate(fullScreenLine, new Vector2(Random.Range(minX, maxX)
              , Random.Range(minY, maxY)), Quaternion.Euler(0, 0, Random.RandomRange(0, 180))).GetComponent<FullScreenLine>());
          
          yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(1.5f);

        foreach (var line in fullScreenLineObjects)
        {
            line.Activate();            
        }

        yield return new WaitForSeconds(linesLifeTime);

        foreach (var line in fullScreenLineObjects)
        {
            line.DestroyLaser();
        }

        fullScreenLineObjects.Clear();
        
        StartCoroutine(SpawnLines());
    }

    #endregion

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
        yield return new WaitForSeconds(spawnEnemyTimeDelay-currentLvl);

        GameObject objectToSpawn = spawnableObjects[Random.Range(0, spawnableObjects.Count)];
        Transform spawnPoint = spawningPoints[Random.Range(0, spawningPoints.Count)];

        objectToSpawn.GetComponent<EnemyBehaviour>().speed = Random.Range(enemySpeedRange.x , enemySpeedRange.y);

        float randomSize = Random.Range(enemySizeRange.x, enemySizeRange.y);
        objectToSpawn.transform.localScale = new Vector2(randomSize, randomSize);
        
        //Set direction, Target and Stage
        SetStage(objectToSpawn);
        // attentionSignBehaviour = Instantiate(enemyAlertSignPrefab, spawnPoint.position, spawnPoint.rotation)
        //     .GetComponent<AttentionSignBehaviour>();
        // SetEnemyDirection(spawnPoint);
        // attentionSignBehaviour.target =
        //     Instantiate(objectToSpawn, positionToSpawn, Quaternion.identity).GetComponent<Transform>();

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
            case "S":
                positionToSpawn = new Vector2(Random.Range(minS, maxS), -6.72f);
                attentionSignBehaviour.enemyDirection = Constants.Directions.S;
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
        yield return new WaitForSeconds(spawnPowerUpsTimeDelay);
        Instantiate(availablePowerUps[Random.Range(0, availablePowerUps.Count-1)], new Vector2(Random.Range(minX, maxX)
            , Random.Range(minY, maxY)), Quaternion.identity);
        StartCoroutine(SpawnPowerUps());
    }

    #endregion
}