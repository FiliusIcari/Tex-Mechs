using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {

	private bool ground;
	private Player player;
	// Use this for initialization
	void Start () {
		player = gameObject.GetComponentInParent<Player> ();
	}
	
	// Update is called once per frame

	void OnTriggerExit2D (Collider2D col)
	{
		player.grounded = false;
	}

	void OnTriggerStay2D (Collider2D col)
	{
		player.grounded = true;
		player.jumpNumber = 0;
		//jumpCountUpdateUI ();
	}
	void OnTriggerEnter2D (Collider2D col)
	{
		player.grounded = true;
		player.jumpNumber = 0;
		//jumpCountUpdateUI ();
	}
	//void jumpCountUpdateUI()
	//{
	//	player.jumpCount.text = "Jump Count: " + player.jumpNumber.ToString ();
	//}

}
