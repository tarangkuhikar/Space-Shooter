using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class SpaceshipController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    PlayerData _playerData;

    Rigidbody2D _spaceShip;
    //Rect cameraRect;

    Vector3 _screenBounds;
    Vector3 viewPos;
    float _objectWidth;
    void OnEnable()
    {
        _screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        _objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x;
        _spaceShip = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        _spaceShip.AddForce(_playerData.Speed * Input.GetAxis("Horizontal") * Vector3.right);
    }

    private void LateUpdate()
    {
        viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, _screenBounds.x * -1 + _objectWidth, _screenBounds.x - _objectWidth);
        transform.position = viewPos;
    }
}
