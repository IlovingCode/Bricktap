
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

                var d = Block.getSize(objectHit.gameObject) * .5f;
                d.x *= dir.x;
                d.y *= dir.y;
                d.z *= dir.z;

                ray.origin = objectHit.position + d;
                objectHit.GetComponent<Block>().Move(dir, Physics.Raycast(ray, .5f));

                // Do something with the object that was hit by the raycast.
            }
        }
    }
}
