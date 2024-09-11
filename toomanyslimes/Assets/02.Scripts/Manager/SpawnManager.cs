using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : TSingletonMono<SpawnManager>
{
    #region Fields
    //몬스터와 캐릭터를 관리 ..
    ActorFactory actorFactory;
    Transform actorFactoryRoot;
    [SerializeField]
    public Transform[,] spawnPoint2DArray;

    #endregion
    //해당 스테이지의 데이터를 받아서 스폰 팩토리가 생성하게 만듬. 
    //몬스터 스폰 팩토리
    //아이템 스폰 팩토리
    //등등 ..
    //웨이브의 데이터를 받아서 1.몬스터 2.아이템(양자택일/젬/장애물)둘 중 하나를 스폰한다 .
    //죽거나 피한것들은 다시 팩토리에 넣어줄것임
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
        var cloneAsset = Instantiate(prefab, this.transform);
        Populate2DArray(cloneAsset);
    }
    void Populate2DArray(Transform parentTransform)
    {
        int rowCount = parentTransform.childCount;
        int colCount = parentTransform.GetChild(0).childCount;
        spawnPoint2DArray = new Transform[rowCount,colCount];

        for(int i=0;i<rowCount;i++)
        {
            var rowPoints = parentTransform.GetChild(i);
            for(int j=0;j<colCount;j++)
                spawnPoint2DArray[i,j] = rowPoints.GetChild(j);
        }
    }
    #endregion

    public void SpawnWave(Data.WaveData waveData,long[]monsterIndexArr)
    {
        var wave2DArray = waveData.Wave2DArray;

        int rowCount = wave2DArray.GetLength(0);
        int colCount = wave2DArray.GetLength(1);

        for (int i=0;i<rowCount;i++)
        {
            for(int j=0;j<colCount;j++)
            {
                var item = wave2DArray[i, j];
                var tr = spawnPoint2DArray[i, j];
                if (item==0)
                {
                    //아무것도 안함
                }
                else if(item==1)
                {
                    actorFactory.GetActor<Actor>(eActorType.Enemy,0,tr);
                }
            }
        }
    }
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


}
