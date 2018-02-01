using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

	public Transform activeTransformer;
	public float rotatePower = 45f;

	void Update () {

		if (Input.GetKeyUp (KeyCode.E)) {
				if (activeTransformer == null)
					return;
				activeTransformer.Rotate (Vector3.up * rotatePower, Space.Self);
		}
		if (Input.GetKeyUp (KeyCode.Q)) {
			if (activeTransformer == null)
				return;
			activeTransformer.Rotate (Vector3.down * rotatePower, Space.Self);
		}

		if (Input.GetKeyUp (KeyCode.Delete)) {
			if (activeTransformer == null)
				return;
			Destroy (activeTransformer.gameObject);
			//activeTransformer
		}
	}
}