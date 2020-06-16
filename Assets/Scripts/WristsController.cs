using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WristsController : MonoBehaviour
{
    private string whatYouHave;

    private bool youHaveSomething = false;

    //Prefabs that we're going to instantiate:
    public GameObject IcePrefab;
    public GameObject LemonPrefab;
    public GameObject OrangeJuicePrefab;
    public GameObject StrawberryJuicePrefab;
    public GameObject GlassPrefab;

    private int objectTaken;
    private Vector3 origialPosition;

    private void Start()
    {
        objectTaken = 0;
        youHaveSomething = false;
        whatYouHave = "";
    }

    private void Update()
    {
        // black
        if (objectTaken == 1)
        {
            GameObject.Find("Black").transform.position = this.transform.position;
        }

        // blue
        if (objectTaken == 2)
        {
            GameObject.Find("Blue").transform.position = this.transform.position;
        }

        // yellow
        if (objectTaken == 3)
        {
            GameObject.Find("Yellow").transform.position = this.transform.position;
        }

        // brown
        if (objectTaken == 4)
        {
            GameObject.Find("Brown").transform.position = this.transform.position;
        }
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Black") && !youHaveSomething)
        {
            whatYouHave = "Black";
            youHaveSomething = true;
            origialPosition = GameObject.Find("Black").transform.position;
            objectTaken = 1;

            SoundManager.PlaySound("grab"); //we reproduce the grab sound
        }
        else if (other.CompareTag("Blue") && !youHaveSomething)
        {
            whatYouHave = "Blue";
            youHaveSomething = true;
            origialPosition = GameObject.Find("Blue").transform.position;
            objectTaken = 2;

            SoundManager.PlaySound("grab");
        }
        else if (other.CompareTag("Yellow") && !youHaveSomething)
        {
            whatYouHave = "Yellow";
            youHaveSomething = true;
            origialPosition = GameObject.Find("Yellow").transform.position;
            objectTaken = 3;

            SoundManager.PlaySound("grab");
        }
        else if (other.CompareTag("Brown") && !youHaveSomething)
        {
            whatYouHave = "Brown";
            youHaveSomething = true;
            origialPosition = GameObject.Find("Brown").transform.position;
            objectTaken = 4;

            SoundManager.PlaySound("grab");
        }
        else if (other.CompareTag("Glass") && youHaveSomething) //in case you enter the glass collider and have something on your hand, then you leave the object in the glass.
        {
            Glass.glass.UpdateGlass(whatYouHave, objectTaken - 1);
            whatYouHave = "Nothing";

            // return black
            if (objectTaken == 1)
            {
                GameObject.Find("Black").transform.position = origialPosition;
            }

            // Return blue
            if (objectTaken == 2)
            {
                GameObject.Find("Blue").transform.position = origialPosition;
            }

            // Return yellow
            if (objectTaken == 3)
            {
                GameObject.Find("Yellow").transform.position = origialPosition;
            }

            // Return brown
            if (objectTaken == 4)
            {
                GameObject.Find("Brown").transform.position = origialPosition;
            }

            youHaveSomething = false;
            objectTaken = 0;

            SoundManager.PlaySound("put");
        }

        else if (other.CompareTag("Accept") && !youHaveSomething && GameManager.gamemanager.inside)
        {
            bool correctOrder = Glass.glass.whatGlassHaveint.SequenceEqual(GameManager.gamemanager.orders);

            if (correctOrder) // if order is correct
            {
                StartCoroutine(GameManager.gamemanager.CorrectOrder());
            }
            else
            {
                StartCoroutine(GameManager.gamemanager.IncorrectOrder());
            }
        }

        // if you have nothing on your hand and redo
        else if (other.CompareTag("Redo") && !youHaveSomething)
        {
            Glass.glass.EmptyGlass();
        }
    }
}