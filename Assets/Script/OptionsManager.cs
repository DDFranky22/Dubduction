using UnityEngine;
using System.Collections;

public class OptionsManager : MonoBehaviour {

	private float SFX;
	private float music;

	// Use this for initialization
	void Start () {
		if(PlayerPrefs.HasKey("SFX")){
			SFX = PlayerPrefs.GetFloat("SFX");
		}
		else{
			SFX = 1.0f;
		}
		if (PlayerPrefs.HasKey ("music")) {
			music = PlayerPrefs.GetFloat("music");		
		}
		else{
			music = 1.0f;
		}
	}

	public void setSFX(float v){
		SFX = v;
	}

	public float getSFX(){
		return SFX;
	}

	public void setMusic(float v){
		music = v;
	}

	public float getMusic(){
		return music;
	}

}
