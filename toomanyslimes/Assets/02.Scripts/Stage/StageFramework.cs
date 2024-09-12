using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum eContentResultState
{
    InProgress,
    Victory,
    Defeat,
}
public abstract class StageFramework
{
    #region Fields
    protected eContentResultState currentContentsResultState;
    #endregion

    #region Framework Method
    //������ ��ũ���� �������� �غ� .
    public virtual IEnumerator SetupStage(long stageIndex)
    {
        BackgroundManager.Instance.SetBackground(DataManager.StageTable[stageIndex].BGIndex);
        // �÷��̾� ��ȯ by Actor Manager
        yield return null;
    }
    public abstract IEnumerator IEStageProcess(long stageIndex);
    protected IEnumerator ExitStage(UnityEngine.Events.UnityAction afterAction,float time)
    {
        yield return null;
        yield return new WaitForSeconds(time);
        afterAction?.Invoke();
    }
    public virtual IEnumerator CleanStage()
    {
        yield return null;
    }
    #endregion
}
