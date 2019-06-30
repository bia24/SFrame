/* * Copyright(C) by Bia All rights reserved. 
 * * Author: Bia
 * * UnityVersion：Unity 2018.2.11f1
 * * Description:   测试脚本--主城界面面板
* */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SFrame;
using UnityEngine.UI;
using UnityEngine.Events;


public class MainPanel : UIPanelBase
{
    //组件
    Button btn_package;
    Button btn_mail;

    //委托
    UnityAction packageClick;
    UnityAction mailClick;

    void Awake()
    {
        //面板属性指定    
        Type._Show = UIShowType.Normal;
        Type._Pos = UIPosType.FullScreen;
        //组件获取
        btn_package = transform.Find("Btn_Package").GetComponent<Button>();
        btn_mail= transform.Find("Btn_Mail").GetComponent<Button>();
        //事件绑定
        packageClick = () =>
        {
            UIManager.Instance.ShowUIPanel("PackagePanel");
        };
        btn_package.onClick.AddListener(packageClick);

        mailClick = () =>
        {
            UIManager.Instance.ShowUIPanel("MapPanel");
        };
        btn_mail.onClick.AddListener(mailClick);
    }
}
