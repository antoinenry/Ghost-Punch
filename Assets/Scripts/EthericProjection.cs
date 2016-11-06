using UnityEngine;
using System.Collections;

public class EthericProjection : MonoBehaviour
{
	public GameObject actor;

	private SpriteRenderer thisRenderer;
	private SpriteRenderer actorRenderer;

	void Start ()
	{
		thisRenderer = GetComponent <SpriteRenderer> ();
		actorRenderer = actor.GetComponent <SpriteRenderer> ();
	}

	void FixedUpdate ()
	{
		thisRenderer.sprite = actorRenderer.sprite;
	}
}
