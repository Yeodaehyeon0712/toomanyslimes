using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Actor
{
    [SerializeField] protected ControllerComponent controllerComponent;
    public override void Initialize()
    {
        base.Initialize();
        controllerComponent = new ControllerComponent(this);
    }

    #region Trigger Method
    //나중에 따로 빼자 ..
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Border_L":
                controllerComponent.TouchedLeft = true;
                break;
            case "Border_R":
                controllerComponent.TouchedRight = true;
                break;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Border_L":
                controllerComponent.TouchedLeft = false;
                break;
            case "Border_R":
                controllerComponent.TouchedRight = false;
                break;
        }
    }
    protected override void FixedUpdate()
    {
        if (controllerComponent != null)
            controllerComponent.FixedUpdate(Time.fixedDeltaTime);
    }
    #endregion
}
