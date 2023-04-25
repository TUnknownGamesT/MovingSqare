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
        await UniTask.Delay(TimeSpan.FromSeconds(3f));
        
        prepareToAttack = true;
        if (wave < 2)
        {
            int rand = Random.Range(0, 2);
            if (rand == 0)
            {
                StartCoroutine(spawnLaser(wave));
            }
            else
            {
                StartCoroutine(spawnBoomerang());
            }
        }
        else
        {
            int rand = Random.Range(0, 3);
            if (rand == 0)
            {
                StartCoroutine(spawnLaser(wave));
            }
            else if (rand == 1) 
            {
                StartCoroutine(spawnMissle(wave));
            }else
            {
                StartCoroutine(spawnBoomerang());
            }
        }

    }
    
    
    IEnumerator spawnLaser(int nrOfLasers)
    {
        Debug.Log("Spawner");
        if (nrOfLasers > 1)
        {
            for (int i = 0; i < particleLasers.Length; i++)
            {
                if(nrOfLasers==2 && i == 1)
                {
                }
                else { particleLasers[i].SetActive(true); }
            }

            yield return new WaitForSeconds(2f);

            for (int i = 0; i < particleColliders.Length; i++)
            {
                if (nrOfLasers == 2 && i == 1)
                {
                }
                else
                {
                    particleColliders[i].SetActive(true);
                }
            }

            yield return new WaitForSeconds(2f);

            for (int i = 0; i < particleLasers.Length; i++)
            {
                particleLasers[i].SetActive(false);
            }

            for (int i = 0; i < particleColliders.Length; i++)
            {
                particleColliders[i].SetActive(false);
            }
        }
        else
        {
            particleLasers[1].SetActive(true);
            yield return new WaitForSeconds(2f);
            particleColliders[1].SetActive(true);
            yield return new WaitForSeconds(2f);
            particleLasers[1].SetActive(false);
            particleColliders[1].SetActive(false);
        }
        prepareToAttack = false;
        Attack().AttachExternalCancellation(cts.Token);
    }
    
    IEnumerator spawnMissle(int nrOfMissle)
    {
        for (int i = 0; i < nrOfMissle; i++)
        {
            Instantiate(missle, particleLasers[1].transform.position, Quaternion.identity);
        }
        yield return new WaitForSeconds(5f);
        prepareToAttack = false;
        Attack().AttachExternalCancellation(cts.Token);
    }
    
    IEnumerator spawnBoomerang()
    {

        Instantiate(boomerang, particleLasers[1].transform.position, Quaternion.identity).GetComponent<Boomerang>().Instantiate(player);
        
        yield return new WaitForSeconds(5f);
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
