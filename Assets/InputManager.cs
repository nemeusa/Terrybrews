using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public LayerMask interactableLayer;

    private Cup vasoSeleccionado;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f, interactableLayer))
            {
                if (hit.collider.TryGetComponent(out Cup vaso))
                {
                    vasoSeleccionado = vaso;
                    Debug.Log("Vaso Seleccionado: " + vaso.cupSize);
                }
                else if (hit.collider.TryGetComponent(out Beverage botella))
                {
                    if (vasoSeleccionado != null)
                    {
                        vasoSeleccionado.Fill(botella.drinkType);
                    }
                    else
                    {
                        Debug.Log("Seleccioná un vaso");
                    }
                }
            }
        }
    }
}