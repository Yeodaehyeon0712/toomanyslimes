using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleComponent : BaseComponent
{
    public BattleComponent(Actor owner) : base(owner, eComponent.BattleComponent)
    {
        OnReset();
    }
    protected override void OnUpdate(float deltaTime)
    {
        //АјАн ..
    }
}
