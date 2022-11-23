using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    public List<TabButton> tabButtons;

    public Color tabIdle;
    public Color tabHover;
    public Color tabActive;

    public TabButton selectedTab;
    public List<GameObject> objectToSwap;

    public void Subscribe(TabButton button)
    {
        if(tabButtons == null)
        {
            tabButtons = new List<TabButton>();
        }

        tabButtons.Add(button);
    }

    public void OnTabEnter(TabButton button)
    {
        ResetTabs();
        if(selectedTab == null || button != selectedTab)
        {
            button.background.color = tabHover;
        }
    }

    public void OnTabExit(TabButton button)
    {
        ResetTabs();
         
    }

    public void OnTabSelected(TabButton button)
    {
        if(selectedTab != null)
        {
            selectedTab.Deselect();
        }


        selectedTab = button;

        selectedTab.Select();

        ResetTabs();
        button.background.color = tabActive;
        int index = button.transform.GetSiblingIndex();
        for (int i = 0; i < objectToSwap.Count; i++)
        {
            if (i == index)
            {
                objectToSwap[i].gameObject.SetActive(true);
            }
            else
            {
                objectToSwap[i].gameObject.SetActive(false);
            }
        }
    }

    public void ResetTabs()
    {
        foreach(TabButton button in tabButtons)
        {
            if(selectedTab != null && button == selectedTab) { continue; }
            button.background.color = tabIdle;
        }
    }
}
