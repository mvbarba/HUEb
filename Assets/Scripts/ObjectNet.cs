using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectNet : MonoBehaviour
{
	public void OnCollisionEnter(Collision collision) {
		Debug.Log("Found naughty boi");
		collision.gameObject.transform.position = new Vector3(0, 13, 0);
	}
}
