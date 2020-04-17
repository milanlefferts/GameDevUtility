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

        if (Input.touchCount < 1) return;

        Touch touch = Input.touches[0];

        if (touch.phase == TouchPhase.Began) {

            // Store time that touch began.
            TouchTime = Time.time;

            // Fire ray on tap and store touched object.
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            if (Physics.Raycast(ray, out hit)) {

                TouchedObject = hit.collider.gameObject;
                Debug.Log(hit.collider.gameObject.name);
            }
        }

        // Don't do interactions if there is nothing to interact with.
        if (TouchedObject == null) return;

        // This is a long press.
        if (!isPressing && touch.phase == TouchPhase.Stationary && Time.time - TouchTime > HoverTimer) {
            
            isPressing = true;
            
            TouchedObject.SendMessage("Press", true, SendMessageOptions.DontRequireReceiver);
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
                    TouchedObject.SendMessage("Press", false, SendMessageOptions.DontRequireReceiver);

                    obj.SendMessage("Press", true, SendMessageOptions.DontRequireReceiver);

                    TouchedObject = obj;

                    Debug.Log("Updated popup to: " + hit.collider.gameObject.name);
                }
            }
        }
        
        // Touch has ended.
        if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) {

            // Tap
            if (Time.time - TouchTime <= HoverTimer) {
                TouchedObject.SendMessage("Tap", SendMessageOptions.DontRequireReceiver);
            }

            // Reset.
            TouchedObject.SendMessage("Press", false, SendMessageOptions.DontRequireReceiver);
            TouchTime = 0f;
            TouchedObject = null;
            isPressing = false;
        }
    }

}
