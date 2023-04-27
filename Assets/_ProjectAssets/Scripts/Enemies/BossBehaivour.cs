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
    public GameObject  missle , boomerang;
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
        Attack().AttachExternalCancellation(cts.Token);
    }

    public void SetBossStatus(int hp)
    {
        this.hp = hp;
    }



    async UniTask Attack()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(4f));
        
        prepareToAttack = true;
        if (wave < 2)
        {
            int rand = Random.Range(0, 3);
            if (rand >1)
            {
                SpawnLaser(wave).AttachExternalCancellation(cts.Token);
            }
            else
            {
                SpawnBoomerang().AttachExternalCancellation(cts.Token);
            }
        }
        else
        {
            int rand = Random.Range(0, 3);
            if (rand == 0)
            {
                SpawnLaser(wave).AttachExternalCancellation(cts.Token);
            }
            else if (rand == 1) 
            {
                SpawnMissle(wave).AttachExternalCancellation(cts.Token);
            }
            else
            {
                SpawnBoomerang().AttachExternalCancellation(cts.Token);
            }
        }

    }
    async UniTask SpawnLaser(int nrOfLasers)
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
        await UniTask.Delay(TimeSpan.FromSeconds(2f));
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
        await UniTask.Delay(TimeSpan.FromSeconds(2f));
        for (int i = 0; i < particleLasers.Length; i++)
        {
            particleLasers[i].SetActive(false);
            particleColliders[i].SetActive(false);
        }

        prepareToAttack = false;
        Attack().AttachExternalCancellation(cts.Token);
    }
    async UniTask SpawnMissle(int nrOfMissle)
    {
        for (int i = 0; i < nrOfMissle; i++)
        {
            Instantiate(missle, particleLasers[1].transform.position, Quaternion.identity);
        }
        await UniTask.Delay(TimeSpan.FromSeconds(5f));
        prepareToAttack = false;
        Attack().AttachExternalCancellation(cts.Token);
    }
    async UniTask SpawnBoomerang()
    {
        Instantiate(boomerang, particleLasers[1].transform.position, Quaternion.identity).GetComponent<Boomerang>().Instantiate(player);

        await UniTask.Delay(TimeSpan.FromSeconds(8f));
        prepareToAttack = false;
        Attack().AttachExternalCancellation(cts.Token);
    }
    
    void Update()
    {
        if (!prepareToAttack)
        {
            Move();
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "projectile")
        {
            Debug.Log("ive got hit my little bitch");
        }
        else
        {
            Debug.Log("my tag is : " + collision.gameObject.tag);
        }
    }
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("particle "+other.transform.parent.gameObject.name);
        Destroy(other.transform.parent.gameObject);
        TakeDmg(1);
    }
   
    void TakeDmg(int value)
    {
        hp -= value;
        if (hp < 1)
        {
            if (lastWave)
            {
                Destroy(gameObject);
            }
            else
            {
               StopAllCoroutines();
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
                bossGameplay.finishedWave = true;
                StopAllCoroutines();
                cts.Cancel();
                prepareToAttack = false;
                MoveToInitialPosition();
                wave++;
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
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.position.x, 4,-2), step);
    }
}
