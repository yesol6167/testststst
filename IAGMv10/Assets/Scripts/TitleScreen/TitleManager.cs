using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class TitleManager : MonoBehaviour
{
    public void OnNewGame()
    {
        string path = Path.Combine(Application.dataPath, "Load", "Load.bin");
        if (File.Exists(path))
        {
            File.Delete(Path.Combine(path));
        }
        SceneManager.LoadScene("Main");
    }
}
