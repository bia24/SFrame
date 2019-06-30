/* * Copyright(C) by Bia All rights reserved. 
 * * Author: Bia
 * * UnityVersion：Unity 2018.2.11f1
 * * Description:   UI框架测试启动脚本
* */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SFrame;

public class UIStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
        UIManager.Instance.ShowUIPanel("LoginPanel");
	}
    
}
