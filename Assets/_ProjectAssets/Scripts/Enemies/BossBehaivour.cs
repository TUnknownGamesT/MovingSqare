using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using Random = UnityEngine.Random;

public class BossBehaivour : MonoBehaviour
{
    public int wave,hp;
    public GameObject hpPanel , missle , boomerang;
    public GameObject[] particleLasers;
    public GameObject[] particleColliders, visual;
    public Transform startPosition;
    public BossGameplay bossGameplay;

    [Header("Boss Visual Components")] 
    public Transform whiteLine;
    public Transform gunn;
    
    [HideInInspector]
    public bool lastWave;
    

    [HideInInspector]
    public bool  prepareToAttack;
    private Transform player;
    private Animator _anim;
    private CancellationTokenSource cts;
    
    
    
    void Start()
    {
        player = GameManager.instance.Player;
        _anim = GetComponent<Animator>(); 
        cts = new CancellationTokenSource();
    }
    
    void Update()
    {
        if (!prepareToAttack)
        {
            Move();
        }
    }

    public void InitBoss()
    {
        cts = new CancellationTokenSource();
        Attack();
    }
    
    public void SetBossStatus(int hp)
    {
        this.hp = hp;

        for(int i =0; i < hp; i++)
        {
            hpPanel.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    
    #region Movement

    void Move()
    {
        float step = 1f * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.position.x, 3.5f,-2), step);
    }
    
    private void MoveToInitialPosition()
    {
        LeanTween.moveLocal(gameObject, startPosition.position, 2f)
            .setEaseInCubic().setOnComplete(()=>gameObject.SetActive(false));
    }

    #endregion


    #region Attacks

     private  void Attack()
    {
        UniTask.Void(async () =>
        {
            await UniTask.Delay(TimeSpan.FromSeconds(4f),cancellationToken:cts.Token);
        
            prepareToAttack = true;
            if (wave < 2)
            {
                int rand = Random.Range(0, 3);
                if (rand >1)
                {
                    SpawnLaser(wave);
                }
                else
                {
                    SpawnBoomerang();
                }
            }
            else
            {
                int rand = Random.Range(0, 3);
                if (rand == 0)
                {
                    SpawnLaser(wave);
                }
                else if (rand == 1) 
                {
                    SpawnMissle(wave);
                }
                else
                {
                    SpawnBoomerang();
                }
            }
        });
    }
    private  void  SpawnLaser(int nrOfLasers)
    {
        UniTask.Void(async () =>
        {
            if (nrOfLasers >= 2)
            {
                //spawn the first and last laser
                particleLasers[0].SetActive(true);
                particleLasers[2].SetActive(true);
            }
            if (nrOfLasers == 3 || nrOfLasers == 1)
            {
                particleLasers[1].SetActive(true);//spawn the laser from middle
            }
            
            await UniTask.Delay(TimeSpan.FromSeconds(2f),cancellationToken:cts.Token);
            
            if (nrOfLasers >= 2)
            {
                //spawn the first and last laser collider
                particleColliders[0].SetActive(true);
                particleColliders[2].SetActive(true);
            }
            if (nrOfLasers == 3 || nrOfLasers == 1)
            {
                particleColliders[1].SetActive(true);
            }
            await UniTask.Delay(TimeSpan.FromSeconds(2f),cancellationToken:cts.Token); 
            
            for (int i = 0; i < particleLasers.Length; i++)
            {
                particleColliders[i].SetActive(false);
            }
            await UniTask.Delay(TimeSpan.FromSeconds(0.7f), cancellationToken: cts.Token);
            for (int i = 0; i < particleLasers.Length; i++)
            {
                particleLasers[i].SetActive(false);
            }
            prepareToAttack = false;
            Attack();
        });
    }
    
    
    private void SpawnMissle(int nrOfMissle)
    {
        UniTask.Void(async () =>
        {
            for (int i = 0; i < nrOfMissle; i++)
            {
                Instantiate(missle, particleLasers[1].transform.position, Quaternion.identity);
            }

            LeanTween.moveLocalY(gunn.gameObject, 4.016f, 2f).setEaseInCubic().setOnComplete(() =>
            {
                LeanTween.moveLocalY(gunn.gameObject, 3.82f, 1f).setEaseInCubic();
            });
            
            await UniTask.Delay(TimeSpan.FromSeconds(5f),cancellationToken:cts.Token);
            prepareToAttack = false;
            Attack();
        });
    }
    
    
    private void SpawnBoomerang()
    {
        UniTask.Void(async () =>
        {
            ChargeEffect();
            
            await UniTask.Delay(TimeSpan.FromSeconds(1f),cancellationToken:cts.Token);
            
            Instantiate(boomerang, particleLasers[1].transform.position, Quaternion.identity).GetComponent<Boomerang>().Instantiate(player);

            await UniTask.Delay(TimeSpan.FromSeconds(4f),cancellationToken:cts.Token);

            ChargeBoomerangAttack();
           
            await UniTask.Delay(TimeSpan.FromSeconds(3f),cancellationToken:cts.Token);
            
            prepareToAttack = false;
            Attack();
        });
    }


    #endregion


    #region HpManagement

    void TakeDmg(int value)
    {
        hpPanel.transform.GetChild(hp-1).gameObject.SetActive(false);
        hp -= value;
        if (hp < 1)
        {
            if (lastWave)
            {
                Destroy(gameObject);
            }
            else
            {
                LoseAllHP();
            }
        }
        _anim.SetTrigger("TakeDmg");
    }

    private void LoseAllHP()
    {
        wave++;
        bossGameplay.finishedWave = true;
        
        //Stop Ready To Attack
        cts.Cancel();
        cts.Dispose();
        cts = new CancellationTokenSource();
        prepareToAttack = false;
        
        MoveToInitialPosition();
       
    }


    #endregion


    #region Visual

    private void ChargeEffect()
    {
        LeanTween.moveLocalY(whiteLine.gameObject, 3.99f, 1f).setEaseInCubic().setOnComplete(() =>
        {
            LeanTween.moveLocalY(whiteLine.gameObject, 4.28f, 4f).setEaseInCubic().setOnComplete(() =>
            {
                LeanTween.moveLocalY(whiteLine.gameObject, 4f, 0.2f).setEaseInCubic()
                    .setDelay(0.8f).setOnComplete(() =>
                    {
                        LeanTween.moveLocalY(whiteLine.gameObject, 4.14f, 0.2f).setEaseInCubic();
                    });
            });
            
        });
    }

    private void ChargeBoomerangAttack()
    {
        LeanTween.moveY(gameObject, transform.position.y + 0.5f, 0.8f).setEaseInQuad().setOnComplete(() =>
        {
            LeanTween.moveY(gameObject, transform.position.y - 0.5f, 0.2f).setEaseInQuint();
        });

    }

    #endregion
   
    private void OnParticleCollision(GameObject other)
    {
        Destroy(other.transform.parent.gameObject);
        TakeDmg(1);
    }
    
    
}
