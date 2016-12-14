using UnityEngine;
using System.Collections;

public class CameraBounds : MonoBehaviour
{
	
	public Vector3 minCameraPos;
	public Vector3 maxCameraPos;

	private Vector3 boundsPos;

	public Vector3 GetBoundedPos (float posX , float posY)
	{
		boundsPos = new Vector3(
				Mathf.Clamp (posX, minCameraPos.x, maxCameraPos.x),
				Mathf.Clamp (posY, minCameraPos.y, maxCameraPos.y),
				Mathf.Clamp (transform.position.z, minCameraPos.z, maxCameraPos.z));

		return boundsPos;
	}
}

