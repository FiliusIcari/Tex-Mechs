using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class Player : MonoBehaviour {



	public float speed;
	public float horizontalMovement;
	public float jumpHeight;
	public float maxSpeed;
	public float maxYSpeed;
	public int curHealth;
	public int maxHealth;

	public float frictionConstant;

	public bool grounded;
	public bool canAct;
	public bool jetpacking;
	public bool hasJetpack;
	public int jumpNumber = 0;

	private Rigidbody2D rb;
	private Animator anim;
	private SpriteRenderer sr;

	void Start ()
	{
		//Get relevant components
		rb = gameObject.GetComponent<Rigidbody2D>();
		anim = gameObject.GetComponent<Animator> ();
		sr = gameObject.GetComponent<SpriteRenderer> ();

		canAct = true;
		//duh

		curHealth = maxHealth;
	}


	void Update () 
	{
		//set animation variables to in script variables
		anim.SetBool("Grounded", grounded);
		anim.SetBool ("CanAct", canAct);
		anim.SetFloat ("Speed", Mathf.Abs(rb.velocity.x));

		if(canAct)
		{
			//Flip sprites depending on input direction. Could be changed like above if desired.
			//Currently in if(grounded) to keep direction in air for aiming, etc. Doesn't need to be, could be changed. Game design thing.
			if (grounded) 
			{
				if (Input.GetAxis ("Horizontal") < -0.1f) {
					transform.localScale = new Vector3 (-1, 1, 1);
				}
				if (Input.GetAxis ("Horizontal") > 0.1f) {
					transform.localScale = new Vector3 (1, 1, 1);
				}
			}

			if (Input.GetButton ("Jump") && hasJetpack) 
			{
				jetpacking = true;
			}
		}

		//basic health stuff. keep health at max, die at 0.
		if (curHealth > maxHealth) {
			curHealth = maxHealth;
		}
		if (curHealth <= 0) {
			curHealth = 0;
			Die ();
		}
	}

	void FixedUpdate ()
	{

		float x = Input.GetAxisRaw ("Horizontal")*Time.deltaTime;

		if (canAct) 
		{
			if (Input.GetButtonDown ("Jump") == true && jumpNumber < 2) 
			{
				rb.velocity = new Vector2(rb.velocity.x,jumpHeight);
				jumpNumber++;
			}
			//basic shitty jumping

			//Movement. Ensures not adding to max velocity in either direction
			if(rb.velocity.x > maxSpeed && x<0)
			{
				rb.AddForce ((Vector2.right * speed) * x);
			}
			else if(rb.velocity.x<-maxSpeed && x>0)
			{
				rb.AddForce ((Vector2.right * speed) * x);
			}
			else if(Mathf.Abs(rb.velocity.x)<maxSpeed)
			{
				rb.AddForce ((Vector2.right * speed) * x);
			}
			if (Mathf.Abs (rb.velocity.y) < maxYSpeed) 
				{
					//Jetpack stuff
				}
				
		}

		//Friction and stopping below 0.01 velocity
		if (x == 0 && grounded && rb.velocity.x != 0) 
		{
			rb.velocity = new Vector2 (rb.velocity.x * frictionConstant, rb.velocity.y);
			if (Mathf.Abs(rb.velocity.x) < 0.01) 
			{
				rb.velocity = new Vector2(0,rb.velocity.y);
			}
		}
		horizontalMovement = rb.velocity.x;
			
	}

	public void damage(int dmg)
	{
		curHealth = curHealth - dmg;
	}

	public void knockback(float hitstunDur, Vector2 knockbackPwr, Vector3 knockbackOri)
	{
		float intX; //Intensity of X
		float intY; //Intensity of Y

		//set proportional to each other, total value of 1(circle stuff).
		intX=Mathf.Sqrt(1/(1+Mathf.Pow(((transform.position.y - knockbackOri.y)/(transform.position.x - knockbackOri.x)),2)));
		intY=Mathf.Abs(((transform.position.y - knockbackOri.y)/(transform.position.x - knockbackOri.x)*intX));

		//Lmao there goes performance for the whole program

		//Set new velocity for knockback
		rb.velocity = (new Vector3( 
			Mathf.Sign((transform.position.x - knockbackOri.x)) * intX * knockbackPwr.x, 
			Mathf.Sign((transform.position.y - knockbackOri.y)) * intY * knockbackPwr.y,
			0));


		StartCoroutine (hitstun (hitstunDur));
	}


	public IEnumerator hitstun (float hitstunDur)
	{
		if(hitstunDur>0)
		canAct = false;
		sr.color = Color.gray;

		yield return new WaitForSeconds (hitstunDur);
		canAct = true;
		sr.color = Color.white;
	}

	void Die()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

}