using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public LayerMask beverageLayer;
    public LayerMask clientLayer;

    public TMP_Text scoreText;
    public TMP_Text requestText;
    public GameObject cliente;

    private string selectedDrink = null;
    private string currentRequest;
    private int score = 0;

    void Start()
    {
        NuevaPeticion();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 100f, beverageLayer))
            {
                Beverage drinkType = hit.collider.GetComponent<Beverage>();
                if (drinkType != null)
                {
                   selectedDrink = drinkType.drinkType.ToString();
                }
            }
            else if (Physics.Raycast(ray, out hit, 100f, clientLayer))
            {
                EntregarBebida();
            }
        }
    }

    void EntregarBebida()
    {
        if (selectedDrink == null) return;

        if (selectedDrink == currentRequest)
        {
            score += 100;
            Debug.Log("Si");
        }
        else
        {
            score -= 10;
            Debug.Log("No");
        }

        scoreText.text = "Puntaje: " + score;
        selectedDrink = null;
        NuevaPeticion();
    }

    void NuevaPeticion()
    {
        string[] opciones = { "Agua", "Jugo", "Cerveza", "Gaseosa" };
        currentRequest = opciones[Random.Range(0, opciones.Length)];
        requestText.text = "El cliente quiere: " + currentRequest;

        cliente.GetComponent<NPCRequest>().requestedItem= currentRequest;
    }
}