using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowWeapon : WeaponBase
{
    public override void InitWeapon()
    {
        damage = 50f;
        speed = 3f;
        lifeTime = 3f;
        isRangeWeaphon = true;
    }
    public override void Spawn()
    {
        base.Spawn();
        Destroy(gameObject, lifeTime);
    }
    private void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
    protected override void OnCollisionMonster(Monster monster)
    {
        base.OnCollisionMonster(monster);
        Destroy(gameObject);
    }
}
