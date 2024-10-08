using UnityEngine;

public class Fruit : MonoBehaviour
{
    public float TimeAdd = 5f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player"){
            GetComponent<Animator>().SetTrigger("Eaten");
            GetComponent<Collider2D>().enabled = false;
            GameManager.Instance.AddTime(TimeAdd);
        }

    }

    void DestroyThis()
    {
        Destroy(gameObject);
    }


}
