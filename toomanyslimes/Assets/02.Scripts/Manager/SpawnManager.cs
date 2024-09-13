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
   
    #region Init Method
    protected override void OnInitialize()
    {
        GenerateFactory();
        GenerateSpawnPoints();
        IsLoad = true;
    }

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
        spawnPoints.transform.SetParent(actorFactoryRoot);
        spawnPoints.transform.localPosition = Vector3.zero;
        spawnPointsQueue.Enqueue(spawnPoints);
    }
    #endregion

    #region Spawn Method
    public T SpawnCharacter<T>(long index) where T : Actor => actorFactory.GetActor<T>(eActorType.Player, index);

    public void SpawnWave(Data.WaveData waveData,long[]monsterIndexArr,long bossIndex)
    {
        var wave2DArray = waveData.Wave2DArray;
        int rowCount = wave2DArray.GetLength(0);
        int colCount = wave2DArray.GetLength(1);

        SpawnPoints points=GetSpawnPoints();

        for (int i=0;i<rowCount;i++)
        {
            for(int j=0;j<colCount;j++)
            {
                var waveType = wave2DArray[i, j];
                var point = points.GetPoint(i, j);
                SpawnByWaveType(waveType, monsterIndexArr, bossIndex, point);
            }
        }

        Transform spawnTargetBG = BackgroundManager.Instance.GetFirstBG();
        points.transform.SetParent(spawnTargetBG);
        points.transform.localPosition = Vector3.zero;
    }  
    void SpawnByWaveType(eWaveType type,long[] monsterIndexArr,long bossIndex,Transform point)
    {
        switch(type)
        {
            case eWaveType.None:
                break;
            case eWaveType.Enemy: 
            case eWaveType.Boss:
                {
                    long monsterIndex =( type==eWaveType.Enemy?GetRandomMonsterIndex(monsterIndexArr):bossIndex);
                    var actor = actorFactory.GetActor<Actor>(eActorType.Enemy, monsterIndex, point);
                    break;
                }
            case eWaveType.Coin:
                {
                    var origin = Resources.Load<Coin>("Item/Coin");
                    Instantiate(origin, point);
                    break;
                }
            case eWaveType.Trap:
                {
                    var origin = Resources.Load<Trap>("Item/Trap");
                    Instantiate(origin, point);
                    break;
                }
            case eWaveType.BuffArea:
                {
                    var origin = Resources.Load<GameObject>("Prefabs/BuffArea");
                    Instantiate(origin, point);
                    break;
                }
            case eWaveType.SkillArea:
                {
                    var origin = Resources.Load<GameObject>("Prefabs/SkillArea");
                    Instantiate(origin, point);
                    break;
                }
            case eWaveType.StoreArea:
                {
                    var origin = Resources.Load<GameObject>("Prefabs/StoreArea");
                    Instantiate(origin, point);
                    break;
                }
        }
    }
    long GetRandomMonsterIndex(long[]monsterIndexArr)
    {
        int selectRandom = Random.Range(0, monsterIndexArr.Length);
        return monsterIndexArr[selectRandom];
    }
    #endregion

    #region RegisterMethod
    public void RegisterActorPool(uint worldID) => actorFactory.RegisterActorAtPool(worldID);
    #endregion
}
