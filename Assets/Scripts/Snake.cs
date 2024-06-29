using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [Header("Move KeyCode")]
    public KeyCode upKey;
    public KeyCode downKey;
    public KeyCode leftKey;
    public KeyCode rightKey;

    [Header("Properties")]
    public Transform segmentPrefabTrans;
    public int initialSize = 4;

    [HideInInspector] public Vector2 direction {get; private set;}

    private List<Transform> segments = new List<Transform>();

    void Awake()
    {
        direction = Vector2.right;
        ResetState();
    }
    
    void Update()
    {
        CheckInput();
    }

    void FixedUpdate()
    {
        Move();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Food")
        {
            Grow();
        }
        else if(other.CompareTag("Obstacle"))
        {
            ResetState();
        }
    }


    private void CheckInput()
    {
        if (Input.GetKey(upKey))
        {
            direction = Vector2.up;
        }
        else if(Input.GetKey(downKey))
        {
            direction = Vector2.down;
        }
        else if(Input.GetKey(leftKey))
        {
            direction = Vector2.left;
        }
        else if(Input.GetKey(rightKey))
        {
            direction = Vector2.right;
        }
    }

    private void Move()
    {
        for (int i = segments.Count - 1; i > 0; i--)
        {
            segments[i].position = segments[i - 1].position;
        }
        
        transform.position = new Vector3(
            Mathf.Round(transform.position.x) + direction.x,
            Mathf.Round(transform.position.y) + direction.y,
            0f
        );

    }

    public void Grow()
    {
        Transform segmentTrans = Instantiate(segmentPrefabTrans);
        segmentTrans.position = segments[segments.Count - 1].position;

        segments.Add(segmentTrans);
    }

    public void ResetState()
    {
        for (int i = 1; i < segments.Count; i++)
        {
            Destroy(segments[i].gameObject);
        }

        segments.Clear();
        segments.Add(transform);

        for (int i = 1; i < initialSize; i++)
        {
            segments.Add(Instantiate(segmentPrefabTrans));
        }

        transform.position = Vector3.zero;
    }
}
