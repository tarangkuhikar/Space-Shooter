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
    float _objectWidth, _objectHeight;
    void OnEnable()
    {
        _screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        _objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x;
        _objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y;
        _spaceShip = GetComponent<Rigidbody2D>();

    }

    void FixedUpdate()
    {
        _spaceShip.AddForce(Input.GetAxis("Horizontal") * _playerData.Speed * Vector3.right);
    }

    private void LateUpdate()
    {
        viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, _screenBounds.x * -1 + _objectWidth, _screenBounds.x - _objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, _screenBounds.y * -1 + _objectHeight, _screenBounds.y - _objectHeight);
        transform.position = viewPos;
    }
}
