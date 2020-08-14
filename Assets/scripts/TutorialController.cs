using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    public GameObject menuTutorial;
    public GameObject menuExplore;
    public GameObject menuInfo;
    public GameObject menuSettings;
    public GameObject[] tutorialSteps;
    public GameObject uiController;

    void Start()
    {
        for (int i = 0; i < tutorialSteps.Length; i++)
        {
            if (i < 1)
            {
                tutorialSteps[i].SetActive(true);
            }
            else
            {
                tutorialSteps[i].SetActive(false);
            }
        }

        setView(tutorialSteps[0], tutorialSteps[0]);
    }

    // go to next tutorial screen
    public void next()
    {
        int index = getActiveStep();
        setStep(index, index + 1);
    }

    // go to previous tutorial screen
    public void back()
    {
        int index = getActiveStep();
        setStep(index, index-1);
    }

    // react to click on info button if current step is step 6
    public void handleInfoClick()
    {
        int triggeredStep = 6;

        if (getActiveStep() == triggeredStep)
        {
            next();
        }
    }

    // react to click on home button if triggered step is step 7
    public void handleHomeClick()
    {
        int triggeredStep = 7;

        if (getActiveStep() == triggeredStep)
        {
            next();
        }
    }

    // show needed ui elements and play sound depending on current step
    private void setView(GameObject nextStep, GameObject actualStep)
    {
        actualStep.gameObject.SetActive(false);
        nextStep.gameObject.SetActive(true);

        switch (nextStep.name)
        {
            case "Tutorial-Step-1":
                menuTutorial.transform.Find("Button-Back").gameObject.SetActive(false);
                menuExplore.gameObject.SetActive(false);
                FindObjectOfType<AudioManager>().Play("tutorial-1");
                break;

            case "Tutorial-Step-2":
                menuTutorial.transform.Find("Button-Back").gameObject.SetActive(false);
                menuTutorial.transform.Find("Button-Next").gameObject.SetActive(false);
                menuExplore.gameObject.SetActive(false);
                FindObjectOfType<AudioManager>().Stop("tutorial-1");
                FindObjectOfType<AudioManager>().Play("tutorial-2");
                break;

            case "Tutorial-Step-3":
                menuTutorial.transform.Find("Button-Back").gameObject.SetActive(false);
                menuTutorial.transform.Find("Button-Next").gameObject.SetActive(true);
                FindObjectOfType<AudioManager>().Stop("tutorial-2");
                FindObjectOfType<AudioManager>().Play("tutorial-3");
                menuExplore.gameObject.SetActive(false);
                break;

            case "Tutorial-Step-4":
                menuTutorial.transform.Find("Button-Back").gameObject.SetActive(false);
                menuTutorial.transform.Find("Button-Next").gameObject.SetActive(true);

                menuExplore.transform.Find("Panel/Menu-Bottom/Button-Info").gameObject.SetActive(false);
                menuExplore.transform.Find("Panel/Menu-Bottom/Button-Home").gameObject.SetActive(false);
                menuExplore.transform.Find("Panel/Menu-Top/Ingredients-Bar").gameObject.SetActive(false);
                menuExplore.transform.Find("Panel/Menu-Top/Ingredients-Bar").gameObject.SetActive(false);
                menuExplore.transform.Find("Panel/Menu-Top/Icon-Star").gameObject.SetActive(true);
                FindObjectOfType<AudioManager>().Stop("tutorial-3");
                FindObjectOfType<AudioManager>().Play("tutorial-4");
                menuExplore.gameObject.SetActive(true);
                break;

            case "Tutorial-Step-5":
                menuTutorial.transform.Find("Button-Back").gameObject.SetActive(false);
                menuTutorial.transform.Find("Button-Next").gameObject.SetActive(false);

                FindObjectOfType<AudioManager>().Stop("tutorial-4");
                FindObjectOfType<AudioManager>().Play("tutorial-5");
                break;

            case "Tutorial-Step-6":
                menuTutorial.transform.Find("Button-Back").gameObject.SetActive(false);
                menuTutorial.transform.Find("Button-Next").gameObject.SetActive(true);

                menuExplore.transform.Find("Panel/Menu-Top/Ingredients-Bar").gameObject.SetActive(true);
                menuExplore.transform.Find("Panel/Menu-Top/Icon-Star").gameObject.SetActive(true);
                menuExplore.transform.Find("Panel/Menu-Bottom/Button-Info").gameObject.SetActive(false);
                menuExplore.transform.Find("Panel/Menu-Bottom/Button-Home").gameObject.SetActive(false);
                menuExplore.gameObject.SetActive(true);
                FindObjectOfType<AudioManager>().Stop("tutorial-5");
                FindObjectOfType<AudioManager>().Play("tutorial-6");
                break;

            case "Tutorial-Step-7":
                menuTutorial.gameObject.SetActive(false);
                menuTutorial.transform.Find("Button-Back").gameObject.SetActive(false);
                menuTutorial.transform.Find("Button-Next").gameObject.SetActive(false);

                menuExplore.transform.Find("Panel/Menu-Top/Ingredients-Bar").gameObject.SetActive(true);
                menuExplore.transform.Find("Panel/Menu-Top/Icon-Star").gameObject.SetActive(true);
                menuExplore.transform.Find("Panel/Menu-Bottom/Button-Info").gameObject.SetActive(true);
                menuExplore.transform.Find("Panel/Menu-Bottom/Button-Home").gameObject.SetActive(false);
                FindObjectOfType<AudioManager>().Stop("tutorial-6");
                FindObjectOfType<AudioManager>().Play("tutorial-7");
                menuExplore.gameObject.SetActive(true);
                break;

            case "Tutorial-Step-8":
                menuTutorial.transform.Find("Button-Back").gameObject.SetActive(false);
                menuTutorial.transform.Find("Button-Next").gameObject.SetActive(true);
                menuTutorial.gameObject.SetActive(true);

                menuExplore.transform.Find("Panel/Menu-Top/Ingredients-Bar").gameObject.SetActive(true);
                menuExplore.transform.Find("Panel/Menu-Top/Icon-Star").gameObject.SetActive(true);
                menuExplore.transform.Find("Panel/Menu-Bottom/Button-Info").gameObject.SetActive(true);
                menuExplore.transform.Find("Panel/Menu-Bottom/Button-Home").gameObject.SetActive(false);
                menuInfo.transform.Find("Panel-Menu-Portrait/Menu-Area/Panel-Background-Portrait/Button-Close").gameObject.GetComponent<Button>().interactable = false;
                menuExplore.gameObject.SetActive(true);
                FindObjectOfType<AudioManager>().Stop("tutorial-7");
                FindObjectOfType<AudioManager>().Play("tutorial-8");
                break;

            case "Tutorial-Step-9":
                uiController.GetComponent<AnimationController>().TogglePanel(menuInfo.gameObject);
                menuTutorial.transform.Find("Button-Back").gameObject.SetActive(false);
                menuTutorial.transform.Find("Button-Next").gameObject.SetActive(false);
                menuTutorial.gameObject.SetActive(true);

                menuExplore.transform.Find("Panel/Menu-Top/Ingredients-Bar").gameObject.SetActive(true);
                menuExplore.transform.Find("Panel/Menu-Top/Icon-Star").gameObject.SetActive(true);
                menuExplore.transform.Find("Panel/Menu-Bottom/Button-Info").gameObject.SetActive(true);
                menuExplore.transform.Find("Panel/Menu-Bottom/Button-Home").gameObject.SetActive(true);
                menuExplore.transform.Find("Panel/Menu-Bottom/Button-Home").gameObject.GetComponent<Button>().interactable = false;
                menuExplore.transform.Find("Panel/Menu-Bottom/Button-Info").gameObject.GetComponent<Button>().interactable = false;
                menuExplore.gameObject.SetActive(true);
                FindObjectOfType<AudioManager>().Stop("tutorial-8");
                FindObjectOfType<AudioManager>().Play("tutorial-9");
                break;
        }
    }

    private void setStep(int actualIndex, int newIndex)
    {
        tutorialSteps[actualIndex].gameObject.SetActive(true);
        tutorialSteps[newIndex].gameObject.SetActive(true);
        setView(tutorialSteps[newIndex], tutorialSteps[actualIndex]);
    }

    private int getActiveStep()
    {
        int i = 0;
        while(i < tutorialSteps.Length && !tutorialSteps[i].gameObject.activeSelf)
        {
            i++;
        }
        return i;
    }
}
