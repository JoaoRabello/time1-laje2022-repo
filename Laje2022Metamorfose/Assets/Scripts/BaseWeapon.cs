using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    [SerializeField] private List<GameObject> _aimArrowList = new List<GameObject>();

    private Vector2 _aimDirection;
    
    /// <summary>
    /// Define a direção de mira e liga a seta correspondente
    /// </summary>
    /// <param name="direction">Direção desejada</param>
    public void SetAimDirection(Vector2 direction)
    {
        _aimDirection = direction;
        HighlightArrowByIndex(GetArrowIndexByDirection(direction));
    }

    /// <summary>
    /// Pega o índice da seta na lista, ordenada em sentido horário, a partir de uma direção.
    /// </summary>
    /// <param name="direction">Direção a ser verificada</param>
    /// <returns>Índice da seta na lista</returns>
    private int GetArrowIndexByDirection(Vector2 direction)
    {
        return direction.x switch
        {
            0 when direction.y > 0 => 0,
            0 when direction.y < 0 => 4,
            > 0 when direction.y > 0 => 1,
            > 0 when direction.y == 0 => 2,
            > 0 when direction.y < 0 => 3,
            < 0 when direction.y < 0 => 5,
            < 0 when direction.y == 0 => 6,
            < 0 when direction.y > 0 => 7,
            _ => 0
        };
    }

    /// <summary>
    /// Desliga todas as setas e liga somente a seta cujo índice é o determinado.
    /// </summary>
    /// <param name="index">Índice da seta a ser ligada</param>
    private void HighlightArrowByIndex(int index)
    {
        for (var i = 0; i < _aimArrowList.Count; i++)
        {
            _aimArrowList[i].SetActive(i == index);
        }
    }
}
