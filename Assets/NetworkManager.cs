using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {
	
	bool Hello = false;
	
	bool Alien = true;
	
	private Manager manager;

	public Alien alienObject;
	public GameObject[] game;
	
	void Start () {
		DontDestroyOnLoad(gameObject);
		manager = Manager.istance;
		alienObject = GameObject.Find ("Alien").GetComponent<Alien>();
	}
	
	
	
	
	[RPC]
	void CallOnlineSwap(int a, int b)
	{
		manager.Swap(a,b);
	}
	
	[RPC]
	void KillOnlineCivilian(int t)
	{
		manager.CivilDead(t);
	}
	
	[RPC]
	void DisableArmyOnline(int t)
	{
		manager.DisarmSoldier(t);
	}
	
	[RPC]
	void BlockUnavaible(int t)
	{
		manager.SetBlock(t);
	}
	
	[RPC]
	void moveAlienHuman(Vector3 target)
	{
		alienObject.moveAlien (target);
	}

	[RPC]
	void decreseLife(){
		manager.vite--;
	}

	[RPC]
	void checkVictoryManager(){
		manager.checkVictory();
	}

	[RPC]
	void checkFlagOnline(){
		manager.checkFlag();
	}

	public void CallSwap(int a, int b)
	{
		networkView.RPC("CallOnlineSwap", RPCMode.All,a,b);
	}
	
	public void KillCivilian(int t)
	{
		networkView.RPC("KillOnlineCivilian", RPCMode.All, t);
	}
	
	public void ArmyCooldown(int t)
	{
		networkView.RPC("DisableArmyOnline", RPCMode.All, t);
	}
	
	public void blockSlot(int t)
	{
		networkView.RPC("BlockUnavaible", RPCMode.All, t);
	}

	public void moveClientAlien(Vector3 t){
		Vector3 target = new Vector3 (t.x, alienObject.transform.position.y, 0);
		networkView.RPC ("moveAlienHuman", RPCMode.Others, target);
	}

	public void decreseAlienLife(){
		networkView.RPC ("decreseLife", RPCMode.All);
	}

	public void checkFuckingVictory(){
		networkView.RPC ("checkVictoryManager",RPCMode.All);
	}

	public void checkFlag(){
		networkView.RPC ("checkFlagOnline", RPCMode.All);
	}

}
