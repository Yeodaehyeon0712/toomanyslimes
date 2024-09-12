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
            //�׾����� ��󿡼� ����.
            if (_fsm.Target.FSMState == eFSMState.Death)
            {
                _fsm.Target = null;
                return;
            }
            //�⺻ ������ �����ϴٸ� �⺻���� ����
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
        //���� ���ݷ��� ��� ..
        _fsm.Target.Hit(3f);
    }

}
