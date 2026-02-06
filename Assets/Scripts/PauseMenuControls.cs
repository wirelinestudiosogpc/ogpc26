using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEditor;

public class PauseMenuControls : MonoBehaviour
{

    public TMP_Dropdown resSelect;
    public Toggle fsToggle;

    public Toggle compassRealistic;

    Resolution[] resolutions;
    bool isFullscreen;
    int selectedResolution;
    List<Resolution> selectedResolutionList = new List<Resolution>();




    public Slider frSlider;
    public TextMeshProUGUI frAmount;



    public Slider Sensitivity;
    public TextMeshProUGUI SensitvityShow;
    public GameObject Sens;

    public GameObject mainCanvas;
    public GameObject pauseCanvas;
    public GameObject settingsCanvas;


    void Start()
    {
        if (Application.targetFrameRate == -1 || Application.targetFrameRate > 240)
        {
            frSlider.value = 241;
        }
        else
        {
            frSlider.value = Application.targetFrameRate;
        }

        if (frSlider.value > 240)
        {
            frAmount.text = "FPS Limit: None";
            Application.targetFrameRate = -1;
        }
        else
        {
            frAmount.text = "FPS Limit: " + frSlider.value.ToString();
            Application.targetFrameRate = (int)frSlider.value;
        }


        if (GameObject.FindGameObjectsWithTag("Sens").Length == 0)
        {
            Instantiate<GameObject>(Sens);
            Sensitivity.value = 1.75558f;//GameObject.FindGameObjectWithTag("Sens").GetComponent<SensitivityValue>().cameraSens;
        }
        else
        {
            Sensitivity.value = GameObject.FindGameObjectWithTag("Sens").GetComponent<SensitivityValue>().cameraSens;
            compassRealistic.isOn = GameObject.FindGameObjectWithTag("Sens").GetComponent<SensitivityValue>().compass;
        }

        if (GameObject.FindGameObjectsWithTag("Sens").Length != 0)
        {
            GameObject.FindGameObjectWithTag("Sens").GetComponent<SensitivityValue>().cameraSens = Sensitivity.value;
            GameObject.FindGameObjectWithTag("Sens").GetComponent<SensitivityValue>().compass = compassRealistic.isOn;

            var percent = ((Sensitivity.value - 0.5f) / (3f - 0.5f)) * 100;
            SensitvityShow.text = "Sensitivity: " + ((int)percent).ToString();
        }


        isFullscreen = Screen.fullScreen;
        resolutions = Screen.resolutions;

        List<string> resolutionList = new List<string>();
        string newRes;
        foreach (Resolution res in resolutions)
        {
            newRes = res.width.ToString() + " x " + res.height.ToString();
            if (!resolutionList.Contains(newRes))
            {
                resolutionList.Add(newRes);
                selectedResolutionList.Add(res);
            }

        }

        resSelect.AddOptions(resolutionList);
    }



    public void ChangeResolution()
    {
        selectedResolution = resSelect.value;
        Screen.SetResolution(selectedResolutionList[selectedResolution].width, selectedResolutionList[selectedResolution].height, isFullscreen);
    }

    public void ChangeFullScreen()
    {
        isFullscreen = fsToggle.isOn;
        Screen.SetResolution(selectedResolutionList[selectedResolution].width, selectedResolutionList[selectedResolution].height, isFullscreen);
    }

    public void ChangeFramerate()
    {
        if (frSlider.value > 240)
        {
            frAmount.text = "FPS Limit: None";
            Application.targetFrameRate = -1;
        }
        else
        {
            frAmount.text = "FPS Limit: " + frSlider.value.ToString();
            Application.targetFrameRate = (int)frSlider.value;
        }
    }

    public void ChangeSensitivity()
    {
        if (GameObject.FindGameObjectsWithTag("Sens").Length != 0)
        {
            GameObject.FindGameObjectWithTag("Sens").GetComponent<SensitivityValue>().cameraSens = Sensitivity.value;

            var percent = ((Sensitivity.value - 0.5f) / (3f - 0.5f)) * 100;
            SensitvityShow.text = "Sensitivity: " + ((int)percent).ToString();
        }
    }
    public void ChangeCompass()
    {
        if (GameObject.FindGameObjectsWithTag("Sens").Length != 0)
        {
            GameObject.FindGameObjectWithTag("Sens").GetComponent<SensitivityValue>().compass = compassRealistic.isOn;

            
        }
    }

    public void ResumeGame()
    {
        mainCanvas.SetActive(true);
        pauseCanvas.SetActive(false);
        settingsCanvas.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void QuitGame()
    {
        SceneManager.LoadScene(0);
    }

    public void OpenSettings()
    {
        pauseCanvas.SetActive(false);
        settingsCanvas.SetActive(true);
    }
    public void CloseSettings()
    {
        settingsCanvas.SetActive(false);
        pauseCanvas.SetActive(true);
    }
}
