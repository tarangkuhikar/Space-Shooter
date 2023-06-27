using System.Collections;
using UnityEngine;
public class AsteroidBehaviour : MonoBehaviour
{
    [SerializeField]
    Vector3 asteroidSpeed;
    private void Update()
    {
        gameObject.transform.position += asteroidSpeed * Time.deltaTime;       
    }
}