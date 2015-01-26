using UnityEngine;
using System.Collections;

public class CivilianAnimationManager : MonoBehaviour {

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

	public void abductionAnimation(bool v){
		animator.SetBool ("abduction", v);
	}

}
