using System.Collections;
using UnityEngine;

public class SpawnManagerLvls : Spawner
{
   
   [Header("Lvl Settings")]
   public LvlSettings lvlSettings;
   
   public Vector2 tetrisRange;
   public Transform tetrisSpawnPoint;
   
   
   private GameObject _mazePrefab;
   private float _geometryFiguresSpeed;
   private float _timeBetweenSpawnMaze;
   private bool _lvlOver;
   private float _timeBetweenSpawnTetris;
   private int _tetrisPeacesOnTime;


   protected override void OnEnable()
   {
      base.OnEnable();
      Timer.onCounterEnd += () =>
      {
         _lvlOver = true;
         StopSpawning();
      };
   }
   
   protected override void OnDisable()
   {
      base.OnDisable();
      Timer.onCounterEnd -= () =>
      {
         _lvlOver = true;
         StopSpawning();
      };
   }


   protected override void Start()
   {
      InitLvlStats();
      InitPowerUps();
   }
   

   protected override void InitLvlStats()
   {
      timeBetweenSpawnsGeometricFigures = lvlSettings.timeBetweenSpawnGeometricFigures;
      timeBetweenSpawnPowerUps = lvlSettings.timeBetweenSpawnPowerUps;
      timeBetweenSpawnMoney = lvlSettings.timeBetweenSpawnMoney;
      timeBetweenSpawnLasers = lvlSettings.timeBetweenSpawnLasers;
      linesLife = lvlSettings.linesLife;
      _timeBetweenSpawnMaze = lvlSettings.timeBetweenSpawnMaze;
      _mazePrefab = lvlSettings.obstaclePrefab;
      _geometryFiguresSpeed = lvlSettings.geometricFiguresSpeed;
      Timer.Duration = lvlSettings.lvlDuration;
   }
   
   protected override void InitPowerUps()
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

   public override void StartSpawning()
   {
      if (_lvlOver)
         return;
      
      if (lvlSettings.geometryFigures)
      {
         StartCoroutine(SpawnGeometricFigures());
      }

      if (lvlSettings.lasers)
      {
         StartCoroutine(SpawnLines());
      }
      
      if(lvlSettings.maze)
      {
         _mazePrefab=lvlSettings.obstaclePrefab;
         StartCoroutine(SpawnMaze());
      }

      if (lvlSettings.tetris)
      {
         StartCoroutine(SpawnTetrisPiece());
      }

      StartCoroutine(SpawnMoney());
      StartCoroutine(SpawnPowerUps());
      
      Timer.instance.StartCounter();
   }


   #region Spawn Maze

   IEnumerator SpawnMaze()
   {
      yield return new WaitForSeconds(_timeBetweenSpawnMaze);
      
      Instantiate(_mazePrefab,obstacleSpawnPoint.position,Quaternion.identity);
      ObstacleBehaviour.speed = lvlSettings.obstacleSpeed;
      StopSpawning();
      
   }

   #endregion
   
   #region Spawn fullScreenLines

   public override IEnumerator SpawnLines()
   {
      yield return new WaitForSeconds(timeBetweenSpawnLasers);

      for (int i = 0; i < lvlSettings.linesCount; i++)
      {
         fullScreenLineObjects.Add(Instantiate(laser, new Vector2(Random.Range(minX, maxX)
            , Random.Range(minY, maxY)), Quaternion.Euler(0, 0, Random.RandomRange(0, 180))).GetComponent<FullScreenLine>());
          
         yield return new WaitForSeconds(0.5f);
      }

      yield return new WaitForSeconds(1.5f);

      foreach (var line in fullScreenLineObjects)
      {
         line.Activate();            
      }

      yield return new WaitForSeconds(linesLife);

      foreach (var line in fullScreenLineObjects)
      {
         line.DestroyLaser();
      }

      fullScreenLineObjects.Clear();
        
      StartCoroutine(SpawnLines());
   }

   #endregion

   #region Spawn Money

   protected override IEnumerator SpawnMoney()
   {
      yield return new WaitForSeconds(timeBetweenSpawnMoney);
      Instantiate(coinPrefab, new Vector2(Random.Range(minX, maxX)
         , Random.Range(minY, maxY)), Quaternion.identity);

      StartCoroutine(SpawnMoney());
   }

   #endregion

   #region Spawn Geometric Figures

   //rename this method with the name of the object you are spawning
   protected override IEnumerator SpawnGeometricFigures()
   {
      yield return new WaitForSeconds(timeBetweenSpawnsGeometricFigures);

      GameObject objectToSpawn = geometricFigures[Random.Range(0, geometricFigures.Count)];
      objectToSpawn.GetComponent<EnemyBehaviour>().UpdateSpeedBasedOnFigure(_geometryFiguresSpeed);
      Transform spawnPoint = spawningPoints[Random.Range(0, spawningPoints.Count)];

      //Set direction and Target 
        
      attentionSignBehaviour = Instantiate(enemyAlertSignPrefab, spawnPoint.position, spawnPoint.rotation)
         .GetComponent<AttentionSignBehaviour>();
      SetEnemyDirection(spawnPoint);
      attentionSignBehaviour.target =
         Instantiate(objectToSpawn, positionToSpawn, Quaternion.identity).GetComponent<Transform>();

      StartCoroutine(SpawnGeometricFigures());
   }


   protected override void SetEnemyDirection(Transform spawnedPoint)
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

   protected override IEnumerator SpawnPowerUps()
   {
      yield return new WaitForSeconds(timeBetweenSpawnPowerUps);
      Instantiate(availablePowerUps[Random.Range(0, availablePowerUps.Count-1)], new Vector2(Random.Range(minX, maxX)
         , Random.Range(minY, maxY)), Quaternion.identity);
      StartCoroutine(SpawnPowerUps());
   }

   #endregion

   #region Spawn Tetris

   IEnumerator SpawnTetrisPiece()
   {
      yield return new WaitForSeconds(_timeBetweenSpawnMaze);
     
      ObstacleBehaviour.speed = lvlSettings.obstacleSpeed;
      
      for (int tetrisIndex = 0; tetrisIndex < lvlSettings.tetrisCount; tetrisIndex++)
      {
         Instantiate(lvlSettings.obstaclePrefab,
            new Vector2(Random.Range(tetrisRange.x,tetrisRange.y),tetrisSpawnPoint.position.y)
            ,Quaternion.identity);
         yield return new WaitForSeconds(1f);
      }
      
      yield return new WaitForSeconds(4f);

      StartCoroutine(SpawnTetrisPiece());
   }

   #endregion

}
