using UnityEngine;
using System.Collections;

public class ScreenFaderModifier : CutsceneAction
{
	public bool oneWay = true;
	public bool fromBlack = true;
	public float fadeOutTime = 1f;
	public float fadeInTime = 1f;

	private Animator faderAnimator;

	void Start ()
	{
		GameObject fader = GameObject.FindGameObjectWithTag("ScreenFader");
		faderAnimator = fader.GetComponent <Animator> ();
	}

	void FadeIn ()
	{
		float speed = (fadeInTime == 0) ? 100 : 1f/fadeInTime;
		faderAnimator.SetFloat("speed", speed);
		faderAnimator.SetBool("blackScreen", false);
	}

	void FadeOut ()
	{
		float speed = (fadeOutTime == 0) ? 100 : 1f/fadeOutTime;
		faderAnimator.SetFloat("speed", speed);
		faderAnimator.SetBool("blackScreen", true);
	}

	public override void TriggerAction ()
	{
		if(fromBlack)
		{
			FadeIn ();
			if(oneWay) return;
			FadeOut ();
		}
		else
		{
			FadeOut ();
			if(oneWay) return;
			FadeIn ();
		}
	}
}
