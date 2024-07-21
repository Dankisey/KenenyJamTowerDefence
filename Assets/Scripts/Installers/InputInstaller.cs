using UnityEngine;
using Zenject;

public class InputInstaller : MonoInstaller
{
    [SerializeField] private RuneSelector _runeSelector;

    public override void InstallBindings()
    {
        PlayerInputActions inputActions = new();
        inputActions.Enable();
        Container.BindInstance(inputActions).AsSingle();
        Container.BindInstance(_runeSelector).AsSingle();
    }
}