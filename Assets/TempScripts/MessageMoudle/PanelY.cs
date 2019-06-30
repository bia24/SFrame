/* * Copyright(C) by Bia All rights reserved. 
 * * Author: Bia
 * * UnityVersion：Unity 2018.2.11f1
 * * Description:   
* */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SFrame;


public class PanelY : MonoBehaviour {

    Button button1;
    Button button2;
    
    void Awake()
    {
        //控件获取
        button1 = transform.Find("Button1").GetComponent<Button>();
        button2 = transform.Find("Button2").GetComponent<Button>();

        //监听绑定
        MsgCenter.AddMsgListener(MoudleType.UI, UICode.OPEN_PANEL_Y,ShowPanel);

        //按钮事件绑定
        button1.onClick.AddListener(() =>
            {
                MsgCenter.SendMsg(MoudleType.UI, UICode.SHOW_TEXT, new MsgArg("加油啊兄弟", gameObject));
            }
        );

        button2.onClick.AddListener(() =>
            {
                MsgCenter.SendMsg(MoudleType.UI, UICode.OPEN_PANEL_R, new MsgArg("", gameObject));
                gameObject.SetActive(false);
            }
        );
       
        //初始状态赋值
        gameObject.SetActive(true); 

    }

     void OnDestroy()
    {
        //取消消息中心监听
        MsgCenter.RemoveMsgListener(MoudleType.UI, UICode.OPEN_PANEL_Y, ShowPanel);
        //取消按钮点击监听
        button1.onClick.RemoveAllListeners();
        button2.onClick.RemoveAllListeners();
    }

    #region callback function

    private void ShowPanel(MsgArg arg)
    {
        gameObject.SetActive(true);
    }

    #endregion

}
