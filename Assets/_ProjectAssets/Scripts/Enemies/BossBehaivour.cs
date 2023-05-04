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
    public Transform[] miniBossPos;
    public Transform[] flankPos;
    public GameObject[] particleLasers;
    public GameObject[] particleColliders, visual;
    public Transform startPosition;
    public BossGameplay bossGameplay;

    [HideInInspector]
    public bool lastWave;
    

    [HideInInspector]
    public bool  prepareToAttack;
    private Transform player;
    private Animator _anim;
    private CancellationTokenSource cts;

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance.Player;
        _anim = GetComponent<Animator>(); 
        cts = new CancellationTokenSource();
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
            await UniTask.Delay(TimeSpan.FromSeconds(5f),cancellationToken:cts.Token);
            prepareToAttack = false;
            Attack();
        });
    }
    
    
    private void SpawnBoomerang()
    {
        UniTask.Void(async () =>
        {
            Instantiate(boomerang, particleLasers[1].transform.position, Quaternion.identity).GetComponent<Boomerang>().Instantiate(player);

            await UniTask.Delay(TimeSpan.FromSeconds(8f),cancellationToken:cts.Token);
            prepareToAttack = false;
            Attack();
        });
    }
    
    void Update()
    {
        if (!prepareToAttack)
        {
            Move();
        }
    }
    
    private void OnParticleCollision(GameObject other)
    {
        Destroy(other.transform.parent.gameObject);
        TakeDmg(1);
    }
   
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
    
    [ContextMenu("Take Damage")]
    void TakeDmg()
    {
        hp -= 1;
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
    
    
    private void MoveToInitialPosition()
    {
        LeanTween.moveLocal(gameObject, startPosition.position, 2f)
            .setEaseInCubic().setOnComplete(()=>gameObject.SetActive(false));
    }
    
    
    
    void Move()
    {
        float step = 1f * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.position.x, 3.5f,-2), step);
    }
}
