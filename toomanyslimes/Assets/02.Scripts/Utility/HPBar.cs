using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    Transform hpBar;
    double MaxHP;

    void Awake()
    {
        hpBar = transform.Find("HPRate");     
    }

    public void Init(double MaxHP)
    {
        this.MaxHP = MaxHP;
        SetHP(MaxHP);
        ShowHPBar(false);
    }
    public void SetHP(double currentHP)
    {
        hpBar.localScale = new Vector3((float)(currentHP / MaxHP), hpBar.localScale.y, 1); 
    }
    public void ShowHPBar(bool isShow)
    {
        gameObject.SetActive(isShow);
    }
}
