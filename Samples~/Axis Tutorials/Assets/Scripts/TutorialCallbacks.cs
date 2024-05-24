using UnityEngine;
using UnityEditor;
#if UNITY_EDITOR
using Unity.Tutorials.Core.Editor;
#endif
using Axis.Elements;
using System.Collections.Generic;

namespace Axis.Tutorials
{
    /// <summary>
    /// Implement your Tutorial callbacks here.
    /// </summary>
#if UNITY_EDITOR
    [CreateAssetMenu(fileName = DefaultFileName, menuName = "Tutorials/" + DefaultFileName + " Instance")]
#endif
    public class TutorialCallbacks : ScriptableObject
    {
#if UNITY_EDITOR
        /// <summary>
        /// The default file name used to create asset of this class type.
        /// </summary>
        public const string DefaultFileName = "TutorialCallbacks";

        public GameObject rotatingCube;

        /// <summary>
        /// Creates a TutorialCallbacks asset and shows it in the Project window.
        /// </summary>
        /// <param name="assetPath">
        /// A relative path to the project's root. If not provided, the Project window's currently active folder path is used.
        /// </param>
        /// <returns>The created asset</returns>
        /// 

        public string axisSDKEventsPath;
        public static ScriptableObject CreateAndShowAsset(string assetPath = null)
        {
            assetPath = assetPath ?? $"{TutorialEditorUtils.GetActiveFolderPath()}/{DefaultFileName}.asset";
            var asset = CreateInstance<TutorialCallbacks>();
            AssetDatabase.CreateAsset(asset, AssetDatabase.GenerateUniqueAssetPath(assetPath));
            EditorUtility.FocusProjectWindow(); // needed in order to make the selection of newly created asset to really work
            Selection.activeObject = asset;
            return asset;
        }

        /// <summary>
        /// Example callback for basic UnityEvent
        /// </summary>
        public void ExampleMethod()
        {
            Debug.Log("ExampleMethod");
        }



        public void InstantiateRotatingCube()
        {
            DestroyGameObjectByName("Rotating Cube");
            var gameObject = Instantiate(rotatingCube);
            gameObject.name = "Rotating Cube";
            gameObject.hideFlags = HideFlags.HideInHierarchy;
        }

        public Tutorial BodyTrackingTutorial;
        public void StartBodyTrackingTutorial()
        {
            TutorialWindowUtils.StartTutorial(BodyTrackingTutorial);
        }

        public Tutorial CommunicationTutorial;
        public void StartCommunicationTutorial()
        {
            TutorialWindowUtils.StartTutorial(CommunicationTutorial);
        }

        public void SetupBodyTrackingTutorial()
        {
            var axisBrains = FindObjectsOfType<AxisBrain>();
            foreach (var axisBrain in axisBrains)
            {
                DestroyImmediate(axisBrain.gameObject);
            }
            
            var characters = FindObjectsOfType<Animator>();
            foreach (var character in characters)
            {
                DestroyImmediate(character.gameObject);
            }
            
        }
        public void SelectMoveTool()
        {
            Tools.current = Tool.Move;
        }




        public void ExitPlayMode()
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        public void FocusOnProjectView()
        {
            Debug.Log("Focosing on project view");

            UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath(axisSDKEventsPath, typeof(UnityEngine.Object));
            Selection.activeObject = obj;
            Debug.Log(obj.name);
            EditorGUIUtility.PingObject(obj);

            EditorUtility.FocusProjectWindow();
        }

        public void DestroyGameObjectByName(string name)
        {

            GameObject toBeDestroyed = GameObject.Find(name);
            if(toBeDestroyed != null)
            {
                
                DestroyImmediate(toBeDestroyed);
            }
        }

        public void SelectCharacterGameobject()
        {
            Animator[] animatorsOnScene = GameObject.FindObjectsOfType<Animator>();
            GameObject characterGameObject = null;
            foreach (var animator in animatorsOnScene)
            {
                if (animator.name != "Y Bot")
                {
                    characterGameObject = animator.gameObject;
                }


                
            }
            Selection.objects = new Object[] { characterGameObject };
        }


        public void EnableMeshRendererOfGameObject(string name)
        {
            GameObject gameObjectToEnable = GameObject.Find(name);
            var meshRenderer = gameObjectToEnable.GetComponent<MeshRenderer>();
            meshRenderer.enabled = true;
        }
        public void SelectGameObjectByName(string GameObjectName)
        {           
            GameObject gameObjectToSelect = GameObject.Find(GameObjectName);
            Selection.objects = new Object[] { gameObjectToSelect };

            //Debug.Log("Selecting " + GameObjectName);
            

            if(gameObjectToSelect != null)
            {
                gameObjectToSelect.hideFlags = HideFlags.None;
                gameObjectToSelect.SetActive(true);
                


                //Debug.Log(gameObjectToSelect.name);
            }
            else
            {
                Debug.Log($"Could not find {GameObjectName}");
            }

        }
        public void PingFolderOrAsset(string folderPath)
        {

            if (string.IsNullOrEmpty(folderPath)) { return; }
            Object obj = AssetDatabase.LoadAssetAtPath<Object>(folderPath);
            //Debug.Log($"{folderPath} {obj.name}");
            EditorGUIUtility.PingObject(obj);
        }


        /// <summary>
        /// Implement the logic to automatically complete the criterion here, if wanted/needed.
        /// </summary>
        /// <returns>True if the auto-completion logic succeeded.</returns>
        public bool AutoComplete()
        {
            var foo = GameObject.Find("Foo");
            if (!foo)
                foo = new GameObject("Foo");
            return foo != null;
        }
#endif
    }

}
