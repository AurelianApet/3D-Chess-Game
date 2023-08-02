using UnityEngine;
using System.Collections;

public class PanelScript : MonoBehaviour 
{
	void Start () 
	{
        Init();
	}

    public void Init()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<SquarScript>().SetPosition(i);
        }
    }

	public void Rotate(float Rotation)
	{
		for (int i = 0; i<transform.childCount; i++)
		{
			GameObject _child = transform.GetChild(i).gameObject as GameObject;
			_child.transform.localRotation = Quaternion.Euler (0,0,Rotation);
		}
	}
}
