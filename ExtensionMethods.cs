using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ExtensionMethods
{
    public static Vector2 FromXZ(this Vector3 v3)
    {
        return new Vector2(v3.x, v3.z);
    }

    public static Vector3 ToXZ(this Vector2 v3)
    {
        return new Vector3(v3.x, 0, v3.y);
    }

    public static Vector3 ToXZ(this Vector2Int v3)
    {
        return new Vector3(v3.x, 0, v3.y);
    }


    public static Vector3 OverrideY(this Vector3 v3, float y)
    {
        return new Vector3(v3.x, y, v3.z);
    }

    public static Vector2 FlipAxis(this Vector2 v2)
    {
        return new Vector2(v2.y, v2.x);
    }

    public static Vector2Int FlipAxis(this Vector2Int v2)
    {
        return new Vector2Int(v2.y, v2.x);
    }

    public static Vector3 FlipXZ(this Vector3 v3)
    {
        return new Vector3(v3.z, v3.y, v3.x);
    }

    public static int Square(this int value)
    {
        return value * value;
    }

    public static float Square(this float value)
    {
        return value * value;
    }

    public static int Pow(this int value, int power)
    {
        return Mathf.RoundToInt(Mathf.Pow(value, power));
    }

    public static float Pow(this float value, float power)
    {
        return Mathf.Pow(value, power);
    }

    public static bool IsOdd(this int value)
    {
        if (value % 2 == 0)
        {
            return false;
        }

        else
        {
            return true;
        }
    }

    public static bool IsEven(this int value)
    {
        if (value % 2 == 0)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    public static float Root(this float value, float root)
    {
        return Mathf.Pow(value, 1 / root);
    }

    public static int Average(this int[] values)
    {
        int totValue = 0;

        for (int i = 0; i < values.Length; i++)
        {
            totValue += values.Length;
        }

        return totValue / values.Length;
    }

    public static float Average(this float[] values)
    {
        float totValue = 0;

        for (int i = 0; i < values.Length; i++)
        {
            totValue += values.Length;
        }

        return totValue / values.Length;
    }

    public static int Round(this float value)
    {
        return Mathf.RoundToInt(value);
    }

    public static Vector2Int Round(this Vector2 vector)
    {
        return new Vector2Int(vector.x.Round(), vector.y.Round());
    }

    public static Vector3Int Round(this Vector3 vector)
    {
        return new Vector3Int(vector.x.Round(), vector.y.Round(), vector.z.Round());
    }

    public static int Floor(this float value)
    {
        return Mathf.FloorToInt(value);
    }

    public static Vector2Int Floor(this Vector2 vector)
    {
        return new Vector2Int(vector.x.Floor(), vector.y.Floor());
    }

    public static Vector3Int Floor(this Vector3 vector)
    {
        return new Vector3Int(vector.x.Floor(), vector.y.Floor(), vector.z.Floor());
    }

    public static int Ceil(this float value)
    {
        return Mathf.CeilToInt(value);
    }

    public static Vector2Int Ceil(this Vector2 vector)
    {
        return new Vector2Int(vector.x.Ceil(), vector.y.Ceil());
    }

    public static Vector3Int Ceil(this Vector3 vector)
    {
        return new Vector3Int(vector.x.Ceil(), vector.y.Ceil(), vector.z.Ceil());
    }


    /// <summary>
    /// Returns true of point is in polygon (untested)
    /// </summary>
    /// <param name="point"></param>
    /// <param name="polygon"></param>
    /// <returns></returns>
    public static bool IsInPolygon(this Vector3 point, Vector3[] polygon)
    {
        int above = 0;
        int below = 0;
        int left = 0;
        int right = 0;

        for (int i = 0; i < polygon.Length; i++)
        {
            if (point.x > polygon[i].x)
                below++;

            else
                above++;

            if (point.z > polygon[i].z)
                left++;

            else
                right++;
        }

        int amountOf0 = 0;

        if (above == 0)
            amountOf0++;

        if (below == 0)
            amountOf0++;

        if (left == 0)
            amountOf0++;

        if (right == 0)
            amountOf0++;

        return (polygon.Length == 3 && amountOf0 < 2) || (polygon.Length > 3 && amountOf0 == 0);





    }
    /// <summary>
    /// Returns true of point is in polygon (untested)
    /// </summary>
    /// <param name="point"></param>
    /// <param name="polygon"></param>
    /// <returns></returns>
    public static bool IsInPolygon(this Vector2 point, Vector2[] polygon)
    {
        int above = 0;
        int below = 0;
        int left = 0;
        int right = 0;

        for (int i = 0; i < polygon.Length; i++)
        {
            if (point.x > polygon[i].x)
                below++;

            else
                above++;

            if (point.y > polygon[i].y)
                left++;

            else
                right++;
        }

        int amountOf0 = 0;

        if (above == 0)
            amountOf0++;

        if (below == 0)
            amountOf0++;

        if (left == 0)
            amountOf0++;

        if (right == 0)
            amountOf0++;

        return (polygon.Length == 3 && amountOf0 < 2) || (polygon.Length > 3 && amountOf0 == 0);





    }

    public static int Area(this Vector2Int v2)
    {
        return v2.x * v2.y;
    }

    public static float Area(this Vector2 v2)
    {
        return v2.x * v2.y;
    }

    public static List<T> RemoveDuplicates<T>(this List<T> list)
    {
        List<T> newList = new List<T>();

        for (int i = 0; i < list.Count; i++)
        {
            if (!newList.Contains(list[i]))
            {
                newList.Add(list[i]);
            }
        }

        return newList;

    }

    public static int Occurrences<T>(this List<T> list, T value)
    {
        int amount = 0;

        for (int i = 0; i < list.Count; i++)
        {
            if (EqualityComparer<T>.Default.Equals(list[i], value))
            {
                amount++;
            }
        }

        return amount;
    }

    public static int Occurrences<T>(this T[] array, T value)
    {
        int amount = 0;

        for (int i = 0; i < array.Length; i++)
        {
            if (EqualityComparer<T>.Default.Equals(array[i], value))
            {
                amount++;
            }
        }

        return amount;
    }

    public static List<T> LimitOccurencesToAmount<T>(this List<T> list, int minAmount)
    {
        List<T> newList = new List<T>();


        for (int i = 0; i < list.Count; i++)
        {
            if (list.Occurrences(list[i]) >= minAmount && !newList.Contains(list[i]))
            {
                newList.Add(list[i]);
            }
        }


        return newList;

    }

    public static bool IsInBounds<T>(this T[] values, int index)
    {
        return index < values.Length && index >= 0;
    }

    public static bool IsInBounds<T>(this T[,] values, int index1, int index2)
    {
        int xSize = values.GetLength(0);
        int ySize = values.GetLength(1);

        return index1 >= 0 && index1 < xSize && index2 >= 0 && index2 < ySize;
    }

    public static Vector2Int To01(this Vector2Int v2)
    {

        Vector2Int ret = Vector2Int.zero;

        if (v2.x != 0)
        {
            ret.x = (int)Mathf.Sign(v2.x);
        }


        if (v2.y != 0)
        {
            ret.y = (int)Mathf.Sign(v2.y);
        }


        return ret;

    }

    public static Vector2Int Simplify(this Vector2Int v2)
    {
        if (Mathf.Abs(v2.x) > Mathf.Abs(v2.y))
        {
            return new Vector2Int((int)Mathf.Sign(v2.x), 0);
        }

        else
        {
            return new Vector2Int(0, (int)Mathf.Sign(v2.y));
        }
    }

    public static Vector2Int Simplify(this Vector2 v2)
    {
        if (Mathf.Abs(v2.x) > Mathf.Abs(v2.y))
        {
            return new Vector2Int((int)Mathf.Sign(v2.x), 0);
        }

        else
        {
            return new Vector2Int(0, (int)Mathf.Sign(v2.y));
        }
    }

    public static Vector2Int RotateSimplifed(this Vector2Int v2)
    {
        Vector2Int vector = v2.Simplify();

        if (Mathf.Abs(vector.x) > Mathf.Abs(vector.y))
        {
            return new Vector2Int(0, vector.x);
        }

        else
        {
            return new Vector2Int(-vector.y, 0);
        }
    }

    public static int Max(this int[] array)
    {
        int max = int.MinValue;

        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] > max)
            {
                max = array[i];
            }
        }

        return max;
    }

    public static bool Contains<T>(this T[] collection, T item)
    {
        for (int i = 0; i < collection.Length; i++)
        {
            if (collection[i].Equals(item))
            {
                return true;
            }
        }

        return false;
    }

    public static T Random<T>(this List<T> list)
    {
        return list[UnityEngine.Random.Range(0, list.Count)];
    }

    public static T Random<T>(this T[] array)
    {
        return array[UnityEngine.Random.Range(0, array.Length)];
    }

    public static T RandomExcluding<T>(this T[] array, T item)
    {
        List<T> container = new List<T>();
        container.AddRange(array);

        if (container.Contains(item))
            container.Remove(item);

        return container.Random();
    }

    public static T RandomExcluding<T>(this T[] array, T[] itemArray)
    {
        List<T> container = new List<T>();
        container.AddRange(array);

        for (int i = itemArray.Length - 1; i >= 0; i--)
        {
            if (container.Contains(itemArray[i]))
            {
                container.RemoveAt(i);
            }
        }

        return container.Random();
    }

    public static T RandomExcluding<T>(this List<T> list, T item)
    {
        return list.ToArray().RandomExcluding(item);
    }

    public static T RandomExcluding<T>(this List<T> list, T[] itemArray)
    {
        return list.ToArray().RandomExcluding(itemArray);
    }

    public static T GetComponentInChildrenOnly<T>(this GameObject obj) where T : Component
    {
        T[] foundComponents = obj.gameObject.GetComponentsInChildren<T>();


        for (int i = 0; i < foundComponents.Length; i++)
        {
            if (foundComponents[i].gameObject != obj)
            {
                return foundComponents[i];
            }
        }

        return null;


    }

    public static T GetComponentInRoot<T>(this GameObject obj) where T : Component
    {
        Transform root = obj.transform.root;

        if (root.TryGetComponent(out T component))
        {
            return component;
        }

        for (int i = 0; i < root.childCount; i++)
        {
            if (root.GetChild(i).TryGetComponent(out T childComponent))
            {
                return childComponent;
            }
        }

        return null;
    }

    public static T[] ReverseOrder<T>(this T[] array)
    {
        if (array == null) return null;

        T[] result = new T[array.Length];

        for (int i = 0; i < array.Length; i++)
        {
            result[i] = array[array.Length - i - 1];
        }

        return result;
    }


    public static void ForEachRandom<T>(this T[] collection, MathHelper.ForEach forEach)
    {
        System.Random r = new System.Random();
        foreach (int i in Enumerable.Range(0, collection.Length).OrderBy(x => r.Next()))
        {
            forEach(i);
        }
    }

    public static void ForEachRandom<T>(this List<T> collection, MathHelper.ForEach forEach)
    {
        System.Random r = new System.Random();
        foreach (int i in Enumerable.Range(0, collection.Count).OrderBy(x => r.Next()))
        {
            forEach(i);
        }
    }

    public static void ForEachRandom<T>(this List<T> collection, MathHelper.ForEach forEach, int seed)
    {
        System.Random r = new System.Random(seed);
        foreach (int i in Enumerable.Range(0, collection.Count).OrderBy(x => r.Next()))
        {
            forEach(i);
        }
    }

    public static bool ContainsAny<T>(this List<T> collection, List<T> other)
    {
        for (int i = 0; i < other.Count; i++)
        {
            if (collection.Contains(other[i]))
            {
                return true;
            }
        }

        return false;
    }

    public static float[,] ApplyGaussianBlur(this float[,] noiseMap, int lengthKernel)
    {
        if (lengthKernel <= 0) return noiseMap;

        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);
        float[,] result = new float[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                result[x, y] = BlurPixel(x, y);
            }
        }

        float BlurPixel(int x, int y)
        {
            float combinedValue = 0;
            int n = 0;

            for (int xi = Mathf.Max(x - lengthKernel, 0); xi < Mathf.Min(x + lengthKernel, width); xi++)
            {
                for (int yi = Mathf.Max(y - lengthKernel, 0); yi < Mathf.Min(y + lengthKernel, height); yi++)
                {
                    combinedValue += noiseMap[xi, yi];
                    n++;
                }
            }

            return combinedValue / n;
        }




        return result;
    }

}
