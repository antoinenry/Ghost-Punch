using UnityEngine;
using System.Collections;

public class CutsceneTrigger : MonoBehaviour
{
	public Cutscene cutscene;
	public bool oneUse;

	void OnTriggerEnter2D(Collider2D other)
	{
		enabled = false;
		if(other.tag == "Player" && !other.isTrigger)
			cutscene.PlayScene ();
		if(oneUse)
			Destroy(this, 1);
	}
}
