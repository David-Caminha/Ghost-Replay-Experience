using UnityEngine;
using System.Collections;

public class ExtraJumpScript : MonoBehaviour {

	public PlayerControllerScript controller;

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			if(controller.numJumps < 5)
				controller.numJumps++;
			Destroy(this.gameObject);
		}
	}
}