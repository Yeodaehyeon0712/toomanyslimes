using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    protected float damage;
    protected float speed;
    protected float lifeTime;
    protected bool isRangeWeaphon;
    //스킬 데이터를 받고 세팅 ..
    public virtual void InitWeapon()
    {
        //원래는 데이터 받아서 처리 ..
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
        // 만약 투사체가 Enemy 태그를 가진 객체에 부딪힌다면
        if (collision.CompareTag("Enemy"))
            OnCollisionMonster(collision.GetComponent<Monster>());           
    }
    protected virtual void OnCollisionMonster(Monster monster)
    {
        monster.GetComponent<Monster>().Hit(damage);
    }

}
