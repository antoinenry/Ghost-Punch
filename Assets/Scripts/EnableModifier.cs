using UnityEngine;
using System.Collections;

public class EnableModifier : CutsceneAction
{
	public GameObject target;
	public bool setActive;
	
	public override void TriggerAction ()
	{
		target.SetActive(setActive);
	}
}
