using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum eComponent
{
    ControllerComponent,
    FSMComponent,
    ColiisionCheckComponent,
    BattleComponent,
    StatComponent,
    SkinComponent,
}
public class BaseComponent 
{
    #region Fields
    [SerializeField] protected eComponent _componentType;
    public eComponent ComponentType => _componentType;
    [SerializeField] protected bool _isActive;
    public virtual bool Active { get => _isActive; set => _isActive = value; }
    [SerializeField] protected Actor _owner;
    public Actor Owner => _owner;
    #endregion

   public BaseComponent(Actor owner,eComponent componentType)
    {
        _isActive = true;
        _owner = owner;
        _componentType = componentType;
        _owner.AddComponent(this);
   }

    #region Public Methods
    public void NextFrame(float deltaTime)
    {
        if (_isActive == false) return;
        OnUpdate(deltaTime);
    }
    public void FixedUpdate(float fixedDeltaTime)
    {
        if (_isActive == false) return;
        OnFixedUpdate(fixedDeltaTime);
    }
    public void Reset()
    {
        OnReset();
    }
    public void Destroy()
    {
        OnDestroy();
    }
    #endregion

    #region Event Handlers
    protected virtual void OnUpdate(float deltaTime) { }
    protected virtual void OnFixedUpdate(float fixedDeltaTime) { }
    protected virtual void OnReset() { }
    protected virtual void OnDestroy() { }
    #endregion
}
