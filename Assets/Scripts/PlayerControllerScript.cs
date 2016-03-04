using UnityEngine;
using System.Collections.Generic;

public class PlayerControllerScript : MonoBehaviour {

    public int currentTick = 0;

    bool movingRight = false;
    bool movingLeft = false;
    float move = 0;

    public float maxSpeed = 10f;
	public float jumpForce = 700f;

	bool facingRight = true;
	public float velMod =1;
	public bool dblJmpGli = false;
	public bool doubleJump = false;
	public int numJumps = 0;

	Animator anim;

	bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.1f;
	public LayerMask whatIsGround;

    public bool recorded = false;
    public List<int> jumpTick;
    public List<int> leftTick;
    public List<int> rightTick;
    int nextJumpTick;
    int nextLeftTick;
    int nextRightTick;

    void OnDisable()
    {
        RecordManagerScript.recorder.Save();
    }

    void Start ()
	{
		anim = GetComponent<Animator> ();
        if (recorded)
        {
            nextJumpTick = jumpTick[0];
            nextLeftTick = leftTick[0];
            nextRightTick = rightTick[0];

            jumpTick.RemoveAt(0);
            leftTick.RemoveAt(0);
            rightTick.RemoveAt(0);
        }
	}

	void Update()
	{
        currentTick++;
        if (!recorded)
        {
            if ((grounded || !doubleJump) && Input.GetButtonDown("Jump"))
            {
                RecordManagerScript.recorder.currentPlayerJumpTick.Add(currentTick);
                jump();
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                RecordManagerScript.recorder.currentPlayerRightTick.Add(currentTick);
                movingRight = true;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                RecordManagerScript.recorder.currentPlayerLeftTick.Add(currentTick);
                movingLeft = true;
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                RecordManagerScript.recorder.currentPlayerRightTick.Add(currentTick);
                movingRight = false;
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                RecordManagerScript.recorder.currentPlayerLeftTick.Add(currentTick);
                movingLeft = false;
            }
        }
        else
        {
            if(nextJumpTick != -1 && currentTick >= nextJumpTick)
            {
                jump();
                if (jumpTick.Count > 0)
                {
                    nextJumpTick = jumpTick[0];
                    jumpTick.RemoveAt(0);
                }
                else
                {
                    nextJumpTick = -1;
                }
            }
            if (nextLeftTick != -1 && currentTick >= nextLeftTick)
            {
                movingLeft = !movingLeft;
                if (leftTick.Count > 0)
                {
                    nextLeftTick = leftTick[0];
                    leftTick.RemoveAt(0);
                }
                else
                {
                    nextLeftTick = -1;
                }
            }
            if (nextRightTick != -1 && currentTick >= nextRightTick)
            {
                movingRight = !movingRight;
                if (rightTick.Count > 0)
                {
                    nextRightTick = rightTick[0];
                    rightTick.RemoveAt(0);
                }
                else
                {
                    nextRightTick = -1;
                }
            }
        }

        if (movingRight)
            move = 1 * velMod;
        else if (movingLeft)
            move = -1 * velMod;
        else
            move = 0;
    }

	public void jump()
	{	
		anim.SetBool("Ground", false);
		
		GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
		
		GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
		if(!grounded)
		{
			doubleJump = true;
			
		}
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		anim.SetBool ("Ground", grounded);

		if(grounded && !dblJmpGli)
			doubleJump = false;

		anim.SetFloat ("vSpeed", GetComponent<Rigidbody2D>().velocity.y);

		anim.SetFloat("Speed", Mathf.Abs(move));

		GetComponent<Rigidbody2D>().velocity = new Vector2 (maxSpeed * move, GetComponent<Rigidbody2D>().velocity.y);

		if(move > 0 && !facingRight)
			Flip();
		else if(move < 0 && facingRight)
			Flip();
	}

	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
