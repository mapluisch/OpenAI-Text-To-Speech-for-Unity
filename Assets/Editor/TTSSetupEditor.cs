using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(TTSSetup))]
public class TTSSetupEditor : Editor
{
    SerializedProperty openAIKey;
    SerializedProperty customText;
    Texture2D bannerTexture;

    readonly float uiElementHeight = 40f;
    private GUIStyle buttonStyle1, buttonStyle2, buttonStyle3, buttonStyle4;

    private void OnEnable()
    {
        openAIKey = serializedObject.FindProperty("openAIKey");
        customText = serializedObject.FindProperty("customText");
        bannerTexture = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Editor/banner.png");
    }

    private Texture2D ColorTex(int width, int height, Color col)
    {
        Texture2D result = new Texture2D(width, height);
        Color[] pix = new Color[width * height];
        for (int i = 0; i < pix.Length; i++) pix[i] = col;
        result.SetPixels(pix);
        result.Apply();
        return result;
    }

    private GUIStyle CreateButtonStyle(Color baseColor)
    {
        GUIStyle style = new GUIStyle()
        {
            normal =
            {
                background = ColorTex(2, 2, baseColor),
                textColor = Color.white
                
            },
            hover = {
                background = ColorTex(2, 2, baseColor*1.2f),
                textColor = Color.white
            },
            
            active =
            {
                background = ColorTex(2, 2, baseColor*0.8f),
                textColor = Color.white
            },
            margin = new RectOffset(10, 10, 4, 4),
            padding = new RectOffset(10, 10, 10, 10)
        };

        return style;
    }

    
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        // -- setup button styles
        GUILayoutOption[] buttonOptions = { GUILayout.Height(uiElementHeight) };
        EnsureButtonStyles();

        // -- banner gfx
        if (bannerTexture != null)
        {
            GUILayout.Space(5);

            float aspectRatio = (float)bannerTexture.height / bannerTexture.width;
            float width = EditorGUIUtility.currentViewWidth;
            float height = width * aspectRatio; 

            Rect rect = GUILayoutUtility.GetRect(width, height);

            GUI.DrawTexture(rect, bannerTexture, ScaleMode.ScaleToFit);
    
            GUILayout.Space(5);
            GUILayout.Box("", new GUILayoutOption[] { GUILayout.ExpandWidth(true), GUILayout.Height(5) });
            GUILayout.Space(5);
        }
        
        // -- section for the main setup of the TTS prefabs etc.
        EditorGUILayout.LabelField("Setup", EditorStyles.boldLabel);
        GUIStyle textFieldStyle = new GUIStyle(EditorStyles.textField)
        {
            alignment = TextAnchor.MiddleLeft,
            normal = { textColor = Color.white },
            margin = new RectOffset(10, 10, 4, 4),
            padding = new RectOffset(10, 10, 10, 10)
        };

        string placeholderKeyText = "0. Enter your OpenAI API key";
        string apiKeyField = string.IsNullOrEmpty(openAIKey.stringValue) ? placeholderKeyText : openAIKey.stringValue;
        apiKeyField = EditorGUILayout.TextField(apiKeyField, textFieldStyle, GUILayout.Height(uiElementHeight));

        if (apiKeyField != placeholderKeyText && !string.IsNullOrEmpty(apiKeyField))openAIKey.stringValue = apiKeyField;
        else if (string.IsNullOrEmpty(apiKeyField))openAIKey.stringValue = "";

        if (GUILayout.Button("1. Add OpenAI Prefab", buttonStyle1, buttonOptions)) AddOpenAIPrefab();

        if (GUILayout.Button("2. Add Text-To-Speech Prefab", buttonStyle2, buttonOptions)) AddTextToSpeechPrefab();

        if (GUILayout.Button("3. Say \"Hello World!\"", buttonStyle3, buttonOptions)) SayHelloWorld();
        
