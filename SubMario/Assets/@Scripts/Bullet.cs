using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 Velocity = new Vector2(10, 0);

    private void Update()
    {
        if (!GetComponent<SpriteRenderer>().isVisible)
        {
            gameObject.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        transform.Translate(Velocity * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Terrain")
        {
            gameObject.SetActive(false);
        }
        if (collision.gameObject.tag == "Enemy")
        {
            gameObject.SetActive(false);
            collision.GetComponent<EnemyController>().Hit(1);
        }
    }
}
