using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Android;
using UnityEngine;

class PostProcessGradleAxisSDKAndroid : IPostGenerateGradleAndroidProject
{
    public int callbackOrder { get { return 0; } }
    public void OnPostGenerateGradleAndroidProject(string path)
    {       
        IncludeDependenciesToGradle(path);
        DirectoryInfo info = Directory.GetParent(path);
        IncludeMaventoSettingGradle(info.FullName);
        IncludeAndroidXToProperties(info.FullName);
    }
    private void IncludeDependenciesToGradle(string path)
    {
        const string fileName = "/build.gradle";

        List<string> lines = new List<string>(File.ReadAllLines(path + fileName));

        for (int i = 0; i < lines.Count; i++)
        {
            if (lines[i].Contains("dependencies {"))
            {
                if (i < lines.Count - 1)
                {
                    lines.Insert(i + 1, "implementation group: 'com.github.mik3y', name: 'usb-serial-for-android', version: 'v3.4.6'");
                    break;
                }

            }
        }
        File.WriteAllLines(path + fileName, lines.ToArray());
    }
    private void IncludeMaventoSettingGradle(string path)
    {
        const string fileName = "/settings.gradle";
        List<string> lines = new List<string>(File.ReadAllLines(path + fileName));
        for (int i = 0; i < lines.Count; i++)
        {
            if (lines[i].Contains("dependencyResolutionManagement {"))
            {
                for(int j = i;  j < lines.Count - 1; j++)
                {
                    if (lines[j].Contains("repositories {"))
                    {
                        lines.Insert(j + 1, "maven{url \"https://jitpack.io\"}");
                        break;
                    }
                }
                break;
            }
        }
        File.WriteAllLines(path + fileName, lines.ToArray());
    }
    private void IncludeAndroidXToProperties(string path) 
    {
        const string fileName = "/gradle.properties";
        List<string> lines = new List<string>(File.ReadAllLines(path + fileName));
        lines.Add("android.useAndroidX=true\r\n");
        lines.Add("android.enableJetifier=true\r\n");
      
        File.WriteAllLines(path + fileName, lines.ToArray());
    }
}