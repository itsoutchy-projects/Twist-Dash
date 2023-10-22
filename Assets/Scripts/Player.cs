using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum Mode
{
    Cube,
    Ship
}

public class Player : MonoBehaviour
{
    [Header("Basic properties")]
    public float speed;
    public float jumpForce;
    [Tooltip("Is this level in platformer mode?")]public bool platformerMode = true;
    public Mode mode = Mode.Cube;
    [Space]

    [Header("Jump pads")]
    public float jumpPadForce1;
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
    public TextMeshPro attemptsCounter;

    [Header("Sound effects")]
    public AudioClip deathSoundEffect;

    [HideInInspector] public bool alive = true;

    private Vector3 startPos;
    [HideInInspector] public float attempts = 1;
    public GameObject deathSoundOBJ;
    private AudioSource deathSound;
    private Mode defaultMode;
    

    private void Start()
    {
        startPos = transform.position;
        deathSoundOBJ = new GameObject("deathSoundOBJ");
        deathSound = deathSoundOBJ.AddComponent<AudioSource>();
        deathSound.playOnAwake = false;
        deathSound.clip = deathSoundEffect;
        deathSound.loop = false;
        defaultMode = mode;
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            if (mode == Mode.Cube)
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
                }
                else
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
            } else if (mode == Mode.Ship)
            {
                if (platformerMode)
                {
                    if (Input.GetKey(left))
                    {
                        rb.AddForce(new Vector2(-speed/(speed/2), 0));
                    }
                    if (Input.GetKey(right))
                    {
                        rb.AddForce(new Vector2(speed/(speed/2), 0));
                    }
                }
                
                if (Input.GetKey(jump) || Input.GetKey(jump2))
                {
                    rb.AddForce(new Vector2(0, 5.2f));
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
        if (collision.gameObject.layer == 7 && alive)
        {
            rb.AddForce(new Vector2(0, jumpPadForce1), ForceMode2D.Impulse);
            animator.Play("Jump");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8 && alive)
        {
            mode = Mode.Ship;
        }
        if (collision.gameObject.layer == 9 && alive)
        {
            mode = Mode.Cube;
        }
    }

    IEnumerator die()
    {
        deathSound.Play();
        death.Play();
        alive = false;
        yield return new WaitForSeconds(1);
        transform.position = startPos;
        alive = true;
        music.Stop();
        music.Play();
        attempts++;
        attemptsCounter.text = $"Attempt {attempts}";
        mode = defaultMode;
    }
}
