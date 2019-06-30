/* * Copyright(C) by Bia All rights reserved. 
 * * Author: Bia
 * * UnityVersion：Unity 2018.2.11f1
 * * Description:   消息传送需要的参数，封装成一个类
* */
using UnityEngine;

namespace SFrame
{
    public class MsgArg
    {
        public object _param;           //  参数对象。若需要多个参数，由模块内自行封装
        public GameObject _sender;//  发送消息的对象游戏物体

        /// <summary>
        /// 默认构造函数，不提供参数
        /// </summary>
        public MsgArg()
        {
            _param = null;
            _sender = null;
        }


     /// <summary>
     /// 无发送者的构造函数
     /// </summary>
     /// <param name="param"></param>
        public MsgArg(object param)
        {
            _param = param;
            _sender = null;
        }



        /// <summary>
        ///  提供全部参数的构造函数
        /// </summary>
        /// <param name="opCode"></param>
        /// <param name="param"></param>
        /// <param name="sender"></param>
        public MsgArg(object param,GameObject sender)
        {
            _param = param;
            _sender = sender;
        }
    }
}
