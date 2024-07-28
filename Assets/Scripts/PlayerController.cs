using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class PlayerContreoller : MonoBehaviour
{

    public float Speed = 3.0F;
    public float SpeedJump = 3.0F;
    public LayerMask GroundLayer;

    private Rigidbody2D body;
    private Animator animator;

    private bool isLeftMove;
    private bool isRunning;
    private bool isJumping;

    private bool isGameOvered;

    private float lastTime;


    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        // Обробка руху персонажа за допомогою клавіатури
        float horizontal = Input.GetAxisRaw("Horizontal");

        // Обробка сенсорного натискання для руху вліво/вправо
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            bool isGround = Physics2D.Raycast(transform.position - transform.localScale / 2, Vector2.down, 0.1f, GroundLayer);


            if ((touch.phase == TouchPhase.Began || Input.GetKeyDown(KeyCode.UpArrow)) && isGround)
            {
                print(lastTime + 0.5f > Time.time);
                if (lastTime + 0.5f > Time.time)
                {
                    Jump();
                } else
                {
                    lastTime = Time.time;
                }
            } else if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            {
                horizontal = touch.position.x < Screen.width / 2 ? -1 : 1;

            }

        }

        Move(horizontal);


        if (transform.position.y < -4)
        {
            GameOver();
        }

        switchAnimation();

    }

    private void Move(float horizontal)
    {
        if (horizontal != 0f)
        {
            isRunning = true;
            animator.SetInteger("Speed", 1);
            body.position += new Vector2(horizontal * Speed * Time.deltaTime, 0);

            if (horizontal > 0f && isLeftMove || horizontal < 0f && !isLeftMove)
            {
                var currentScale = transform.localScale;
                currentScale.x *= -1;
                transform.localScale = currentScale;
                isLeftMove = !isLeftMove;
            }
        }
        else
        {
            isRunning = false;
            animator.SetInteger("Speed", 0);
        }
    }

    private void Jump()
    {
        isJumping = true;
        body.AddForce(new Vector2(0, SpeedJump), ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Win();
    }

    private void Win()
    {
        Destroy(gameObject, 1);
    }

    private void OnDestroy()
    {
        GameOver();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isJumping = false;

        if (collision.gameObject.tag == "Enemy")
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        if (isGameOvered)
        {
            return;
        }

        isGameOvered = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void switchAnimation()
    {
        if (isJumping)
        {
            if (body.velocity.y > 0)
            {
                animator.SetTrigger("ToJump");
            } else
            {
                animator.SetTrigger("ToDown");   
            }
        }
        else if (isRunning)
        {
            animator.SetTrigger("ToRun");
        }
        else
        {
            animator.SetTrigger("ToIdle");
        }
    }
}
