/* * Copyright(C) by Bia All rights reserved. 
 * * Author: Bia
 * * UnityVersion：Unity 2018.2.11f1
 * * Description:   提供UI模块使用的各类枚举、常量定义，方便管理和调用
* */


namespace SFrame
{
    //UI 面板的位置类型
    public enum UIPosType
    {
        FullScreen, //全屏位置
        Fixed,         //固定位置
        Pop            //弹出类
    }
    
    //UI面板显示类型
    public enum UIShowType
    {
        Normal,         //普通显示
        ChangeOver,//切换显示
        HideOther    //隐藏其他显示
    }

    //UI面板透明度类型
    public enum UIPellucidityType
    {
         Cant,       //不透明，不能点击
         CantButClear ,              //全透明，不能点击
         Can            //全透明，可以点击
    }


    //使用本类对各个状态进行封装
    public class UIPanelType
    {
        public UIPosType _Pos = UIPosType.FullScreen;
        public UIShowType _Show = UIShowType.Normal;
        public UIPellucidityType _Pellucidity = UIPellucidityType.Can;
    }

}