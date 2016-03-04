using UnityEngine;
using System.Collections;

public class SpwanScript : MonoBehaviour {

	public GameObject[] obj;
	public float spawnMin = 1f;
	public float spawnMax = 2f;
	PlayerControllerScript playerScript;
	public GameObject mainCamera;


	//ACRESCENTAR PARTE DO HUB PARA ADICIONAR PONTOS

	void Start () {
		playerScript = GameObject.Find ("Character").GetComponent<PlayerControllerScript> ();
		Invoke("Spawn", Random.Range(0f, 4f));
	}

	void Spawn()
	{	
		GameObject instance = Instantiate(obj[Random.Range(0, obj.Length)], transform.position, Quaternion.identity) as GameObject;
		Invoke("Spawn", Random.Range(spawnMin, spawnMax));
	}
}
