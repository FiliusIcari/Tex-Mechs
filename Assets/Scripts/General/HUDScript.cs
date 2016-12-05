using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour {

	public Sprite[] HeartSprites;
	public Image HeartUI;
	public GameObject HUD;
	private Player player;

	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player> ();
	}
	
	// Update is called once per frame
	void Update () {
		HealthUpdate ();
		AmmoUpdate ();
	}

	void HealthUpdate()
	{
		HeartUI.sprite = HeartSprites [player.curHealth];
	}
	void AmmoUpdate ()
	{
		//lmao maybe later.
	}
}
