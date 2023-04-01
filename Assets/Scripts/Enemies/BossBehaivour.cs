using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BossBehaivour : MonoBehaviour
{
    public int wave;
    public GameObject barrier, miniBossPrefab;
    public Transform spawnPos;
    private bool goLeft, prepareToAttack;
    private GameObject player;
    public Transform[] miniBossPos;
    // Start is called before the first frame update
    void Start()
    {
        goLeft = true;
        prepareToAttack = false;
        //StartCoroutine(TimeToAttack());
        player = GameObject.Find("Player");
        StartCoroutine(SendMiniBoss(2));
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
                Instantiate(miniBossPrefab, miniBossPos[i].position, Quaternion.identity);
            }
        }
        StartCoroutine(SendMiniBoss(newPos));
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
