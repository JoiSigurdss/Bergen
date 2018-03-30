using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPrefab : MonoBehaviour {

	public GameObject prefab;

	void Update () {
		if (Input.GetKeyDown (KeyCode.T)) {
				prefab.SetActive (!prefab.activeInHierarchy);
		}
	}
}
