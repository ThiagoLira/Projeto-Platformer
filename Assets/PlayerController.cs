using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float moveSpeed;
	public float jumpHeight;
	bool lookRight;
	private Animator animator;
	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask groundLayer;
	private bool grounded;
	int jumpCount;
	// Use this for initialization
	void Start () {
		lookRight = true;
		animator = GetComponent<Animator>();
	}

	void FixedUpdate() {
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, groundLayer);

	}

	// Update is called once per frame
	void Update () {
		if (grounded)
			jumpCount = 0;
		if(Input.GetKeyDown (KeyCode.Space))
		{
			if(grounded)
				rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, jumpHeight);
			else if(jumpCount == 0)
			{
				rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, jumpHeight);
				animator.SetTrigger("special");
				jumpCount++;
			}
		}
		if(Input.GetKey(KeyCode.D))
		{
			rigidbody2D.velocity = new Vector2 (moveSpeed, rigidbody2D.velocity.y);
			if(!lookRight)
				Flip();
		}
		if(Input.GetKey(KeyCode.A))
		{
			rigidbody2D.velocity = new Vector2 (-moveSpeed, rigidbody2D.velocity.y);
			if(lookRight)
				Flip();
		}
		if(Input.GetKeyUp(KeyCode.A) && !Input.GetKey(KeyCode.D))
			rigidbody2D.velocity = new Vector2 (0, rigidbody2D.velocity.y);
		if(Input.GetKeyUp(KeyCode.D) && !Input.GetKey(KeyCode.A))
			rigidbody2D.velocity = new Vector2 (0, rigidbody2D.velocity.y);
		if(Input.GetMouseButtonDown(1))
		{
			animator.SetTrigger("attack");
		}
		animator.SetFloat ("speed", Mathf.Abs (rigidbody2D.velocity.x));
	}

	public void Flip()
	{
		var s = transform.localScale;
		s.x *= -1;
		transform.localScale = s;
		lookRight = !lookRight;
	}
}
