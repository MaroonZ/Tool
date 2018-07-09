using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System;

public class AddBoxEidtor  {
    static GameObject targetObj;
    static List<Bounds> boundsList;
    //static List<Renderer> childList;

        /// <summary>
        /// 为选中的GameObject下所有的带Render的子物体添加BoxCollider
        /// </summary>
    [MenuItem("Xigema/AddBoxCollision")]
    static void AddBoxCollision()
    {
        targetObj = Selection.activeGameObject;
        if (targetObj)
        {
            bool isok = false;
            Renderer[] rends = targetObj.GetComponentsInChildren<Renderer>();
            for (int i = 0; i < rends.Length; i++)
            {
                rends[i].gameObject.AddComponent<BoxCollider>();
            }
        }
    }

    /// <summary>
    /// 为选中的GameObject下添加BoxCollider 包含所有的带Render组件的物体
    /// </summary>
    [MenuItem("Xigema/AddBoxCollisionInChoose")]
    static void AddBoxCollisionInChoose()
    {
        targetObj = Selection.activeGameObject;
       
        if (targetObj)
        {
            if(boundsList!=null)
                boundsList.Clear();
            boundsList = new List<Bounds>();
            Renderer[] rends = targetObj.GetComponentsInChildren<Renderer>();
            for (int i = 0; i < rends.Length; i++)
            {
                BoxCollider box=rends[i].gameObject.AddComponent<BoxCollider>();
                boundsList.Add(box.bounds);
            }
            Bounds parentBox = new Bounds(targetObj.transform.position, Vector3.zero);
            foreach (Bounds bound in boundsList)
            {
                parentBox = TestPoint(parentBox, bound.center);
                parentBox = TestPoint(parentBox, bound.min);
                parentBox = TestPoint(parentBox, bound.max);
            }
            BoxCollider mybox=targetObj.AddComponent<BoxCollider>();
            
            mybox.center = targetObj.transform.InverseTransformPoint(parentBox.center);
            mybox.size = targetObj.transform.InverseTransformVector(parentBox.size);
            for (int i = 0; i < rends.Length; i++)
            {
                BoxCollider box = rends[i].gameObject.GetComponent<BoxCollider>();
                box.enabled = false;
                UnityEngine.Object.DestroyImmediate(box);
            }
        }

    //private static Transform GetAllRender(Transform parent)
    //{

    //    foreach (Transform child in parent)
    //    {
    //        Renderer rend = child.GetComponent<Renderer>();
    //        if (rend != null)
    //        {
    //            childList.Add(rend);
    //        }
    //    }
    //    return null;
    //}
}

    private static Bounds TestPoint(Bounds parent, Vector3 point)
    {
        if (parent.Contains(point) == false)
        {
            parent.Encapsulate(point);
        }

        return parent;
    }
}
