using UnityEngine;
using System.Collections;

public class HUDScript : MonoBehaviour {

	int MINS_GLI = 5;
	int MAXIN_GLI=5;
	int NGLI = 4;
	int runGliN=0;
	float RUNT_GLI=2.8f;
	float pps= 0.83f;
	float playerScore = 0;
	ArrayList timGli;
	ArrayList runningGli;
	ArrayList runningGliTime;
	float timer=0;
	float oTimer=0;
	int refresher=0;
	int nextGli;
	public AudioSource audioGli;
	public AudioSource audioCoin;
	public GameObject player;
	InvertGravityScript grav;
	PlayerControllerScript pcs;
	public float coinsCombo = 0f;


	void OnDisable()
	{
		PlayerPrefs.SetInt ("YourScore", (int)playerScore);
		int[] scores = new int[10];
		int temp;
		for(int i = 1; i < 10; i++)
		{
			scores[i-1] = PlayerPrefs.GetInt("Score"+i);
			if(scores[i-1] < playerScore)
			{
				temp = scores[i-1];
				scores[i-1] = (int) playerScore;
				playerScore = temp;
			}
		}
		for(int j = 1; j < 10; j++)
		{
			PlayerPrefs.SetInt("Score"+j, scores[j-1]);
		}
	}

	void Start(){
		nextGli=(int)(Random.value*MAXIN_GLI + MINS_GLI);
		runningGli = new ArrayList();
		runningGliTime = new ArrayList();
		timGli = new ArrayList ();
		grav = player.GetComponent<InvertGravityScript> ();
		pcs = player.GetComponent<PlayerControllerScript> ();

		//Setting time for glitches

		timGli.Add (2.6f);//grav
		timGli.Add (2.5f);//keys
		timGli.Add (1.3f);//jump
		timGli.Add (0.4f);//speed
		timGli.Add (0.2f);//instajump
		timGli.Add (1.0f);//
		//-//

		grav.enabled = true;
		pcs.enabled = true;
	}


	void Update () {
		timer += Time.deltaTime;
		oTimer += Time.deltaTime;
		coinsCombo -= Time.deltaTime;
		if (coinsCombo < 0f) {
			coinsCombo =0f;		
		}
		runGliN = runningGli.Count;

		for (int i=0; i<runGliN; i++) {
			runningGliTime[i] =(float)runningGliTime[i]- Time.deltaTime;
			if((float)runningGliTime[i]<= 0.0f){
				disableGli((int)runningGli[i]);
				runningGli.RemoveAt(i);
				runningGliTime.RemoveAt(i);
				runGliN--;
				i--;
			}
		}
		if (oTimer >= 1.0f) {

			refresher++;
			oTimer=0;

			playerScore += pps;
			pps += 0.01f;
			if(refresher % nextGli ==0){
				refresher=0;
				int r = activateGlitch();
				runningGli.Add(r);
				runningGliTime.Add((float)timGli[r]);
				Debug.Log ("activate glitch number " + r + " for " + (float)timGli[r] + " s");
				nextGli=(int)(Random.value*MAXIN_GLI + MINS_GLI);
			}

		}


	}

	public void changeScore(int amount){
		playerScore += amount;
	}

	void OnGUI()
	{	

		GUI.Label (new Rect (10, 10, 100, 30), "Score: " + (int)playerScore);
		GUI.Label (new Rect(Screen.width-120,10,100,30),"" + timer);
		//debug
		//GUI.Label (new Rect(Screen.width-120,40,100,30),"GliDeBug: " + refresher);
		//GUI.Label (new Rect(Screen.width-135,70,120,30),"RunningGlitches: " + runGliN);
	}

	int activateGlitch(){
		int gli = (int)(Random.value * NGLI);
		//int gli = 3;
		audioGli.Play ();
		switch (gli) {
		case 0:
			grav.activate();
			return 0;
		case 1:
			pcs.velMod= -1;
			return 1;
		case 2:
			pcs.dblJmpGli=true;
			pcs.doubleJump=true;
			return 2;
		case 3:
			float res=(float)(Random.value*2 -1); 
			if(res>0){
				pcs.velMod=(float)(1.4 + res);
			}
			else{
				pcs.velMod= (float)(0.5 + res*0.4);
			}
			return 3;
		case 4:

			return 4;
		}

		return 0;
	}
	void disableGli(int n){
		switch (n) {

		case 0:
			grav.activate();
			break;
		case 1:
			pcs.velMod= 1;
			break;
		case 2:
			pcs.dblJmpGli=false;
			break;
		case 3:
			pcs.velMod=1;
			break;
		case 4:
			pcs.jump();
			if(Random.value < 0.5){
				pcs.doubleJump=false;
			}
			break;
		}
	}
}
