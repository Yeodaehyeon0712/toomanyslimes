using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : BaseState
{
    public DeathState(Actor owner):base(owner)
    {

    }
    public override void OnStateEnter()
    {
        //캐릭터와 닿음을 감지 ?
    }

    public override void OnStateExit()
    {

    }

    public override void OnStateStay(float deltaTime)
    {

    }
}
