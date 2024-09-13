using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeWeapon : WeaponBase
{
    public override void InitWeapon()
    {
        damage = 3f;
        speed = 1f;
        lifeTime = 2f;
        isRangeWeaphon = false;
    }
}
