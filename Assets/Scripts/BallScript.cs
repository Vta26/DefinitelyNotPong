using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public float _speed = 5.0f;
    public int x_value = -1;
    public int y_value = 1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(x_value, y_value, 0);
        transform.position += move * _speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            print("Player Collision");
            if (x_value == -1)
            {
                x_value = 1;
            }
            else
            {
                x_value = -1;
            }
        }

        if (collision.gameObject.tag == "Wall")
        {
            print("Wall Collision");
            if (y_value == -1)
            {
                y_value = 1;
            }
            else
            {
                y_value = -1;
            }
        }
    }
}
