using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clients : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _exitSpeed;
    [SerializeField] float _orderingTime;
    [SerializeField] float _orderTimer;
    [SerializeField] bool _served;
    [SerializeField] Transform _servePoint;
    float _intoExit;

    void Start()
    {
        _intoExit = Random.Range(0, 2) == 0 ? -1 : 1;
        Debug.Log(_intoExit);
    }

    void Update()
    {
        var dir = _servePoint.transform.position - transform.position;
        if (transform.position.x  <= 0.6 && !_served)
        {
            Debug.Log("pidiendo");
            transform.right = -dir;
            _orderTimer += 0.1f * Time.deltaTime;
        }
        if (_orderTimer >= _orderingTime)
        {
            Debug.Log("se va");
            _served = true;
            transform.forward = -dir;
            transform.position = new Vector3(transform.position.x + _intoExit * (_exitSpeed * Time.deltaTime), transform.position.y, transform.position.z);
            //Debug.Log(transform.position + _intoExit * dir * _speed * Time.deltaTime);
            //Destroy(gameObject, 10);
        }
        if (transform.position.x >= 0.6 && !_served)
        {
            transform.forward = dir;
            transform.position += (dir * _speed * Time.deltaTime);
            Debug.Log("entrando");
        }
    }
}
