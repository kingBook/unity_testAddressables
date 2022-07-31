using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LoadWithReference : MonoBehaviour {

    [SerializeField] AssetReference m_assetReference;

    private void OnCompleteHandler(AsyncOperationHandle operation) {
        if(operation.Status == AsyncOperationStatus.Succeeded) {
            Instantiate(m_assetReference.Asset);
        } else {
            Debug.LogError($"AssetReference {m_assetReference.RuntimeKey} failed to load.");
        }
    }

    private void Start() {
        AsyncOperationHandle handle = m_assetReference.LoadAssetAsync<GameObject>();
        handle.Completed += OnCompleteHandler;
    }

    private void OnDestroy() {
        // 需要手动释放
        m_assetReference.ReleaseAsset();
    }
}
