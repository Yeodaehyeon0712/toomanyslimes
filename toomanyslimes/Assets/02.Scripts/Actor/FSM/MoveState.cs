using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : BaseState
{
    
    public MoveState(Actor owner) : base(owner)
    {

    }

    #region Abstract Method
    public override void OnStateEnter()
    {

    }
    public override void OnStateStay(float deltaTime)
    {

    }
    public override void OnStateExit()
    {

    }
    public override void Reset()
    {
        base.Reset();
    }
    #endregion
}
