using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    bool touchedLeft;
    bool touchedRight;

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");

        if ((touchedRight && h > 0) || (touchedLeft && h < 0))
            h = 0;

        Vector3 nextPos = new Vector3(h * speed * Time.deltaTime, 0, 0);
        transform.Translate(nextPos);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.tag)
        {
            case "Border_L":
                touchedLeft = true;
                break;
            case "Border_R":
                touchedRight = true;
                break;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Border_L":
                touchedLeft = false;
                break;
            case "Border_R":
                touchedRight = false;
                break;
        }
    }
}
