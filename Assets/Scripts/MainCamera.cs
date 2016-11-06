using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour
{
	public Camera defaultView;
	public float smoothing;
	public bool etheric;

	private Camera view;
    private Vector3 wantedPos;
	private float wantedSize;
	private bool inEthericTransition = false;

	void Start ()
	{
		view = GetComponent <Camera> ();
		SetView(defaultView);
	}

    void FixedUpdate ()
    {
		wantedPos.z = etheric ? 5 : -5;
		if(Vector3.Distance(transform.position, wantedPos) > float.Epsilon )
			transform.position = Vector3.Lerp (transform.position, wantedPos,
		                                   		smoothing * Time.deltaTime);

		if(Mathf.Abs(view.orthographicSize - wantedSize) > float.Epsilon)
			view.orthographicSize = Mathf.Lerp(view.orthographicSize, wantedSize,
			                                   smoothing * Time.deltaTime);

		if(inEthericTransition)
			SmoothEthericTransition ();
    }

	public void SmoothEthericTransition()
	{
		Color bgColor = view.backgroundColor;
		Color wantedColor = etheric ? Color.white : Color.black;
		Vector3 bgColorV = new Vector3(bgColor.r, bgColor.g, bgColor.b);
		Vector3 wantedColorV = new Vector3(wantedColor.r, wantedColor.g, wantedColor.b);

		if (Vector3.Distance (bgColorV, wantedColorV) > float.Epsilon) {
			inEthericTransition = true;
			view.backgroundColor = Color.Lerp (bgColor, wantedColor,
				smoothing * Time.deltaTime);
		}
		else
			inEthericTransition = false;
	}

	public void SetView (Camera newView)
	{
		wantedPos = newView.transform.position;
		wantedSize = newView.orthographicSize;
	}
}
