using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum MenuState
{
    PressAnyKey = 0,
    MainMenu,
    OptionsMenu,
    Transition
}

public class TitleScreen : MonoBehaviour {

    MenuState state;
    float time = 0f;
    [SerializeField] GameObject startText;
    [SerializeField] GameObject titleText;
    [SerializeField] GameObject menuList;
    [SerializeField] Transform canvasCenter;
    [SerializeField] Text[] menuListText;
    [SerializeField] Transform titleTextStartPosition;
    [SerializeField] Transform titleTextEndPosition;
    [SerializeField] AnimationCurve titleTextMoveCurve;

    int menuListSize;
    int currentIndex;

    Color transparent = new Color(1f, 1f, 1f, 0f);

    void Start () {
        state = MenuState.PressAnyKey;
        menuListSize = 0;
        currentIndex = 0;
	}

    void SetListText(params string[] text)
    {
        menuListSize = text.Length;
        for (int i = 0; i < menuListText.Length; i++)
        {
            if (i < menuListSize)
            {
                menuListText[i].text = text[i];
            }
            else
            {
                menuListText[i].text = "";
            }
        }
    }

    IEnumerator ChangeMenu(MenuState newState)
    {
        if (state == newState)
            yield return 0;

        switch (newState)
        {
            case MenuState.MainMenu:
                state = MenuState.Transition;
                yield return MoveCurrentMenu(1, true);
                SetListText("Start", "Options", "Exit");
                yield return MoveCurrentMenu(-1, false);
                currentIndex = 0;
                state = MenuState.MainMenu;
                break;

            case MenuState.OptionsMenu:
                state = MenuState.Transition;
                yield return MoveCurrentMenu(-1, true);
                SetListText("Hi", "Hello");
                yield return MoveCurrentMenu(1, false);
                currentIndex = 0;
                state = MenuState.OptionsMenu;
                break;
        }
    }

    IEnumerator MoveCurrentMenu(float side, bool fadeOut)
    {
        Vector3 startPosition = canvasCenter.position;
        Vector3 newPosition = startPosition + new Vector3(side * 1 * 100f, 0f, 0f);
        for (float i = 0f; i <= 1f; i += Time.deltaTime * 5f, i = Mathf.Min(1f, i))
        {
            float t = (fadeOut) ? i : (1f - i);
            menuList.transform.position = Vector3.Lerp(startPosition, newPosition, t);
            Color newColor = Color.Lerp(Color.white, transparent, t);
            foreach(Text text in menuList.GetComponentsInChildren<Text>())
            {
                text.color = newColor;
            }
            if (i == 1)
                break;
            yield return 0;
        }
    }

    IEnumerator TransitionToMainMenu()
    {
        yield return DeletePressAnyKeyText();
        yield return MoveUpTitle();
        SetListText("Start", "Options", "Exit");
        yield return FadeInMenuList();
        state = MenuState.MainMenu;
    }

    IEnumerator FadeInMenuList()
    {
        for (float i = 0; i < 1; i += Time.deltaTime * 8f)
        {
            Color newColor = Color.Lerp(transparent, Color.white, i);
            foreach(Text t in menuListText)
            {
                t.color = newColor;
                yield return 0;
            }
        }
    }

    IEnumerator DeletePressAnyKeyText()
    {
        Vector3 startScale = startText.transform.localScale;
        for (float i = 0; i < 1f; i += Time.deltaTime * 5f)
        {
            startText.transform.localScale = startScale + Vector3.one * (i);
            Color newColor = Color.Lerp(Color.white, transparent, i);
            startText.GetComponent<Text>().color = newColor;
            yield return 0;
        }
        startText.SetActive(false);
        yield return new WaitForSeconds(0.2f);
    }

    IEnumerator MoveUpTitle()
    {
        for (float i = 0f; i < 1f; i += Time.deltaTime * 2f)
        {
            float t = titleTextMoveCurve.Evaluate(i);
            titleText.transform.position = Vector3.Lerp(titleTextStartPosition.position, titleTextEndPosition.position, t);
            yield return 0;
        }
    }

    void MovePressAnyKeyTextInAndOut()
    {
        time += Time.deltaTime;
        float scale = Mathf.Cos(time * 4f);
        startText.transform.localScale = Vector3.one * (scale * 0.1f + 1f);
    }

    void ScaleTextUp(Text text)
    {
        Vector3 currentScale = text.gameObject.transform.localScale;
        Vector3 newScale = text.gameObject.transform.localScale;
        if (currentScale.x < 1.5f)
        {
            newScale = currentScale + Time.deltaTime * 5f * Vector3.one;
        }
        if (newScale.x > 1.5f)
        {
            newScale = 1.5f * Vector3.one;
        }
        text.gameObject.transform.localScale = newScale;
    }

    void ScaleTextDown(Text text)
    {
        Vector3 currentScale = text.gameObject.transform.localScale;
        Vector3 newScale = text.gameObject.transform.localScale;
        if (currentScale.x > 1f)
        {
            newScale = currentScale + Time.deltaTime * -5f * Vector3.one;
        }
        if (newScale.x < 1f)
        {
            newScale = 1f * Vector3.one;
        }
        text.gameObject.transform.localScale = newScale;
    }

    void SetTextColorFromScale(Text text)
    {
        float i = (text.gameObject.transform.localScale.x - 1f) * 2f;
        text.color = Color.Lerp(Color.white, Color.yellow, i);
    }
    void MainMenuConfirm()
    {
        if (menuListText[currentIndex].text == "Start")
        {

        }
        else if (menuListText[currentIndex].text == "Options")
        {
            StartCoroutine(ChangeMenu(MenuState.OptionsMenu));
        }
    }

    void CheckForUpAndDownArrows()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            currentIndex--;
            currentIndex = (currentIndex < 0) ? currentIndex + menuListSize : currentIndex;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            currentIndex++;
            currentIndex = (currentIndex >= menuListSize) ? currentIndex - menuListSize : currentIndex;
        }
    }

    void ScaleTextAndSetColors()
    {
        for (int i = 0; i < menuListSize; i++)
        {
            if (i == currentIndex)
            {
                ScaleTextUp(menuListText[i]);
            }
            else
            {
                ScaleTextDown(menuListText[i]);
            }
            SetTextColorFromScale(menuListText[i]);
        }
    }

    void Update () {
        //Waiting for the user to press any key
        if (state == MenuState.PressAnyKey)
        {
            if (Input.anyKeyDown)
            {
                state = MenuState.Transition;
                StartCoroutine(TransitionToMainMenu());
            }

            MovePressAnyKeyTextInAndOut();
        }

        //At main menu
        else if (state == MenuState.MainMenu)
        {
            //Check for up and down arrow input
            CheckForUpAndDownArrows();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                MainMenuConfirm();
            }

            //Set the colors
            ScaleTextAndSetColors();
        }

        //At options menu
        else if (state == MenuState.OptionsMenu)
        {
            CheckForUpAndDownArrows();
            ScaleTextAndSetColors();

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                StartCoroutine(ChangeMenu(MenuState.MainMenu));
            }
        }
	}
}
