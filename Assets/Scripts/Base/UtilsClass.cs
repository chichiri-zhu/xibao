//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class UtilsClass
{
    public static Camera mainCamera;

    public static Vector3 GetMouseWorldPosition()
    {
        if (mainCamera == null) mainCamera = Camera.main;

        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        return mouseWorldPosition;
    }

    public static Vector3 GetRandomDir()
    {
        return new Vector3(
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f)
            ).normalized;
    }

    public static float GetAngleFromVector(Vector3 vector)
    {
        float radians = Mathf.Atan2(vector.y, vector.x);
        float degrees = radians * Mathf.Rad2Deg;
        return degrees;
    }

    public static bool IsPointerOverUIObject(Vector2 screenPosition)
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(screenPosition.x, screenPosition.y);

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    public static float ColliderDistance(Collider2D a, Collider2D b)
    {
        ColliderDistance2D colliderDistance = Physics2D.Distance(a, b);
        return colliderDistance.distance;
        //return Vector3.Distance(a.ClosestPoint(b.transform.position),
        //                        b.ClosestPoint(a.transform.position));
    }

    /// <summary>
    /// 固定数组中的不重复随机
    /// </summary>
    /// <param name="nums">数组</param>
    /// <param name="count">要随机的个数</param>
    /// <returns></returns>
    public static List<T> GetRandom<T>(this List<T> nums, int count, List<T> igonreList = null)
    {
        if(igonreList != null && igonreList.Count > 0)
        {
            foreach (var item in igonreList)
            {
                if (nums.Contains(item))
                {
                    nums.Remove(item);
                }
            }
        }
        if (count > nums.Count)
        {
            count = nums.Count;
        }
        List<T> result = new List<T>();
        List<int> id = new List<int>();

        for (int i = 0; i < nums.Count; i++)
        {
            id.Add(i);
        }

        int r;
        while (id.Count > nums.Count - count)
        {
            r = Random.Range(0, id.Count);
            result.Add(nums[id[r]]);
            id.Remove(id[r]);
        }
        return (result);
    }

    public static Transform FindDeepChild(Transform parent, string name)
    {
        // 检查父对象是否具有所需名称的子对象
        Transform child = parent.Find(name);

        // 如果找到了子对象，则返回它
        if (child != null)
        {
            return child;
        }

        // 否则，递归搜索所有子对象的子对象
        foreach (Transform childTransform in parent)
        {
            Transform foundChild = FindDeepChild(childTransform, name);

            // 如果找到了子对象，则返回它
            if (foundChild != null)
            {
                return foundChild;
            }
        }

        // 如果没有找到子对象，则返回null
        return null;
    }

    public static Color HexToColor(string hex)
    {
        Color color = Color.white;

        if (ColorUtility.TryParseHtmlString(hex, out color))
        {
            return color;
        }

        return Color.white;
    }
}
