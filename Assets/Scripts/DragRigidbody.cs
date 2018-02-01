﻿using UnityEngine;

/// <summary>
/// Drag a rigidbody with the mouse using a spring joint.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class DragRigidbody : MonoBehaviour
{
	public float force = 600;
	public float damping = 6;
	Rigidbody activeObject;
	Transform activeTransform;
	GameObject toRotate;
	GameObject toSun;
	GameObject toIndLight;

	Transform jointTrans;
	float dragDepth;

	void Start () {
		toRotate = GameObject.Find("Rotator");
		//toSun = GameObject.Find ("Sun");
		//toIndLight = GameObject.Find ("IndLight");
		//toIndLight.SetActive (false);
	}

	void OnMouseDown ()
	{
		HandleInputBegin (Input.mousePosition);
	}

	void OnMouseUp ()
	{
		HandleInputEnd (Input.mousePosition);
	}

	void OnMouseDrag ()
	{
		HandleInput (Input.mousePosition);
	}


	Plane grabPlane;

	public void HandleInputBegin (Vector3 screenPosition)
	{
		var ray = Camera.main.ScreenPointToRay (screenPosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit)) {
			if (hit.transform.gameObject.layer == LayerMask.NameToLayer ("Interactive")) {
				activeObject = hit.rigidbody;
				toRotate.GetComponent<Rotator> ().activeTransformer = hit.transform;
				activeTransform = hit.transform;
				dragDepth = CameraPlane.CameraToPointDepth (Camera.main, hit.point);
				jointTrans = AttachJoint (hit.rigidbody, hit.point);
				grabPlane = new Plane (Vector3.up, hit.point);
			}
		}
	}

	public void HandleInput (Vector3 screenPosition)
	{
		if (jointTrans == null)
			return;

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		float rayDistance;
		if (grabPlane.Raycast(ray, out rayDistance))
			jointTrans.position = ray.GetPoint(rayDistance);
		if (Input.GetMouseButtonDown (1)) {
			Debug.Log ("Up");
			activeObject.useGravity = false;
			activeTransform.Translate (Vector3.up * 2);
			//toSun.SetActive (false);
			//toIndLight.SetActive (true);
		}
		if (Input.GetMouseButtonUp (1)) {
			Debug.Log ("Down");
			activeTransform.Translate (Vector3.down * 1.9f);
			activeObject.useGravity = true;
			//toIndLight.SetActive (false);
			//toSun.SetActive (true);
		}
	}
		
	public void HandleInputEnd (Vector3 screenPosition)
	{
		Destroy (jointTrans.gameObject);
		activeObject.useGravity = true;
		//toSun.SetActive (true);
	}

	Transform AttachJoint (Rigidbody rb, Vector3 attachmentPosition)
	{
		GameObject go = new GameObject ("Attachment Point");
		go.hideFlags = HideFlags.HideInHierarchy; 
		go.transform.position = attachmentPosition;

		var newRb = go.AddComponent<Rigidbody> ();
		newRb.isKinematic = true;

		var joint = go.AddComponent<ConfigurableJoint> ();
		joint.connectedBody = rb;
		joint.configuredInWorldSpace = true;
		joint.xDrive = NewJointDrive (force, damping);
		//joint.yDrive = NewJointDrive (force, damping);
		joint.zDrive = NewJointDrive (force, damping);
		joint.slerpDrive = NewJointDrive (force, damping);
		joint.rotationDriveMode = RotationDriveMode.Slerp;

		return go.transform;
	}

	private JointDrive NewJointDrive (float force, float damping)
	{
		JointDrive drive = new JointDrive ();
		drive.mode = JointDriveMode.Position;
		drive.positionSpring = force;
		drive.positionDamper = damping;
		drive.maximumForce = Mathf.Infinity;
		return drive;
	}
}