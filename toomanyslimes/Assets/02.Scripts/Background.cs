using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    float speed=3;
    int startIndex;
    int endIndex;
    float viewHeight;
    public Transform[] sprites;

    private void Awake()
    {
        viewHeight = Camera.main.orthographicSize * 2;
        startIndex = 0;
        endIndex = sprites.Length-1;
    }

    private void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (sprites[endIndex].position.y < -viewHeight)
        {
            Vector3 newPos = sprites[startIndex].localPosition + Vector3.up * viewHeight;
            sprites[endIndex].localPosition = newPos;

            startIndex = endIndex;
            endIndex = (endIndex == 0) ? sprites.Length - 1 : (endIndex - 1);
        }
    }
}
