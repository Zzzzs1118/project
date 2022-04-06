using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerParentMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed = 10f;
    Rigidbody2D rig;
    public float jumpHeight = 5;
    public float aSpeed = -9.8f;
    public bool pickIce = false;
    GameObject[] ice;
    private Animator animator;
    private Vector2 direction;
    private bool isJump = false;
    private float velocity_Y;
    GameObject chicken;
    Vector2 lookDirection = new Vector2(1, 0);

    public Transform childTransform;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        pickIce = false;
        chicken =GameObject.Find("Chicken");
        animator =chicken.GetComponent<Animator>();
        
        
    }
    // Update is called once per frame
    void Update()
    {
        
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical")*0.7f;

        Vector2 move = new Vector2(direction.x, direction.y);

        if (Input.GetKeyDown(KeyCode.Space) && !isJump)
        {
            isJump = true;
            ReadyJump();
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
        /* if (Input.GetKeyDown(KeyCode.D))
         {
             animator.SetBool("Right", true);
             animator.SetBool("Left", false);
         }
         else if (Input.GetKeyDown(KeyCode.A))
         {
             animator.SetBool("Right", false);
             animator.SetBool("Left", true);
         }*/
         
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
                if (pickIce && Input.GetKeyDown(KeyCode.J))
                {
                    StartCoroutine(Slow(ice[i]));                
                }
            } 
        }
    }
    private IEnumerator Slow(GameObject player2)
    {
        Debug.Log("qq");
        Player2 controller = player2.GetComponent<Player2>();
        controller.moveSpeed = 1f;
        pickIce = false;
        yield return new WaitForSeconds(3.0f);
        controller.moveSpeed = 3f;


    }
}