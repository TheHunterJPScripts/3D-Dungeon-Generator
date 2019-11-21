using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

public class RoomWindow : EditorWindow
{
    GameObject gameObject;
    Editor gameObjectEditor;

    public static void ShowWindow() {
        GetWindow<RoomWindow>("Room Editor");
    }

    private void OnGUI() {
        gameObject = (GameObject)EditorGUILayout.ObjectField(gameObject, typeof(GameObject), true);

        if (gameObject != null) {
            if (gameObjectEditor == null)
                gameObjectEditor = Editor.CreateEditor(gameObject);

            gameObjectEditor.OnPreviewGUI(GUILayoutUtility.GetRect(100, 100), EditorStyles.whiteLabel);
        }
    }
    onsce
}
