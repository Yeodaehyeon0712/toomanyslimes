using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : TSingletonMono<StageManager>
{
    #region Fields
    Data.StageData currentStageData;
    long currentStageIndex;// 임시

    #endregion

    protected override void OnInitialize()
    {
        IsLoad = true;
    }
    public void SetupStage(long stageIndex)
    {
        //스테이지를 셋업 . 
        // 1. 맵세팅 2. 캐릭터 세팅 3.몬스터 세팅
        // 세팅이 완료되면 맵을 움직인다 .
        // 이제 스테이지의 성공 여부를 계속 체크할것임 .
        // 스테이지가 끝난후 결과를 체크하고 마무리 하여 UI를 띄워준다 .

        StartCoroutine(IESetupStage(stageIndex));
        
    }
    IEnumerator IESetupStage(long stageIndex)
    {
        //현제 스테이지의 데이터를 받음
        currentStageIndex = stageIndex;
        currentStageData = DataManager.StageTable[currentStageIndex];

        //웨이브를 세팅하자 .
        SetNextWave();


        yield return null;
    }
    int maxWaveCount;
    int currentWaveIndex;
    public void SetNextWave()
    {
        //내가 치뤄야 하는 웨이브의 수
        maxWaveCount = currentStageData.WaveIndexArr.Length;
        //다음 웨이브의 데이터
        var nextWaveIndex=currentStageData.WaveIndexArr[currentWaveIndex];
        Data.WaveData waveData = DataManager.WaveTable[nextWaveIndex];
        //해당 웨이브 데이터를 기반으로 스테이지에 요소들을 스폰
        SpawnManager.Instance.SpawnWave(waveData, currentStageData.MonsterIndexArr);

        //여기까지 웨이브가 세팅되었다면 스테이지의 시작

    }

    IEnumerator IEWarpSetupStage(long stageIndex)
    {
        yield return null;
    }
}
