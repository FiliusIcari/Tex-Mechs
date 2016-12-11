using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	private Vector2 velocity;

	public bool bounds;

	public float smoothTimeY;
	public float smoothTimeX;
	private float posX;
	private float posY;

	private CameraBounds cB;




	private GameObject player;


	void Start () 
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		cB = gameObject.GetComponent<CameraBounds> ();
	}

	void FixedUpdate ()
	{
		posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
		posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);

		transform.position = new Vector3 (posX, posY, transform.position.z);

		if (bounds)
			transform.position = cB.GetBoundedPos (posX, posY);

	}

}
