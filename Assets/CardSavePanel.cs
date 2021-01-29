using System.Collections;
using System.Collections.Generic;
using System.IO;
using DeadMosquito.AndroidGoodies;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CardSavePanel : MonoBehaviour
{
    public InputField categoryInputField;
    public InputField cardNameInputField;
    private Cat_so _cat;
    private string _categoryName;
    private string _cardName;
    private Sprite _cardImage;
    private AudioSource _cardAudioClip;

    private const string SavePath = "Assets/cards/";
    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    static Sprite SpriteFromTex2D(Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
       
    }
    public void OnCardImage()
    {	var shouldGenerateThumbnails = false;
        var imageResultSize = ImageResultSize.Max2048;
        AGGallery.PickImageFromGallery(
            selectedImage =>
            {
                var imageTexture2D = selectedImage.LoadTexture2D();

                string msg = string.Format("{0} was loaded from gallery with size {1}x{2}",
                    selectedImage.OriginalPath, imageTexture2D.width, imageTexture2D.height);
                _cardImage = SpriteFromTex2D(imageTexture2D);
                AssetDatabase.CreateAsset(_cardImage,SavePath+_categoryName+"/"+_cardImage.name+".jpg");
                AssetDatabase.SaveAssets();
                // Clean up
                Resources.UnloadUnusedAssets();
            },
            errorMessage => AGUIMisc.ShowToast("Cancelled picking image from gallery: " + errorMessage),
            imageResultSize);
    }
    

    public void OnAudioPicking()
    {
        AGFilePicker.PickAudio(audioFile =>
            {
                var msg = "Audio file was picked: " + audioFile;
                AGUIMisc.ShowToast(msg);
              
            },
            error => AGUIMisc.ShowToast("Cancelled picking audio file"));
    }
    public void CategoryName()
    {
        _categoryName = categoryInputField.text;
        Debug.Log(_categoryName);
    }

    public void CardName()
    {
        _cardName = cardNameInputField.name;
        
    }
    public void OnCreat()
    {
        _cat = ScriptableObject.CreateInstance<Cat_so>();
        _cat.Name = _categoryName;
        _cat.cardImgs=new Sprite[1];
        _cat.cardImgs[0] = _cardImage;
       
        if (!Directory.Exists(SavePath + _categoryName))
        {
            Directory.CreateDirectory(SavePath + _categoryName);
        }
        AssetDatabase.CreateAsset(_cat,SavePath+_categoryName+"/"+_cardName+".asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = _cat;
    }
    
}
