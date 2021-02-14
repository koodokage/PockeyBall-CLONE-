using UnityEngine;
using System;
using System.Collections.Generic;

public class ObjectPoints : MonoBehaviour
{
	public List<ObjectsPoint> ListObject = new List<ObjectsPoint>();
}

[Serializable]
public struct ObjectsPoint
{
	public enum GameObjects
	{
		PointableObject,
		VariantObject

	}
	public GameObjects Type;
	public GameObject[] PrefabVariant;
	public GameObject Prefab;
	public int Point;
}


