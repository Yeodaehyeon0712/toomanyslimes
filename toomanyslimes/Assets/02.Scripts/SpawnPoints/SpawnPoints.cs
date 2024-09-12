using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    Transform[,] points;
    List<Actor> actorList=new List<Actor>();
    public void Init()
    {
        Populate2DArray();
    }
    void Populate2DArray()
    {
        int rowCount = transform.childCount;
        int colCount = transform.GetChild(0).childCount;
        points = new Transform[rowCount, colCount];

        for (int i = 0; i < rowCount; i++)
        {
            var rowPoints = transform.GetChild(i);
            for (int j = 0; j < colCount; j++)
                points[i, j] = rowPoints.GetChild(j);
        }
    }
    public Transform GetPoint(int a,int b)
    {
        return points[a, b];
    }
    public void ResetPoints()
    {
        //남아 있던 액터들 모두 풀로 돌려보냄
        foreach(var actor in actorList)
        {
            actor.gameObject.SetActive(false);
            Debug.Log("제거");
        }
        SpawnManager.Instance.RegisterCleanedSpawnPoints(this);
    }
    public void RegisterActor(Actor actor)
    {
        actorList.Add(actor);
    }

}
