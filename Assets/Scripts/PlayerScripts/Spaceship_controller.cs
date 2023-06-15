using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Spaceship_controller : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    float Spaceship_speed;
    Rigidbody2D Spaceship;
    Rect cameraRect;
    
    void OnEnable()
    {
        Camera camera= Camera.main;
        Spaceship = GetComponent<Rigidbody2D>();
        var bottomLeft = camera.ScreenToWorldPoint(Vector3.zero);
        var topRight = camera.ScreenToWorldPoint(new Vector3(
            camera.pixelWidth, camera.pixelHeight));

        cameraRect = new Rect(
            bottomLeft.x,
            bottomLeft.y,
            topRight.x - bottomLeft.x,
            topRight.y - bottomLeft.y);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Spaceship.AddForce(new Vector2(Input.GetAxis("Horizontal") * Spaceship_speed, 0));
        Spaceship.AddForce(new Vector2(0, Input.GetAxis("Vertical") * Spaceship_speed));
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, cameraRect.xMin, cameraRect.xMax), Mathf.Clamp(transform.position.y, cameraRect.yMin,cameraRect.yMax),transform.position.z);
        
    }
}
