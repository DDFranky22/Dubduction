using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour 
{
	public GUIStyle GameGUI;

	public int vite = 3;
	public int nCivili = 3;
	float time = 0;
	float tempoRound = 10;
	
	private bool ExecutingActions = false;
	
	public bool victoryAlien = false;
	public bool victoryHuman = false;
	
	public int turn = 1;
	
	public AudioSource ring;
	
	
	public bool matchStarted = false;
	
	public NetworkManager networkManager;
	public bool alien;
	
	public int[] exchangeSlot;
	public byte aux{get;set;}
	
	public int target;
	
	public Vector3[] posizioni;
	public GameObject[] game;
	public int[] flags;
	bool endSetup = false;
	
	
	#region Singleton
	
	public static Manager controller; //un puntatore statico ad un oggetto della classe LaikaDeath (per Singleton)
	
	/*
	 * Un singleton per la classe LaikaDeath, visto che nel gioco sarà attivo solo e soltanto un oggetto di tale classe.
	 * */
	public static Manager istance
	{
		get 
		{
			if (controller == null) 
			{
				controller = FindObjectOfType(typeof(Manager)) as Manager;
			}
			return controller;
		}
	}
	
	#endregion
	
	// Use this for initialization
	IEnumerator Start () 
	{
		exchangeSlot = new int[2];
		posizioni = new Vector3[5];
		game = new GameObject[5];
		flags = new int[5];
		
		for (int i = 0; i < 5; i++)
		{
			flags[i] = 0;
		}
		
		target = -1;
		
		GameObject pos = GameObject.Find("GameManager");
		networkManager = gameObject.GetComponent<NetworkManager>();
		posizioni[0] = pos.transform.GetChild(0).transform.position;
		posizioni[1] = pos.transform.GetChild(1).transform.position;
		posizioni[2] = pos.transform.GetChild(2).transform.position;
		posizioni[3] = pos.transform.GetChild(3).transform.position;
		posizioni[4] = pos.transform.GetChild(4).transform.position;
		
		game[0] = pos.transform.GetChild(0).gameObject;
		game[1] = pos.transform.GetChild(1).gameObject;
		game[2] = pos.transform.GetChild(2).gameObject;
		game[3] = pos.transform.GetChild(3).gameObject;
		game[4] = pos.transform.GetChild(4).gameObject;
		
		game[0].GetComponent<Uman>().currentState = Uman.humanState.human;
		game[1].GetComponent<Uman>().currentState = Uman.humanState.army;
		game[2].GetComponent<Uman>().currentState = Uman.humanState.human;
		game[3].GetComponent<Uman>().currentState = Uman.humanState.army;
		game[4].GetComponent<Uman>().currentState = Uman.humanState.human;
		networkManager.game = game;

		endSetup = false;
		//preparazione + animazioni entrata
		if(!endSetup){
			GameObject.Find("Alien").GetComponent<Alien>().Intro();
			game[0].GetComponent<Uman>();
			yield return new WaitForSeconds(7.0f);
			endSetup = true;
		}
		//fine animazioni

		time = 5.9f;
		while (true)
		{
			if (time <= 0 && matchStarted)
			{
				ExecutingActions = true;
				if (aux == 2)
				{
					networkManager.CallSwap(exchangeSlot[0], exchangeSlot[1]);
					
				}
				
				yield return new WaitForSeconds(0.2f);
				
				if (alien && target != -1)
				{
					networkManager.moveClientAlien(game[target].GetComponent<Uman>().target);
					yield return new WaitForSeconds(3.0f);
					Attack(target);
				}
				else if((!alien)){
					yield return new WaitForSeconds(3.0f);
				}
				else if (alien && target == -1)
				{
					//perde una vita
					//networkManager.decreseAlienLife();
					yield return new WaitForSeconds(3.0f);
				}
				time = 5.9f;
				//audio.Play();
				turn++;

				if(alien)
					networkManager.checkFlag();
				yield return new WaitForSeconds(0.1f);
				if(alien)
					networkManager.checkFuckingVictory();
				yield return new WaitForSeconds(0.1f);
				
				ExecutingActions = false;
			}
			
			yield return null;
		}
		
	}

	public void Import(int uman)
	{
		exchangeSlot[aux] = uman;
		aux++;
	}
	
	public void Deport(byte number)
	{
		if (number == 1)
		{
			exchangeSlot[0] = exchangeSlot[1];
			exchangeSlot[1] = 0;
			aux = 1;
		}
		else if (number == 0)
		{
			exchangeSlot[0] = 0;
			aux = 0;
		}
	}
	
	public void Swap(int a, int b)
	{
		GameObject temp;
		int aux2;
		
		temp = game[a];
		
		aux2 = game[a].GetComponent<Uman>().pos;
		game[a].GetComponent<Uman>().pos = game[b].GetComponent<Uman>().pos;
		game[b].GetComponent<Uman>().pos=aux2;
		
		game[a]=game[b];
		game[b]=temp;
		
		//game[b].transform.position = posizioni[b];
		//game[a].transform.position = posizioni[a];
		Vector3 targetA = game [b].transform.position;
		Vector3 targetB = game [a].transform.position;
		
		game [a].GetComponent<Uman> ().moveHuman (new Vector3 (targetA.x,targetA.y,0.0f));
		game [b].GetComponent<Uman> ().moveHuman (new Vector3 (targetB.x,targetB.y,0.0f));
		
		
		game[b].GetComponent<Uman>().Disable();
		game[a].GetComponent<Uman>().Disable();
		
		
	}
	
	
	
	void Update()
	{
		if (matchStarted && !ExecutingActions && endSetup) 
		{
			time -= Time.deltaTime;
		}
		if (Application.loadedLevelName == "MainMenu") {
			Destroy(this.gameObject);		
		}	
	}
	
	
	
	void OnGUI()
	{
		GUI.Label(new Rect(0, 0, 100, 50), "Time : "+((int)time),GameGUI);
		GUI.Label(new Rect(0, 30, 100, 50), "Alieno - vite : " +vite,GameGUI);
		GUI.Label(new Rect(80, 30, 100, 50), "Umano : " + nCivili,GameGUI);
		GUI.Label(new Rect(0, 60, 100, 50), "Turni : " + turn,GameGUI);
		
	}
	
	public void Attack(int t)
	{
		
		switch(game[t].GetComponent<Uman>().currentState)
		{
		case Uman.humanState.empty: 
		{
			if (alien)
			{
				networkManager.blockSlot(t);
			}
			
		} 
			break;
		case Uman.humanState.human: 
		{
			//diventa vuoto e muore il civile
			if (alien)
			{
				networkManager.KillCivilian(t);
			}
			
		} 
			break;
		case Uman.humanState.army: 
		{
			//l'alieno perde una vita
			if (alien)
			{
				networkManager.ArmyCooldown(t);                    
			}
			
		} 
			break;
		case Uman.humanState.noArmy: 
		{
			//diventa vuoto e muore il militare
			if (alien)
			{
				networkManager.KillCivilian(t);
			}
			
		} 
			break;
		case Uman.humanState.block: 
		{
			if (alien)
			{
				networkManager.blockSlot(t);
			}
		} 
			break;
			
		}
		
		
		
		game[t].transform.localScale = game[t].GetComponent<Uman>().scale;
		
		target = -1;
		
		
		
		
	}
	
	public void CivilDead(int p)
	{
		//
		game[p].GetComponent<Uman>().currentState = Uman.humanState.empty;
		game [p].GetComponent<Uman> ().Kill (0);
		nCivili--;
	}
	
	public void checkVictory()
	{
		if (vite <= 0)
		{
			victoryHuman = true;
		}
		
		if (nCivili <= 0)
		{
			victoryAlien = true;
		}
		if (turn == 9 && !victoryAlien)
		{
			victoryHuman = true;
		}
		if (victoryAlien){
			if(alien)
				Application.LoadLevel("WinAlien");		
			else
				Application.LoadLevel("LoseHumanity");	
		}
		else if(victoryHuman){
			if(alien)
				Application.LoadLevel("LoseAlien");	
			else
				Application.LoadLevel("WinHumanity");	
		}
	}
	
	public void checkFlag()
	{
		for (int i = 0; i < flags.Length; i++)
		{
			if (flags[i] > 0)
			{
				if (game[i].GetComponent<Uman>().currentState == Uman.humanState.noArmy)
				{
					flags[i] -= 1;
				}
				
				if (game[i].GetComponent<Uman>().currentState == Uman.humanState.block)
				{
					flags[i] -= 1;
				}
				
				
			}
			
			if (flags[i] == 0)
			{
				if (game[i].GetComponent<Uman>().currentState == Uman.humanState.noArmy)
				{
					game[i].GetComponent<Uman>().currentState = Uman.humanState.army;
					game[i].GetComponent<Uman>().Reload();
				}
				
				if (game[i].GetComponent<Uman>().currentState == Uman.humanState.block)
				{
					game[i].GetComponent<Uman>().currentState = Uman.humanState.empty;
				}
			}
			
		}
	}
	
	/*         */
	public void DisarmSoldier(int t)
	{
		game[t].GetComponent<Uman>().currentState = Uman.humanState.noArmy;
		game [t].GetComponent<Uman> ().Kill (1);
		vite--;
		flags[t]=2;
	}
	
	public void SetBlock(int t)
	{
		game[t].GetComponent<Uman>().currentState = Uman.humanState.block;
		game [t].GetComponent<Uman> ().Land ();
		flags[t]=2;        
	}
}

