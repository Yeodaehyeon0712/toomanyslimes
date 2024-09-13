using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : Item
{
    float damage = 5000f;
    protected override void OnCollisionItem()
    {
        Player.PlayerCharacter.Hit(damage);
    }
}
