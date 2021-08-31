using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUIManager : MonoBehaviour
{
    public GameObject tutorialText, areYouSurePanel, tutorialPlusPanel;
    public GameObject controlsPanel, uiPanel, secondaryWeaponsPanel, itemsPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HidePanel(GameObject panel)
    {
        panel.SetActive(false);
    }
    public void ShowPanel(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void HideAllPanels()
    {
        tutorialText.SetActive(false);
        areYouSurePanel.SetActive(false);
        tutorialPlusPanel.SetActive(false);
        controlsPanel.SetActive(false);
        uiPanel.SetActive(false);
        secondaryWeaponsPanel.SetActive(false);
        itemsPanel.SetActive(false);
    }
}
