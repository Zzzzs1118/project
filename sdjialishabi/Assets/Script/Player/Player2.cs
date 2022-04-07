using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed = 10;
    Rigidbody2D rig;
    public float jumpHeight = 5;
    public float aSpeed = -9.8f;
    GameObject[] ice;
    public bool pickIce = false;
    private Animator animator;
    private Vector2 direction;
    private bool isJump = false;
    private float velocity_Y;
    GameObject pig;
    Vector2 lookDirection = new Vector2(1, 0);

    public Transform childTransform;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        pickIce = false;
        pig = GameObject.Find("Pig");
        animator = pig.GetComponent<Animator>();

    }
    // Update is called once per frame
    void Update()
    {
        
        direction.x = Input.GetAxisRaw("Horizontal2");
        direction.y = Input.GetAxisRaw("Vertical2") * 0.7f;

        Vector2 move = new Vector2(direction.x, direction.y);

        if (Input.GetKeyDown(KeyCode.KeypadEnter) && !isJump)
        {
            isJump = true;
            ReadyJump();
            animator.SetTrigger("Hit");
        }
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);



        /*Vector2 position = transform.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;
        transform.position = position;
        rigidbody2d.MovePosition(position);*/

    }

    private void FixedUpdate()
    {
        Move();
        Jump();
        SlowEnemy();
    }
    void ReadyJump()
    {
        velocity_Y = Mathf.Sqrt(jumpHeight * -2f * aSpeed);
    }
    void Move()
    {
        rig.velocity = direction * moveSpeed * 50 * Time.fixedDeltaTime;

    }
    void Jump()
    {
        velocity_Y += aSpeed * Time.fixedDeltaTime;
        if (childTransform.position.y <= transform.position.y + 0.05f && velocity_Y < 0)
        {
            velocity_Y = 0;
            childTransform.position = transform.position;

            if (childTransform.position == transform.position)
            {
                isJump = false;

            }
        }
        childTransform.Translate(new Vector3(0, velocity_Y) * Time.fixedDeltaTime);
    }
    void SlowEnemy()
    {
        ice = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < ice.Length; i++)
        {
            if (ice[i] != this.gameObject)
            {
                if (pickIce && Input.GetKeyDown(KeyCode.Keypad1))
                {
                    StartCoroutine(Slow(ice[i]));

                }
            }
        }
    }
    private IEnumerator Slow(GameObject player)
    {
        Debug.Log("qq");
        PlayerParentMovement controller = player.GetComponent<PlayerParentMovement>();
        controller.moveSpeed = 3f;
        pickIce = false;
        yield return new WaitForSeconds(3.0f);

        controller.moveSpeed = 5f;


    }
}
