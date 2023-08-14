using System;
using System.Collections;
using UnityEngine;

public class Boomerang : MonoBehaviour
{

    public static Action onBoomerangHit;
    
   
    private Vector2 destination;
    private Transform player;
    public LayerMask[] RayCastLayer;
    // Start is called before the first frame update
    
    public void Instantiate(Transform player)
    {
        this.player = player;
        StartCoroutine(wait(5,0));
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
        
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, player.position - this.transform.position, Mathf.Infinity, RayCastLayer[turn]);
        if (hit.collider != null)
        {
            destination = hit.point;
            LeanTween.move(this.gameObject, destination, 1.8f).setEase(LeanTweenType.linear).setOnComplete(() =>
            {
                 onBoomerangHit?.Invoke();
            });
            
            StartCoroutine(turn >0 ? Kill() : wait(4, 1));
        }
        
    }
}
