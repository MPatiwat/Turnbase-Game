using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revive : MonoBehaviour
{
    [SerializeField] GameObject reviveButton;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "P")
        {
            reviveButton.SetActive(true);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "P")
        {
            reviveButton.SetActive(false);

        }
    }
}
