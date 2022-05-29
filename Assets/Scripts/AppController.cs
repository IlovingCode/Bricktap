
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
                objectHit.GetComponent<Block>().Move(objectHit.GetChild(0).GetChild(0).up);

                // Do something with the object that was hit by the raycast.
            }
        }
    }
}
