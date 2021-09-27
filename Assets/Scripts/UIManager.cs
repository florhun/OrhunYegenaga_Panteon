using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    public GameObject prepare, task1, task2, task3;

    public void Prepare()
    {
        prepare.SetActive(true);
        task1.SetActive(false);
        task2.SetActive(false);
        task3.SetActive(false);
    }
    public void Task1()
    {
        prepare.SetActive(false);
        task1.SetActive(true);
        task2.SetActive(false);
        task3.SetActive(false);
    }
    
    public void Task2()
    {
        prepare.SetActive(false);
        task1.SetActive(false);
        task2.SetActive(true);
        task3.SetActive(false);
    }
    
    public void Task3()
    {
        prepare.SetActive(false);
        task1.SetActive(false);
        task2.SetActive(false);
        task3.SetActive(true);
    }
}
