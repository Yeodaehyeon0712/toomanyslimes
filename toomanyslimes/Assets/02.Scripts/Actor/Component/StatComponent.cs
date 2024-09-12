using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatComponent : BaseComponent
{
    public StatComponent(Actor owner) : base(owner, eComponent.StatComponent)
    {
        OnReset();
    }
    //체력
    //공격력
    //원격 공격 여부

    // Main Attack Status
    public double AttackDamage;
    public float AttackSpeed;
}
