using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
	public float drainSpeed;
	public float recoverSpeed;
	public float mentalMax = 400;
	public int physicalMax = 3;
	public GameObject ui;

	private float mentalMin = 10;
	private float mental;
	private int physical;
	private PlayerControl player;
	private Slider[] sliders;
	private Animator uiAnimator;
		
	void Start ()
	{
		uiAnimator = ui.GetComponent<Animator> ();
		sliders = ui.GetComponentsInChildren<Slider> ();
		mental = mentalMax;
		physical = physicalMax;

		player = GetComponent <PlayerControl> ();
	}

	void FixedUpdate ()
	{
		if(player.IsEtheric ()) DrainMental ();
		else RecoverMental ();
	}

	void DrainMental ()
	{
		if (mental > mentalMin)
			mental -= drainSpeed * Time.deltaTime;
		else
			player.SetEtheric (false);

		sliders [0].value = mental / mentalMax;
		uiAnimator.SetBool ("showPlayerInfos", true);
	}

	void RecoverMental ()
	{
		if (mental == mentalMax) {
			uiAnimator.SetBool ("showPlayerInfos", false);
			return;
		}
		if (mental > mentalMax) {
			mental = mentalMax;
			return;
		}
		mental += recoverSpeed * Time.deltaTime;
		sliders [0].value = mental / mentalMax;
		uiAnimator.SetBool ("showPlayerInfos", true);
	}
}
