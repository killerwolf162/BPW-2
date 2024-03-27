using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public void load_intro_level()
    {
        SceneManager.LoadScene("Intro_Level");
    }

    public void load_level_1()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void load_game_over_screen()
    {
        SceneManager.LoadScene("Game_Over", LoadSceneMode.Additive);
    }

    public void load_win_screen()
    {
        SceneManager.LoadScene("Win_Scene", LoadSceneMode.Additive);
    }

    public void load_main_menu()
    {
        SceneManager.LoadScene("Main_Menu");
    }

    public void quit_game()
    {
        Application.Quit();
    }
}
