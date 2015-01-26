using UnityEngine;
using System.Collections;

public class AlienAnimationManager : MonoBehaviour {

	public Animator animator;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void idleAnimation(bool v){
		animator.SetBool ("idle", v);
	}


	public void attackAnimation(bool v){
		animator.SetBool ("attack", v);
	}

	public void hitAnimation(bool v){
		animator.SetBool ("hit", v);
	}

	public void arriveAnimation(bool v){
		animator.SetBool ("arrive", v);
	}

	public void winAnimation(bool v){
		animator.SetBool ("win", v);
	}

	public void loseAnimation(bool v){
		animator.SetBool ("lose", v);
	}

}
