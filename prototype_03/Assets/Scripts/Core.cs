using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    [SerializeField] private float _speed, _jumpSpeed, _centerSpeed, _maxVelocity;
    [SerializeField] private bool _core;

    private List<GameObject> _tiles = new List<GameObject>();
    private Rigidbody2D _rb;

    private bool _left, _right, _jump, _up;
    private bool _freeze;

    //private bool _rbFound;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        //if (TryGetComponent<Rigidbody2D>(out Rigidbody2D test))
        //    _rb = test;
        //else
        //    _rbFound = false; // write logic for this case
    }

    void Update()
    {
        if (_core && !_freeze)
        {
            Inputs();
            Movements();
        }
    }
    private void FixedUpdate()
    {
        ToCenter();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "tile" && _core)
            _tiles.Add(collision.gameObject);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "tile" && _core)
            _tiles.Remove(collision.gameObject);
    }

    private void Move(Vector3 dir)
    {
        _rb.AddForce(dir * _speed * Time.deltaTime);

        if (_rb.velocity.magnitude >= _maxVelocity)
        {
            _rb.velocity = _rb.velocity.normalized * _maxVelocity;
        }
    }
    private void Inputs()
    {
        _up = Input.GetKey(KeyCode.UpArrow);
        _jump = Input.GetKey(KeyCode.Space);
        _left = Input.GetKey(KeyCode.LeftArrow);
        _right = Input.GetKey(KeyCode.RightArrow);
    }
    private void Movements()
    {
        if (_left && !_right)
            Move(new Vector3(-1, 0, 0));
        else if (!_left && _right)
            Move(new Vector3(1, 0, 0));

        if (_jump && !_up)
            Move(new Vector3(0, _jumpSpeed, 0));
        if (!_jump && _up)
            Move(new Vector3(0, 3, 0));

        if (_up && _left)
            Move(new Vector3(-1, 1 / 1.5f, 0));
        else if (_up && _right)
            Move(new Vector3(1, 1 / 1.5f, 0));
    }

    private void ToCenter()
    {
        for (int i = 0; i < _tiles.Count; i++)
        {
            _tiles[i].gameObject.GetComponent<Rigidbody2D>().AddForce((transform.position - _tiles[i].transform.position)
                * _centerSpeed * Time.deltaTime);
        }
    }

    private GameObject[] Append(GameObject[] array, GameObject item)
    {
        GameObject[] result = new GameObject[array.Length + 1];
        for (int i = 0; i < array.Length; i++)
        {
            result[i] = array[i];
        }
        result[result.Length - 1] = item;
        return result;
    }
    private GameObject[] Remove(GameObject[] array, GameObject item)
    {
        GameObject[] result = new GameObject[array.Length - 1];

        int counter = 0;
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] != item)
            {
                result[counter] = array[i];
                counter++;
            }
        }
        return result;
    }
    public void Freeze()
        => _freeze = true;
    public void UnFreeze()
        => _freeze = false;
}
