using UnityEngine;
using UnityEngine.UI;
using System.Collections; 
using UnityStandardAssets._2D;

public class Jugador : MonoBehaviour {

	//SKILLS
	int Energy = 100;
	int record=0;
	int Sprints;
	float currentSpeed = 10f;

	//TERRAIN
	public GameObject storyGO;
	public GameObject baseBlock;
	public GameObject flyinBlock;


	//chraracteres
	public GameObject BigEnemy;
	public GameObject DataBaseEnemy;
	public GameObject ArchEnemy;
	public GameObject LiberationPorcess;


	//GUI
	public Text Level;
	public Text Stories;

	void OnGUI(){
		Level.text = "Sprint: " + Sprints;
		Stories.text = record + " Historias";
	}

	void Start () {
		Sprints = 1;
		generateLevel ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void generateLevel(){
		int hundred = 0;
		bool normalBlock=false;
		bool stories = false;
		int i;
		for(i =0; i< Sprints * 100;i++){
			if(i%30 == 0){
				normalBlock = !normalBlock;
			}
			if(i%3 == 0){
				Instantiate(storyGO,new Vector2(i, 3), new Quaternion());
			}
			Instantiate(baseBlock,new Vector2(i, normalBlock ? 0 : -1), new Quaternion());
		}


		for (int j=i; i< (i + 200); i++) {

			Instantiate(baseBlock,new Vector2(i, normalBlock ? 0 : -2), new Quaternion());
			if(j+i == 50){
				Instantiate(DataBaseEnemy,new Vector2(i, normalBlock ? 0 : -2), new Quaternion());
			}

			if(j+i == 100){
				Instantiate(ArchEnemy,new Vector2(i, normalBlock ? 0 : -2), new Quaternion());
			}

			if(j+i == 150){
				Instantiate(LiberationPorcess,new Vector2(i, normalBlock ? 0 : -2), new Quaternion());
			}
		}

	}
	void OnCollisionEnter2D(Collision2D col){
		if (col.collider.tag == "story") {
			record += 1;
			Destroy(col.collider.gameObject);
		}
	}


	void OnTriggerEnter2D(Collider2D col){
		Debug.Log (col.tag);
		if (col.tag == "database") {
			record -= 1;
			gameObject.transform.position = new Vector2(1,1);
		}
		if (col.tag == "code") {
			record -= 3;
			gameObject.transform.position = new Vector2(1,1);
		}
	}


	void OnTriggerStay2D(Collider2D col){
		if (col.tag == "liberacion") {
			gameObject.GetComponent<PlatformerCharacter2D>().maxVel = 2f;
		}
	}
	void OnTriggerExit2D(Collider2D col){
		if (col.tag == "liberacion") {
			gameObject.GetComponent<PlatformerCharacter2D>().maxVel = currentSpeed;
		}
	}





}
