using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    protected float damage;
    protected float speed;
    protected float lifeTime;
    protected bool isRangeWeaphon;
    //��ų �����͸� �ް� ���� ..
    public virtual void InitWeapon()
    {
        //������ ������ �޾Ƽ� ó�� ..
    }
    public virtual void Spawn()
    {
        gameObject.SetActive(true);
    }
    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���� ����ü�� Enemy �±׸� ���� ��ü�� �ε����ٸ�
        if (collision.CompareTag("Enemy"))
            OnCollisionMonster(collision.GetComponent<Monster>());           
    }
    protected virtual void OnCollisionMonster(Monster monster)
    {
        monster.GetComponent<Monster>().Hit(damage);
    }

}
