using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
#   1 - Criar um Prefab de um "3D Text" e adicionar esse script a ele
#   2 - Colocar o Prefab no objeto que vai ter o nome mostrado
#   Vinicius Rezendrix - Brasil
#   06/06/2012
#
#   1 - Create a 3Dtext prefab and add this script to it;
#   2 - Put the prefab under the object who will have the name displayed
#   Vinicius Rezendrix - Brazil
#   06/06/2012
# 
#   https://forum.unity.com/threads/name-above-object-in-c.142935/
*/

public class DisplayVisitorName : MonoBehaviour
{
    public Transform target;
    private string textToDisplay;

    public bool displayName = true;
    public bool displayTAG = false;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        nameDisplayer();
        tagDisplayer();
    }

    void LateUpdate()
    {
        //Make the text allways face the camera
        transform.rotation = Camera.main.transform.rotation;
    }

    //displays the name of the parent
    void nameDisplayer()
    {
        if (displayName)
        {
            displayTAG = false;
            textToDisplay = (string)this.transform.parent.name;
            //changes the text to the Name
            changeTextColor();
        }
    }

    //displays the TAG of the parent
    void tagDisplayer()
    {
        if (displayTAG)
        {
            displayName = false;
            //changes the text to the TAG
            textToDisplay = (string)this.transform.parent.tag;
            changeTextColor();
        }
    }

    /* # Exibe o texto armazenado na variavel publica "textToDisplay",
       # possibilitando que outros scripts alterem a variavel e, consequentemente, o texto em si */

    //Changes the color
    public void changeTextColor()
    {

        //Enemy = red
        if (this.transform.parent.tag == "Enemy")
        {
            GetComponent<Renderer>().material.color = Color.red;
        }

        //Player = Green
        if (this.transform.parent.tag == "Player")
        {
            GetComponent<Renderer>().material.color = Color.green;
        }

        //Neutral = yellow
        if (this.transform.parent.tag == "Neutral")
        {
            GetComponent<Renderer>().material.color = Color.yellow;
        }

        // Access the TextMesh component and change it for "textToDisplay" value
        // Modo de acessar o component TextMesh do Texto3d e mudá-lo para "textToDisplay"  
        TextMesh tm = GetComponent<TextMesh>();
        tm.text = textToDisplay;
    }
}


