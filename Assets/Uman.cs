using UnityEngine;
using System.Collections;

public class Uman : MonoBehaviour
{
	
	bool select;
	byte sw;
	
	public Alien alienObject;
	//what == 0; casella vuola
	//what == 1; casella cittadino
	//what == 2; casella soldato
	
	public Vector3 scale;
	public int pos;
	
	public bool move = false;
	public bool abduct = false;
	public Vector3 startingAbduct;
	public Vector3 target;
	
	public enum humanState { empty = 0, human = 1, army = 2, noArmy = 3, block = 4 };
	public humanState currentState;

	public AudioSource source;
	public CivilianAnimationManager anim;
	public HumanAudioScript audioscript;

	public Sprite soldatoArmy;
	public Sprite soldatoNoArmy;

	void Start()
	{
		//currentState = humanState.empty;
		scale = transform.localScale;
		pos = int.Parse(gameObject.name);
		alienObject = GameObject.Find ("Alien").GetComponent<Alien>();
		source = GetComponent<AudioSource> ();
		audioscript = GetComponent<HumanAudioScript> ();
		target = this.transform.position;
	}
	
	void Update()
	{
		/*if (currentState == humanState.empty) GetComponent<SpriteRenderer>().color = Color.white;
		if (currentState == humanState.human) GetComponent<SpriteRenderer>().color = Color.yellow;
		if (currentState == humanState.army) GetComponent<SpriteRenderer>().color = Color.green;
		if (currentState == humanState.noArmy) GetComponent<SpriteRenderer>().color = Color.red;
		if (currentState == humanState.block) GetComponent<SpriteRenderer>().color = Color.cyan;*/
		
		if (move) {
			this.transform.position = Vector3.MoveTowards(this.transform.position,target,Time.deltaTime*5.0f);
		}
		if(this.transform.position.Equals(target)){
			move = false;
			target = this.transform.position;
		}    

		if (abduct) {
			this.transform.position = Vector3.MoveTowards(this.transform.position,alienObject.transform.position,Time.deltaTime*3.0f);	
		}
		if(this.transform.position.y.Equals(alienObject.transform.position.y)){
			abduct = false;
			this.transform.position = startingAbduct;
	
		}    
	}
	
	void OnMouseDown()
	{
		//command as alien
		
		if (Manager.istance.target == -1 && Manager.istance.alien)
		{
			Manager.istance.target = pos;
			//animazione
			//Vector3 newScale = scale;
			//newScale.y *= 1.1f;
			//transform.localScale = newScale;
			alienObject.moveAlien(new Vector3(this.transform.position.x,alienObject.transform.position.y,0));
			//end animazione
			select = true;
		}
		else if (select)
		{
			Manager.istance.target = -1;
			//animazione
			transform.localScale = scale;
			//end animazione
			select = false;
		}
		
		//command as human
		
		if (!Manager.istance.alien)
		{
			if (Manager.istance.aux != 2 && !select)
			{
				Enable();
			}
			else if (select)
			{
				Disable();
			}
		}
		
	}
	
	public void Enable()
	{
		//animation
		Vector3 newScale = scale;
		newScale.y *= 1.1f;
		transform.localScale = newScale;
		
		
		//save current slot
		sw = Manager.istance.aux;
		Manager.istance.Import(pos);
		select = true;
	}
	
	public void Disable()
	{
		transform.localScale = scale;
		select = false;
		sw = 0;
		Manager.istance.Deport( sw );
	}
	
	public void Destroy()
	{
		Destroy();
	}
	
	public void moveHuman(Vector3 target){
		this.target = target;
		startingAbduct = this.transform.position;
		move = true;
	}

	public void Kill(int i){
		if (currentState == humanState.empty || (currentState == humanState.noArmy && anim.getAttack())) {
			anim.abductionAnimation (true);
			abduct = true;
		}
		if (currentState == humanState.noArmy && !anim.getAttack ()) {
			anim.reloadAnimation(false);
			anim.attackAnimation(true);
		}
		source.clip = audioscript.getClip (i);
		source.Play ();
	}

	public void Reload(){
		//GetComponent<SpriteRenderer>().sprite = soldatoArmy;
		anim.attackAnimation(false);
		anim.reloadAnimation(true);
	}

	public void Land(){
		source.clip = audioscript.getClip (2);
		source.Play ();
	}
	
}
