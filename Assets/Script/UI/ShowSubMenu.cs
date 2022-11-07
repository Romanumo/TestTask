using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSubMenu : MonoBehaviour
{
    public void OpenSubMenu(GameObject subMenu)
    {
        this.gameObject.SetActive(false);
        subMenu.SetActive(true);
    }
}
