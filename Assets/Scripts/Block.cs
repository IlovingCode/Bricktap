using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // Start is called before the first frame update
    public static float minSpeed = 1f;
    public static float maxSpeed = 5f;

    static Dictionary<string, Vector3> sizeMap = new Dictionary<string, Vector3>() {
        {"1x1", new Vector3(1, 1, 1)},
        {"1x2", new Vector3(2, 1, 1)},
        {"1x4", new Vector3(4, 1, 1)},
        {"2x2", new Vector3(2, 2, 1)},
    };

    public static Vector3 getSize(GameObject obj) => obj.transform.TransformDirection(sizeMap[obj.name]);

    Vector3 direction;
    Vector3 origin;
    float timer = 0f;
    float sign = 0f;
    bool indirect = false;

    void Start()
    {
        origin = transform.position;
        enabled = false;
        gameObject.name = gameObject.name.Substring(0, 3);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime * sign;

        if (timer <= 0)
        {
            timer = 0;
            sign = 0;
            enabled = false;
            indirect = false;
        }

        if (timer > .2f && indirect)
        {
            Stop();
        }

        this.transform.position = origin + direction * timer;
    }

    public void Move(Vector3 dir, bool indir = false)
    {
        direction = dir;
        enabled = true;
        sign = indir ? minSpeed : maxSpeed;
        indirect = indir;

        if(!indir) {
            Invoke(nameof(End), 5f);
        }
    }

    void End() {
        Destroy(gameObject);
    }

    void Stop(GameObject block = null)
    {
        sign = -minSpeed;
        if (!indirect)
        {
            timer = .2f;

            var d = block.transform.position - origin;
            d.x *= Mathf.Abs(direction.x);
            d.y *= Mathf.Abs(direction.y);
            d.z *= Mathf.Abs(direction.z);

            var s = getSize(block) + getSize(gameObject);
            s *= .5f;
            s.x *= direction.x;
            s.y *= direction.y;
            s.z *= direction.z;
            origin += d - s;

            CancelInvoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var block = other.GetComponent<Block>();
        if (!block.enabled)
        {
            Stop(other.gameObject);
            block.Move(direction, true);
        }
    }
}
