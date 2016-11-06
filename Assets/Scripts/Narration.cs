using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Narration : MonoBehaviour
{
	public float defaultSpeed = 20f;
	public float openDelay = 10f;

	private Text content;
	private string text = " ";
	private int writingProgress = 0;
	private float timer = 0f;
	private float writingSpeed;
	private bool isOpen = false;
	
	void Start ()
	{
		content = GetComponentInChildren <Text> ();
	}
	
	void Update ()
	{
		if(writingProgress <= text.Length) Write();
	}

	void Write()
	{
		if(Time.time > timer + 1f/writingSpeed)
		{
			if(writingProgress == text.Length)
			{
				content.text = text;
			}
			else
			{
				float delay = 0f;
				if(text[writingProgress] == '<')
				{
					int delayInfoEnd = text.IndexOf('>');
					string delayInfo = text.Substring(writingProgress + 1, delayInfoEnd - writingProgress - 1);
					delay = int.Parse(delayInfo);
					text = text.Remove(writingProgress, delayInfoEnd - writingProgress + 1);
				}
				content.text = text.Remove(writingProgress);
				timer = Time.time + delay/1000;
			}		
			writingProgress ++;
		}
	}
	
	public void OpenNarration()
	{
		Animator anim = GetComponent <Animator> ();
		anim.SetBool("show", true);
		timer = Time.time + openDelay;
		isOpen = true;
	}
	
	public void CloseNarration()
	{
		Animator anim = GetComponent <Animator> ();
		anim.SetBool("show", false);
		content.text = " ";
		isOpen = false;
	}
	
	public void SetNarration(string t)
	{
		writingSpeed = defaultSpeed;
		writingProgress = 0;
		text = t;
	}

	public bool isWriting()
	{
		return writingProgress < text.Length;
	}

	public void SpeedUp()
	{
		writingSpeed = defaultSpeed * 2;
	}

	public bool IsOpen ()
	{
		return isOpen;
	}
}
