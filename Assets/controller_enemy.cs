using UnityEngine;

public class controller_enemy : MonoBehaviour
{
    private GameManager GameManager;
    void Start()
    {
        GameManager = GameObject.Find("GameManager")
            .GetComponent<GameManager>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        GameManager.Death();
    }
    
    // for triggers
    /*void Update()
    {
        GameManager.Death();   
    }*/
}
