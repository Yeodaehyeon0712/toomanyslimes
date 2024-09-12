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
        Debug.Log("���� ���¿� ����");

    }
    public override void OnStateStay(float deltaTime)
    {
        //���� ����� �ִٸ�
        if (_fsm.Target != null)
        {
            //�׾����� ��󿡼� ����.
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
        Debug.Log("���� ���¿��� ���");

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
        Debug.Log("����");
        _owner.DefaultAttackElapsedTime = 0f;
        _fsm.Target.Hit(3f);
    }
    void SkillAttack()
    {

    }

}
