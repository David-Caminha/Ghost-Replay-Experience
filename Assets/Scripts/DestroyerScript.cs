using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DestroyerScript : MonoBehaviour {
	
	public GameObject lastPlatform;
	public GameObject lastMountain;
	public GameObject lastTree;
	public int appToLoad;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			SceneManager.LoadScene(appToLoad);
			return;
		}
		else if (other.tag == "Floor")
		{
			Vector3[] positions = new [] {new Vector3(6, 0, 0), new Vector3(9, 0, 0), new Vector3(12, 0, 0)};
			int index = Random.Range(0, positions.Length);
			other.transform.position = new Vector3(lastPlatform.transform.position.x + positions[index].x,
			                                       lastPlatform.transform.position.y + positions[index].y,
			                                       lastPlatform.transform.position.z);
			lastPlatform = other.gameObject;
		}
		else if(other.tag == "mount1")
		{
			float deltaX = Random.Range(21f, 43f);
			float deltaY = Random.Range(-4.5f, -1.7f);
			other.transform.position = new Vector3(lastMountain.transform.position.x + deltaX,
			                                       deltaY,
			                                       lastMountain.transform.position.z);
			lastMountain = other.gameObject;
		}
		else if(other.tag == "mount2")
		{
			float deltaX = Random.Range(21f, 43f);
			float deltaY = Random.Range(-4.5f, -1.9f);
			other.transform.position = new Vector3(lastMountain.transform.position.x + deltaX,
			                                       deltaY,
			                                       lastMountain.transform.position.z);
			lastMountain = other.gameObject;
		}
		else if(other.tag == "tree")
		{
			float deltaX = Random.Range(6f, 20f);
			float deltaY = Random.Range(-6.5f, -3.7f);
			other.transform.position = new Vector3(lastTree.transform.position.x + deltaX,
			                                       deltaY,
			                                       lastTree.transform.position.z);

			//trocar layer order
			int val = Random.Range(-1, 2);
			if(val == 1)
				val=3;
			else
				val = -1;
			other.GetComponent<SpriteRenderer>().sortingOrder = val;
			lastTree = other.gameObject;
		}
		else
		{
			Destroy(other.gameObject);
		}
	}
}
