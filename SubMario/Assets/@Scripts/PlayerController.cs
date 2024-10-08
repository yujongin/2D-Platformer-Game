using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.VFX;

public class PlayerController : MonoBehaviour
{

    enum State
    {
        Playing,
        Dead
    }
    public float Speed = 5f;
    public float JumpSpeed = 5f;
    public Collider2D BottomCollider;
    public CompositeCollider2D TerrainCollider;

    float vx = 0;
    bool isGrounded;

    float prevVx = 0;

    State state;
    Vector2 originalPosition;

    void Start()
    {
        originalPosition = transform.position;
        state = State.Playing;
    }

    public void Restart()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        GetComponent<Rigidbody2D>().angularVelocity = 0;
        GetComponent<BoxCollider2D>().enabled = true;


        state = State.Playing;
        transform.eulerAngles = Vector3.zero;
        transform.position = originalPosition;
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
    }
    void Update()
    {
        if (state == State.Dead) return;

        vx = Input.GetAxisRaw("Horizontal") * Speed;
        float vy = GetComponent<Rigidbody2D>().linearVelocityY;
        if (vx < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (vx > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        if (BottomCollider.IsTouching(TerrainCollider))
        {
            if (!isGrounded)
            {
                if (vx == 0)
                {
                    GetComponent<Animator>().SetTrigger("Idle");
                }
                else
                {
                    GetComponent<Animator>().SetTrigger("Run");
                }
            }
            else
            {
                if (vx != prevVx)
                {
                    if (vx == 0)
                    {
                        GetComponent<Animator>().SetTrigger("Idle");
                    }
                    else
                    {
                        GetComponent<Animator>().SetTrigger("Run");
                    }
                }
            }
        }
        else
        {
            if (isGrounded)
            {
                GetComponent<Animator>().SetTrigger("Jump");
            }
        }

        isGrounded = BottomCollider.IsTouching(TerrainCollider);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            vy = JumpSpeed;
        }
        prevVx = vx;
        GetComponent<Rigidbody2D>().linearVelocity = new Vector2(vx, vy);

        if (Input.GetButtonDown("Fire1"))
        {
            Vector2 bulletV = new Vector2(10, 0);
            if (GetComponent<SpriteRenderer>().flipX)
            {
                bulletV.x = -bulletV.x;
            }

            GameObject bullet = GameManager.Instance.BulletPool.GetObject();
            bullet.transform.position = transform.position;
            bullet.GetComponent<Bullet>().Velocity = bulletV;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Dead();
        }
    }

    void Dead()
    {
        state = State.Dead;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        GetComponent<Rigidbody2D>().angularVelocity = 720;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
        GetComponent<BoxCollider2D>().enabled = false;

        GameManager.Instance.Dead();
    }
}
