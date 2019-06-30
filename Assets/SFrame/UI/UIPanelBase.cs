/* * Copyright(C) by Bia All rights reserved. 
 * * Author: Bia
 * * UnityVersion：Unity 2018.2.11f1
 * * Description:   ui面板(以一个panel为单位组织ui)的基类，提供panel的共有方法
* */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SFrame
{
    public class UIPanelBase : MonoBehaviour
    {
        private UIPanelType _Type = new UIPanelType();

        public UIPanelType Type { get { return _Type; } }



        public virtual void Display()
        {
            gameObject.SetActive(true);
            if (Type._Pos == UIPosType.Pop)
            {
                UIMaskMgr.Instance.OpenMask(gameObject,Type._Pellucidity);
            }
        }

        public virtual void Hiding()
        {
            gameObject.SetActive(false);
            if (Type._Pos == UIPosType.Pop)
            {
                UIMaskMgr.Instance.CloseMask();
            }
        }

        public  virtual void Freeze()
        {
            gameObject.SetActive(false);
        }
         
        public virtual void Redisplay()
        {
            gameObject.SetActive(true);
            if (Type._Pos == UIPosType.Pop)
            {
                UIMaskMgr.Instance.OpenMask(gameObject, Type._Pellucidity);
            }
        }

    }
}
