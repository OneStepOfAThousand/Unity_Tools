using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class QuickVectorMath
{
    /// <summary>
    /// Converts a vector 2 to vector 3.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static Vector3 Vec2To3(this Vector2 input)
    {
        return Vec2To3(input, 0f);
    }

    /// <summary>
    /// Converts a vector 2 to vector 3 with a set z axis.
    /// </summary>
    /// <param name="input"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public static Vector3 Vec2To3(this Vector2 input, float z)
    {
        return new Vector3(input.x, input.y, z);
    }

    /// <summary>
    /// Returns either vector 1 or 2 depending on which is closer to target vector.
    /// </summary>
    /// <param name="vector1"></param>
    /// <param name="vector2"></param>
    /// <param name="targetVector"></param>
    /// <returns></returns>
    public static Vector2 GetClosestVector(Vector2 vector1, Vector2 vector2, Vector2 targetVector)
    {
        Vector2 result = GetClosestVector(Vec2To3(vector1), Vec2To3(vector2), Vec2To3(targetVector));
        return result;
    }

    /// <summary>
    /// Returns either vector 1 or 2 depending on which is closer to target vector.
    /// </summary>
    /// <param name="vector1"></param>
    /// <param name="vector2"></param>
    /// <param name="targetVector"></param>
    /// <returns></returns>
    public static Vector3 GetClosestVector(this Vector3 targetVector, Vector3 vector1, Vector3 vector2)
    {
        Vector3 result;
        if ((vector1 - targetVector).magnitude > (vector2 - targetVector).magnitude)
            result = vector2;
        else
            result = vector1;
        return result;
    }

    /// <summary>
    /// Changes the x coordinate of the "vector" to "x".
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static Vector3 ChangeX(this Vector3 vector, float x)
    {
        return new Vector3(x, vector.y, vector.z);
    }

    /// <summary>
    /// Changes the y coordinate of the "vector" to "y".
    /// </summary>
    /// <param name="vector"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static Vector3 ChangeY(this Vector3 vector, float y)
    {
        return new Vector3(vector.x, y, vector.z);
    }

    /// <summary>
    /// Changes the z coordinate of the "vector" to "z".
    /// </summarz>
    /// <param name="vector"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public static Vector3 ChangeZ(this Vector3 vector, float z)
    {
        return new Vector3(vector.x, vector.y, z);
    }
}
