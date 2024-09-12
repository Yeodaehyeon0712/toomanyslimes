using System.Collections.Generic;
using UnityEngine;

public class FSMComponent : BaseComponent
{
    #region Fields
    [SerializeField] Dictionary<eFSMState, BaseState> _fsmDictionary = new Dictionary<eFSMState, BaseState>();
    eFSMState currentState = eFSMState.Idle;
    public Actor Target;

    public eFSMState State
    {
        get => currentState;
        set
        {
            if (currentState == value) return;
            _fsmDictionary[currentState].OnStateExit();
            currentState = value;
            _fsmDictionary[currentState].OnStateEnter();
        }
    }
    #endregion

    public FSMComponent(Actor owner) : base(owner, eComponent.ControllerComponent)
    {
        GenerateFSMState();
    }

    #region Component Method
    protected override void OnUpdate(float fixedDeltaTime)
    {
        _fsmDictionary[currentState].OnStateStay(fixedDeltaTime);
    }
    #endregion

    #region FSM Method
    public void GenerateFSMState()
    {
        _fsmDictionary.Add(eFSMState.Idle, new IdleState(_owner));
        _fsmDictionary.Add(eFSMState.Move, new MoveState(_owner));
    }
    #endregion
}
