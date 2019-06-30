/* * Copyright(C) by Bia All rights reserved. 
 * * Author: Bia
 * * UnityVersion：Unity 2018.2.11f1
 * * Description:   
* */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SFrame;
using UnityEngine.UI;
using UnityEngine.Events;

public class DetailPanel :UIPanelBase  {

    //组件
    Button btn_Cancle;
    //委托
    UnityAction cancle;

     void Awake()
    {
        //面板属性指定
        Type._Pos = UIPosType.Pop;
        Type._Show = UIShowType.ChangeOver;
        Type._Pellucidity = UIPellucidityType.Cant;
        //组件获取
        btn_Cancle = transform.Find("BtnCancle").GetComponent<Button>();
        //事件绑定
        cancle = () =>
        {
            UIManager.Instance.CloseUIPanel("DetailPanel");
        };
        btn_Cancle.onClick.AddListener(cancle);
    }
}
