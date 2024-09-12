using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : TSingletonMono<SpawnManager>
{
    #region Fields
    ActorFactory actorFactory;
    Transform actorFactoryRoot;
    Queue<SpawnPoints> spawnPointsQueue = new Queue<SpawnPoints>();
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
        var prefab = Resources.Load<SpawnPoints>("Prefabs/SpawnPoints");
        var cloneAsset = Instantiate(prefab,actorFactoryRoot);
        cloneAsset.Init();
        spawnPointsQueue.Enqueue(cloneAsset);
    }    
    SpawnPoints GetSpawnPoints()
    {
        if (spawnPointsQueue.Count == 0)
            GenerateSpawnPoints();

        return spawnPointsQueue.Dequeue();
    }
    public void RegisterCleanedSpawnPoints(SpawnPoints spawnPoints)
    {
        spawnPointsQueue.Enqueue(spawnPoints);
    }
    #endregion

    #region Spawn Method
    public void SpawnWave(Data.WaveData waveData,long[]monsterIndexArr)
    {
        var wave2DArray = waveData.Wave2DArray;
        int rowCount = wave2DArray.GetLength(0);
        int colCount = wave2DArray.GetLength(1);

        SpawnPoints points=GetSpawnPoints();

        for (int i=0;i<rowCount;i++)
        {
            for(int j=0;j<colCount;j++)
            {
                var item = wave2DArray[i, j];
                var point = points.GetPoint(i, j);
                if (item==0)
                {
                    //아무것도 안함
                }
                else if(item==1)
                {
                    long monsterIndex = GetRandomMonsterIndex(monsterIndexArr);
                    var actor = actorFactory.GetActor<Actor>(eActorType.Enemy,monsterIndex,point);
                    points.RegisterActor(actor);
                }
            }
        }

        Transform spawnTargetBG = BackgroundManager.Instance.GetFirstBG();
        points.transform.SetParent(spawnTargetBG);
        points.transform.localPosition = Vector3.zero;
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
    #endregion

}
