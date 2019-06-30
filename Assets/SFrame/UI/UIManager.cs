/* * Copyright(C) by Bia All rights reserved. 
 * * Author: Bia
 * * UnityVersion：Unity 2018.2.11f1
 * * Description:   UI框架的核心类，存储UI加载路径、UI缓存预制体、UI当前显示预制体，提供加载方法
 * * Notice： 在UI大面板进行关闭和切换时，请使用判定方法，判定当前显示面板和栈情况，不然会出错；大面板请定义为Normal
* */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SFrame
{
    public class UIManager : MonoBehaviour
    {

        private static UIManager _instance = null;

        //UI 面板 路径字典
        private Dictionary<string, string> _panelPathDic = new Dictionary<string, string>();
        //UI 缓存已经加载的所有面板 字典
        private Dictionary<string, UIPanelBase> _PanelLoadedDic = new Dictionary<string, UIPanelBase>();
        //UI 当前显示的面板 字典
        private Dictionary<string, UIPanelBase> _PanelShowingDic = new Dictionary<string, UIPanelBase>();
        //需要进行切换的面板 栈
        private Stack<UIPanelBase> _PanelChangeStack= new Stack<UIPanelBase>();

        //UI根节点，Canvas
        private Transform _UIRoot=null;
        //UI-FullScreen节点
        private Transform _fullScreen=null;
        //UI-Fixed节点
        private Transform _fixed=null;
        //UI-Pop节点
        private Transform _pop=null;
        //UI-脚本挂载节点
        private Transform _scripts = null;

          
        //获得单例
        public static UIManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance= new GameObject("_UIManager").AddComponent<UIManager>();
                }
                return _instance;
            }
        }


        /// <summary>
        /// 作用：按照面板的类别显示一个面板，参数：面板名字
        /// 1.  从已缓存的字典中取出一个面板
        /// 2.  依据面板展示类型， 进行对应操作
        /// </summary>
        /// <param name="panelName"></param>
        public void ShowUIPanel(string panelName)
        {
            if (string.IsNullOrEmpty(panelName))
            {
                Debug.LogWarning("显示面板失败：无效的面板名称");
                return;
            }
            //从_PanelLoadedDic加载一个面板，获取成功的panel都已经存至loadedDic中
            UIPanelBase panel= LoadPanelFromLoadedDic(panelName);
            if (panel == null)
            {
                Debug.LogWarning("显示面板失败：加载出现错误");
                return;
            }
            switch (panel.Type._Show)
            {
                case UIShowType.Normal:
                    //Normal面板显示方法：
                    NormalPanelShow(panelName);
                    break;
                case UIShowType.ChangeOver:
                    //ChangeOver面板显示方法：
                    ChangeOverPanelShow(panelName);
                    break;
                case UIShowType.HideOther:
                    //HideOther面板显示方法：
                    HideOtherPanelShow(panelName);
                    break;
            }
        }

        /// <summary>
        /// 作用：按照面板的类别关闭一个面板    参数：面板名字
        /// 1. 安全检查
        /// 2. 依据面板展示类型，进行对应的关闭操作
        /// </summary>
        public void CloseUIPanel(string panelName)
        {
            if (string.IsNullOrEmpty(panelName)) return;
            UIPanelBase panel = null;
            if (!_PanelLoadedDic.TryGetValue(panelName, out panel)) return;
            //依据指定panel的显示类型，进行关闭
            switch (panel.Type._Show)
            {
                case UIShowType.Normal:
                    NormalPanelClose(panelName);
                    break;
                case UIShowType.ChangeOver:
                    ChangeOverPanelClose(panelName);
                    break;
                case UIShowType.HideOther:
                    HideOtherPanelClose(panelName);
                    break;

            }
        }

        /// <summary>
        /// 作用：是否正在显示列表和栈中为空
        /// </summary>
        /// <returns></returns>
        public bool IsShowingDicAndStackEmpty()
        {
            if (_PanelShowingDic.Count == 0 && _PanelChangeStack.Count == 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 作用：清空正在显示的字典
        /// </summary>
        public void ClearShowingDic()
        {
            foreach (UIPanelBase temp in _PanelShowingDic.Values)
                temp.Hiding();
            _PanelShowingDic.Clear();
        }
        

        /// <summary>
        /// 作用：清空栈
        /// </summary>
        public void ClearStack()
        {
            while (_PanelChangeStack.Count > 0)
            {
                UIPanelBase top = _PanelChangeStack.Pop();
                top.Hiding();
            }
        }


        //Awake 初始化
        private void Awake()
        {
            //加载 Canvas预制体，即UIRoot
            LoadUIRoot();
            //初始化各面板所挂的节点
            InitUINode();
            //设置本物体为_scripts的子节点
            gameObject.transform.SetParent(_scripts,false);
            //初始化UI 面板 路径字典
            InitUIPanelPathDic();
            

        }

        #region 私有方法
        private void LoadUIRoot()
        {
            GameObject go = GameObject.Find("Canvas");
            if (go != null)
            {
                Debug.LogWarning("please check the Canvas，has existed");
                return;
            }
            go=Resources.Load("UI/Prefabs/Canvas") as GameObject; //加载至内存
            go = Instantiate(go);
            go.name = "Canvas";
            _UIRoot = go.transform;
            DontDestroyOnLoad(go);
        }
        private void InitUINode()
        {
            _fullScreen = _UIRoot.transform.Find("FullScreen");
            _fixed = _UIRoot.transform.Find("Fixed");
            _pop = _UIRoot.transform.Find("Pop");
            _scripts = _UIRoot.transform.Find("MgrScripts");
        }
        private void InitUIPanelPathDic()
        {
            _panelPathDic.Add("LoginPanel", "UI/Prefabs/LoginPanel");//todo:::::
            _panelPathDic.Add("SelectCharacterPanel", "UI/Prefabs/SelectCharacterPanel");
            _panelPathDic.Add("MsgPanel_SelectCharacterPanel", "UI/Prefabs/MsgPanel_SelectCharacterPanel");
            _panelPathDic.Add("DetailPanel", "UI/Prefabs/DetailPanel");
            _panelPathDic.Add("HeadPanel", "UI/Prefabs/HeadPanel");
            _panelPathDic.Add("MainPanel", "UI/Prefabs/MainPanel");
            _panelPathDic.Add("MapPanel", "UI/Prefabs/MapPanel");
            _panelPathDic.Add("PackagePanel", "UI/Prefabs/PackagePanel");
            
        }
        
        /// <summary>
        /// 作用：从已经实例化的字典中取出一个panel  参数：面板名字
        /// 1.  如果存在，说明面板已经实例化，并且已经存放至对应的层级目录下Pos
        /// 2.  如果不存在，说明面板还没有实例化，从资源文件夹中加载
        /// </summary>
        /// <param name="panelName"></param>
        /// <returns></returns>
        private UIPanelBase LoadPanelFromLoadedDic(string panelName)
        {
            UIPanelBase panel = null;
           if(! _PanelLoadedDic.TryGetValue(panelName, out panel))
            {
                //还未加载过，进行加载并缓存至字典中
                panel = LoadPanelFromResources(panelName);
            }
            return panel;
        }

        /// <summary>
        /// 作用：从资源文件夹中加载对象，并实例化，放置在对应结构目录下；存储该实例的uibase引用至缓存dic 参数：面板名字
        /// 1.  从路径dic中获取对象资源的路径
        /// 2.  加载资源到内存
        /// 3.  实例化对象，并取得uibase脚本引用
        /// 4.  依据uibase的位置属性，将对象物体放置在对应的结构目录
        /// 5.  初始隐藏对象，uibase引用入Loaded字典
        /// </summary>
        /// <param name="panelName"></param>
        /// <returns></returns>
        private UIPanelBase LoadPanelFromResources(string panelName)
        {
            string path = null;

            if(!_panelPathDic.TryGetValue(panelName,out path))
            {
                Debug.LogWarning("加载面板失败：" + panelName + " 不存在指定路径，请检查");
                return null;
            }

            GameObject prefab = Resources.Load<GameObject>(path);

            if (_UIRoot == null || prefab == null)
            {
                Debug.LogWarning("加载面板失败：_UIRoot为空，或路径错误，请检查");
                return null;
            }

            GameObject go = Instantiate(prefab);
            go.name = panelName;
            UIPanelBase panel = go.GetComponent<UIPanelBase>();

            if (panel == null)
            {
                Debug.LogWarning("加载面板失败：指定面板中不存在UIPanelBase脚本，请检查");
                return null;
            }
            //设置父节点，SetParent(,false)保持局部坐标不变，世界坐标改变。-因为制作预制体是在Canvas下的，保存的是局部坐标信息
            //加载的时候，实例化后，世界坐标和局部坐标一致了。设置父节点，需要保持局部坐标一致，改变世界坐标。
            switch (panel.Type._Pos)
            {
                case UIPosType.FullScreen:
                    go.transform.SetParent(_fullScreen,false);
                    break;
                case UIPosType.Fixed:
                    go.transform.SetParent(_fixed, false);
                    break;
                case UIPosType.Pop:
                    go.transform.SetParent(_pop, false);
                    break;
            }

            //初始隐藏
            go.SetActive(false);
            //加入缓存
            _PanelLoadedDic.Add(panelName, panel);

            return panel;
        }

        /// <summary>
        /// 作用：对显示类型为Normal的面板进行显示操作     参数：面板名字
        /// 1.  若面板已经存在，不做处理
        /// 2.  不存在，依据名字，从loadedDic中找出面板，并添加到当前显示面板dic中
        /// 3.  display面板
        /// </summary>
        /// <param name="panelName"></param>
        private void NormalPanelShow(string panelName)
        {
            UIPanelBase panel = null;
            //若已经存在了，不做处理
            if (_PanelShowingDic.TryGetValue(panelName, out panel)) return;
            panel = null;
            if(!_PanelLoadedDic.TryGetValue(panelName,out panel))
            {
                Debug.LogWarning("panel获取失败：未知错误");
                return;
            }
            _PanelShowingDic.Add(panelName, panel);
            panel.Display();
        }

        /// <summary>
        ///  作用：对显示类型为 Changeover的面板进行显示操作  参数：面板名字
        ///  警告：适用于Pop类的面板
        ///  1. 栈不空，取出栈顶元素，冻结它
        ///  2. 本panel进行display并入栈
        /// </summary>
        /// <param name="panelName"></param>
        private void ChangeOverPanelShow(string panelName)
        {
            UIPanelBase panel = null;
            if(!_PanelLoadedDic.TryGetValue(panelName,out panel))
            {
                Debug.LogWarning("获取面板失败：未知错误");
                return;
            }
            
            if (_PanelChangeStack.Count > 0)
            {
                //栈顶元素冻结
                UIPanelBase top = _PanelChangeStack.Peek();
                top.Freeze();
            }
            //指定panel显示
            panel.Display();
            _PanelChangeStack.Push(panel);
        }

        /// <summary>
        /// 作用：对显示类型为HideOther的面板进行显示操作     参数：面板名字
        /// 警告：适用于pop类的面板，对于整个UI面板进行切换不要使用；
        /// 在“隐藏其它”面板上禁止使用changeover面板，不然会出错！！
        /// 也禁止再使用“隐藏其它”面板
        /// </summary>
        /// <param name="panelName"></param>
        private void HideOtherPanelShow(string panelName)
        {
            if (string.IsNullOrEmpty(panelName)) return;
            UIPanelBase panel = null;
            if (_PanelShowingDic.TryGetValue(panelName, out panel)) return;//本情况一般不存在

            //将正在显示的面板隐藏
            foreach(UIPanelBase temp in _PanelShowingDic.Values)
            {
                temp.Hiding();//暂时隐藏，本panel关闭后，这些panel会重新显示。因此只适合一个ui大面板中的pop子面板；
            }
            //若栈中存在元素，将栈顶元素元素隐藏
            if (_PanelChangeStack.Count > 0)
            {
                UIPanelBase top = _PanelChangeStack.Peek();
                top.Hiding();
            }
            
            //找到该panel，display，并加入正在显示面板
            if(!_PanelLoadedDic.TryGetValue(panelName,out panel))
            {
                Debug.LogWarning("获取面板失败：未知错误");
                return;
            }
            //加入正在显示面板
            _PanelShowingDic.Add(panelName, panel);
            panel.Display();
        }

        /// <summary>
        /// 作用：Normal的面板进行关闭操作
        /// </summary>
        /// <param name="panelName"></param>
        private void NormalPanelClose(string panelName)
        {
            UIPanelBase panel=null;
            if (!_PanelShowingDic.TryGetValue(panelName, out panel)) return;
            panel.Hiding();
            _PanelShowingDic.Remove(panelName);
        }

        /// <summary>
        /// 作用：ChangeOver面板进行关闭操作
        /// </summary>
        /// <param name="panelName"></param>
        private void ChangeOverPanelClose(string panelName)
        {
            if (_PanelChangeStack.Count == 0) return;
            UIPanelBase panel = null;
            _PanelLoadedDic.TryGetValue(panelName, out panel);
            if (_PanelChangeStack.Count >= 2)
            {
                if(_PanelChangeStack.Peek() != panel)
                {
                    Debug.Log("栈首元素与目标不匹配：未知错误，请检查");
                    return;
                }
                //出栈，隐藏
                panel = _PanelChangeStack.Pop();
                panel.Hiding();
                //下一个隐藏
                panel = _PanelChangeStack.Peek();
                panel.Redisplay();
            }
            else if(_PanelChangeStack .Count== 1){
                if (_PanelChangeStack.Peek() != panel)
                {
                    Debug.Log("栈首元素与目标不匹配：未知错误，请检查");
                    return;
                }
                panel = _PanelChangeStack.Pop();
                panel.Hiding();
            }
        }

        /// <summary>
        /// 作用：HideOther面板进行关闭操作
        /// </summary>
        /// <param name="panelName"></param>
        private void HideOtherPanelClose(string panelName)
        {
            if (string.IsNullOrEmpty(panelName)) return;
            UIPanelBase panel = null;
            if(!_PanelShowingDic.TryGetValue(panelName,out panel))
            {
                Debug.LogWarning("未知错误：未找到指定panel,请检查");
                return;
            }
            //移除，隐藏
            panel.Hiding();
            _PanelShowingDic.Remove(panelName);

            //所有正在显示的panel重新显示  
            foreach(UIPanelBase temp in _PanelShowingDic.Values)
                temp.Redisplay();

            //栈中第一个panel 重新显示
            if (_PanelChangeStack.Count > 0)
            {
                _PanelChangeStack.Peek().Redisplay();
            }
        }
        #endregion
    }
}
