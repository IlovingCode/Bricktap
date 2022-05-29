using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // Start is called before the first frame update
    public static float minSpeed = 1f;
    public static float maxSpeed = 5f;

    Vector3 direction;
    Vector3 origin;
    float timer = 0f;
    float sign = 0f;
    bool indirect = false;

    void Start()
    {
        origin = transform.position;
        enabled = false;
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
        }

        if (timer > .2f)
        {
            if (indirect) Stop();
            else sign = maxSpeed;
        }

        this.transform.position = origin + direction * timer;
    }

    public void Move(Vector3 dir, bool indir = false)
    {
        direction = dir;
        enabled = true;
        sign = minSpeed;
        indirect = indir;
    }

    void Stop()
    {
        sign = -minSpeed;
        indirect = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        var block = other.GetComponent<Block>();
        if (sign > 0 && block.sign == 0)
        {
            Stop();
            Debug.Log("OnTriggerEnter " + other.name);
            block.Move(direction, true);
        }
    }
}
