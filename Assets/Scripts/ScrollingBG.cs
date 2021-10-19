using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBG : MonoBehaviour
{
    [SerializeField] private float scrollingSpeed;
    [SerializeField] private float bound;
    private void Update()
    {
        transform.Translate(-1 * scrollingSpeed * Time.deltaTime, 0, 0);
        if(transform.position.x <= bound)
        {
            transform.position = new Vector2(0, 0);
        }
    }
}
