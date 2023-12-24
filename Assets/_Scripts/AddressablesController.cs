using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class AddressablesController : MonoBehaviour
{
    [SerializeField]
    private AssetReference boardImage;
    [SerializeField]
    private AssetReference playerPawnImage;

    public Image Board;
    public Image PlayerPawn;
    // Start is called before the first frame update
    void Start()
    {       
        Addressables.InitializeAsync().Completed += Addressables_Completed;
    }

    private void Addressables_Completed(AsyncOperationHandle<IResourceLocator> obj)
    {
        boardImage.LoadAssetAsync<Sprite>().Completed += (go) =>
        {
            Board.sprite = go.Result;
        };
        playerPawnImage.LoadAssetAsync<Sprite>().Completed += (go) =>
        {
            PlayerPawn.sprite = go.Result;
        };
    }
}
