/* * Copyright(C) by Bia All rights reserved. 
 * * Author: Bia
 * * UnityVersion：Unity 2018.2.11f1
 * * Description:   SelectCharacterPanel的脚本
* */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using SFrame;

public class SelectCharacterPanel :UIPanelBase {
    //组件
    Button btn1;
    Button btn2;
    Button btn3;
    Button btn4;

    //委托
    UnityAction head1;
    UnityAction head2;
    UnityAction head3;
    UnityAction enterGame;


    void Awake()
    {
        //面板属性指定
        //组件获取
        btn1 = transform.Find("Button1").GetComponent<Button>();
        btn2= transform.Find("Button2").GetComponent<Button>();
        btn3 = transform.Find("Button3").GetComponent<Button>();
        btn4 = transform.Find("Button4").GetComponent<Button>();
        //事件绑定
        head1 = () =>
        {
            UIManager.Instance.ShowUIPanel("MsgPanel_SelectCharacterPanel");
            MsgCenter.SendMsg(MoudleType.UI, UICode.SHOW_TEXT, new MsgArg("角色1：威整天"));
        };
        btn1.onClick.AddListener(head1);

        head2 = () =>
        {
            UIManager.Instance.ShowUIPanel("MsgPanel_SelectCharacterPanel");
            MsgCenter.SendMsg(MoudleType.UI, UICode.SHOW_TEXT, new MsgArg("角色2：神秘人物"));
        };
        btn2.onClick.AddListener(head2);

        head3 = () =>
        {
            UIManager.Instance.ShowUIPanel("MsgPanel_SelectCharacterPanel");
            MsgCenter.SendMsg(MoudleType.UI, UICode.SHOW_TEXT, new MsgArg("角色3：威整天"));
        };
        btn3.onClick.AddListener(head3);

        enterGame = () =>
        {
            if (!UIManager.Instance.IsShowingDicAndStackEmpty())
            {
                UIManager.Instance.ClearShowingDic();
                UIManager.Instance.ClearStack();
            }
            UIManager.Instance.ShowUIPanel("HeadPanel");
            UIManager.Instance.ShowUIPanel("MainPanel");
           
        };
        btn4.onClick.AddListener(enterGame);
    }
}
