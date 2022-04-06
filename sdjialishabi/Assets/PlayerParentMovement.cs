using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParentMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed = 10;
    Rigidbody2D rig;
    public float jumpHeight = 5;
    public float aSpeed = -9.8f;

    private Vector2 direction;
    private bool isJump = false;
    private float velocity_Y;
    private bool jumpattack;

    public Transform childTransform;
    private GameObject[] play;
    private GameObject[] light1;
    private GameObject lig;

    public int maxHealth = 5;
    public int health { get { return currentHealth; } }
    int currentHealth;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        jumpattack = false;
    }
    // Update is called once per frame
    void Update()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical")*0.7f;

        if (Input.GetKeyDown(KeyCode.Space) && !isJump)
        {
            isJump = true;
            ReadyJump();
            jumpattack = true;
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
            if(jumpattack == true)
            {
                jumpattack = false;
                attack();
            }
            if (childTransform.position == transform.position)
            {
                isJump = false;

            }
        }
        
        childTransform.Translate(new Vector3(0, velocity_Y) * Time.fixedDeltaTime);
    }

    public bool UmbrellaAttact(Transform attacker, Transform attacked, float angle, float radius)
    {
        light1 = GameObject.FindGameObjectsWithTag("Light");
        for (int i = 0; i < light1.Length; i++)
        {
            if(light1[i].GetComponent<Light>().enabled == true)
            {
                lig = light1[i];
            }
        }

        Vector2 deltaA = attacker.position - attacked.position;
        Vector2 direct = attacked.position - lig.transform.position;

        float tmpAngle = Mathf.Acos(Vector2.Dot(deltaA.normalized, direct.normalized)) * Mathf.Rad2Deg;
    
        if (tmpAngle<angle* 0.5f && deltaA.magnitude<radius)
        {
            return true;
        }
        return false;
    }

    void attack()
    {

        play = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < play.Length; i++)
        {
            if (play[i] != this.gameObject)
            {
                if (UmbrellaAttact(this.gameObject.transform, play[i].transform, 45, 2))
                {
                    PlayerParentMovement controller = play[i].GetComponent<PlayerParentMovement>();
                    controller.ChangeHealth(-1);
                }
            }

        }
        
    }

    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }

}
