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
        if (currentWaveProgress >= RaceStageCount)
        {
            BackgroundManager.Instance.IsBGMove=false;
            Debug.Log("��� ����");
            return;
        }
        currentWaveProgress++;
        Data.WaveData waveData = DataManager.WaveTable[currentWaveProgress];
        SpawnManager.Instance.SpawnWave(waveData, CurrentStageData.MonsterIndexArr);
    }
    void CheckStageState()
    {

    }
    //�÷��̾� Ȥ�� ���� ������ ������ üũ��
    //IEnumerator IESubProcess()
    //{
    //    //�÷��̾��� ������ üũ
    //    if (Player.PlayerCharacter.FSMState == eFSMState.Death)
    //    {
    //        _currentContentsResultState = eContentResultState.Defeat;
    //        break;
    //    }

    //    if (constructure.LoadActor.FSMState == eFSMState.Death)
    //    {
    //        _currentContentsResultState = eContentResultState.Victory;
    //        break;
    //    }
    //}
    public void ASDASD()
    {
        //������ ���� �� ������ ���̺갡 �ش� ���� ��ҿ� ��ġ�Ǹ� �� .
        
    }
}
public enum eStageType
{
    Race,
    Loop,
    Boss,
}
