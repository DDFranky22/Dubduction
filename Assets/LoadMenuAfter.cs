using UnityEngine;
using System.Collections;

public class LoadMenuAfter : MonoBehaviour {
	//stupid comment is stupid for a reason

	// Use this for initialization
	IEnumerator Start () {
		yield return new WaitForSeconds(10.0f);
		Application.LoadLevel ("MainMenu");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
