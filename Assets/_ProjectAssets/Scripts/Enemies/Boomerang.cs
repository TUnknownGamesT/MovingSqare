using System.Collections;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    [SerializeField] private GameObject trail;
    private Vector2 destination;
    private Transform player;
    public Transform temp;
    public LayerMask[] RayCastLayer;
    // Start is called before the first frame update
    void Start()
    {
        trail.transform.LookAt(destination);
    }
    public void Instantiate(Transform player)
    {
        this.player = player;
        StartCoroutine(wait(5,0));
    }
    private void Temp()
    {
        this.player = temp;
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
            trail.transform.LookAt(destination);
            LeanTween.move(this.gameObject, destination, 3f).setEase(LeanTweenType.easeInOutSine);
            
            StartCoroutine(turn >0 ? Kill() : wait(4, 1));
        }
        
    }
}
