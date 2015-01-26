using UnityEngine;
using System.Collections;

public class TileAnimationManager : MonoBehaviour {

	public Animator animator;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void selectedAnimation(bool v){
		animator.SetBool ("selected", v);
	}

	public void terrainAnimation(bool v){
		animator.SetBool ("terrain", v);
	}

}
