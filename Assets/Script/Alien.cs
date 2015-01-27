using UnityEngine;
using System.Collections;

public class Alien : MonoBehaviour {

	public Vector3 preGame;
	public Vector3 starting;
	public bool intro;

	public AlienAnimationManager anim;
	public AudioSource source;

	public Vector3 target;
	public bool move;

	// Use this for initialization, test for phpstorm
	void Start () {
	}


	public void moveAlien(Vector3 target){
		this.target = target;
		move = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (move) {
			this.transform.position = Vector3.MoveTowards(this.transform.position,target,Time.deltaTime*5.0f);
		}
		if(this.transform.position.Equals(target)){
			move = false;
		}
		if (intro) {
			this.transform.position = Vector3.MoveTowards(this.transform.position,starting,Time.deltaTime*1.0f);
		}
		if(this.transform.position.Equals(starting)){
			intro = false;
		}
	}

	public void Intro(){
		anim.arriveAnimation (true);
		intro = true;
		source.Play ();
	}
}
