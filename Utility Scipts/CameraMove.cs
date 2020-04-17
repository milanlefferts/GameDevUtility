using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {
    public static CameraMove Instance {
        get {
            return instance;
        }
    }
    private static CameraMove instance;

    new Camera camera;
    Coroutine cameraMoving;

    private Transform cameraTransformOriginal;
    private Vector3 cameraPositionOriginal;
    private Vector3 cameraRotationOriginal;

    // Dice Hospital
    public Transform cameraTransformAlternate;

    // Smooth
    private Vector3 velocity = Vector3.zero;

    void Awake() {
        
        instance = this;
    }

    void Start() {

        camera = GetComponent<Camera>();
        cameraPositionOriginal = camera.transform.position;
        cameraRotationOriginal = camera.transform.eulerAngles;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.W)) {
            MoveCamera(cameraPositionOriginal, cameraRotationOriginal, 1f, true);
        }

        if (Input.GetKeyDown(KeyCode.S)) {
            MoveCamera(cameraTransformAlternate, 1f, true);
        }

        if (Input.GetKeyDown(KeyCode.A)) {
            MoveCamera(cameraPositionOriginal, cameraRotationOriginal, 0.3f, false);
        }

        if (Input.GetKeyDown(KeyCode.D)) {
            MoveCamera(cameraTransformAlternate, 0.3f, false);
        }
    }


    public void ResetCamera() {
        Debug.LogWarning("Reset Camera");
        MoveCamera(cameraPositionOriginal, cameraRotationOriginal, 0.3f, true);
    } 

    public void CameraToAmbulance() {
        Debug.LogWarning("Camera To Alternative Camera");
        MoveCamera(cameraTransformAlternate, 0.3f, true);
    }

    public void MoveCamera(Transform locationToMoveTo, float timeToMove, bool smooth) {
        if (cameraMoving != null) StopCoroutine(cameraMoving);
        cameraMoving = StartCoroutine(MoveCameraToPosition(camera.transform, locationToMoveTo.position, locationToMoveTo.eulerAngles, timeToMove, smooth));
    }

    public void MoveCamera(Vector3 targetPos, Vector3 targetRot, float timeToMove, bool smooth) {
        if (cameraMoving != null) StopCoroutine(cameraMoving);
        cameraMoving = StartCoroutine(MoveCameraToPosition(camera.transform, targetPos, targetRot, timeToMove, smooth));
    }


    private IEnumerator MoveCameraToPosition(Transform cameraToMove, Vector3 targetPos, Vector3 targetRot, float timeToMove, bool smooth) {
        Vector3 currentPos = cameraToMove.position;
        Vector3 currentRot = cameraToMove.eulerAngles;
        float t = 0f;

        if (smooth) {

            while (cameraToMove.position != targetPos) {
                t += Time.deltaTime / timeToMove;

                cameraToMove.position = Vector3.SmoothDamp(cameraToMove.position, targetPos, ref velocity, 0.3f);
                cameraToMove.rotation = Quaternion.Slerp(cameraToMove.rotation, Quaternion.Euler(targetRot), t);

                yield return null;

                if (Vector3.Distance(cameraToMove.position, targetPos) < 0.05f &&
                    Vector3.Distance(cameraToMove.eulerAngles, targetRot) < 0.05f) {
                    cameraToMove.position = targetPos;
                    cameraToMove.eulerAngles = targetRot;
                }
            }

        }
        else {
            while (t < 1) {
                t += Time.deltaTime / timeToMove;

                cameraToMove.position = Vector3.Lerp(currentPos, targetPos, t);
                cameraToMove.rotation = Quaternion.Slerp(cameraToMove.rotation, Quaternion.Euler(targetRot), t);

                yield return null;
            }

        }



    }
}
