using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager gamemanager;

    private int numCustomers = 2;
    private int attendedCustomers = 0;
    private int correctPoints = 0;
    private float timeToShowOrder = 3;
    private float timeToDeleteCustomers = 2;
    public int[] orders;
    public bool inside;

    public Text attendedCustomersText;
    public Text correctPointsText;
    public Text order;
    public GameObject finalTextCanvas;
    public GameObject customer;

    private GameObject currentCustomer;

    void Awake()
    {
        gamemanager = this;
        orders = new int[4] { 0, 0, 0, 0 };
        attendedCustomersText.text = "Attended: 0/" + numCustomers.ToString();

        // create first order
        StartCoroutine(CreateOrder());

    }


    // drink colors
    // 0 ice - black
    // 1 lemon - blue
    // 2 orange juice - yellow
    // 3 strawberry juice - brown

    public IEnumerator CreateOrder()
    {
        // generate random order and show it on screen

        currentCustomer = Instantiate(customer, customer.transform.position, Quaternion.identity);
        inside = true;

        yield return new WaitForSeconds(timeToShowOrder);

        // 0 or 1 
        int orange = Random.Range(0, 2);
        int strawberry = 1 - orange;

        //optional drinks
        int option1 = Random.Range(0, 2);
        int option2 = Random.Range(0, 2);

        // orders array
        orders[0] = option1;
        orders[1] = option2;
        orders[2] = orange;
        orders[3] = strawberry;


        // text generation
        if (orange == 1)
        {
            order.text = order.text + " Olmeca(Yellow)";
        }
        else
        {
            order.text = order.text + " Jack Daniel's(Brown)";
        }

        if (option1 == 1)
        {
            order.text = order.text + ", Baileys(Black)";
        }
        if (option2 == 1)
        {
            order.text = order.text + ", Curacao(Blue)";
        }
    }

    public IEnumerator CorrectOrder()
    {
        // if the glass contents match the order

        attendedCustomers++;
        correctPoints++;
        attendedCustomersText.text = "Attended: " + attendedCustomers.ToString() + "/" + numCustomers.ToString();
        correctPointsText.text = "Points: " + correctPoints.ToString();

        Animator animator = currentCustomer.GetComponent<Animator>();
        animator.Play("GoHome");
        inside = false;

        order.text = "Order: ";

        yield return new WaitForSeconds(timeToDeleteCustomers);

        if (attendedCustomers < numCustomers)
        {
            Glass.glass.EmptyGlass();
            StartCoroutine(CreateOrder());

        }
        else
        {
            // finish game
            Glass.glass.EmptyGlass();
            int final = Mathf.RoundToInt(100*correctPoints/numCustomers);
            Text ff = finalTextCanvas.GetComponentInChildren<Text>();
            ff.text = ff.text + final.ToString() + "%";
            finalTextCanvas.SetActive(true);
        }

    }

    public IEnumerator IncorrectOrder()
    {
        // another customer arrives but no points are added

        attendedCustomers++;
        attendedCustomersText.text = "Attended: " + attendedCustomers.ToString() + "/" + numCustomers.ToString();

        Animator animator = currentCustomer.GetComponent<Animator>();
        animator.Play("GoHome");
        inside = false;

        order.text = "Order: ";
        Glass.glass.EmptyGlass();

        yield return new WaitForSeconds(timeToDeleteCustomers);

        if (attendedCustomers < numCustomers)
        {
            Glass.glass.EmptyGlass();
            StartCoroutine(CreateOrder());

        }
        else
        {
            // finish game
            Glass.glass.EmptyGlass();
            int final = Mathf.RoundToInt(100 * correctPoints / numCustomers);
            Text ff = finalTextCanvas.GetComponentInChildren<Text>();
            ff.text = ff.text + final.ToString() + "%";
            finalTextCanvas.SetActive(true);
        }
    }
}