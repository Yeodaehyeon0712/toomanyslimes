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
    #endregion



    void OnMove(float deltaTime)
    {
        //_owner.Transform.Translate(_owner.StatComponent.MoveSpeed * Vector3.right * deltaTime, Space.Self);
    }
}
