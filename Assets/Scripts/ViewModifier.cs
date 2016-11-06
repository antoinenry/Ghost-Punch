using UnityEngine;
using System.Collections;

public class ViewModifier : CutsceneAction
{
	public Camera changeViewTo;

	private MainCamera view;
	
	private LightManager lightManager;
	
	void Start ()
	{
		GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
		view = camera.GetComponent <MainCamera> ();
	}
	
	public override void TriggerAction ()
	{
		view.SetView(changeViewTo);
	}
}
