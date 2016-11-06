using UnityEngine;
using System.Collections;

public class CarriableObject : MonoBehaviour
{
	public float throwForce = 6000;

	private GameObject player;
	private BoxCollider2D col2D;
	private Rigidbody2D rb2d;
	private bool isCarried = false;
	private bool isReachable = false;

	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		col2D = GetComponent <BoxCollider2D> ();
		rb2d = GetComponent <Rigidbody2D> ();
	}

	void FixedUpdate ()
	{
		PlayerControl p = player.GetComponent <PlayerControl> ();
		BoxCollider2D playerCollider = player.GetComponent <BoxCollider2D> ();

		if(p.IsUsing ())
		{
			if(isCarried)
			{
				Rigidbody2D prb2D = player.GetComponent <Rigidbody2D> ();
				rb2d.constraints = RigidbodyConstraints2D.None;
				rb2d.velocity = prb2D.velocity;
				isCarried = false;
			}
			else if(isReachable)
			{
				rb2d.rotation = 0f;
				rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
				Physics2D.IgnoreCollision(col2D, playerCollider, true);
				isCarried = true;
			}
			p.isCarrying = isCarried;
		}
		if(p.IsPunching ())
		{
			if(isCarried)
			{
				int dir = p.GetDir ();
				Vector2 f = new Vector2 (dir * throwForce, 2 * throwForce);
				Vector3 randomPoint = RandomPointOnObject ();

				rb2d.constraints = RigidbodyConstraints2D.None;
				rb2d.AddForceAtPosition(f, randomPoint);
				isCarried = false;
			}
			p.isCarrying = isCarried;
		}

		if(!isCarried) return;
		transform.position = player.transform.position;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player")
			isReachable = true;
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			isReachable = false;
			Physics2D.IgnoreCollision(col2D, other, false);
		}
	}

	Vector2 RandomPointOnObject ()
	{
		Vector3 randomPoint = Random.insideUnitSphere;
		randomPoint.x *= col2D.size.x/4; randomPoint.y *= col2D.size.y/4; randomPoint.z = 0f;
		randomPoint += transform.position;
		return randomPoint;
	}
}
