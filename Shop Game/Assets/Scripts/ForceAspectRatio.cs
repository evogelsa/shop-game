
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceAspectRatio : MonoBehaviour {

    private int screenX = 0;
    private int screenY = 0;

    private void RescaleCamera() {
        if (Screen.width == screenX && Screen.height == screenY) {
            return;
        }

        float targetAspect = 16f / 9f;
        float windowAspect = (float) Screen.width / (float) Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        if (scaleHeight < 1f) {
            Rect rect = GetComponent<Camera>().rect;
            rect.width = 1f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1f - scaleHeight) / 2f;

            GetComponent<Camera>().rect = rect;
        } else {
            float scaleWidth = 1f / scaleHeight;

            Rect rect = GetComponent<Camera>().rect;
            rect.width = scaleWidth;
            rect.height = 1f;
            rect.x = (1f - scaleWidth) / 2f;
            rect.y = 0;

            GetComponent<Camera>().rect = rect;
        }

        screenX = Screen.width;
        screenY = Screen.height;
    }

    void OnPreCull() {
        if (Application.isEditor) {
            return;
        }

        Rect wp = Camera.main.rect;
        Rect nr = new Rect(0, 0, 1, 1);

        Camera.main.rect = nr;
        GL.Clear(true, true, Color.black);
        Camera.main.rect = wp;
    }

    void Start() {
        RescaleCamera();
    }

    void Update() {
        RescaleCamera();
    }
}
