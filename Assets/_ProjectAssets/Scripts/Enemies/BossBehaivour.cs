using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BossBehaivour : MonoBehaviour
{
    public int wave;
    public GameObject barrier, miniBossPrefab, disc;
    public Transform spawnPos;
    private bool goLeft, prepareToAttack;
    private GameObject player;
    public Transform[] miniBossPos;
    public Transform[] flankPos;
    // Start is called before the first frame update
    void Start()
    {
        goLeft = true;
        prepareToAttack = false;
        //StartCoroutine(TimeToAttack());
        player = GameObject.Find("Player");
        //StartCoroutine(SendMiniBoss(2));
        //StartCoroutine(FlankBoss());
        StartCoroutine(SendDisc());
    }

    IEnumerator spawn()
    {
        yield return new WaitForSeconds(2f);
        prepareToAttack = false;
        Instantiate(barrier, spawnPos.position, Quaternion.identity);
        StartCoroutine(TimeToAttack());
    }
    IEnumerator TimeToAttack()
    {
        yield return new WaitForSeconds(3f);
        prepareToAttack = true;
        StartCoroutine(spawn());
        
    }
    IEnumerator SendMiniBoss(int lastPos)
    {
        
        int newPos = lastPos + Random.Range(-1, 2);
        if (newPos < 0) { newPos = 0; }else if (newPos > 3) { newPos = 3; }
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 4; i++)
        {
            if (i != newPos)
            {
                Vector3 temp = miniBossPos[i].position;
                temp.y = -10f;
                Instantiate(miniBossPrefab, miniBossPos[i].position, Quaternion.identity).GetComponent<MiniBoss>().SetPos(temp, false);
            }
        }
        StartCoroutine(SendMiniBoss(newPos));
    }

    IEnumerator FlankBoss()
    {
        yield return new WaitForSeconds(1f);
        for(int i =0; i < flankPos.Length; i++)
        {
            Vector3 spawn = flankPos[i].position;
            Vector3 temp = flankPos[i].position;
            if (i % 2 == 0)
            {
                temp.x = 3;
                spawn.x = -3;
                Instantiate(miniBossPrefab, spawn, Quaternion.Euler(0, 0, 90)).GetComponent<MiniBoss>().SetPos(temp, true);
            }
            else
            {
                temp.x = -3;
                spawn.x = 3;
                Instantiate(miniBossPrefab,spawn, Quaternion.Euler(0, 0, -90)).GetComponent<MiniBoss>().SetPos(temp, true);
            }
           
        }
    }
    IEnumerator SendDisc()
    {
        yield return new WaitForSeconds(1f);
        int newPos = Random.Range(0, 4);
        Vector3 temp = miniBossPos[newPos].position;
        temp.y = -10f;
        Instantiate(disc, miniBossPos[newPos].position, Quaternion.identity).GetComponent<Disc>().SetPos(temp);
    }
    // Update is called once per frame
    void Update()
    {
        //if (prepareToAttack)
        //{
        //    PrepareToAttack();
        //}
        //else
        //{
        //    Move();
        //}
    }
    void PrepareToAttack()
    {
        if (transform.rotation.z <0 ) {
            transform.Rotate(0, 0, 1, Space.World);
        }
        else
        {
            transform.Rotate(0, 0, -3, Space.World);
        }
        
    }
    void Move()
    {
        float step = 1f * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, 4,-2), step);
    }
}
