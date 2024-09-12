public abstract class BaseState 
{
    protected Actor _owner;
    protected FSMComponent _fsm;
    public BaseState(Actor owner)
    {
        _owner = owner;
        _fsm = _owner.GetComponent<FSMComponent>(eComponent.ControllerComponent);
    }
    public abstract void OnStateEnter();
    public abstract void OnStateStay(float deltaTime);
    public abstract void OnStateExit();
    public virtual void Reset()
    {

    }
}
public enum eFSMState
{
    Idle = 1 << 0,
    Move = 1 << 1,
    Battle = 1 << 2,
    Death = 1 << 3,
}
