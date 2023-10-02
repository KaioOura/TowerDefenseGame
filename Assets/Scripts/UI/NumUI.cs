using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumUI : MonoBehaviour
{
    [SerializeField]
    private string _title;

    [SerializeField]
    private TextMeshProUGUI _textMesh;

    [SerializeField]
    private FloatEventChannel OnFloatEventChannel;

    // Start is called before the first frame update
    void Awake()
    {
        OnFloatEventChannel.OnEventRaised += UpdateText;
    }

    private void OnDestroy()
    {
        OnFloatEventChannel.OnEventRaised -= UpdateText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateText(float value)
    {
        _textMesh.text = _title + value.ToString();
    }
}
