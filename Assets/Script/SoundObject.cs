using UnityEngine;
using System.Collections;

public class SoundObject : MonoBehaviour {

	public AudioSource sourceMusica;
	public GameObject so;
	bool only =  true;
	public AudioClip[] clips;
	public bool changed = false;
	// Use this for initialization
	void Start () {
		so = GameObject.Find ("SoundObject");
		if (so != this.gameObject) {
			only = false;
		}
		sourceMusica.clip = clips[0];
		//sourceMusica = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (only)
			DontDestroyOnLoad (this.gameObject);
		else
			Destroy (this.gameObject);
		if (Application.loadedLevelName== "NetworkGameScene" && !changed) {
			sourceMusica.Stop();
			sourceMusica.clip = clips[1];
			sourceMusica.PlayDelayed(7.0f);
			changed = true;
		}
	}
}
