using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
	public float walkSpeed = 7f;
	public float skid = .1f;
	public float fallThreshhold = .001f;
	public Vector2 jumpForce;
	public Vector2 midAirControl;
	public RuntimeAnimatorController heroAC;
	public MainCamera mainCamera;

	private Rigidbody2D rb;
	private Animator anim;

	private bool punchKeyLock = false;
	private bool jumpKeyLock = false;
	private bool useKeyLock = false;
	private bool switchEthericKeyLock = false;
	private bool controlsAreLocked = false;

	private bool isFalling = false;
	private bool isUsing = false;
	private bool isOnGround = false;
	private int facingDirection = 1;
	private bool isEtheric = false;
	
	[HideInInspector] public bool isCarrying = false;

	void Start ()
	{
		rb = GetComponent <Rigidbody2D> ();
		anim = GetComponent <Animator> ();
	}

	void FixedUpdate ()
	{
		if (Input.GetButtonDown ("Cancel") == true)
			Application.Quit ();
			
		int walkAction = 0;
		bool jumpAction = false;
		bool punchAction = false;
		bool useAction = false;
		bool switchEthericAction = false;

		if(!controlsAreLocked)
		{
			if(Input.GetAxisRaw("Horizontal") > float.Epsilon) 		 walkAction = 1;
			else if(Input.GetAxisRaw("Horizontal") < -float.Epsilon) walkAction = -1;
			jumpAction = Input.GetAxisRaw("Jump") > float.Epsilon;
			punchAction = Input.GetAxisRaw("Punch") > float.Epsilon;
			useAction = Input.GetAxisRaw("Use") > float.Epsilon;
			switchEthericAction = Input.GetAxisRaw("Switch Etheric") > float.Epsilon;
		}
		
		Fall ();
		if(isOnGround)
		{
			Walk (walkAction);
			if(jumpAction) Jump (walkAction);
			else jumpKeyLock = false;
		}
		else
			MidAirControl (walkAction, jumpAction);

		if(punchAction) Punch ();
		else punchKeyLock = false;

		if(useAction)
			Use ();
		else
		{
			useKeyLock = false;
			isUsing = false;
		}

		if(switchEthericAction) ToggleEtheric ();
		else switchEthericKeyLock = false;

		isOnGround = false;
	}

	void Fall ()
	{
		if(isOnGround && isFalling)
		{
			anim.SetBool("falling", false);
			isFalling = false;
		}
		else if(rb.velocity.y < -fallThreshhold)
		{
			anim.SetBool("walking", false);
			anim.SetBool("falling", true);
			isFalling = true;
		}
	}

	void Walk (int direction)
	{
		if(direction != 0 && direction != facingDirection)
		{
			facingDirection = direction;
			SetFacing(direction);
		}

		anim.SetBool("walking", direction != 0);

		Vector2 newVelocity = new Vector2(direction * walkSpeed, rb.velocity.y);
		float inverseSkid = 1f/skid;

		rb.velocity = Vector2.Lerp(rb.velocity, newVelocity, inverseSkid * Time.deltaTime);
	}
	
	void Jump (int walkAction)
	{
		if(jumpKeyLock) return;
		else jumpKeyLock = true;

		Vector2 jump = new Vector2(walkAction * jumpForce.x, jumpForce.y);

		anim.SetTrigger("jump");
		rb.AddForce(jump);
	}
	
	void Punch ()
	{
		if(punchKeyLock) return;
		else punchKeyLock = true;
		if(!isCarrying) anim.SetTrigger("punch");
	}

	void Use ()
	{
		if(useKeyLock) isUsing = false;
		else
		{
			isUsing = true;
			useKeyLock = true;
		}
	}

	void ToggleEtheric ()
	{
		if(switchEthericKeyLock) return;
		else switchEthericKeyLock = true;
		SetEtheric(!isEtheric);
	}

	public void SetEtheric (bool toEther)
	{
		if(isEtheric == toEther) return;

		isEtheric = toEther;
		mainCamera.etheric = toEther;
		mainCamera.SmoothEthericTransition ();
		anim.SetBool ("bw", toEther);
		anim.SetTrigger ("changeSprite");
	}

	void MidAirControl (int walkControl, bool jumpControl)
	{
		Vector2 midAir = new Vector2 ();
		if(jumpControl && rb.velocity.y > 0f)
		{
			midAir.y = midAirControl.y;
		}
		midAir.x = midAirControl.x * walkControl;
		rb.AddForce(midAir);
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if(other.isTrigger) return;
		if(other.tag == "Ground" || other.tag == "Prop" ) isOnGround = true;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.isTrigger) return;
		if(other.tag == "Ground" || other.tag == "Prop" ) isOnGround = true;
	}

	public void SetFacing(int dir)
	{
		if (dir != 1 && dir != -1)
			return;

		anim.SetTrigger ("changeSprite");
		if (dir == 1) anim.SetBool("flipped", false);
		if (dir == -1) anim.SetBool("flipped", true);
	}

	public void SetPos (Transform pos, bool keepVelocity = false)
	{
//		transform.position = pos.position;
//		if(pos.rotation.y != 0)
//			anim.runtimeAnimatorController = heroFlippedAOC;
//		else
//			anim.runtimeAnimatorController = heroAC;
		if(!keepVelocity) rb.velocity = new Vector2(0f, 0f);
	}

	public void Freeze ()
	{
		rb.constraints = RigidbodyConstraints2D.FreezeAll;
		anim.enabled = false;
	}

	public void Unfreeze()
	{
		rb.constraints = RigidbodyConstraints2D.FreezeRotation;
		anim.enabled = true;
	}

	public void FreezeForSeconds (float t)
	{
		Freeze();
		Invoke("Unfreeze", t);
	}
	
	public bool IsUsing()
	{
		return isUsing;
	}
	
	public bool IsPunching()
	{
		return punchKeyLock;
	}
	
	public int GetDir()
	{
		return facingDirection;
	}

	public bool IsEtheric()
	{
		return isEtheric;
	}

	public void LockControls(bool locked)
	{
		controlsAreLocked = locked;
		punchKeyLock = true;
		jumpKeyLock = true;
		useKeyLock = true;
	}
}
