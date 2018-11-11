using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingToCast : StateMachineBehaviour
{
	public FishingRod fishingRod;
	
  // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    Debug.Log("Entering");

		if(fishingRod == null)
		{
			fishingRod = animator.GetComponent<FishingRod>();
		}
  }

  // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
		// fishingRod.ReelingUpdate();
  }

  // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
  override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    Debug.Log("Leaving");
    animator.SetBool("isCast", false);
  }
}
