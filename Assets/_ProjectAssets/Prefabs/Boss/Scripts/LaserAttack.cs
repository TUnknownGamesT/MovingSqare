using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAttack : StateMachineBehaviour
{
    private BossController _bossController;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _bossController = animator.GetComponent<BossController>();
        _bossController.ActivateLasers();
        _bossController.inAnimation = true;
    }

    // override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
    //     
    // }
    //
    // override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
    //     
    // }

    
}
