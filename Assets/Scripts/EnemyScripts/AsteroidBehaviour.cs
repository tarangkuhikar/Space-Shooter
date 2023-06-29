using UnityEngine;
public class AsteroidBehaviour : MonoBehaviour
{
    [SerializeField]
    EnemyData _asteroidData;
    [SerializeField]
    float _asteroidRotation;
    Vector3 _moveDirection;

    private void Start()
    {
        _moveDirection = GameManager.Player.position - new Vector3(Random.Range(-2, 2), Random.Range(-2, 2), 0) - gameObject.transform.position;
    }
    private void Update()
    {
        gameObject.transform.position += _asteroidData.BulletSpeed * Time.deltaTime * _moveDirection.normalized;
        gameObject.transform.Rotate(Vector3.forward, _asteroidRotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Destroy(gameObject);
            ScoreScript.ScoreChanged(_asteroidData.Experience);
            collision.gameObject.SetActive(false);
        }
    }

}