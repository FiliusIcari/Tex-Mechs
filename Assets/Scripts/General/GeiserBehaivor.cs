using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeiserBehaivor : MonoBehaviour {

	private Player player;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.CompareTag ("Player")) 
		{
			player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
			player.Damage (1);
			player.Knockback(0.5f,new Vector2(0.1f,12),new Vector3(transform.position.x,transform.position.y-20f,transform.position.z), false);
		}
	}
}
