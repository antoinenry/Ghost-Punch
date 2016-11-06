using UnityEngine;
using System.Collections;

public class RoomTransition : MonoBehaviour
{
	public Transform destinationPos;
	public Camera destinationView;

	private GameObject mainCamera;
	private GameObject player;
	private GameObject fader;

	void Start ()
	{
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
		player = GameObject.FindGameObjectWithTag("Player");
		fader = GameObject.FindGameObjectWithTag("ScreenFader");

	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag != "Player")
			return;
		
		PlayerControl p = player.GetComponent <PlayerControl> ();

		p.FreezeForSeconds(1f);
		FadeOut();
		Invoke ("ChangeRoom", 1f);
		Invoke ("FadeIn", 1.5f);
	}

	void ChangeRoom()
	{
		PlayerControl p = player.GetComponent <PlayerControl> ();
		p.SetPos(destinationPos);
		
		MainCamera mc = mainCamera.GetComponent <MainCamera> ();
		mc.SetView(destinationView);
	}

	void FadeIn()
	{
		Animator anim = fader.GetComponent <Animator> ();
		anim.SetBool("blackScreen", false);
	}

	void FadeOut()
	{
		Animator anim = fader.GetComponent <Animator> ();
		anim.SetBool("blackScreen", true);
	}
}
