using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Actor
{
    [SerializeField] protected MovementComponent _movementComponent;
    public override void Initialize()
    {
        base.Initialize();
        _movementComponent = new MovementComponent(this);
    }

    #region Trigger Method
    //나중에 따로 빼자 ..
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Border_L":
                _movementComponent.TouchedLeft = true;
                break;
            case "Border_R":
                _movementComponent.TouchedRight = true;
                break;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Border_L":
                _movementComponent.TouchedLeft = false;
                break;
            case "Border_R":
                _movementComponent.TouchedRight = false;
                break;
        }
    }
    #endregion
}
