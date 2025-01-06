using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2.3f;
    private void Update()
    {
        transform.position += Vector3.left * Time.deltaTime * moveSpeed;
    }
}
