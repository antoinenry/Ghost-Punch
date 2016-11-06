using UnityEngine;
using System.Collections;

public class LightManager : MonoBehaviour
{
	public Sprite[] lightSprites;
	public float[] foregroundDarkness;
	public int defaultLight = 1;
	public GameObject room;

	private SpriteRenderer sr;
	private int lightIndex;
	private SpriteRenderer[] renderersInRoom;
	private SpriteRenderer playerRenderer;
	private Animator faderAnim;

	void Start()
	{
		sr = GetComponent <SpriteRenderer> ();
		renderersInRoom = room.GetComponentsInChildren <SpriteRenderer> ();
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		playerRenderer = player.GetComponent <SpriteRenderer> ();
		GameObject fader = GameObject.FindGameObjectWithTag("ScreenFader");
		faderAnim = fader.GetComponent <Animator> ();

		SetLight(defaultLight, 0f);
	}

	void FadeIn()
	{
		faderAnim.SetBool("blackScreen", false);
	}

	void FadeOut()
	{
		faderAnim.SetBool("blackScreen", true);
	}

	void UpdateLight()
	{
		sr.sprite = lightSprites[lightIndex];
		float l = 1f - foregroundDarkness[lightIndex];
		playerRenderer.color = new Color(l + .2f, l + .2f, l + .2f) ;
		for(int i = 0; i < renderersInRoom.Length; i++)
			if(renderersInRoom[i].sortingLayerName == "BeforePlayer")
				renderersInRoom[i].color = new Color(l, l, l) ;
	}

	public void SetLight(int index, float transitionTime)
	{
		if(index > lightSprites.Length)
			return;
		
		lightIndex = index;

		if(transitionTime > float.Epsilon)
		{
			faderAnim.SetFloat("speed", .5f/transitionTime);
			FadeOut();
			Invoke("FadeIn", transitionTime/2f);
			Invoke("UpdateLight", transitionTime/2f);
		}
		else
		{
			UpdateLight();
		}
	}

	public bool IsOff()
	{
		return lightIndex == 0;
	}

	public int Capacity()
	{
		return lightSprites.Length;
	}
}