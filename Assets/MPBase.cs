using UnityEngine;
using System.Collections;

public class MPBase : MonoBehaviour {
	public float XSGS = 2.2f;
	public float YSGS= 1.4f;
	public float XSIPL= 3.2f;
	public float YSIPL= 1.4f;
	public float XSIP= 1.96f;
	public float YSIP= 1.4f;
	public float XIPL= 2.77f;
	public float YIPL= 1.27f;
	public float XC= 2.2f;
	public float YC= 1.4f;


	public GameObject manager;
	
	public string connectToIp =  "127.0.0.1";
	public int connectPort = 25000;
	public bool useNAT = false;
	public string ipAddress = "";
	public string port = "";
	
	public GUIStyle Background;
	public GUIStyle ConnectButton;
	public GUIStyle StartServerButton;
	
	public GUIStyle TextName;
	public GUIStyle TextIP;
	
	//to edit
	public GUIStyle ConnectionData;
	public GUIStyle DisconnectButton;
	
	
	
	public GUIStyle ReadyButton;
	public GUIStyle BottomMenuButton;
	
	public GUIStyle TextPort;
	
	public GUIStyle LastBox;
	public GUIStyle TitleStyle;
	private RectOffset rectofs;
	
	private bool Ready;
	private bool OpponentReady;
	
	private bool Starting = false;
	
	private int StartCounter = 5;
	
	string playerName = "<NAME_ME>";

	private GameObject GameManager;

	public bool showClient;

	public void Start(){
		   XSGS = 2.2f;
		   YSGS= 1.4f;
		   XSIPL= 3.2f;
		   YSIPL= 1.4f;
		   XSIP= 1.96f;
		   YSIP= 1.4f;
		   XIPL= 2.77f;
		   YIPL= 1.27f;
		   XC= 2.2f;
		   YC= 1.4f;

		GameManager = GameObject.Find ("GameManager");
		GameManager.SetActive (false);
	}

	public void StartServer(){
		Manager.istance.alien = true; 
		//if(playerName != "<NAME ME>")
		//{
		Network.useNat = useNAT;
		Network.InitializeServer(32,connectPort);
		foreach( GameObject go in FindObjectsOfType(typeof(GameObject)))
		{
			go.SendMessage("OnNetworkLoadedLevel", SendMessageOptions.DontRequireReceiver);
		}
		
		PlayerPrefs.SetString("Server", playerName);
		PlayerPrefs.SetString("ServerPlayer", playerName);
		PlayerPrefs.SetString("ClientPlayer", null);
		//networkView.RPC("SetReady", RPCMode.Others);
		
		//}
	}
	
	public void StartClient(){
		//if(playerName != "<NAME ME>")
		//{
		Network.useNat = useNAT;
		Network.Connect(connectToIp, connectPort);
		PlayerPrefs.SetString("Client", playerName);
		PlayerPrefs.SetString("ServerPlayer", null);
		PlayerPrefs.SetString("ClientPlayer", playerName);
		//networkView.RPC("SetReady", RPCMode.Others);
		//}
	}

