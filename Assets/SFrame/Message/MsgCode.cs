/* * Copyright(C) by Bia All rights reserved. 
 * * Author: Bia
 * * UnityVersion：Unity 2018.2.11f1
 * * Description:   消费发送接收需要的识别码，分为模块类型和各模块各自需要定义的子识别码
* */

namespace SFrame
{
    //各个模块的类型码
    public enum MoudleType
    {
        NULL=0,
        UI=1,
        Audio=2
        //...
    }

    //UI模块 操作码
    public class UICode
    {
        public const uint NULL = 0;
        public const uint SHOW_TEXT = 1;
        public const uint OPEN_PANEL_R = 3;
        public const uint OPEN_PANEL_Y = 4;
        //...
    }

    //Audio模块 操作码
    public class AudioCode
    {
        public const uint NULL = 0;
        public const uint PLAY = 1;
        //...
    }


    /*需要使用的模块自行添加*/
}
