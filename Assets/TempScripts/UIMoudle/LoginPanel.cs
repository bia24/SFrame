/* * Copyright(C) by Bia All rights reserved. 
 * * Author: Bia
 * * UnityVersion：Unity 2018.2.11f1
 * * Description:   LoginPanel的脚本
* */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SFrame;
using UnityEngine.UI;
using UnityEngine.Events;

public class LoginPanel:UIPanelBase {
    
    //组件
    Button btn_Enter;

    //委托
    UnityAction enterGame;

     void Awake()
    {
        //面板属性指定    

        //组件获取
        btn_Enter = transform.Find("Button").GetComponent<Button>();

        //事件绑定
        enterGame = () =>
        {
            UIManager.Instance.CloseUIPanel("LoginPanel");
            UIManager.Instance.ShowUIPanel("SelectCharacterPanel");
        };
        btn_Enter.onClick.AddListener(enterGame);
    }
}
