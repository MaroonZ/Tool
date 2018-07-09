using UnityEngine;
using System.Collections;

public class SimpleVector3 {
    public float x;
    public float y;
    public float z;

    public SimpleVector3() { }
    public SimpleVector3(float x,float y,float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public static SimpleVector3 ToSimple(Vector3 v)
    {
        return new SimpleVector3(v.x, v.y, v.z);
    }
    public Vector3 toVector3()
    {
        return new Vector3(x, y, z);
    }
}
