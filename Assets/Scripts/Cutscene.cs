using UnityEngine;
using System.Collections;

public class Cutscene : MonoBehaviour
{
	public bool startImmedialtely;
	public string[] narration;
	public GameObject[] action;

	private Narration narrator;
	private int step = 0;
	private int actionStep = 0;
	private bool nextKeyLock = false;
	private float timer = -1f;
	private bool loop = false;

	void Start ()
	{
		GameObject n = GameObject.FindGameObjectWithTag ("Narration");
		narrator = n.GetComponent <Narration> ();
		if(startImmedialtely) PlayScene ();
		else enabled = false;
	}

	void Update ()
	{
		bool next = Input.GetAxisRaw("Punch") > float.Epsilon;
		if (step == 0)
		{
			NextStep ();
			return;
		}
		if(loop)
		{
			NextStep ();
		}
		else if(next)
		{
			if(nextKeyLock == false) return;
			NextStep ();
			nextKeyLock = false;
		}
		else
			nextKeyLock = true;
	}

	void NextStep()
	{
		if(narrator.isWriting ())
		{
			narrator.SpeedUp();
			return;
		}
		if(step >= narration.Length)
		{
			narrator.CloseNarration();
			StopScene ();
			return;
		}
		if(narration[step].Remove(5) == ">wait")
		{
			string delayInfo = narration[step].Substring(5);
			float delay = int.Parse(delayInfo);

			if(timer < 0f)
				timer = Time.time;
			if(Time.time > timer + delay/1000f)
			{
				loop = false;
				timer = -1f;
				step += 1;
				NextStep();
			}
			else loop = true;
			return;
		}
		else if(narration[step] == "<close_narration>")
		{
			narrator.CloseNarration ();
			step ++;
			NextStep ();
			return;
		}
		else if(narration[step] != "<action>" && narration[step] != "<stopping_action>")
		{
			if(!narrator.IsOpen ()) narrator.OpenNarration();
			narrator.SetNarration(narration[step]);
		}
		else if(actionStep < action.Length)
		{
			CutsceneAction csa = action[actionStep].GetComponent <CutsceneAction> ();
			csa.TriggerAction ();
			actionStep ++;
			if(narration[step] != "<stopping_action>")
			{
				step++;
				NextStep ();
				return;
			}
		}

		step++;
	}

	public void PlayScene ()
	{
		step = 0;
		actionStep = 0;
		enabled = true;
	}

	public void StopScene ()
	{
		enabled = false;
	}
}






