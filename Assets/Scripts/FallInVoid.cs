using UnityEngine;
using System.Collections;

public class FallInVoid : MonoBehaviour
{
	public GameObject fallingObject;
	public float rotationSpeed;

	private Rigidbody2D rb2D;

	void Start ()
	{
		rb2D = fallingObject.GetComponent <Rigidbody2D> ();
		rb2D.angularVelocity = rotationSpeed;
	}
}
