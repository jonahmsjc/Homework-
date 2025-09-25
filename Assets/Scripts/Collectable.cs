using UnityEngine;

public class CollectableMovement : MonoBehaviour
{
    public float moveSpeed = 2f;       // sideways speed
    public float moveRange = 2f;       // how far it moves from start X
    public float rotationSpeed = 50f;  // spin speed

    private Vector3 startPos;
    private int direction; // +1 = right, -1 = left

    void Start()
    {
        startPos = transform.position;

        // Randomize initial direction
        direction = Random.value > 0.5f ? 1 : -1;
    }

    void Update()
    {
        // Rotate slowly
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        // Move left or right
        transform.position += Vector3.right * direction * moveSpeed * Time.deltaTime;

        // If out of range, flip direction
        if (transform.position.x > startPos.x + moveRange)
            direction = -1;
        if (transform.position.x < startPos.x - moveRange)
            direction = 1;
    }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                // Tell the manager this one was collected
                FindObjectOfType<CollectableManager>().Collect(gameObject);
            }
        }
    }
