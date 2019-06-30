/* * Copyright(C) by Bia All rights reserved. 
 * * Author: Bia
 * * UnityVersion：Unity 2018.2.11f1
 * * Description:   UI遮罩功能的管理类，负责模态面板的开启和禁用，实现UI面板的禁止点击功能
* */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SFrame
{
    public class UIMaskMgr : MonoBehaviour
    {

        #region 字段
        private static UIMaskMgr _instance = null;

        //UI根节点
        private Transform _UIRoot = null;
        //UI脚本挂载节点
        private Transform _UIScripts = null;
        //UI遮罩面板，处于pop节点下
        private GameObject _mask = null;
        //UI相机
        private Camera _UICamera = null;
        //UI相机原始层深度
        private float _originUICameraDepth;

        #endregion


        //单例获取
        public static UIMaskMgr Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameObject("_UIMaskMgr").AddComponent<UIMaskMgr>();
                }
                return _instance;
            }
        }
        /// <summary>
        /// UI遮罩功能开启
        /// </summary>
        /// <param name="topPanel">需要使用遮罩的面板</param>
        /// <param name="pellucidity">遮罩力度</param>
        public void OpenMask(GameObject topPanel, UIPellucidityType pellucidity)
        {
            //启用遮罩窗体，并设置透明
            switch (pellucidity)
            {
                case UIPellucidityType.Can:
                    break;
                case UIPellucidityType.Cant:
                    _mask.SetActive(true);
                    _mask.GetComponent<Image>().color = new Color(0, 0, 0, 62 / 255f);
                    break;
                case UIPellucidityType.CantButClear:
                    _mask.SetActive(true);
                    _mask.GetComponent<Image>().color = new Color(0, 0, 0, 0);
                    break;
            }
            //遮罩窗体下移
            _mask.transform.SetAsLastSibling();
            //目标窗体下移，保证目标窗体在最下面
            topPanel.transform.SetAsLastSibling();
            //增加摄像机层深为最大，保证UI摄像机渲染为最上层
            _UICamera.depth = 100f;
        }
        /// <summary>
        /// UI遮罩功能关闭
        /// </summary>
        public void CloseMask()
        {
            //禁用遮罩窗体
            if (_mask.activeInHierarchy) _mask.SetActive(false);
            //恢复摄像机的层深
            _UICamera.depth = _originUICameraDepth;
        }

        #region 私有方法
        void Awake()
        {
            //UI根节点
            _UIRoot = GameObject.Find("Canvas").transform;
            if (_UIRoot == null)
            {
                Debug.LogError("Canvas组件还未挂载，请检查");
                return;
            }
            //挂载UI-脚本挂载节点
            _UIScripts = _UIRoot.Find("MgrScripts");
            gameObject.transform.SetParent(_UIScripts, false);
            //UI遮罩面板，处于pop节点下
            _mask = _UIRoot.Find("Pop").Find("_Mask").gameObject;
            if (_mask == null) { Debug.LogError("未找到mask面板，请检查"); return; }
            //UI相机
            _UICamera = _UIRoot.Find("UIRenderCamera").GetComponent<Camera>();
            //UI相机原始层深度
            _originUICameraDepth = _UICamera.depth;
        }

        #endregion

    }
}