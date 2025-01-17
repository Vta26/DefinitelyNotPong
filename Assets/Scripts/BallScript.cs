using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public float _speed = 5.0f;
    public float x_value = -1.0f;
    public float y_value = 1.0f;
    public int scored = 0;
    private int score_left = 0;
    private int score_right = 0;
    private bool game_over = false;
    public float target_x;
    public double target_y;
    public TextMeshProUGUI _text;
    public TextMeshProUGUI _winner_text;
    private bool isPaused = false;

    void Start()
    {
        y_value = Random.Range(-1.0f,1.0f);
        _text.text = score_left.ToString() + ":" + score_right.ToString();
        print("Game Start");
    }

    // Update is called once per frame
    void Update()
    {
        if (game_over & Input.GetKeyDown(KeyCode.Return))
        {
            game_over = false;
            score_left = 0;
            score_right = 0;
            _text.text = score_left.ToString() + ":" + score_right.ToString();
            _winner_text.text = "";
        }

        if (!isPaused & !game_over)
        {
            Vector3 move = new Vector3(x_value, y_value, 0);
            transform.position += move * _speed * Time.deltaTime;
        }
    }

    private void prediction(double tempy, double changey)
    {
        //Wall collisions are at 4.69 and -4.69 => 9.38
        //Player collisions are at 9.656 and -9.669 => 19.325
        double total_change = changey + tempy;
        if (total_change>9.38)
        {
            changey = -(total_change - 9.38);
            prediction(9.38,changey);
            return;
        }
        if (total_change<0)
        {
            changey = -(changey + tempy);
            prediction(0,changey);
            return;
        }
        if (changey < 0)
        {
            Debug.Log("Final direction Down");
            if (tempy==9.38)
            {
                target_y = 4.69 + changey;
            }
            else
            {
                target_y = tempy - 4.69 + changey;
            }
        }
        else
        {
            Debug.Log("Final direction Up");
            if (tempy==0)
            {
                target_y = -4.69 + changey;
            }
            else
            {
                target_y = tempy - 4.69 + changey;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "KillZone")
        {
            x_value = 0;
            y_value = 0;
            if (transform.position.x>0){
                scored = 1;
                score_left++;
            }
            else
            {
                scored = -1;
                score_right++;
            }
            transform.position = new Vector3(0,0,0);
            _speed = 5.0f;
            _text.text = score_left.ToString() + ":" + score_right.ToString();
            if (score_left >= 10)
            {
                print("Press Enter to continue");
                _winner_text.text = "Player 1 Wins!";
                game_over = true;
            }
            if (score_right >= 10)
            {
                print("Press Enter to continue");
                _winner_text.text = "Player 2 Wins!";
                game_over = true;
            }
            StartCoroutine(PauseBeforeRestart());
        }
    }

    private IEnumerator PauseBeforeRestart()
    {
        isPaused = true;
        yield return new WaitForSeconds(3f);
        isPaused = false;
        y_value = Random.Range(-1.0f,1.0f);
        if (scored < 0)
        {
            x_value = -1.0f;
        }
        else
        {
            x_value = 1.0f;
        }
        scored = 0;
    }

    private double absolutedouble(double x)
    {
        if(x<0)
        {
            return -x;
        }
        return x;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            x_value = -x_value;
            target_x = x_value;
            if (y_value > 0)
            {
                y_value = Random.Range(0.0f,1.0f);
            }
            else
            {
                y_value = Random.Range(-1.0f,0.0f);
            }
            //Predict the final y-value of the ball as distance from the bottom wall
            prediction(this.transform.position.y - -4.69, absolutedouble(19.325/x_value)*y_value);
            _speed = _speed + 0.2f;
        }

        if (collision.gameObject.tag == "Wall")
        {
            y_value = -y_value;
        }
    }
}