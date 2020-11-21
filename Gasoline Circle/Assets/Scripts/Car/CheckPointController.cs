using UnityEngine;

/// <summary>
/// Отвечает за работу чек поинтов, чтобы игроку
/// давались очки за прохождение кругов
/// заданном направлениии
/// </summary>
public class CheckPointController : MonoBehaviour
{
    /// <summary>
    /// Выбор игрока, которого будет контролировать 
    /// </summary>
    [SerializeField]
    private CarControllMode target;

    /// <summary>
    /// Значения 
    /// </summary>
    private bool[] resultCheck;

    /// <summary>
    /// Свойство, которое передает информацию, 
    /// о том на кого этот контроллер нацелен
    /// </summary>
    public CarControllMode Target { get => target; }

    private void Awake()
    {
        resultCheck = new bool[3];
    }

    /// <summary>
    /// При прохождении определенной зоны, устанавливает флажок true
    /// на этом учестке
    /// </summary>
    /// <param name="type"></param>
    public void SetResultCheck(CheckPointType type) 
    {
        switch (type) 
        {
            case CheckPointType.FirstZone:             
                resultCheck[0] = true;        
                break;
            case CheckPointType.SecondZone:
                if(resultCheck[0])
                    resultCheck[1] = true;
                break;
            case CheckPointType.ThirdZone:
                if (resultCheck[0] && resultCheck[1])
                    resultCheck[2] = true;
                break;
        }
    }
    /// <summary>
    /// При прохождении финаши сверяет, чтобы все зоны имели true,
    /// иначе сброс. (Например проехал полтрассы и поехал в обратную сторону
    /// </summary>
    public void FinishDetected() 
   {
        if (CheckResults())
            SetScorePlayer();
        else
            ResetResults();
    }

    /// <summary>
    /// Проверяет все результаты на истину.
    /// Возвращает ответ на этот запрос.
    /// </summary>
    /// <returns></returns>
    private bool CheckResults() 
    {
        foreach (bool r in resultCheck) 
        {
            if (r == false)
                return false;
        }
        return true;
    }

    /// <summary>
    /// Сбрасывает значения прохождения зон чекпоинтов
    /// </summary>
    private void ResetResults() 
    {
        for (int i = 0; i < resultCheck.Length; i++) 
        {
            resultCheck[i] = false;
        }
    }

    /// <summary>
    /// Если на финише все зоны пройдены, то запускается эта функция.
    /// Она повышает количество очков определенного игрока на 1.
    /// Затем сбрасывает результы зон
    /// </summary>
    private void SetScorePlayer() 
    {
        GameController.Instance.IncreaseThePlayerPoint(Target);
        ResetResults();
    }
}
