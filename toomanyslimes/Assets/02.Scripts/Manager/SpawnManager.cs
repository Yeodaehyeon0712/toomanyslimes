using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : TSingletonMono<SpawnManager>
{
    #region Fields
    ActorFactory actorFactory;
    Transform actorFactoryRoot;
    Queue<(Transform parent,Transform[,]points)> spawnPointsQueue=new Queue<(Transform, Transform[,])>();
    #endregion
   
    protected override void OnInitialize()
    {
        GenerateFactory();
        GenerateSpawnPoints();
        IsLoad = true;
    }

    #region Factory Method
    void GenerateFactory()
    {
        actorFactoryRoot = new GameObject("actorFactory").transform;
        actorFactoryRoot.SetParent(this.transform);
        actorFactory = new ActorFactory(actorFactoryRoot);
    }
    #endregion

    #region SpawnPoints Method
    void GenerateSpawnPoints()
    {
        var prefab = Resources.Load<Transform>("Prefabs/SpawnPoints");
        var cloneAsset = Instantiate(prefab,actorFactoryRoot);
        Populate2DArray(cloneAsset);
    }
    void Populate2DArray(Transform parentTransform)
    {
        int rowCount = parentTransform.childCount;
        int colCount = parentTransform.GetChild(0).childCount;
        var spawnPoint2DArray = new Transform[rowCount,colCount];

        for(int i=0;i<rowCount;i++)
        {
            var rowPoints = parentTransform.GetChild(i);
            for(int j=0;j<colCount;j++)
                spawnPoint2DArray[i,j] = rowPoints.GetChild(j);
        }
        spawnPointsQueue.Enqueue((parentTransform,spawnPoint2DArray));
    }
    (Transform parent,Transform[,] points) GetSpawnPoints()
    {
        if (spawnPointsQueue.Count == 0)
            GenerateSpawnPoints();

        return spawnPointsQueue.Dequeue();
    }
    public void ReturnPoint(Transform returnPoint)
    {
        //returnPoint.SetParent(actorFactoryRoot);
        //Populate2DArray(returnPoint);
    }
    #endregion

    public void SpawnWave(Data.WaveData waveData,long[]monsterIndexArr)
    {
        var wave2DArray = waveData.Wave2DArray;

        int rowCount = wave2DArray.GetLength(0);
        int colCount = wave2DArray.GetLength(1);

        var touple = GetSpawnPoints();
        var parent = touple.parent;
        var spawnPoint = touple.points;

        for (int i=0;i<rowCount;i++)
        {
            for(int j=0;j<colCount;j++)
            {
                var item = wave2DArray[i, j];
                var tr = spawnPoint[i, j];
                if (item==0)
                {
                    //아무것도 안함
                }
                else if(item==1)
                {
                    long monster = GetRandomMonsterIndex(monsterIndexArr);
                    actorFactory.GetActor<Actor>(eActorType.Enemy,monster,tr);
                }
            }
        }

        Transform spawnTargetBG = BackgroundManager.Instance.GetFirstBG();
        parent.SetParent(spawnTargetBG);
        parent.localPosition = Vector3.zero;
    }   
    long GetRandomMonsterIndex(long[]monsterIndexArr)
    {
        int selectRandom = Random.Range(0, monsterIndexArr.Length);
        return monsterIndexArr[selectRandom];
    }
    #region Valid Check Method
    //(int,int)? ValidateWave(Data.WaveData waveData)
    //{
    //    var wave2DArray = waveData.Wave2DArray;

    //    int waveRowCount = wave2DArray.GetLength(0);
    //    int waveColCount = wave2DArray.GetLength(1);

    //    int pointRowCount=spawnPoint2DArray.GetLength(0);
    //    int pointColCount=spawnPoint2DArray.GetLength(1);

    //    if((waveRowCount==pointRowCount)&&(waveColCount==pointColCount))
    //    {
    //        return (waveRowCount, waveColCount);
    //    }

    //    Debug.LogWarning($"Warning : Wave {waveData}is Not Valid .");
    //    return null;
    //}
    #endregion

}
