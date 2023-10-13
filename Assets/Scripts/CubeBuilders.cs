using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBuilders : MonoBehaviour
{
	public int MinX;
	public int MaxX;
	public int MinY;
	public int MaxY;

	public int Count;
	public GameObject obj;

    void Start()
    {
		for (int i = 0; i < Count; i++)
		{
			GameObject go = Instantiate(obj, new Vector3(Random.Range(MinX, MaxX), Random.Range(MinY, MaxY), 0), new Quaternion());
		}
    }
}
