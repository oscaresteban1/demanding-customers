using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Glass : MonoBehaviour
{
    public static Glass glass;
    public Text whatGlassHave;
    public int[] whatGlassHaveint;


    void Awake()
    {
        glass = this;

        whatGlassHave.text = "Nothing";
        whatGlassHaveint = new int[4] { 0, 0, 0, 0 };
    }

    // Called to fill the glass with new ingredients
    public void UpdateGlass(string ingredient, int objectid)
    {
        if (whatGlassHave.text == "Nothing")
        {
            whatGlassHave.text = ingredient;
        }
        else if (whatGlassHave.text.IndexOf(ingredient) == -1)
        {
            // Show the new ingredient
            whatGlassHave.text = whatGlassHave.text + ", " + ingredient;
        }

        whatGlassHaveint[objectid] = 1;
    }
    public void EmptyGlass()
    {
        whatGlassHave.text = "Nothing";

        for (int j = 0; j<whatGlassHaveint.Length; j++)
        {
            whatGlassHaveint[j] = 0;
        }

    }
}