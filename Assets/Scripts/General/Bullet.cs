using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
	public int damage;
	public float hitstunDur;
	public float knockbackPower;

	Vector3 knockbackOri;
	void OnTriggerEnter2D(Collider2D other)
	{
		IDamageable damageable;
		IKnockbackable knockbackable;
		Vector2 fixedVelocity;

		fixedVelocity = new Vector2 (Mathf.Abs (gameObject.GetComponent<Rigidbody2D> ().velocity.x), gameObject.GetComponent<Rigidbody2D> ().velocity.y);

		if(other.gameObject.GetComponent(typeof(IDamageable)))
			{
			damageable = other.gameObject.GetComponent(typeof(IDamageable)) as IDamageable;
			damageable.Damage (damage);
			}
		if(other.gameObject.GetComponent(typeof(IKnockbackable)))
		{
			knockbackable = other.gameObject.GetComponent(typeof(IKnockbackable)) as IKnockbackable;
			knockbackable.Knockback (hitstunDur, fixedVelocity*knockbackPower, gameObject.transform.position, true);
		}
	
		if(!other.isTrigger)
			Destroy (gameObject);
	}
}

