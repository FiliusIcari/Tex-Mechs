using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

	//References
	private GameObject target;
	private Animator anim;
	private PolygonCollider2D fireArea;
	private Transform firePointRight;
	private Transform firePointLeft;
	public GameObject bullet;

	//public variables for changing behaivor or for UI
	public string targetTag;
	public float wakeRange;
	public float cooldown;
	public float bulletSpeed;
	public int maxHealth;
	public int curHealth;

	//Internal placeholder variables
	private float distanceFromTarget;
	private bool canFire;
	private int targetSide;
	private bool awake;

	void Start () {
		target = GameObject.FindGameObjectWithTag (targetTag);
		anim = gameObject.GetComponent<Animator> ();
		curHealth = maxHealth;
		canFire = true;
		firePointRight = gameObject.transform.FindChild("FirePointRight").transform;
		firePointLeft = gameObject.transform.FindChild("FirePointLeft").transform;
	}

	// Update is called once per frame
	void Update () {
		distanceFromTarget = Vector3.Distance(gameObject.transform.position, target.transform.position);

		if (distanceFromTarget > wakeRange) 
		{
			awake = false;
		} 
		else 
		{
			awake = true;
		}

		targetSide = System.Math.Sign (target.transform.position.x - gameObject.transform.position.x);

		if (curHealth > maxHealth) 
		{
			curHealth = maxHealth;
		}
		if (curHealth <= 0) 
		{
			curHealth = 0;
			StartCoroutine(Die());
		}

		anim.SetBool ("Awake", awake);
		anim.SetInteger ("TargetSide", targetSide);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject == target && canFire) 
		{
			fire ();
			StartCoroutine (cooldownTimer());
		}
	}
	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject == target && canFire) 
		{
			fire ();
			StartCoroutine (cooldownTimer());
		}
	}

	void fire()
	{
		if (targetSide == -1) 
		{
			Vector2 direction = target.transform.position - firePointLeft.transform.position;
			direction.Normalize ();

			GameObject bulletClone;
			bulletClone = Instantiate (bullet, firePointLeft.transform.position, firePointLeft.transform.rotation) as GameObject;
			bulletClone.GetComponent<Rigidbody2D> ().velocity = direction * bulletSpeed;
		}
		if (targetSide == 1) 
		{
			Vector2 direction = target.transform.position - firePointRight.transform.position;
			direction.Normalize ();

			GameObject bulletClone;
			bulletClone = Instantiate (bullet, firePointRight.transform.position, firePointRight.transform.rotation) as GameObject;
			bulletClone.GetComponent<Rigidbody2D> ().velocity = direction * bulletSpeed;
		}


	}

	IEnumerator cooldownTimer()
	{
		canFire = false;
		yield return new WaitForSeconds (cooldown);
		canFire = true;
	}

	public void damage(int damage)
	{
		curHealth -= damage;
	}
	IEnumerator Die()
	{
		gameObject.GetComponent<SpriteRenderer>().color = Color.red;
		yield return new WaitForSeconds (1);
		gameObject.SetActive (false);
	}
}
