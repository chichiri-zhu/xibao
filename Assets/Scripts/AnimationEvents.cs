using System;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
	/// <summary>
	/// Subscribe it to get animation callback.
	/// </summary>
	public event Action<string> OnCustomEvent = s => { };

	/// <summary>
	/// Set bool param, usage example: Idle=false
	/// </summary>
	public void SetBool(string value)
	{
		var parts = value.Split('=');

		GetComponent<Animator>().SetBool(parts[0], bool.Parse(parts[1]));
	}

	/// <summary>
	/// Set integer param, usage example: WeaponType=2
	/// </summary>
	public void SetInteger(string value)
	{
		var parts = value.Split('=');

		GetComponent<Animator>().SetInteger(parts[0], int.Parse(parts[1]));
	}

	/// <summary>
	/// Called from animation.
	/// </summary>
	public void CustomEvent(string eventName)
	{
		OnCustomEvent(eventName);
	}
}