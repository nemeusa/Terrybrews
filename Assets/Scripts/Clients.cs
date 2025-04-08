using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clients : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _orderingTime;
    [SerializeField] float _orderTimer;
    [SerializeField] bool _served;
    [SerializeField] Transform _servePoint;

    void Start()
    {
        
    }

    void Update()
    {
        var dir = _servePoint.transform.position - transform.position;
        if (transform.position.x  <= 0.6 && !_served)
        {
            transform.position = Vector3.zero;
            _orderTimer -= 0.1f * Time.deltaTime;
            Debug.Log("pidiendo");
        }
        else if (_orderTimer >= _orderingTime)
        {
            Debug.Log("se va");
            _served = true;
            _orderTimer = 0;
        }
        else
        {
            transform.position += (dir * _speed * Time.deltaTime);
            Debug.Log("entrando");
        }
    }
}
