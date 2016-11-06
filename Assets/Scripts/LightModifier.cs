using UnityEngine;
using System.Collections;

public class LightModifier : CutsceneAction
{
	public GameObject lightObject;
	public int changeLightTo;
	public float transitionTime = -10f;

	public override void TriggerAction ()
	{
		LightManager l = lightObject.GetComponent <LightManager> ();
		if(changeLightTo < l.Capacity ())
		{
			l.SetLight(changeLightTo, transitionTime);
		}
		else
			Debug.LogError("Change light to " + changeLightTo + 
			               "exceed LightManager capacity (" + l.Capacity () + ")");
	}
}
