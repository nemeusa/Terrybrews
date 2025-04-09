using System.Collections.Generic;
using UnityEngine;

public class VasoInteractivo : MonoBehaviour
{
    public LayerMask interactableLayer;

    private Cup vasoSeleccionado;

    public CupSize cupSize;
    //public DrinkName currentDrink;
    public List<DrinkType> drinks = new List<DrinkType>();

    public Transform liquidContainer; // Donde se instancian los líquidos visuales
    public GameObject liquidPrefab;

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
                        Llenar(botella.drinkType);
                    }
                    else
                    {
                        Debug.Log("Seleccioná un vaso");
                    }
                }
            }
        }
    }

    private int MaxBebidas => cupSize switch
    {
        CupSize.Pequeño => 1,
        CupSize.Mediano => 2,
        CupSize.Grande => 3,
        _ => 2
    };

    public void Llenar(DrinkType bebida)
    {
        if (drinks.Count >= MaxBebidas)
        {
            Debug.Log($"El vaso {cupSize} ya está lleno.");
            return;
        }

        drinks.Add(bebida);
        Debug.Log($"Agregado: {bebida} al vaso {cupSize}");

        // Crear visual de líquido
        CrearLiquidoVisual(bebida, drinks.Count);
    }

    private void CrearLiquidoVisual(DrinkType bebida, int index)
    {
        GameObject liquido = Instantiate(liquidPrefab, liquidContainer);
        Color color = ObtenerColorPorBebida(bebida);
        liquido.GetComponent<Renderer>().material.color = color;

        float altura = 0.05f * index;
        liquido.transform.localPosition = new Vector3(0, altura, 0);
        liquido.transform.localScale = new Vector3(1, 0.05f, 1); // ancho fijo, altura pequeña
    }

    private Color ObtenerColorPorBebida(DrinkType bebida)
    {
        return bebida switch
        {
            DrinkType.Agua => new Color(0.5f, 0.8f, 1f),
            DrinkType.Jugo => new Color(1f, 0.6f, 0.1f),
            DrinkType.Cerveza => new Color(1f, 0.85f, 0.3f),
            DrinkType.Gaseosa => new Color(0.3f, 0.3f, 0.3f),
            _ => Color.white
        };
    }
}
