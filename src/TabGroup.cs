using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TabGroup : MonoBehaviour
{
    [SerializeField] private List<GameObject> pages;

    [SerializeField] private Sprite tabIdle;
    [SerializeField] private Sprite tabHover;
    [SerializeField] private Sprite tabActive;

    [SerializeField] private List<TabButton> tabButtons;
    private TabButton selectedTabButton;

    private void Start()
    {
        if (tabButtons.Count > 0 )
        {
            tabButtons = tabButtons.OrderBy(b => b.transform.GetSiblingIndex()).ToList();

            // default selected tab is the first child
            OnTabSelected(tabButtons[0]);
        }
    }

    public void Subscribe(TabButton tabButton)
    {
        if (tabButtons == null) tabButtons = new List<TabButton>();

        tabButtons.Add(tabButton);
    }

    public void OnTabEnter(TabButton tabButton)
    {
        ResetTabButtons();
        if (selectedTabButton == null || tabButton != selectedTabButton) 
            tabButton.background.sprite = tabHover;
    }

    public void OnTabExit(TabButton tabButton)
    {
        ResetTabButtons();
    }

    public void OnTabSelected(TabButton tabButton)
    {
        if (selectedTabButton != null) selectedTabButton.Deselect();
        selectedTabButton = tabButton;

        selectedTabButton.Select();
        selectedTabButton.background.sprite = tabActive;

        ResetTabButtons();

        int index = tabButton.transform.GetSiblingIndex();
        for (int i = 0; i < tabButtons.Count; i++)
        {
            if (i == index) pages[i].SetActive(true);
            else pages[i].SetActive(false);
        }
    }

    public void ResetTabButtons()
    {
        foreach (TabButton button in tabButtons)
        {
            if (selectedTabButton != null && button == selectedTabButton) continue;
            button.background.sprite = tabIdle ?? button.background.sprite;
        }
    }
}
