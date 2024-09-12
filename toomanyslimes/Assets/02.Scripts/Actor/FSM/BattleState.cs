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
        Debug.Log("공격 상태에 진입");

    }
    public override void OnStateStay(float deltaTime)
    {
        //공격 대상이 있다면
        if (_fsm.Target != null)
        {
            //죽었으면 대상에서 제외.
            if (_fsm.Target.FSMState == eFSMState.Death)
            {
                _fsm.Target = null;
                return;
            }

            Attack();            
        }
    }

    public override void OnStateExit()
    {
        Debug.Log("공격 상태에서 벗어남");

        _fsm.Target = null;
    }

    void Attack()
    {
        DefaultAttack();
        SkillAttack();
    }
    void DefaultAttack()
    {
        if (_owner.DefaultAttackElapsedTime < 1f) return;
        Debug.Log("공격");
        _owner.DefaultAttackElapsedTime = 0f;
        _fsm.Target.Hit(3f);
    }
    void SkillAttack()
    {

    }

}
