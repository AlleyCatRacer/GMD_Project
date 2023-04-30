using UnityEditor;
using UnityEngine;

namespace Editor
{
    /// <summary>
    /// A helper class that runs only on IDE to help speedup or slow down the game
    /// </summary>
    public class TimeScaleHelper : EditorWindow
    {
        [MenuItem("Tools/Utils/Time Scale")]
        private static void Init()
        {
            EditorWindow.GetWindow<TimeScaleHelper>("Time Scale");
        }

        private void OnGUI()
        {
            if (!EditorApplication.isPlaying)
            {
                GUILayout.Label("Time Scale can be updated in play mode only!");
            }

            EditorGUI.BeginDisabledGroup(!EditorApplication.isPlaying);

            // Custom Layout
            GUILayout.BeginVertical();
            GUILayout.Label($"Time Scale: {Time.timeScale}x");
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("-0.5"))
                Time.timeScale -= .5f;
            if (GUILayout.Button("-0.1"))
                Time.timeScale -= .1f;
            if (GUILayout.Button("1"))
                Time.timeScale = 1;
            if (GUILayout.Button("+0.1"))
                Time.timeScale += .1f;

            if (GUILayout.Button("+0.5"))
                Time.timeScale += .5f;
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();


            // GUILayout.BeginVertical();
            // GUILayout.BeginHorizontal();
            // if (GUILayout.Button("0"))
            //     Time.timeScale = 0F;
            // if (GUILayout.Button("0.5"))
            //     Time.timeScale = .5F;
            // if (GUILayout.Button("1x"))
            //     Time.timeScale = 1F;
            // if (GUILayout.Button("2X"))
            //     Time.timeScale = 2F;
            // if (GUILayout.Button("10X"))
            //     Time.timeScale = 10F;
            // GUILayout.EndHorizontal();
            // GUILayout.EndVertical();
            // GUILayout.Label("Time Scale: " + Time.timeScale);
            EditorGUI.EndDisabledGroup();
            Repaint();
        }
    }
}