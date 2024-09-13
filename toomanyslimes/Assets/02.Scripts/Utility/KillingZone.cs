using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillingZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Actor actor = collision.GetComponent<Actor>();
            actor?.Death();
        }
        if (collision.CompareTag("Item"))
        {
            Item item = collision.GetComponent<Item>();
            item.DestroyItem();
        }
        if(collision.CompareTag("Area"))
        {
            AreaBase area = collision.GetComponent<AreaBase>();
            area.DestroyArea();
        }
    }
}
