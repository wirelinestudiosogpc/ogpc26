using UnityEngine;

public class SensitivityValue : MonoBehaviour
{
	public float cameraSens;
	public bool compass;
	void Start()
	{
		DontDestroyOnLoad(this.gameObject);
	}
}
