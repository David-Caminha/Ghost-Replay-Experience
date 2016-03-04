using UnityEngine;
using System.Collections;

public class ChangeSpriteScript : MonoBehaviour {

	public Sprite newSprite;
	SpriteRenderer sr;

	void Start () {
		sr = GetComponent<SpriteRenderer> ();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		sr.sprite = newSprite;
		this.GetComponent<ChangeSpriteScript> ().enabled = false;
	}
}
