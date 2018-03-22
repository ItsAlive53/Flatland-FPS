using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS_Player : Generics.Damageable {

    public Camera ConnectedCamera;
    [Range(0, 10f)]
    public float Sensitivity = 1f;
    [Range(0, 10f)]
    public float MovementSpeed = 1f;
    [Range(0, 100f)]
    [Tooltip("Range at which interactable objects will be highlighted")]
    public float HighlightRange = 15f;

    public float JumpStrength = 5f;

    bool GoForward;
    bool GoBackward;
    bool StrafeLeft;
    bool StrafeRight;

    bool GoUp;
    bool canJump;

    Vector3 TargetPosition;

    Transform Head;

    GameObject HighlightedObject;

    Generics.EquippableObject EquippedObject;

	void Start() {
        GoForward = GoBackward = StrafeLeft = StrafeRight = false;

        Head = transform.Find("Head");

        if (!Head) {
            Head = new GameObject("Head").transform;
            Head.tag = tag;
            Head.position = transform.position + new Vector3(0, 0.5f, 0);
        }

        Head.parent = transform;

		if (!ConnectedCamera) {
            Debug.LogWarningFormat("No camera connected, check {0} in the Unity Editor.", name);
        }

        TargetPosition = transform.position;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        canJump = true;
	}
	
	protected new void Update() {
        base.Update();

        if (HasDied()) {
            GoForward = GoBackward = StrafeLeft = StrafeRight = false;
            return;
        }

        GoForward = Input.GetKey(KeyCode.W);
        GoBackward = Input.GetKey(KeyCode.S);
        StrafeLeft = Input.GetKey(KeyCode.A);
        StrafeRight = Input.GetKey(KeyCode.D);

        GoUp = Input.GetKeyDown(KeyCode.Space);

        CheckInteractableObjects();

        if (HighlightedObject && Input.GetKeyDown(KeyCode.E)) {
            if (HighlightedObject.GetComponent<Generics.EquippableObject>()) {
                HighlightedObject.GetComponent<Generics.EquippableObject>().GrabbingPlayer = transform;
                EquippedObject = HighlightedObject.GetComponent<Generics.EquippableObject>();
                HighlightedObject = null;
            }
        }

        if (EquippedObject && Input.GetKeyDown(KeyCode.Mouse0)) {
            if (EquippedObject.GetComponent<ProjectileWeapon>()) {
                EquippedObject.GetComponent<ProjectileWeapon>().Fire();
            }
        }

        if (EquippedObject && Input.GetKey(KeyCode.Mouse0)) {
            if (EquippedObject.GetComponent<HitscanWeapon>()) {
                EquippedObject.GetComponent<HitscanWeapon>().Fire();
            }
        }
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

        if (GoUp) {
            if (GetComponent<Rigidbody>() && canJump) {
                canJump = false;
                Debug.Log("JUMP f:" + Time.frameCount);
                GetComponent<Rigidbody>().AddForce(0, JumpStrength, 0, ForceMode.VelocityChange);
            }
        }

        TargetPosition.y = 0;

        transform.position += TargetPosition;

        var rot = Quaternion.Euler(transform.eulerAngles + Head.eulerAngles);

        if (ConnectedCamera) {
            ConnectedCamera.transform.SetPositionAndRotation(Head.position, rot);
        }
    }

    void CheckInteractableObjects() {
        var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit rcHit;

        var castedRay = Physics.Raycast(ray, out rcHit, HighlightRange);

        if (castedRay) {
            if (rcHit.collider.GetComponent<Generics.EquippableObject>()) {
                rcHit.collider.GetComponent<Generics.EquippableObject>().Highlight();
                HighlightedObject = rcHit.collider.gameObject;
            } else if (rcHit.collider.GetComponentInParent<Generics.EquippableObject>()) {
                rcHit.collider.GetComponentInParent<Generics.EquippableObject>().Highlight();
                HighlightedObject = rcHit.collider.GetComponentInParent<Generics.EquippableObject>().gameObject;
            }
        } else {
            if (HighlightedObject) {
                HighlightedObject = null;
            }
        }
    }

    protected override void Die() {
        EquippedObject.UnEquip();
        EquippedObject = null;
        HighlightedObject = null;
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.tag == "Ground") {
            canJump = true;
        }
    }

    private void OnCollisionExit(Collision collision) {
        if (collision.collider.tag == "Ground") {
            canJump = false;
        }
    }
}
