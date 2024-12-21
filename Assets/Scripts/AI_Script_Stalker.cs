using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AI_Script_Stalker : MonoBehaviour
{
    public float _speed = 10.0f;
    public GameObject ball;
    private int y_move;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _speed = ball.GetComponent<BallScript>()._speed*2;
        if (ball.transform.position.y > this.transform.position.y)
        {
            y_move = 1;
        }
        if (ball.transform.position.y < this.transform.position.y)
        {
            y_move = -1;
        }
        if ((y_move == -1 && transform.position.y <= -4.185)|(y_move == 1 && transform.position.y >= 4.185))
        {
            return;
        }
        Vector3 move = new Vector3(0,y_move,0);
        transform.position += move * _speed * Time.deltaTime;
    }
}
