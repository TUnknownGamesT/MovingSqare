using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    [SerializeField] private GameObject trail;
    private Vector2 destination;
    private Transform player;
    public Transform temp;
    
    // Start is called before the first frame update
    void Start()
    {
        trail.transform.LookAt(destination);
    }
    public void Instantiate(Transform player)
    {
        this.player = player;
        StartCoroutine(wait(5,1));
    }
    private void Temp()
    {
        this.player = temp;
        StartCoroutine(wait(5,1));
    }
    
    IEnumerator wait(int time, int turn)
    {
        yield return new WaitForSeconds(time);
        SetDirection(turn);
    }
    
    IEnumerator Kill()
    {
        yield return new WaitForSeconds(4);
        Destroy(this.gameObject);
    }
    
    private void SetDirection(int turn)
    {
        
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, player.position - this.transform.position, Mathf.Infinity, 1 << LayerMask.NameToLayer("MovingZone"));
        if (hit.collider != null)
        {
            destination = hit.point;
            if(destination.y < -2.5f)
            {
                destination.y = -2.5f;
            }
            if(destination.x > 2.1f)
            {
                destination.x = 2.1f;
            }else if (destination.x < -2.1f)
            {
                destination.x = -2.1f;
            }
            
            trail.transform.LookAt(destination);
            LeanTween.move(this.gameObject, destination, 4f).setEase(LeanTweenType.easeInOutSine);
            
            StartCoroutine(turn > 1 ? Kill() : wait(4, 2));
        }
        else
        {
            Debug.Log("did not hit anything");
        }
        
    }
}
