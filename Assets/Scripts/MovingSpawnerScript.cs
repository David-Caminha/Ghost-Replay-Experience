using UnityEngine;
using System.Collections;

public class MovingSpawnerScript : MonoBehaviour {

	public GameObject[] obj;
	public float spawnMin = 1f;
	public float spawnMax = 2f;
	public float spawnLow = 1f;
	public float spawnHigh = 2f;
	public float increment = 1f;
	PlayerControllerScript playerScript;
	public GameObject mainCamera;

	
	//ACRESCENTAR PARTE DO HUB PARA ADICIONAR PONTOS
	
	void Start () {
		playerScript = GameObject.Find ("Character").GetComponent<PlayerControllerScript> ();
		Invoke("Spawn", Random.Range(0f, 4f));
	}
	
	void Spawn()
	{
//		GameObject instance = Instantiate(obj[Random.Range(0, obj.Length)], transform.position, Quaternion.identity) as GameObject;
//		Invoke("Spawn", Random.Range(spawnMin, spawnMax));
	}

	void FixedUpdate()
	{
		transform.position = new Vector3 (transform.position.x, transform.position.y + increment, 0);
		if(transform.position.y > spawnHigh && increment > 0)
			increment *= -1;
		else if(transform.position.y < spawnLow && increment < 0)
			increment *= -1;
	}
}
