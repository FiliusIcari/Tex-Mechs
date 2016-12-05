using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class Player : MonoBehaviour {



	public float speed = 50f;
	public float horizontalMovement;
	public float jumpPower = 150f;
	public float jumpHeight = 2; //Not used at the same time as jumpPower
	public float maxSpeed = 3;
	public float maxYSpeed = 3;

	public float frictionConstant = 0f;

	//public Text jumpCount;

	public bool grounded;
	public bool canAct;
	public bool jetpacking;
	public bool hasJetpack;

	private Rigidbody2D rb;
	private Animator anim;
	private SpriteRenderer sr;
	public int jumpNumber = 0;

	public int curHealth;
	public int maxHealth = 6;

	void Start ()
	{
		rb = gameObject.GetComponent<Rigidbody2D>();
		anim = gameObject.GetComponent<Animator> ();
		sr = gameObject.GetComponent<SpriteRenderer> ();
		//Get relevant components

		canAct = true;
		//duh

		curHealth = maxHealth;
	}


	void Update () 
	{
		anim.SetBool("Grounded", grounded);
		anim.SetBool ("CanAct", canAct);

		//anim.SetFloat ("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));
		anim.SetFloat ("Speed", Mathf.Abs(rb.velocity.x));
		//Choose one, one is set for input and other for actual velocity

		if(canAct)
		{
			if (grounded) 
			{
				if (Input.GetAxis ("Horizontal") < -0.1f) {
					transform.localScale = new Vector3 (-1, 1, 1);
				}
				if (Input.GetAxis ("Horizontal") > 0.1f) {
					transform.localScale = new Vector3 (1, 1, 1);
				}
			}
			//Flip sprites depending on input direction. Could be changed like above if desired.

			if (Input.GetButton ("Jump") && hasJetpack) 
			{
				jetpacking = true;
			}
		}

		if (curHealth > maxHealth) {
			curHealth = maxHealth;
		}
		if (curHealth <= 0) {
			Die ();
		}
		//Basic health stuff
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
			//movement
			if (Mathf.Abs (rb.velocity.y) < maxYSpeed) 
				{
					//Jetpack stuff
				}
				
		}

		if (x == 0 && grounded && rb.velocity.x != 0) 
		{
			rb.velocity = new Vector2 (rb.velocity.x * frictionConstant, rb.velocity.y);
			if (Mathf.Abs(rb.velocity.x) < 0.01) 
			{
				rb.velocity = new Vector2(0,rb.velocity.y);
			}
		}
		//Friction and stopping
		horizontalMovement = rb.velocity.x;
			
	}

	public void damage(int dmg)
	{
		curHealth = curHealth - dmg;
	}

	public void knockback(float hitstunDur, Vector2 knockbackPwr, Vector3 knockbackOri)
	{
		float intX;
		float intY;
		intX=Mathf.Sqrt(1/(1+Mathf.Pow(((transform.position.y - knockbackOri.y)/(transform.position.x - knockbackOri.x)),2)));
		intY=Mathf.Abs(((transform.position.y - knockbackOri.y)/(transform.position.x - knockbackOri.x)*intX));

		//work on the ABS issue with 1+whatever
		//Also fix the problem of shooting off screen. 
		//Lmao there goes performance for the whole program

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
	//void jumpCountUpdateUI()
	//{
	//	jumpCount.text = "Jump Count: " + jumpNumber.ToString ();
	//}

}