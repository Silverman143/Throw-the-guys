using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class CharactersSwitcher : MonoBehaviour
{
    [SerializeField] private List<CharacterController> _characters;
    [SerializeField] private CharacterController _centralCharacter;
    [SerializeField] private CharacterController _currentCharacter;
    private Vector3 _startPos;
    private Quaternion _startRotation;

    private InputHandler _inputHandler;

    public delegate void CharactersFinished();
    public static event CharactersFinished OnCharactersFinished;


    private void Start()
    {
        _inputHandler = FindObjectOfType<InputHandler>();
        //_characters = FindObjectsOfType<CharacterController>().ToList<CharacterController>();
        _startPos = _currentCharacter.GetPosition();
        _startRotation = _currentCharacter.GetRotation();
    }

    private void OnEnable()
    {
        CharacterController.OnFinished += ChangeCharacter;
    }

    private void OnDisable()
    {
        CharacterController.OnFinished -= ChangeCharacter;
    }

    private void OnDestroy()
    {
        CharacterController.OnFinished -= ChangeCharacter;
    }

    private void ChangeCharacter(CharacterController character)
    {
        _characters.Remove(character);
        character.Remove();
        if (_characters.Count > 0)
        {
            _currentCharacter = _characters[Random.RandomRange(0, _characters.Count - 1)];
            _currentCharacter.SetStart(_startPos, _startRotation);
            _inputHandler.SetNewCharacter(_currentCharacter.GetPelvisPos());
        }
        else
        {
            OnCharactersFinished();
        }
        
    }

    public void HideCharacters()
    {

        for (int i = 0; i < _characters.Count; i++)
        {

            if (_characters[i] != _currentCharacter && _characters[i] != _centralCharacter)
            {
                CharacterController character = _characters[i];

                _characters.Remove(character);
                character.enabled = false;
                Destroy(character.gameObject);
                i--;
            }
        }
    }
}
