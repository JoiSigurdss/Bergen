using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandom : MonoBehaviour {

	public GameObject[] ObjectPrefabs;
	public Transform selfMe;

	void Start(){
		Instantiate (ObjectPrefabs[Random.Range(0, ObjectPrefabs.Length)], selfMe);
	}
}
