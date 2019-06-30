/* * Copyright(C) by Bia All rights reserved. 
 * * Author: Bia
 * * UnityVersion：Unity 2018.2.11f1
 * * Description:  消息中心，基于观察者模式减少组件之间消息传递的耦合;
 * *                      优化后，嵌套dictionary，防止moudleType产生过多，也使子操作更加清晰
* */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SFrame
{
    //需要传递一个MsgArg封装类参数的回调委托
    public delegate void MsgCallback(MsgArg arg);

    public class MsgCenter
    {
        //存储所有模块回调委托，由MoudleType唯一识别回调委托
        private static Dictionary<MoudleType, Dictionary<uint, MsgCallback>> _dic = new Dictionary<MoudleType, Dictionary<uint, MsgCallback>>();

       
        //增加一个指定type的消息回调函数
        public static void AddMsgListener(MoudleType type,uint code,MsgCallback callback)
        {
            Dictionary<uint, MsgCallback> subDic = null;
            if(!_dic.TryGetValue(type,out subDic))
            {
                subDic = new Dictionary<uint, MsgCallback>();
                _dic.Add(type, subDic);
            }
            
            if (!subDic.ContainsKey(code))//委托引用不是原地赋值，不能使用trygetvalue
            {
                MsgCallback cb = null;
                subDic.Add(code, cb);
            }
            subDic[code] += callback;
        }

        //增加同一个type，多个code的消息回调函数
        public static void AddMsgListener(MoudleType type,uint[]codes,MsgCallback callback)
        {
            Dictionary<uint, MsgCallback> subDic = null;
            if (!_dic.TryGetValue(type, out subDic))
            {
                subDic = new Dictionary<uint, MsgCallback>();
                _dic.Add(type, subDic);
            }

            for (int i = 0; i < codes.Length; i++)
            {
                if (!subDic.ContainsKey(codes[i]))
                {
                    MsgCallback cb = null;
                    subDic.Add(codes[i], cb);
                }
                subDic[codes[i]] += callback;
            }
        }



        //移除一个指定type,code的消息回调函数
        public static void RemoveMsgListener(MoudleType type,uint code,MsgCallback callback)
        {
            Dictionary<uint, MsgCallback> subDic = null;
            if(!_dic.TryGetValue(type,out subDic))
            {
                Debug.LogWarning("移除监听失败：" + type.ToString() + " MoudleType 的委托字典不存在");
                return;
            }

            if (!subDic.ContainsKey(code))
            {
                Debug.LogWarning("移除监听失败：" + type.ToString() + " MoudleType ,"+ code.ToString()+ " code 的委托不存在");
                return;
            }

            subDic[code] -= callback;

            if (subDic[code] == null)
                subDic.Remove(code);

            if (subDic.Keys.Count == 0)
                _dic.Remove(type);
        }

        //移除一个指定type,多个code的消息回调函数
        public static void RemoveMsgListener(MoudleType type, uint[] codes, MsgCallback callback)
        {
            Dictionary<uint, MsgCallback> subDic = null;
            if (!_dic.TryGetValue(type, out subDic))
            {
                Debug.LogWarning("移除监听失败：" + type.ToString() + " MoudleType 的委托字典不存在");
                return;
            }

            for (int i=0;i<codes.Length;i++)
            {
                if (!subDic.ContainsKey(codes[i]))
                {
                    Debug.LogWarning("移除监听失败：" + type.ToString() + " MoudleType ," + codes[i].ToString() + " code 的委托不存在");
                    continue;
                }

                subDic[codes[i]] -= callback;

                if (subDic[codes[i]] == null)
                    subDic.Remove(codes[i]);

            }

            if (subDic.Keys.Count == 0)
                _dic.Remove(type);
        }


        //清空指定type的所有消息回调函数
        public static void ClearMsgListener(MoudleType type)
        {
            Dictionary<uint, MsgCallback> subDic = null;
            if (!_dic.TryGetValue(type, out subDic))
            {
                Debug.LogWarning("清空监听失败：" + type.ToString() + " MoudleType 的委托字典不存在");
                return;
            }
            subDic.Clear();
            _dic.Remove(type);
        }

        //发送消息
        public static void SendMsg(MoudleType type, uint code,MsgArg arg)
        {
            Dictionary<uint, MsgCallback> subDic = null;
            if(_dic.TryGetValue(type,out subDic))
            {
                MsgCallback callback = null;
                if(subDic.TryGetValue(code,out callback))
                {
                    if (callback != null)
                        callback(arg);
                }
            }
        }


    }
}
