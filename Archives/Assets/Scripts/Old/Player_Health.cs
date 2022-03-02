using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Health : MonoBehaviour
{
    public bool died;

    // Start is called before the first frame update
    void Start()
    {
        died = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y < -7)
        {
            Die();
            Debug.Log("Player has died - Fallen below -7");
        }
    }

    void Die()
    {
        SceneManager.LoadScene("Prototype_1");
    }
}
