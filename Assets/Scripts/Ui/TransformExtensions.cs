//// author: Dmitriy Yukhanov / focus@codestage.ru

using UnityEngine;

public static class TransformExtensions
{
	public static void SetPositionX(this Transform t, float x)
	{
		Vector3 pos = t.position;
		t.position = new Vector3(x, pos.y, pos.z);
	}

	public static void SetPositionY(this Transform t, float y)
	{
		Vector3 pos = t.position;
		t.position = new Vector3(pos.x, y, pos.z);
	}

	public static void SetPositionZ(this Transform t, float z)
	{
		Vector3 pos = t.position;
		t.position = new Vector3(pos.x, pos.y, z);
	}

	#region localPosition

	public static void SetLocalPositionX(this Transform t, float x)
	{
		t.SetLocalPositionX(x, false);
	}

	public static void SetLocalPositionX(this Transform t, float x, bool relative)
	{
		Vector3 pos = t.localPosition;
		if (!relative)
		{
			pos.x = x;
		}
		else
		{
			pos.x += x;
		}
		t.localPosition = pos;
	}

	public static void SetLocalPositionY(this Transform t, float y)
	{
		t.SetLocalPositionY(y, false);
	}

	public static void SetLocalPositionY(this Transform t, float y, bool relative)
	{
		Vector3 pos = t.localPosition;
		if (!relative)
		{
			pos.y = y;
		}
		else
		{
			pos.y += y;
		}
		t.localPosition = pos;
	}

	public static void SetLocalPositionZ(this Transform t, float z)
	{
		t.SetLocalPositionZ(z, false);
	}

	public static void SetLocalPositionZ(this Transform t, float z, bool relative)
	{
		Vector3 pos = t.localPosition;
		if (!relative)
		{
			pos.z = z;
		}
		else
		{
			pos.z += z;
		}
		t.localPosition = pos;
	}

	#endregion

	#region localRotation
	public static void SetLocalRotationEulerX(this Transform t, float x)
	{
		t.SetLocalRotationEulerX(x, false);
	}

	public static void SetLocalRotationEulerX(this Transform t, float x, bool relative)
	{
		Vector3 pos = t.localRotation.eulerAngles;
		if (!relative)
		{
			pos.x = x;
		}
		else
		{
			pos.x += x;
		}
		t.localRotation = Quaternion.Euler(pos);
	}

	public static void SetLocalRotationEulerY(this Transform t, float y)
	{
		t.SetLocalRotationEulerY(y, false);
	}

	public static void SetLocalRotationEulerY(this Transform t, float y, bool relative)
	{
		Vector3 pos = t.localRotation.eulerAngles;
		if (!relative)
		{
			pos.y = y;
		}
		else
		{
			pos.y += y;
		}
		t.localRotation = Quaternion.Euler(pos);
	}

	public static void SetLocalRotationEulerZ(this Transform t, float z)
	{
		t.SetLocalRotationEulerZ(z, false);
	}

	public static void SetLocalRotationEulerZ(this Transform t, float z, bool relative)
	{
		Vector3 pos = t.localRotation.eulerAngles;
		if (!relative)
		{
			pos.z = z;
		}
		else
		{
			pos.z += z;
		}
		t.localRotation = Quaternion.Euler(pos);
	}
	#endregion

	public static void SetLocalScale(this Transform t, float newScale)
	{
		t.localScale = new Vector3(newScale, newScale, newScale);
	}
}
