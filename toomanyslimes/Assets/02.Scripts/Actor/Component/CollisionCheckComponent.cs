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
        CheckForward();
        CheckLeft();
        CheckRight();
    }
    #endregion
    GameObject a;
    #region Check Method  
    public void CheckLeft()
    {
        Vector2 direction1 = Quaternion.Euler(0, 0, -45) * Vector2.left; 
        Vector2 direction2 = Vector2.left;                                
        Vector2 direction3 = Quaternion.Euler(0, 0, 45) * Vector2.left; 

        controllerComponent.TouchedLeft = CheckRaycastCollisions(direction1, direction2, direction3);
    }
    public void CheckRight()
    {
        Vector2 direction1 = Quaternion.Euler(0, 0, 45) * Vector2.right;  
        Vector2 direction2 = Vector2.right;                               
        Vector2 direction3 = Quaternion.Euler(0, 0, -45) * Vector2.right; 
        
        controllerComponent.TouchedRight = CheckRaycastCollisions(direction1, direction2, direction3);
    }
    public void CheckForward()
    {
        Vector2 direction1 = Quaternion.Euler(0, 0, -45) * Vector2.up;
        Vector2 direction2 = Vector2.up;
        Vector2 direction3 = Quaternion.Euler(0, 0, 45) * Vector2.up;

        bool collisionDetected = CheckRaycastCollisions(direction1, direction2, direction3,0.1f);
        BackgroundManager.Instance.IsBGMove = (collisionDetected==false);
    }
    bool CheckRaycastCollisions(Vector2 dir1,Vector2 dir2,Vector2 dir3,float extraLength=0)
    {
        Vector2 basePosition = ownerTransform.position;

        RaycastHit2D hit1 = Physics2D.Raycast(basePosition, dir1, rayLength+extraLength, obstacleLayer);
        RaycastHit2D hit2 = Physics2D.Raycast(basePosition, dir2, rayLength, obstacleLayer);
        RaycastHit2D hit3 = Physics2D.Raycast(basePosition, dir3, rayLength + extraLength, obstacleLayer);


        bool touched = (hit1.collider != null && (hit1.collider.CompareTag("Enemy") || hit1.collider.CompareTag("Obstacle"))) ||
                       (hit2.collider != null && (hit2.collider.CompareTag("Enemy") || hit2.collider.CompareTag("Obstacle"))) ||
                       (hit3.collider != null && (hit3.collider.CompareTag("Enemy") || hit3.collider.CompareTag("Obstacle")));
#if UNITY_EDITOR
        Debug.DrawRay(basePosition, dir1 * (rayLength + extraLength), Color.blue);
        Debug.DrawRay(basePosition, dir2 * rayLength, Color.blue);
        Debug.DrawRay(basePosition, dir3 * (rayLength + extraLength), Color.blue);
#endif
        return touched;
    }
    void CheckTarget()
    {
        //if(touched)
        //    Debug.Log("전면출돌");

        //if(hit2.collider != null&& hit2.collider.CompareTag("Enemy"))
        //{
        //    if(a!=hit2.collider.gameObject)
        //    {
        //        a = hit2.collider.gameObject;
        //        Debug.Log("공격");
        //    }
        //}
    }
    #endregion
}
