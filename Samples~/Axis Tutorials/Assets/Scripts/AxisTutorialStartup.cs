using UnityEngine;
using UnityEditor;
using System.Linq;
#if UNITY_EDITOR
using Unity.Tutorials.Core.Editor;
#endif
public class AxisTutorialStartup: ScriptableObject
{
#if UNITY_EDITOR
    public bool firstTime = false;
    public static AxisTutorialStartup Instance
    {
        get
        {
            if (s_Instance == null)
            {
                var guids = AssetDatabase.FindAssets("t:" + typeof(AxisTutorialStartup).Name);
                if (!guids.Any())
                {
                    s_Instance = CreateInstance<AxisTutorialStartup>();
                } else
                {
                    Debug.Log("There is something");
                    string path = AssetDatabase.GUIDToAssetPath(guids[0]);
                    s_Instance = AssetDatabase.LoadAssetAtPath<AxisTutorialStartup>(path);
                }
            }

            return s_Instance;
        }
        set
        {
            s_Instance = value;
        }
    }



    static AxisTutorialStartup s_Instance;
    public TutorialWelcomePage welcomePage;

    [InitializeOnLoadMethod]
    static void Startup()
    {
        
        //Debug.Log("Starting up Scriptable!");
        //if(Instance.firstTime == true)
        //{
        //    //TutorialWindow.ShowWindow();
        //    //Instance.Load();
        //    Instance.Load();
        //    Instance.firstTime = false;
        //} 

    }

    public void ShowViewTutorialLaterPopup()
    {
        EditorUtility.DisplayDialog("Check the tutorials later!", "If you feel lost, go check the tutorials! \n\nIn the toolbar: Tutorials > Show Tutorials", "Ok");
        
    }

    private void Load()
    {
        TutorialModalWindow.Instance.Close();
        TutorialModalWindow.Show(welcomePage);
        //Debug.Log("Loading!");
    }
#endif
}
