using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using UnityEngine;

public class Clients : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _exitSpeed;
    [SerializeField] float _orderingTime;
    [SerializeField] float _orderTimer;
    [SerializeField] bool _served;
    [SerializeField] Transform _servePoint;
    [SerializeField] Transform _enterPoint;
    [SerializeField] GameObject pedidoTexto;
    float _intoExit;

    Vector3 dir;
    Vector3 dirEnter;

    public Color nuevoColor = Color.red;


    void Start()
    {
        //_servePoint = transform.Find("Servepoint");
        //pedidoTexto = GameObject.Find("Pedido");

        pedidoTexto.SetActive(false);
        _intoExit = Random.Range(0, 2) == 0 ? -1 : 1;
        Debug.Log(_intoExit);
    }

    void Update()
    {
        dir = _servePoint.transform.position - transform.position;
        dirEnter = _enterPoint.transform.position - transform.position;
        if (transform.position.x  <= 0.6 && !_served)
        {
            pedidoTexto.SetActive(true);
            Debug.Log("pidiendo");
            transform.right = -dir;
            _orderTimer += 0.1f * Time.deltaTime;
            if(_intoExit == -1)
            {
                ColorXD();
            }
        }
        if (_orderTimer >= _orderingTime)
        {
            pedidoTexto.SetActive(false);
            Debug.Log("se va");
            _served = true;
            transform.forward = dirEnter;
            transform.position += (dirEnter * _speed * Time.deltaTime);
            //transform.forward = -dir;
            //transform.position = new Vector3(transform.position.x + _intoExit * (_exitSpeed * Time.deltaTime), transform.position.y, transform.position.z);
            //Debug.Log(transform.position + _intoExit * dir * _speed * Time.deltaTime);
            //Destroy(gameObject, 10);
            if(transform.position.x >= 11)
            {
                _orderTimer = 0;

                entrando();
                Debug.Log("vuelve");
                _served = false;
            }
        }
        if (transform.position.x >= 0.6 && !_served || _orderTimer <= 0 && !_served)
        {
            entrando();
            Debug.Log("entrando");
        }
    }

    void entrando()
    {
        transform.forward = dir;
        transform.position += (dir * _speed * Time.deltaTime);
    }

    void ColorXD()
    {
        GetComponent<Renderer>().material.color = nuevoColor;

    }
}
