using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleState : BaseState
{
    #region Fields
    protected float _elapsedTime = 0f;

    #endregion
    public BattleState(Actor owner) : base(owner)
    {

    }
    public override void OnStateEnter()
    {
    
    }
    public override void OnStateStay(float deltaTime)
    {
        if (_fsm.Target != null)
        {
            //죽었으면 대상에서 제외.
            if (_fsm.Target.FSMState == eFSMState.Death)
            {
                _fsm.Target = null;
                return;
            }
            //기본 공격이 가능하다면 기본공격 수행
            if (_owner.DefaultAttackElapsedTime > 1f)
            {
                DefaultAttack();
                return;
            }
        }
    }

    public override void OnStateExit()
    {
        _fsm.Target = null;
    }

    void DefaultAttack()
    {
        _owner.DefaultAttackElapsedTime = 0f;
        //나의 공격력을 담아 ..
        _fsm.Target.Hit(3f);
    }

}
