using UnityEngine;
using System.Collections;

public class OneWayCollider : MonoBehaviour
{
	private Collider2D platform;

	void Start ()
	{
		Collider2D[] colliders;
		colliders = GetComponents <Collider2D>();
		if(colliders[0].isTrigger)
			platform = colliders[1];
		else
			platform = colliders[0];
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		Physics2D.IgnoreCollision(platform, other, true);
	}

	void OnTriggerExit2D(Collider2D other)
	{
		Physics2D.IgnoreCollision(platform, other, false);
	}
}
