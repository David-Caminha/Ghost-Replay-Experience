using UnityEngine;
using System.Collections;

public class CameraMoveScript : MonoBehaviour {

	public Transform Wall;
	public float maxSpeed = 5f;
	bool firstTime = true;
	float time = 0;

	void Start(){
		Vector3 pos = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(0, GetComponent<Camera>().pixelHeight, GetComponent<Camera>().nearClipPlane));
		Wall.position = pos;
//		StartCoroutine(CoroutineA());
//		transform.position = new Vector3 (0, 0, -10);


	}

	void Update () {
			time += Time.deltaTime;
			transform.position = new Vector3 (maxSpeed * time, 0, -10);
	}

	IEnumerator CoroutineA()
	{
		yield return new WaitForSeconds(5);
		firstTime = false;
	}
}
