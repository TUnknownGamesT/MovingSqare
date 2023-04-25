using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BossBehaivour : MonoBehaviour
{
    public int wave,hp;
    public GameObject  missle , boomerang;
    private bool  prepareToAttack;
    private Transform player;
    public Transform[] miniBossPos;
    public Transform[] flankPos;
    public GameObject[] particleLasers;
    public GameObject[] particleColliders, visual;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        prepareToAttack = false;
        StartCoroutine(TimeToAttack());
        
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
        StartCoroutine(TimeToAttack());
    }

    IEnumerator TimeToAttack()
    {
        yield return new WaitForSeconds(3f);
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
    IEnumerator spawnMissle(int nrOfMissle)
    {
        for (int i = 0; i < nrOfMissle; i++)
        {
            Instantiate(missle, particleLasers[1].transform.position, Quaternion.identity);
        }
        yield return new WaitForSeconds(5f);
        prepareToAttack = false;
        StartCoroutine(TimeToAttack());
    }
    IEnumerator spawnBoomerang()
    {

        Instantiate(boomerang, particleLasers[1].transform.position, Quaternion.identity).GetComponent<Boomerang>().Instantiate(player);
        
        yield return new WaitForSeconds(5f);
        prepareToAttack = false;
        StartCoroutine(TimeToAttack());
    }
    
    // Update is called once per frame
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
            Destroy(this.gameObject);
        }
        StartCoroutine(VisualFeedback());
    }
    IEnumerator VisualFeedback()
    {
        
        for (int i = 0; i < 3; i++)
        {
            foreach (var obj in visual)
            {
                obj.GetComponent<SpriteRenderer>().color = Color.red;
            }
            yield return new WaitForSeconds(0.1f);
            foreach (var obj in visual)
            {
                obj.GetComponent<SpriteRenderer>().color = Color.white;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    void Move()
    {
        float step = 1f * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.position.x, 4,-2), step);
    }
}
