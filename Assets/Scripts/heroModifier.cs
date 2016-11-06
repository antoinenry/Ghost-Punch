using UnityEngine;
using System.Collections;

public class heroModifier : CutsceneAction
{
	public GameObject hero;
	public string command = "none";

	private PlayerControl playerControl;

	void Start ()
	{
		playerControl = hero.GetComponent <PlayerControl> ();
	}

	public override void TriggerAction ()
	{
		Animator anim = hero.GetComponent <Animator> ();
		switch(command)
		{
		case "lie_down":
			anim.SetTrigger("lieDown");
			break;
		case "stand_up":
			anim.SetTrigger("standUp");
			break;
		case "face_right":
			playerControl.SetFacing(1);
			break;
		case "face_left":
			playerControl.SetFacing(-1);
			break;
		case "lock_controls":
			playerControl.LockControls(true);
			break;
		case "unlock_controls":
			playerControl.LockControls(false);
			break;
		default:
			Debug.LogError("Unknown command (" + command + ")");
			break;
		}
	}
}
