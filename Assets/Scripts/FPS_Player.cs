using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS_Player : MonoBehaviour {

    public Camera ConnectedCamera;
    [Range(0, 10f)]
    public float Sensitivity = 1f;
    [Range(0, 10f)]
    public float MovementSpeed = 1f;

    bool GoForward;
    bool GoBackward;
    bool StrafeLeft;
    bool StrafeRight;

    Vector3 TargetPosition;

    Transform Head;

	void Start() {
        GoForward = GoBackward = StrafeLeft = StrafeRight = false;

        Head = transform.Find("Head");

        if (!Head) {
            Head = new GameObject("Head").transform;
            Head.position = transform.position + new Vector3(0, 0.5f, 0);
        }

        Head.parent = transform;

		if (!ConnectedCamera) {
            Debug.LogWarningFormat("No camera connected, check {0} in the Unity Editor.", name);
        }

        TargetPosition = transform.position;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
	}
	
	void Update() {
        GoForward = Input.GetKey(KeyCode.W);
        GoBackward = Input.GetKey(KeyCode.S);
        StrafeLeft = Input.GetKey(KeyCode.A);
        StrafeRight = Input.GetKey(KeyCode.D);
	}

    void FixedUpdate() {
        float xEuler = Head.rotation.eulerAngles.x;

        if (xEuler > 180) xEuler -= 360;

        float xRot = xEuler + Input.GetAxisRaw("Mouse Y") * Sensitivity * -1f;

        if (xRot > 89f) xRot = 89f;
        if (xRot < -89f) xRot = -89f;

        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + Input.GetAxisRaw("Mouse X") * Sensitivity, 0);

        Head.rotation = Quaternion.Euler(xRot, 0, 0);

        TargetPosition = Vector3.zero;

        if (GoForward) {
            TargetPosition += transform.forward * MovementSpeed * 0.1f;
        }

        if (GoBackward) {
            TargetPosition -= transform.forward * MovementSpeed * 0.1f;
        }

        if (StrafeLeft) {
            TargetPosition -= transform.right * MovementSpeed * 0.1f;            
        }

        if (StrafeRight) {
            TargetPosition += transform.right * MovementSpeed * 0.1f;
        }

        TargetPosition.y = 0;

        transform.position += TargetPosition;

        var rot = Quaternion.Euler(transform.eulerAngles + Head.eulerAngles);

        if (ConnectedCamera) {
            ConnectedCamera.transform.SetPositionAndRotation(Head.position, rot);
        }
    }
}
