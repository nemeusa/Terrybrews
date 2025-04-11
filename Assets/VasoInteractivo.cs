using System.Collections.Generic;
using UnityEngine;

public class VasoInteractivo : MonoBehaviour
{
    public CupSize cupSize;
    public List<DrinkType> drinks = new List<DrinkType>();

    public Transform liquidContainer; // Donde se instancian los l�quidos visuales
    public GameObject liquidPrefab;

    private int MaxBebidas => cupSize switch
    {
        CupSize.Peque�o => 1,
        CupSize.Mediano => 2,
        CupSize.Grande => 3,
        _ => 2
    };

    public void Llenar(DrinkType bebida)
    {
        if (drinks.Count >= MaxBebidas)
        {
            Debug.Log($"El vaso {cupSize} ya est� lleno.");
            return;
        }

        drinks.Add(bebida);
        Debug.Log($"Agregado: {bebida} al vaso {cupSize}");

        // Crear visual de l�quido
        CrearLiquidoVisual(bebida, drinks.Count);
    }

    private void CrearLiquidoVisual(DrinkType bebida, int index)
    {
        GameObject liquido = Instantiate(liquidPrefab, liquidContainer);
        Color color = ObtenerColorPorBebida(bebida);
        liquido.GetComponent<Renderer>().material.color = color;

        float altura = 0.05f * index;
        liquido.transform.localPosition = new Vector3(0, altura, 0);
        liquido.transform.localScale = new Vector3(1, 0.05f, 1); // ancho fijo, altura peque�a
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
