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

public class PanelR : MonoBehaviour {

    Text text;
    Button button3;

     void Awake()
    {
        //组件绑定
        text = transform.Find("showtext").GetComponent<Text>();
        button3 = transform.Find("Button3").GetComponent<Button>();

        //按钮事件绑定
        button3.onClick.AddListener(()=> 
            {
                MsgCenter.SendMsg(MoudleType.UI, UICode.OPEN_PANEL_Y, new MsgArg("", gameObject));
                gameObject.SetActive(false);
            }
        );


        //监听消息中心
        MsgCenter.AddMsgListener(MoudleType.UI,UICode.OPEN_PANEL_R, ShowPanel);
        MsgCenter.AddMsgListener(MoudleType.UI, UICode.SHOW_TEXT, ShowText );

        //初始状态
        gameObject.SetActive(false);
    }

     void OnDestroy()
    {
        //监听消息中心解除绑定
        MsgCenter.RemoveMsgListener(MoudleType.UI, UICode.OPEN_PANEL_R, ShowPanel);
        MsgCenter.RemoveMsgListener(MoudleType.UI, UICode.SHOW_TEXT, ShowText);
        //按钮解除绑定
        button3.onClick.RemoveAllListeners();
    }



    #region callback function

    private void ShowPanel(MsgArg arg)
    {
        gameObject.SetActive(true);
    }

    private void ShowText(MsgArg arg)
    {
        text.text = text.text + "\n" + arg._param;
    }
    #endregion

}
