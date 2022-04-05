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

    private Vector2 direction;
    private bool isJump = false;
    private float velocity_Y;

    public Transform childTransform;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        direction.x = Input.GetAxisRaw("Horizontal2");
        direction.y = Input.GetAxisRaw("Vertical2") * 0.7f;

        if (Input.GetKeyDown(KeyCode.KeypadEnter) && !isJump)
        {
            isJump = true;
            ReadyJump();
        }
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
}
