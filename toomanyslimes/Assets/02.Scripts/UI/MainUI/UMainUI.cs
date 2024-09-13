using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UMainUI : UBaseUI
{
    #region Field
    Button btn_Setting;
    TextMeshProUGUI text_title;
    Button btn_LeftModel;
    Button btn_RightModel;
    Button btn_Info;

    #endregion

    #region Init
    protected override void InitReference()
    {      
        btn_Setting = transform.Find("Panel_Title/Btn_Setting").GetComponent<Button>();
        btn_Setting.onClick.AddListener(OpenSetting);
        btn_LeftModel = transform.Find("Panel_NextModel/Btn_Left").GetComponent<Button>();
        btn_LeftModel.onClick.AddListener(() => { NextModel(isRight: false); });
        btn_RightModel = transform.Find("Panel_NextModel/Btn_Right").GetComponent<Button>();
        btn_RightModel.onClick.AddListener(() => { NextModel(isRight: true); });

        btn_Info = transform.Find("Btn_Info").GetComponent<Button>();
        btn_Info.onClick.AddListener(OpenDescription); 
    }

    protected override void OnRefresh()
    {
    }
    #endregion
    public void OpenSetting()
    {
        //�̰� enum ������ ó���Ҽ��� ���� ��� .
        UIManager.Instance.SettingUI.Enable();
    }
    public void OpenDescription()
    {
        UIManager.Instance.DescriptionUI.MoveIn(1f);
    }
    #region Model Method
    void NextModel(bool isRight)
    {
        //ModelManager.Instance.NextModel(isRight);
    }
    #endregion

}
