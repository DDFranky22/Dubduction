using UnityEngine;
using System.Collections;

public class BackgroundScript : MonoBehaviour {

	public bool only= true;

	// Use this for initialization
	void Start () {
		GameObject back = GameObject.Find ("Background");
		if (this.gameObject != back) {
			only = false;		
		}
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!only) {
			Destroy (this.gameObject);		
		}
		else{
			DontDestroyOnLoad(this.gameObject);
		}
	}
}
