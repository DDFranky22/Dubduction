using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TutorialScript : MonoBehaviour {

	public GUIStyle [] Pages;
	public GUIStyle Prev;
	public GUIStyle Next;

	int i =0;

	void Start(){

	}

	void OnGUI()
	{

				GUI.Label (new Rect (0, 0, Screen.width, Screen.height), "", Pages [i]);
				if (i > 0) {
						if (GUI.Button (new Rect (50, 0, 100, 50), "prev", Next)) {
								i--;
						} 
				}else {
								if (GUI.Button (new Rect (50, 0, 100, 50), "Main Menu", Next)) {
				
										Application.LoadLevel ("MainMenu");
								}
						}
						if (i < 8) {
								if (GUI.Button (new Rect (Screen.width - 100, 0, 100, 50), "next", Next)) {
										i++;
								}
						} else {
								if (GUI.Button (new Rect ((Screen.width/2)-200,(Screen.height/2)-100, 400, 200), "Start Game", Prev)) {

										Application.LoadLevel ("MenuNnetwork");
								}
						}
				}
		
}
