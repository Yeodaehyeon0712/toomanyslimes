public abstract class BaseState 
{
    protected Actor _owner;
    protected FSMComponent _fsm;
    public BaseState(Actor owner)
    {
        _owner = owner;
        _fsm = _owner.GetComponent<FSMComponent>(eComponent.FSMComponent);
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
    Move = 1 << 0,
    Battle = 1 << 1,
    Death = 1 << 2,
}
