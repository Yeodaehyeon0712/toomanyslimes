using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalStageFramework : StageFramework
{
    Data.StageData CurrentStageData => DataManager.StageTable[stageIndex];
    long stageIndex;

    int RaceStageCount;
    int currentWaveProgress;
    public int CurrentStageProgress => currentWaveProgress;

    public override IEnumerator IEStageProcess(long _stageIndex)
    {
        stageIndex = _stageIndex;
        currentContentsResultState = eContentResultState.InProgress;
        RaceStageCount = CurrentStageData.WaveIndexArr.Length;

        yield return IEWaveProcess();
    }
    IEnumerator IEWaveProcess()
    {
        //����� �������� .
        BackgroundManager.Instance.ReqisterBGResetAction(SpawnWave);
        BackgroundManager.Instance.IsBGMove = true;
        //���� ������ ������ üũ
        while (currentContentsResultState == eContentResultState.InProgress)
        {
            CheckStageState();
            yield return null;
        }

        //�ش� ������ ������ �Ǿ��ٸ�
        switch (currentContentsResultState)
        {
            case eContentResultState.Victory:
                yield return ExitStage(() =>Debug.Log("Rmx"),1f);
                break;
            case eContentResultState.Defeat:
                break;
        }
    }
    void SpawnWave()
    {
        currentWaveProgress++;
        Data.WaveData waveData = DataManager.WaveTable[currentWaveProgress];
        SpawnManager.Instance.SpawnWave(waveData, CurrentStageData.MonsterIndexArr);
        if (currentWaveProgress >= RaceStageCount)
        {
            BackgroundManager.Instance.IsLastWave = true;
            Debug.Log("������ ���̺�");
        }
    }
    void CheckStageState()
    {
        ////�÷��̾ �׾��ٸ�
        //if (Player.PlayerCharacter.FSMState == eFSMState.Death)
        //{
        //    currentContentsResultState = eContentResultState.Defeat;
        //    return;
        //}
        ////������ �׾��ٸ�
        //if(true)
        //{
        //    currentContentsResultState = eContentResultState.Victory;
        //    return;
        //}
    }
}

