using UnityEngine;

public class StageThree : StateMachineBehaviour
{
    public Transform player;
    public Rigidbody2D rb;
    public BossController bossController;
    public int numberOfLasersBeforeBoomerang;
    
    public int numbersOfLasersHappens;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        rb = animator.GetComponent<Rigidbody2D>();
        bossController = animator.GetComponent<BossController>();
        bossController.inAnimation = false; 
    }
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Mathf.Abs(player.position.x - rb.position.x)<= 0.01f&&bossController.inAnimation == false&&!bossController.nextStage)
        {
            if (numbersOfLasersHappens == numberOfLasersBeforeBoomerang)
            {
                bossController.inAnimation = true;
                numbersOfLasersHappens = 0;
                animator.SetTrigger("Boomerang");
                
            }
            else
            {
                bossController.inAnimation= true;
                Debug.Log(numbersOfLasersHappens);
                numbersOfLasersHappens++;
                bossController.ThirdAttackBegin();
                animator.SetTrigger("AvoidingObstacle");
            }
        }
    }
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
