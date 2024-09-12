using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheckComponent : BaseComponent
{
    #region Fields
    LayerMask obstacleLayer=1<<3;
    Transform ownerTransform;
    float rayLength = 0.5f; 

    ControllerComponent controllerComponent;
    #endregion

    #region Component Method
    public CollisionCheckComponent(Actor owner) : base(owner, eComponent.ColiisionCheckComponent)
    {
        OnReset();
        controllerComponent = _owner.GetComponent<ControllerComponent>(eComponent.ControllerComponent);
        ownerTransform = _owner.transform;
    }
    protected override void OnUpdate(float fixedDeltaTime)
    {
        CheckLeft();
        CheckRight();
    }
    #endregion

    #region Check Method  
    public void CheckLeft()
    {
        Vector2 basePosition = ownerTransform.position;

        Vector2 direction1 = Quaternion.Euler(0, 0, -45) * Vector2.left; 
        Vector2 direction2 = Vector2.left;                                
        Vector2 direction3 = Quaternion.Euler(0, 0, 45) * Vector2.left; 

        RaycastHit2D hit1 = Physics2D.Raycast(basePosition, direction1, rayLength, obstacleLayer);
        RaycastHit2D hit2 = Physics2D.Raycast(basePosition, direction2, rayLength, obstacleLayer);
        RaycastHit2D hit3 = Physics2D.Raycast(basePosition, direction3, rayLength, obstacleLayer);


        bool touched = (hit1.collider != null && (hit1.collider.CompareTag("Obstacle") || hit1.collider.CompareTag("Enemy"))) ||
                       (hit2.collider != null && (hit2.collider.CompareTag("Obstacle") || hit2.collider.CompareTag("Enemy"))) ||
                       (hit3.collider != null && (hit3.collider.CompareTag("Obstacle") || hit3.collider.CompareTag("Enemy")));

        controllerComponent.TouchedLeft = touched;

#if UNITY_EDITOR
        Debug.DrawRay(basePosition, direction1 * rayLength, Color.red);  
        Debug.DrawRay(basePosition, direction2 * rayLength, Color.red); 
        Debug.DrawRay(basePosition, direction3 * rayLength, Color.red); 
#endif
    }
    public void CheckRight()
    {
        Vector2 basePosition = ownerTransform.position;

        Vector2 direction1 = Quaternion.Euler(0, 0, 45) * Vector2.right;  
        Vector2 direction2 = Vector2.right;                               
        Vector2 direction3 = Quaternion.Euler(0, 0, -45) * Vector2.right; 

        RaycastHit2D hit1 = Physics2D.Raycast(basePosition, direction1, rayLength, obstacleLayer);
        RaycastHit2D hit2 = Physics2D.Raycast(basePosition, direction2, rayLength, obstacleLayer);
        RaycastHit2D hit3 = Physics2D.Raycast(basePosition, direction3, rayLength, obstacleLayer);


        bool touched = (hit1.collider != null && (hit1.collider.CompareTag("Obstacle") || hit1.collider.CompareTag("Enemy"))) ||
                       (hit2.collider != null && (hit2.collider.CompareTag("Obstacle") || hit2.collider.CompareTag("Enemy"))) ||
                       (hit3.collider != null && (hit3.collider.CompareTag("Obstacle") || hit3.collider.CompareTag("Enemy")));

        controllerComponent.TouchedRight = touched;

#if UNITY_EDITOR
        Debug.DrawRay(basePosition, direction1 * rayLength, Color.blue);
        Debug.DrawRay(basePosition, direction2 * rayLength, Color.blue);
        Debug.DrawRay(basePosition, direction3 * rayLength, Color.blue);
#endif
    }
    #endregion
}
