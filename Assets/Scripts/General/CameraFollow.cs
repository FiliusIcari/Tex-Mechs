using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	private Vector2 velocity;

	public float smoothTimeY;
	public float smoothTimeX;

	public bool bounds;

	public Vector3 minCameraPos;
	public Vector3 maxCameraPos;

	public GameObject player;


	void Start () 
	{
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void FixedUpdate ()
	{
		float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
		float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);

		transform.position = new Vector3 (posX, posY, transform.position.z);

		if (bounds) 
		{
			transform.position = new Vector3 (
				Mathf.Clamp (posX, minCameraPos.x, maxCameraPos.x),
				Mathf.Clamp (posY, minCameraPos.y, maxCameraPos.y),
				Mathf.Clamp (transform.position.z, minCameraPos.z, maxCameraPos.z));
		}


	}

}
