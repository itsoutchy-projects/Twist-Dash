using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Basic properties")]
    public float speed;
    public float jumpForce;
    [Tooltip("Is this level in platformer mode?")]public bool platformerMode = true;
    [Space]

    [Header("Jumping")]
    [Tooltip("The player will only be able to jump if velocity < this")]public float range = 0.25f;
    [Space]

    [Header("Keybinds")]
    public KeyCode left = KeyCode.A;
    public KeyCode jump = KeyCode.W;
    public KeyCode jump2 = KeyCode.Space;
    public KeyCode right = KeyCode.D;
    [Space]

    [Header("Particles")]
    public ParticleSystem death;
    [Space]

    [Header("Required components")]
    public Rigidbody2D rb;
    public Animator animator;
    public Collider2D collider;
    [Space]

    [Header("Required")]
    public AudioSource music;

    [HideInInspector] public bool alive = true;

    private Vector3 startPos;
    [HideInInspector] public float attempts = 1;

    private void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            if (platformerMode)
            {
                if (Input.GetKey(left))
                {
                    rb.velocity = new Vector2(-speed, rb.velocity.y);
                }
                if (Input.GetKey(right))
                {
                    rb.velocity = new Vector2(speed, rb.velocity.y);
                }
                if (!Input.GetKey(left) && !Input.GetKey(right))
                {
                    rb.velocity = new Vector2(0, rb.velocity.y);
                }
            } else
            {
                rb.MovePosition(new Vector2(rb.position.x + (speed / 20), rb.position.y));
            }
            
            if (Input.GetKeyDown(jump) || Input.GetKeyDown(jump2))
            {
                if (/*collider.IsTouchingLayers(3)*/ /*rb.velocity.y < range*/ rb.velocity.y == 0)
                {
                    rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                    if (!platformerMode)
                    {
                        rb.MoveRotation(360);
                    }
                    animator.Play("Jump");
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6 && alive)
        {
            StartCoroutine(die());
        }
    }

    IEnumerator die()
    {
        death.Play();
        alive = false;
        yield return new WaitForSeconds(1);
        transform.position = startPos;
        alive = true;
        music.Stop();
        music.Play();
        attempts++;
    }
}
