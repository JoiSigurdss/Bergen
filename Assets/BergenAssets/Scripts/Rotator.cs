using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Rotator : MonoBehaviour {

	public Transform activeTransformer;
	public float rotatePower = 45f;

	AudioSource audioSource;

	public AudioClip WhenRotatingLeft;
	public AudioClip WhenRotatingRight;
	public AudioClip WhenDeleting;

	void Start () {
		audioSource = GetComponent<AudioSource> ();
	}

	void Update () {

		if (Input.GetKeyUp (KeyCode.E)) {
				if (activeTransformer == null)
					return;
				activeTransformer.Rotate (Vector3.up * rotatePower, Space.Self);
			audioSource.PlayOneShot(WhenRotatingLeft, 0.7F);
				
		}
		if (Input.GetKeyUp (KeyCode.Q)) {
			if (activeTransformer == null)
				return;
			activeTransformer.Rotate (Vector3.down * rotatePower, Space.Self);
			audioSource.PlayOneShot(WhenRotatingRight, 0.7F);
		}

		if (Input.GetKeyUp (KeyCode.Delete)) {
			if (activeTransformer == null)
				return;
			Destroy (activeTransformer.gameObject);
			audioSource.PlayOneShot(WhenDeleting, 0.7F);
			//activeTransformer
		}
	}
}