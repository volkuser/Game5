using UnityEngine;

public class handler_food : MonoBehaviour
{
    private GameManager GameManager;
    void Start()
    {
        GameManager = GameObject.Find("GameManager")
            .GetComponent<GameManager>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        GameManager.AddScore(10);
        Destroy(gameObject);
    }
}
