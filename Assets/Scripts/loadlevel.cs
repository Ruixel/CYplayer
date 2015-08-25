using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class loadlevel : MonoBehaviour
{

    public InputField input;

    public void doLoad()
    {
        int id;
        Int32.TryParse(input.text, out id);

        PlayerPrefs.SetInt("levelID", id);
        Application.LoadLevel(1);
    }

}
