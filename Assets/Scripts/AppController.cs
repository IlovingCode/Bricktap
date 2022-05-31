
using UnityEngine;

public class AppController : MonoBehaviour
{
    [SerializeField] float minSpeed;
    [SerializeField] float maxSpeed;

    void Awake()
    {
    }

    void Start()
    {
        Block.minSpeed = minSpeed;
        Block.maxSpeed = maxSpeed;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;
                // Debug.Log(objectHit.GetChild(0).GetChild(0).up);
                var dir = objectHit.GetChild(0).GetChild(0).up;
                ray.direction = dir;
                ray.origin = objectHit.position;
                objectHit.GetComponent<Block>().Move(dir, Physics.Raycast(ray, 1f));

                // Do something with the object that was hit by the raycast.
            }
        }
    }
}
