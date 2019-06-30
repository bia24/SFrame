/* * Copyright(C) by Bia All rights reserved. 
 * * Author: Bia
 * * UnityVersion：Unity 2018.2.11f1
 * * Description:   MsgPanel_SelectCharacterPanel 面板脚本
* */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SFrame;
using UnityEngine.UI;
using UnityEngine.Events;

public class MsgPanel_SelectCharacterPanel :UIPanelBase {

    //组件
    Text text;

    //委托
    MsgCallback showText;

    void Awake()
    {
        //面板属性指定    
        Type._Pos = UIPosType.Pop;
        Type._Show = UIShowType.Normal;
        
        //组件获取
        text = transform.Find("Text").GetComponent<Text>();

        //消息绑定
        showText = p =>
        {
            text.text = p._param.ToString();
        };
        MsgCenter.AddMsgListener(MoudleType.UI,UICode.SHOW_TEXT,showText);
    }
}