	void OnGUI()
	{
		
		if(!Starting){
			
			GUI.Box(new Rect(0,0,Screen.width,Screen.height), "",Background);
			/* INSERIRE NOMEGIOCO
             * GUI.Label(new Rect(Screen.width/2-350,30,700,100),"Field of War: Play Mode",TitleStyle);
             */
			if (Network.peerType == NetworkPeerType.Disconnected)
			{
				
				/*if(GUI.Button(new Rect(Screen.width/2,Screen.height/2,400,50),"Connect",ConnectButton))
				{
					if(playerName != "<NAME ME>")
					{
						Network.useNat = useNAT;
						Network.Connect(connectToIp, connectPort);
						PlayerPrefs.SetString("Client", playerName);
						PlayerPrefs.SetString("ServerPlayer", null);
						PlayerPrefs.SetString("ClientPlayer", playerName);
					}
				}*/
				
				/*if(GUI.Button(new Rect(Screen.width/2-250,Screen.height/2,400,50),"Start Server",StartServerButton))
				{
					Manager.istance.alien = true; 
					if(playerName != "<NAME ME>")
					{
						Network.useNat = useNAT;
						Network.InitializeServer(32,connectPort);
						foreach( GameObject go in FindObjectsOfType(typeof(GameObject)))
						{
							go.SendMessage("OnNetworkLoadedLevel", SendMessageOptions.DontRequireReceiver);
						}
						
						PlayerPrefs.SetString("Server", playerName);
						PlayerPrefs.SetString("ServerPlayer", playerName);
						PlayerPrefs.SetString("ClientPlayer", null);
						
					}
				}*/
				//GUI.Label(new Rect(Screen.width/2-250,Screen.height/2-190,250,50),"Player name : ",TextName);
				//playerName = GUI.TextField(new Rect(Screen.width/2,Screen.height/2-190,250,50),playerName,TextName);
				GUI.Label(new Rect(Screen.width/XSIPL,Screen.height/YSIPL,250,50),"  Insert IP Address : ",TextIP);
				connectToIp = GUI.TextField(new Rect(Screen.width/XSIP,Screen.height/YSIP,250,50), connectToIp, TextIP);
				//GUI.Label(new Rect(Screen.width/2-250,Screen.height/2-90,250,50),"Server Port : ",TextPort);
				//connectPort = int.Parse(GUI.TextField(new Rect(Screen.width/2,Screen.height/2-90,250,50),connectPort.ToString(),TextPort));
			}else
			{
				//if(Network.peerType == NetworkPeerType.Connecting) 
				//	GUILayout.Label("Connect Status: Connecting");
				/*else if (Network.peerType == NetworkPeerType.Client){ 
					
					GUI.Label(new Rect(Screen.width/2,Screen.height/2-60,200,30),"Connect Status: Client",ConnectionData);
					GUILayout.Label("Ping to Server: "+Network.GetAveragePing(Network.connections[0]));
					
				}*/
				/*else*/ if (Network.peerType == NetworkPeerType.Server){
					//GUI.Label(new Rect(Screen.width/2-125,Screen.height/2-60,250,30),"Connect Status: Server",ConnectionData);
					//GUI.Label(new Rect(Screen.width/2-125,Screen.height/2+30,250,30),"Connections: " +Network.connections.Length,ConnectionData);
					//if(Network.connections.Length >= 1){
						//GUI.Label(new Rect(Screen.width/2,Screen.height/2-30,200,30),"Ping to Server: "+Network.GetAveragePing(Network.connections[0])+"",ConnectionData);
					//}
					//if(Ready && OpponentReady){
					if(GUI.Button(new Rect(Screen.width/XSGS,Screen.height/YSGS,400,50),"Start Game",StartServerButton)){
						networkView.RPC("StartGame", RPCMode.All);
						/* 
							SOSTITUIRE CON GENERATEMANAGER()
							SpawnField(); 

							*/ 
						
						//	}
					}
					
					
				}
				else if(Network.peerType == NetworkPeerType.Client && showClient){
					if(GUI.Button(new Rect(Screen.width/XSGS,Screen.height/YSGS,400,50),"Start Game",StartServerButton)){
						StartClient();
						/* 
							SOSTITUIRE CON GENERATEMANAGER()
							SpawnField(); 

							*/ 
						
						//	}
					}
				}
				/*if(Ready){
					GUI.Label(new Rect(Screen.width/2-125,Screen.height/2+100,250,30),"My Status : Ready",ConnectionData);
				}else{
					GUI.Label(new Rect(Screen.width/2-125,Screen.height/2+100,250,30),"My Status : Not Ready",ConnectionData);
				}
				if(OpponentReady){
					GUI.Label(new Rect(Screen.width/2-125,Screen.height/2+130,250,30),"Opponent Status : Ready",ConnectionData);
				}else{
					GUI.Label(new Rect(Screen.width/2-125,Screen.height/2+130,250,30),"Opponent Status : Not Ready",ConnectionData);
				}*/
				
				/*if(GUI.Button(new Rect(Screen.width/2-50,Screen.height/2+60,100,40), "Ready", ReadyButton) ){
					
					if(Ready == true){
						Ready = false;
					}else{
						Ready = true;
					}
					networkView.RPC("SetReady", RPCMode.Others);
				}
				if(GUI.Button(new Rect(Screen.width/2-100,Screen.height/2+160,200,30), "Disconnect", DisconnectButton)){
					
					networkView.RPC("Disconnect", RPCMode.All);
				}*/
				
				
				
				ipAddress = Network.player.externalIP;
				port = Network.player.port.ToString();
				GUI.Label(new Rect(Screen.width/XIPL,Screen.height/YIPL,400,50),"IP Address: "+ ipAddress + " : " + port, ConnectionData);
			}
		}else{
			//GUI.Box(new Rect(0,0,Screen.width,Screen.height), "",Background);
			//GUI.Label(new Rect(Screen.width/2-350,30,700,100),"Field of War",TitleStyle);
			///GUI.Box (new Rect(0,0,Screen.width, Screen.height),"",LastBox);
			GUI.Label(new Rect(Screen.width/XC,Screen.height/YC,500,30), "Starting in : "+StartCounter+"", StartServerButton);
			if (StartCounter == 0)
			{
				Manager.istance.matchStarted = true;
			}
		}
	}
	
	
	[RPC] void Disconnect(){
		Ready = false;
		Network.Disconnect(200);
	}
	
	[RPC] void SetReady(){
		if(OpponentReady == true){
			OpponentReady = false;
		}else{
			OpponentReady = true;
		}
	}
	
	IEnumerator StartCountDown(){
		while(StartCounter > 0){
			StartCounter-=1;
			yield return new WaitForSeconds(1);
		}
		if(StartCounter < 1 ){
			GameManager.SetActive(true);
			GameManager.GetComponent<Manager>().StartCoroutine("Start");
			Application.LoadLevel("NetworkGameScene");
		}
		yield return new WaitForSeconds(1);
		
		
	}
	
	[RPC]
	void StartGame(){
		//Application.LoadLevel("spawn-scene");
		Starting = true;
		StartCoroutine(StartCountDown());
	}
	
	/*

	SOSTITUIRE CON IL MANAGER

	void SpawnField(){
		GameObject newField = Field;
		newField.GetComponent<MyGridManager>().gridHeightInHexes = 25;
		newField.GetComponent<MyGridManager>().gridWidthInHexes = 25;
		int [] a = {56,68,556,568,308,316,162,462};
		newField.GetComponent<TerrainModifier>().mountainCells = a ;
		Network.Instantiate(newField, Vector3.zero, Quaternion.identity, 0);
	}*/
	
	
	void OnConnectedToServer(){
		foreach( GameObject go in FindObjectsOfType(typeof(GameObject)))
		{
			go.SendMessage("OnNetworkLoadedLevel", SendMessageOptions.DontRequireReceiver);
		}
	}
	
	/*
	void StartGame(){
		gameObject.GetComponent<SpawnMyPlayer>().Spawn();
		gameObject.GetComponent<MPBase>().enabled=false;
		Application.LoadLevel("spawn-scene");
	}*/
}
