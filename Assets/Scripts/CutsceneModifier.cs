using UnityEngine;
using System.Collections;

public class CutsceneModifier : CutsceneAction
{
	public GameObject toPlay;

	private Cutscene cutscene;

	void Start ()
	{
		cutscene = toPlay.GetComponent <Cutscene> ();
	}
	
	public override void TriggerAction ()
	{
		cutscene.PlayScene ();
	}
}
