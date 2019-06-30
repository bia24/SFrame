/* * Copyright(C) by Bia All rights reserved. 
 * * Author: Bia
 * * UnityVersion：Unity 2018.2.11f1
 * * Description:   测试脚本--背包面板
* */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SFrame;
using UnityEngine.UI;
using UnityEngine.Events;

public class PackagePanel : UIPanelBase
{
    //组件
    Button btn_Gold;
    Button btn_Cancle;

    //委托
    UnityAction goldClick;
    UnityAction cancleClick;

    void Awake()
    {
        //面板属性指定    
        Type._Show = UIShowType.ChangeOver;
        Type._Pos = UIPosType.Pop;
        Type._Pellucidity = UIPellucidityType.CantButClear;

        //组件获取
        btn_Gold = transform.Find("BtnGold").GetComponent<Button>();
        btn_Cancle = transform.Find("BtnCancel").GetComponent<Button>();

        //事件绑定
        goldClick = () =>
        {
            UIManager.Instance.ShowUIPanel("DetailPanel");
        };
        btn_Gold.onClick.AddListener(goldClick);

        cancleClick = () =>
        {
            UIManager.Instance.CloseUIPanel("PackagePanel");
        };
        btn_Cancle.onClick.AddListener(cancleClick);
    }

}
