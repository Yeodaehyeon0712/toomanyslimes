using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UMainUI : UBaseUI
{
    #region Field

    GameObject panel_normalStage;
    Button btn_Pause;
    TextMeshProUGUI text_CoinCount;
    Slider slider_progress;

    GameObject panel_bossStage;
    TextMeshProUGUI text_bossHP;
    Slider slider_bossHP;
    #endregion

    #region Init
    protected override void InitReference()
    {
        panel_normalStage = transform.Find("Panel_NormalStage").gameObject;
        btn_Pause = transform.Find("Btn_Pause").GetComponent<Button>();
        btn_Pause.onClick.AddListener(OpenSetting);
        text_CoinCount = transform.Find("Panel_NormalStage/Panel_Coin/Text_Title").GetComponent<TextMeshProUGUI>();
        slider_progress = transform.Find("Panel_NormalStage/Slider_Progress").GetComponent<Slider>();
        slider_progress.value = 0;

        panel_bossStage= transform.Find("Panel_BossStage").gameObject;
        text_bossHP= transform.Find("Panel_BossStage/Text_BossHP").GetComponent<TextMeshProUGUI>();
        slider_progress = transform.Find("Panel_BossStage/Slider_BossHP").GetComponent<Slider>();
        slider_progress.value = 1f;
    }

    protected override void OnRefresh()
    {
    }
    #endregion
    public void OpenSetting()
    {
        //이건 enum 형으로 처리할수도 있음 고민 .
        UIManager.Instance.SettingUI.Enable();
    }
    #region Normal Stage Method
    public void ShowNormalStagePanel(bool isShow)
    {
        panel_normalStage.SetActive(isShow);
    }
    public void SetCoinCount(int coinCount)
    {
        text_CoinCount.text = coinCount.ToString();
    }    
    public void SetProgressSlider(float progressValue)
    {
        slider_progress.value = progressValue;
    }
    #endregion

    #region Boss Stage Method
    public void ShowBossStagePanel(bool isShow)
    {
        panel_bossStage.SetActive(isShow);
    }
    public void SetHPSlider(float currentHP,float maxHP)
    {
        slider_bossHP.value = currentHP / maxHP;
        text_bossHP.text = currentHP.ToString();
    }
    #endregion
}
