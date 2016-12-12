using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBehaivor : MonoBehaviour {

	private Player player;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag ("Player")) 
		{
			player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
			player.Damage (1);
			player.Knockback(0.2f,new Vector2(5,7),new Vector3 (transform.position.x, transform.position.y-0.5f,0), false);
		}
	}
}