        // -- spacer
        EditorGUILayout.Space();
        GUILayout.Box("", new GUILayoutOption[] { GUILayout.ExpandWidth(true), GUILayout.Height(5) });
        EditorGUILayout.Space();
        
        // -- section to synthesize your own text (after completing the main setup)
        string placeholderTestText = "4. Synthesize your own text";
        string customTextField = string.IsNullOrEmpty(customText.stringValue) ? placeholderTestText : customText.stringValue;
        customTextField = EditorGUILayout.TextField(customTextField, textFieldStyle, GUILayout.Height(uiElementHeight));

        if (customTextField != placeholderTestText && !string.IsNullOrEmpty(customTextField)) customText.stringValue = customTextField;
        else if (string.IsNullOrEmpty(customTextField)) customText.stringValue = "";
        
        if (GUILayout.Button("Synthesize", buttonStyle4, buttonOptions)) SynthesizeCustomText();
        
        GUIStyle wrappedBoldLabel = new GUIStyle(EditorStyles.boldLabel)
        {
            wordWrap = true
        };
        
        // -- spacer
        GUILayout.Space(5);
        GUILayout.Box("", new GUILayoutOption[] { GUILayout.ExpandWidth(true), GUILayout.Height(5) });
        GUILayout.Space(5);
        
        // -- info texts
        EditorGUILayout.LabelField("Make sure to tinker around with the TTSManager settings to change the model, voice, and speed.", wrappedBoldLabel);
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("For synthesizing text via script, just reference the TTSManager and call TTSManager.SynthesizeAndPlay(\"your text\").", wrappedBoldLabel);
    
        serializedObject.ApplyModifiedProperties();
    }
    
    private void EnsureButtonStyles()
    {
        if (buttonStyle1 == null)
        {
            buttonStyle1 = CreateButtonStyle(new Color(21f/255, 32f/255, 49f/255));
        }
        if (buttonStyle2 == null)
        {
            buttonStyle2 = CreateButtonStyle(new Color(36f/255, 54f/255, 71f/255));
        }
        if (buttonStyle3 == null)
        {
            buttonStyle3 = CreateButtonStyle(new Color(55f/255, 79f/255, 100f/255));
        }
        if (buttonStyle4 == null)
        {
            buttonStyle4 = CreateButtonStyle(new Color(93f/255, 136f/255, 165f/255));
        }
    }

    private void AddOpenAIPrefab()
    {
        if (string.IsNullOrEmpty(openAIKey.stringValue))
        {
            Debug.LogError("Please enter your OpenAI API key.");
            return;
        }
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Core/OpenAI.prefab");
        if (prefab)
        {
            GameObject openAI = (GameObject)PrefabUtility.InstantiatePrefab(prefab, EditorSceneManager.GetActiveScene());
            OpenAIWrapper openAIWrapper = openAI.GetComponent<OpenAIWrapper>();
            if (openAIWrapper)
            {
                openAIWrapper.SetAPIKey(openAIKey.stringValue);
                EditorUtility.SetDirty(openAIWrapper); 
            }
        }
        else Debug.LogError("Couldn't find OpenAI Prefab at the specified path.");
    }

    private void AddTextToSpeechPrefab()
    {
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Core/TTSManager.prefab");
        if (prefab) PrefabUtility.InstantiatePrefab(prefab, EditorSceneManager.GetActiveScene());
        else Debug.LogError("Couldn't find Text-To-Speech Prefab at the specified path.");
    }

    private void SayHelloWorld()
    {
        TTSManager ttsManager = GameObject.FindObjectOfType<TTSManager>();
        if (ttsManager != null)
        {
            // note: editor doesn't await the async op
            ttsManager.SynthesizeAndPlay("Hello World");
        }
    }
    
    private void SynthesizeCustomText()
    {
        TTSManager ttsManager = GameObject.FindObjectOfType<TTSManager>();
        if (ttsManager != null && customText != null) ttsManager.SynthesizeAndPlay(customText.stringValue);
    }
}
