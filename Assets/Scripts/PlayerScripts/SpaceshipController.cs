using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class SpaceshipController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    PlayerData playerdata;

    Rigidbody2D Spaceship;
    //Rect cameraRect;

    Vector3 screenBounds;
    float objectWidth, objectHeight;
    void OnEnable()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,Camera.main.transform.position.z));
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x; 
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y;
        Spaceship = GetComponent<Rigidbody2D>();
       
    }

    void FixedUpdate()
    {
        Spaceship.AddForce(new Vector2(Input.GetAxis("Horizontal") * playerdata.Speed, 0));
    }

    private void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x*-1 + objectWidth, screenBounds.x - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y*-1 + objectHeight, screenBounds.y - objectHeight);
        transform.position = viewPos;
    }
}
