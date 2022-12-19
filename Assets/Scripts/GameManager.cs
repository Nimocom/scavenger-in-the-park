using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Vector3 cameraEndPoint;
    [SerializeField] Image splashScreen;

    [SerializeField] float splashScreenFadingSpeed;
    [SerializeField] float cameraMovingSpeed;

    new Camera camera;

    void Start()
    {
        camera = Camera.main;
        Cursor.visible = false;
    }

    void Update()
    {
        var color = splashScreen.color;
        color.a = Mathf.Lerp(color.a, 0f, splashScreenFadingSpeed * Time.deltaTime);
        splashScreen.color = color;

        camera.transform.position = Vector3.Lerp(camera.transform.position, cameraEndPoint, cameraMovingSpeed * Time.deltaTime);

        if (Vector3.Distance(camera.transform.position, cameraEndPoint)<0.08f)
        {
            CameraController.inst.enabled = true;
            Hero.inst.enabled = true;

            color.a = 0f;
            splashScreen.color = color;
            camera.transform.position = cameraEndPoint;
            Destroy(gameObject);
        }
    }

}
