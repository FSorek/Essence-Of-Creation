namespace ExtendedColliders3DEditor {
    using UnityEngine;
    using UnityEditor;

    public class ExtendedColliders3DVersionChanges : EditorWindow {

        //Variables.
        Vector2 scrollPosition = Vector2.zero;
        GUIStyle _headerLabel = null;
        GUIStyle headerLabel {
            get {
                if (_headerLabel == null) {
                    _headerLabel = new GUIStyle(EditorStyles.boldLabel);
                    _headerLabel.alignment = TextAnchor.MiddleCenter;
                    _headerLabel.normal.textColor = EditorGUIUtility.isProSkin ? Color.white : Color.black;
                }
                return _headerLabel;
            }
        }
        GUIStyle _subHeaderLabel = null;
        GUIStyle subHeaderLabel {
            get {
                if (_subHeaderLabel == null) {
                    _subHeaderLabel = new GUIStyle(EditorStyles.boldLabel);
                    _subHeaderLabel.normal.textColor = EditorGUIUtility.isProSkin ? Color.white : Color.black;
                }
                return _subHeaderLabel;
            }
        }
        GUIStyle _boldWrappedLabel = null;
        GUIStyle boldWrappedLabel {
            get {
                if (_boldWrappedLabel == null) {
                    _boldWrappedLabel = new GUIStyle(EditorStyles.boldLabel);
                    _boldWrappedLabel.wordWrap = true;
                }
                return _boldWrappedLabel;
            }
        }
        GUIStyle _wrappedLabel = null;
        GUIStyle wrappedLabel {
            get {
                if (_wrappedLabel == null) {
                    _wrappedLabel = new GUIStyle(GUI.skin.label);
                    _wrappedLabel.padding = new RectOffset(25, 0, 0, 0);
                    _wrappedLabel.wordWrap = true;
                }
                return _wrappedLabel;
            }
        }

        //Draw the GUI.
        void OnGUI() {

            //Display the version change text.
            EditorGUILayout.LabelField("Extended Colliders 3D Version Changes", headerLabel);
            EditorGUILayout.GetControlRect();
            EditorGUILayout.LabelField("If you have any comments or suggestions as to how we could improve Extended Colliders 3D, or if you want to report a " +
                    "bug in the software, feel free to e-mail us on info@battenbergsoftware.com and we'll get back to you.", boldWrappedLabel);
            EditorGUILayout.GetControlRect();
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
            EditorGUILayout.LabelField("Version 1.0.3", subHeaderLabel);
            EditorGUILayout.LabelField(
                    "Fixed memory leaks when drawing gizmos for Extended Colliders 3D components and when unloading a scene that contained one.", wrappedLabel);
            addBulletPoint();
            EditorGUILayout.LabelField("Added scripts to \"ExtendedColliders3D\" namespace.", wrappedLabel);
            addBulletPoint();
            EditorGUILayout.LabelField("Added this version changes window.", wrappedLabel);
            addBulletPoint();
            EditorGUILayout.GetControlRect();
            EditorGUILayout.LabelField("Version 1.0.2", subHeaderLabel);
            EditorGUILayout.LabelField("Added support for sphere colliders, the detail of which can be customised. The main advantage of these over Unity's " +
                    "built-in sphere collider is that they can be scaled in a non-uniform way.", wrappedLabel);
            addBulletPoint();
            EditorGUILayout.GetControlRect();
            EditorGUILayout.LabelField("Version 1.0.1", subHeaderLabel);
            EditorGUILayout.LabelField("Fixed an issue whereby the position, rotation and scale of the colliders in the scene view was incorrect if the " +
                    "Extended Colliders 3D component was on a game object that was more than one level down in the hierarchy. The parent, grandparent, etc. " +
                    "transforms weren't being applied properly.", wrappedLabel);
            addBulletPoint();
            EditorGUILayout.GetControlRect();
            EditorGUILayout.LabelField("Version 1.0.0", subHeaderLabel);
            EditorGUILayout.LabelField("Initial release.", wrappedLabel);
            EditorGUILayout.EndScrollView();
        }

        //Adds a bullet point before the label that has just been added.
        void addBulletPoint() {
            Rect rect = GUILayoutUtility.GetLastRect();
            EditorGUI.LabelField(new Rect(17, rect.yMin - 1, 10, rect.height), "•");
        }
    }
}