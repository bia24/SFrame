/* * Copyright(C) by Bia All rights reserved. 
 * * Author: Bia
 * * UnityVersion：Unity 2018.2.11f1
 * * Description:   SFrame中不继承mono的单例基类
* */

namespace SFrame
{
    public class SingletonBase<T>where T : new ()
    {
        private static T _instance;

        private static readonly object sync = new object();

        public  static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (sync)
                    {
                        if (_instance == null)
                            _instance = new T();
                    }
                }
                return _instance;
            }
        }
    }
}