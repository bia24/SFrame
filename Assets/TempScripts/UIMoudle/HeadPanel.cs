/* * Copyright(C) by Bia All rights reserved. 
 * * Author: Bia
 * * UnityVersion：Unity 2018.2.11f1
 * * Description:   测试脚本--头像框面板
* */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SFrame;
using UnityEngine.UI;
using UnityEngine.Events;


public class HeadPanel : UIPanelBase
{
    void Awake()
    {
        //面板属性指定    
        Type._Show = UIShowType.Normal;
        Type._Pos = UIPosType.Fixed;
      
    }

}
