using System;
using UnityEngine;

[Serializable]
public class pb_UV
{
	public enum Fill
	{
		Fit = 0,
		Tile = 1,
		Stretch = 2,
	}

	public enum Justify
	{
		Right = 0,
		Left = 1,
		Top = 2,
		Center = 3,
		Bottom = 4,
		None = 5,
	}

	public bool useWorldSpace;
	public bool flipU;
	public bool flipV;
	public bool swapUV;
	public Fill fill;
	public Vector2 scale;
	public Vector2 offset;
	public float rotation;
	public Justify justify;
	public Vector2 localPivot;
	public Vector2 localSize;
}
