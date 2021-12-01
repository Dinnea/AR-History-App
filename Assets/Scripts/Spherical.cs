using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script partly from https://community.gamedev.tv/t/spherical-coordinates-for-camera-offset/33859

[System.Serializable]
public class Spherical
{
	[SerializeField]
	public float radius, theta, phi;
	//theta = "inclination"
	//phi = "heading"

	public static Vector3 ToCartesian(float radius, float theta, float phi)
	{
		float x = radius * Mathf.Sin(theta * Mathf.Deg2Rad) * Mathf.Cos(phi * Mathf.Deg2Rad);
		float y = radius * Mathf.Cos(theta * Mathf.Deg2Rad);
		float z = radius * Mathf.Sin(theta * Mathf.Deg2Rad) * Mathf.Sin(phi * Mathf.Deg2Rad);
		return new Vector3(x, y, z);
	}

	public static Vector3 ToCartesian(Vector3 v)
	{
		return ToCartesian(v.x, v.y, v.z);
	}

	public static Spherical FromCartesian(float x, float y, float z)
    {
		Spherical spherical = new Spherical(0, 0, 0);

		spherical.radius = new Vector3(x, y, z).magnitude;
		spherical.theta = Mathf.Atan2(y, x);
		spherical.phi = Mathf.Atan2(new Vector2(x, y).magnitude, z);

		return spherical;
    }

	public static Spherical FromCartesian(Vector3 v)
    {
		return FromCartesian(v.x, v.y, v.z);
    }

	public Spherical(float radius, float theta, float phi)
	{
		this.radius = radius;
		this.theta = theta;
		this.phi = phi;
	}

	public Vector3 ToCartesian()
	{
		return Spherical.ToCartesian(radius, theta, phi);
	}
}
