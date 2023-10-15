using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "LvlSettings", menuName = "ScriptableObjects/LvlSettings", order = 1)]
public class LvlSettings : ScriptableObject
{
   [Header("Obstacles")]
   public bool geometryFigures;
   public bool lasers;
   public bool maze;
   public bool tetris;
   public bool tetrisBoss;
   public bool lvl10Boss;

   [Header("Spawn Time")]
   public float timeBetweenSpawnGeometricFigures;
   public float timeBetweenSpawnLasers;
   public float timeBetweenSpawnPowerUps;
   public float timeBetweenSpawnMoney;
   public float timeBetweenSpawnMaze;
   public float timeBetweenSpawnTetrisEnemies;
   
   [Header("Duration")]
   public float linesLife;
   public float powerUpsLife;
   public float moneyLife;
   public float lvlDuration;
   
   [Header("Obstacle Speed")]
   public float geometricFiguresSpeed;
   public float obstacleSpeed;
   public float tetrisEnemiesSpeed;

   [Header("Obstacle Count")] 
   public int linesCount;
   public int tetrisCount;
   
   public GameObject obstaclePrefab;

}
