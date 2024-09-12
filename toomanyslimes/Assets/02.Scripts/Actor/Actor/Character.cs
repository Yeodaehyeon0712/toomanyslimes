using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Actor
{
    [SerializeField] protected ControllerComponent controllerComponent;
    [SerializeField] protected CollisionCheckComponent collisionCheckComponent;

    public override void Initialize()
    {
        base.Initialize();
        controllerComponent = new ControllerComponent(this);
        collisionCheckComponent = new CollisionCheckComponent(this);
    }
  
    protected override void FixedUpdate()
    {
        if (controllerComponent != null)
            controllerComponent.FixedUpdate(Time.fixedDeltaTime);
    }
}
