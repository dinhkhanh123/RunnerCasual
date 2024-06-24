using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerSelector : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Runner runner;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectRunner(int runnerIndex) 
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if(i == runnerIndex)
            {
                transform.GetChild(i).gameObject.SetActive(true);  
                runner.SetAnimator(transform.GetChild(i).GetComponent<Animator>());
            }
            else
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
