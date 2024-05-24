using Axis.Elements;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class TutorialUtils : MonoBehaviour
{
#if UNITY_EDITOR
    public CustomCriteria customCriteria;

    void Update()
    {
        var axisBrain = FindObjectOfType<AxisBrain>();
        if (Application.isPlaying == false)
        {
            
            var humanoidCharacters = GameObject.FindObjectsOfType<Animator>();
            customCriteria.isThereAnHumanoidCharacterOnScene = humanoidCharacters.Length > 1 ? true : false;

            

            if (axisBrain != null)
            {
                //Debug.Log("The brain is not null");
                //if(axisBrain.OutputCharacters.Count > 0)
                //{
                //    if(axisBrain.OutputCharacters[0] != null)
                //    {
                //        //Debug.Log("Connected axis brain to avatar");
                //        customCriteria.isAxisBrainConnectedToAvatar = true;
                //    }
                //    
                //}
                //Debug.Log("There is an axis brain on the scene");
            }
        } else
        {
            if(axisBrain != null)
            {
                axisBrain.SetVisibility();
            }

        }
        
    }
#endif

}
