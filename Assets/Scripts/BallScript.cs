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
        print("Game Start");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(x_value, y_value, 0);
        transform.position += move * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "KillZone")
        {
            x_value = 0;
            y_value = 0;
            transform.position = new Vector3(0,0,0);
            //if enter is pressed, randomize between y = 1 and y = -1, and set x to whichever side got scored on
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("Collision Detected");
        if (collision.gameObject.tag == "Player")
        {
            print("Player Collision");
            x_value = -x_value;
        }

        if (collision.gameObject.tag == "Wall")
        {
            print("Wall Collision");
            y_value = -y_value;
        }
    }
}
