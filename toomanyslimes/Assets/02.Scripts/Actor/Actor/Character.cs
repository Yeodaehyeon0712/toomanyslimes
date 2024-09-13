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
    #endregion
}
