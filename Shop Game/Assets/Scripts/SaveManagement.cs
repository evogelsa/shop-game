using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManagement : MonoBehaviour
{
    public void ResetGame()
    {
        PlayerPrefs.DeleteAll();
    }
}
