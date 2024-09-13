using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleState : BaseState
{
    #region Fields
    float defaultAttackTime = 1.5f;
    #endregion

    #region State Method
    public BattleState(Actor owner) : base(owner)
    {

    }
    public override void OnStateEnter()
    {
        _owner.DefaultAttackElapsedTime = 0;
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
        _owner.DefaultAttackElapsedTime = 0;
        _fsm.Target = null;
    }
    public override void Reset()
    {

    }
    #endregion

    #region Battle Method

    void Attack()
    {
        DefaultAttack();
        SkillAttack();
    }
    void DefaultAttack()
    {
        if (_owner.DefaultAttackElapsedTime < defaultAttackTime) return;

        _owner.DefaultAttackElapsedTime = 0f;
        _fsm.Target.Hit(_owner.Stat.AttackDamage);
    }
    void SkillAttack()
    {

    }
    #endregion
}
