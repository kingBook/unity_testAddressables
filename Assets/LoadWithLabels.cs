using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LoadWithLabels : MonoBehaviour {

    [SerializeField] private List<string> m_keys = new List<string>() { "Cube", "Sphere" };

    private AsyncOperationHandle<IList<GameObject>> m_handle;

    private void OnCompleteHandler(AsyncOperationHandle<IList<GameObject>> operation) {
        if (operation.Status != AsyncOperationStatus.Succeeded)
            Debug.LogWarning("Some assets did not load.");
    }

    private void Start() {
        m_handle = Addressables.LoadAssetsAsync<GameObject>(
            m_keys,
            addressable => {
                // 获取每一个资源的回调
                if (addressable != null) {
                    Instantiate<GameObject>(addressable);
                }
            }, Addressables.MergeMode.Union, // 如何合并多个标签
            false); // 如果任意一个资源加载失败时，是否失败
        m_handle.Completed += OnCompleteHandler;
    }

    private void OnDestroy() {
        Addressables.Release(m_handle);
    }
}

