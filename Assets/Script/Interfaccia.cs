using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Interfaccia : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Inizio()
	{
		Application.LoadLevel ("MenuNnetwork");  
	}

	public void Fine(){
		Application.Quit ();
	}

	public void Back(){
		GameObject manager = GameObject.Find ("GameManager");
		Destroy (manager);
		Application.LoadLevel ("MainMenu");
	}

	public void StartServer(){
		MPBase scriptMp = Camera.main.GetComponent<MPBase> ();
		scriptMp.StartServer ();
	}

	public void StartClient(){
		MPBase scriptMp = Camera.main.GetComponent<MPBase> ();
		//scriptMp.showClient = true;
		scriptMp.StartClient ();
	}

	public void changeIp(string value){
		MPBase scriptMp = Camera.main.GetComponent<MPBase> ();
		scriptMp.connectToIp = value;
	}

	public void goToTutorial(){
		Application.LoadLevel ("Tutorial");
	}
}
