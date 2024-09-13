using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AreaBase : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            OnCollisionItem();
    }
    protected abstract void OnCollisionItem();

    public virtual void DestroyArea()
    {
        Destroy(transform.parent.gameObject);
    }
}
