using UnityEngine;
using System.Collections;

public interface IDamageable
{
	void Die ();
	void Damage (int damageTaken);
}

public interface IKnockbackable
{
	void Knockback (float hitstunDur, Vector2 knockbackPwr, Vector3 knockbackOri, bool knockbackTypeForce);
}