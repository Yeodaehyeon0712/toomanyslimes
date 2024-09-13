using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Actor
{
    public ControllerComponent Controller => controllerComponent;
    [SerializeField] protected ControllerComponent controllerComponent;
    public CollisionCheckComponent Collision => collisionCheckComponent;
    [SerializeField] protected CollisionCheckComponent collisionCheckComponent;


    #region Actor Method
    public override void Initialize()
    {
        base.Initialize();
        controllerComponent = new ControllerComponent(this);
        collisionCheckComponent = new CollisionCheckComponent(this);
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (controllerComponent != null)
            controllerComponent.FixedUpdate(Time.fixedDeltaTime);
    }
    public override void Spawn(long _index, uint _worldID, int _spawnHashCode, eActorType _type)
    {
        base.Spawn(_index, _worldID, _spawnHashCode, _type);
        hpBar.ShowHPBar(true);
    }
    #endregion
}
