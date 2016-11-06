using UnityEngine;
using System.Collections;

public class LightSwitch : MonoBehaviour
{
	public GameObject lightObject;
	public float switchDelay = .1f;

	void OnTriggerStay2D(Collider2D other)
	{
		if(other.tag != "Player")
			return;

		PlayerControl p = other.GetComponent <PlayerControl> ();
		LightManager l = lightObject.GetComponent <LightManager> ();
		if(p.IsUsing())
		{
			if(l.IsOff()) l.SetLight(1, switchDelay);
			else l.SetLight(0, switchDelay);
		}
	}
}
