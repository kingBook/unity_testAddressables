using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LoadWithAddress : MonoBehaviour {
    
    [SerializeField] private string m_address;

    private AsyncOperationHandle<GameObject> m_handle;

    private void Start() {
        m_handle = Addressables.LoadAssetAsync<GameObject>(m_address);
        m_handle.Completed += OnCompleteHandler;
    }

    private void OnCompleteHandler(AsyncOperationHandle<GameObject> operation) {
        if (operation.Status == AsyncOperationStatus.Succeeded) {
            Instantiate(operation.Result);
        } else {
            Debug.LogError($"Asset for {m_address} failed to load.");
        }
    }

    private void OnDestroy() {
        Addressables.Release(m_handle);
    }
}