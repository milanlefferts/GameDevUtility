using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControls : MonoBehaviour {

    public float TouchTime;
    public float HoverTimer;
    public GameObject TouchedObject;
    public bool isPressing;

    void Start() {
        HoverTimer = 0.5f;
    }

    void Update() {

#if UNITY_IOS || UNITY_ANDROID
        if (Input.touchCount > 0) return;

        Touch touch = Input.touches[0];

        if (touch.phase == TouchPhase.Began) {

            // Store time that touch began.
            TouchTime = Time.time;

            RayCast();
        }

        // Don't do interactions if there is nothing to interact with.
        if (TouchedObject == null) return;

        // This is a long press.
        if (!isPressing && touch.phase == TouchPhase.Stationary && Time.time - TouchTime > HoverTimer) {
            
            isPressing = true;
            LongPress(TouchedObject, true);
        }

        // This is still a long press, but in a different location.
        if (isPressing && touch.phase == TouchPhase.Moved && Time.time - TouchTime > HoverTimer) {

            // Determine if a new object is being touched.
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            GameObject obj;
            if (Physics.Raycast(ray, out hit)) {

                obj = hit.collider.gameObject;
                if (obj != TouchedObject) {
                    LongPress(TouchedObject, false);
                    LongPress(obj, true);
                    TouchedObject = obj;
                    Debug.Log("Updated popup to: " + hit.collider.gameObject.name);
                }
            }
        }
        
        // Touch has ended.
        if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) {

            Release();

            // Tap
            if (Time.time - TouchTime <= HoverTimer) {
                Tap(TouchedObject);
            }

            // Reset.
            LongPress(TouchedObject, false);
            TouchTime = 0f;
            TouchedObject = null;
            isPressing = false;
        }

#endif

#if UNITY_STANDALONE_WIN || UNITY_EDITOR
        if (Input.GetMouseButtonDown(0)) {
            RayCast();

        if (TouchedObject == null) return;

        // Store time that touch began.
            TouchTime = Time.time;

               // This is a long press.
        if (!isPressing && Time.time - TouchTime > HoverTimer) {
            
            isPressing = true;
            LongPress(TouchedObject, true);
        } 
        

        if (Input.GetMouseButtonUp(0)) {
            Release();

        if (Time.time - TouchTime <= HoverTimer) {
                Tap(TouchedObject);
            }

            // Reset.
            LongPress(TouchedObject, false);
            TouchTime = 0f;
            TouchedObject = null;
            isPressing = false;


        }
#endif


    }



    private void Tap(GameObject obj) {
        if (IsPointerOverGameObject()) return;
        obj.SendMessage("Tap", SendMessageOptions.DontRequireReceiver);
    }

    private void Release(GameObject obj) {
        if (IsPointerOverGameObject()) return;
        obj.SendMessage("Release", SendMessageOptions.DontRequireReceiver);
    }

    private void LongPress(GameObject obj, bool sameLocation) {
        if (IsPointerOverGameObject()) return;
        obj.SendMessage("LongPress", sameLocation, SendMessageOptions.DontRequireReceiver);
    }

    /// <summary>
    /// Fire ray and store touched object.
    /// </summary>
    private void RayCast() {
        // 
        RaycastHit hit;
#if UNITY_IOS || UNITY_ANDROID
        Ray ray = Camera.main.ScreenPointToRay(touch.position);
#endif

#if UNITY_STANDALONE_WIN || UNITY_EDITOR
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

#endif
        if (Physics.Raycast(ray, out hit)) {

            TouchedObject = hit.collider.gameObject;
            Debug.Log(hit.collider.gameObject.name);
        }
    }

    private bool IsPointerOverGameObject() {
        if (EventSystem.current.IsPointerOverGameObject()) {
            return true;
        }

        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) {
            if (EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId)) {
                return true;
            }
        }
        return false;
    }


}
