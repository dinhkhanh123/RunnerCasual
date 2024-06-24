using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelector : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private Transform runnerParent;
    [SerializeField] private RunnerSelector runnerSelectPrefab;


    void Start()
    {
        ShopManager.onSkinSelected += SelectSkin;
    }

    private void OnDestroy()
    {
        ShopManager.onSkinSelected -= SelectSkin;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
            SelectSkin(Random.Range(0, 3));
    }

    public void SelectSkin(int skinIndex)
    {
        for (int i = 0; i < runnerParent.childCount; i++)
        runnerParent.GetChild(i).GetComponent<RunnerSelector>().SelectRunner(skinIndex);

        runnerSelectPrefab.SelectRunner(skinIndex);  
    }
}
