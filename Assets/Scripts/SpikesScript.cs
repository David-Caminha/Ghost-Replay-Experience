using UnityEngine;
using System.Collections;

public class SpikesScript : MonoBehaviour {



	// Use this for initialization
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{

			Application.LoadLevel(3);
			return;
		}
	}
}
