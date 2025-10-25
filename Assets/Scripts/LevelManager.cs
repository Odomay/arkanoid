using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{  

    public BlockSpawner BlockSpawner;
    public BlockV1 BlockV1;
    public BlockV2 BlockV2;
    public BlockV3 BlockV3;
    public BlockV4 BlockV4;

    private delegate void SpawnShape();
    private delegate void BlockBehaviour();
    private List<(int, int)> _usedCombinations = new List<(int, int)>();

    private void Awake()
    {
        int levelIndex = SceneManager.GetActiveScene().buildIndex;
        SetupLevel(levelIndex);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void SetupLevel(int levelIndex)
    {
        if (levelIndex >= 4)
        {
            Debug.Log("Все уровни пройдены");
            return;
        }

        SpawnShape[] spawns = new SpawnShape[]
        {
            BlockSpawner.SpawnBlocks1,
            BlockSpawner.SpawnBlocks2,
            BlockSpawner.SpawnBlocks3,
            BlockSpawner.SpawnBlocks4,
        };

        BlockBehaviour[] behaviours = new BlockBehaviour[]
        {
            BlockV1.ChangeBlockColor,
            BlockV2.ChangeBlockSprite,
            BlockV3.HandleCollision,
            BlockV4.BlockReducing
        };

        int behaviourIndex, spawnIndex;
        do
        {
            behaviourIndex = Random.Range(0, 4);
            spawnIndex = Random.Range(0, 4);
        }
        while (_usedCombinations.Contains((behaviourIndex, spawnIndex)));

        _usedCombinations.Add((behaviourIndex, spawnIndex));

        spawns[spawnIndex]();
        behaviours[behaviourIndex]();
    }
}
/*
    Делегаты (delegate) это "указатели на методы" или типобезопасные (свойство языка программирования, характеризующее безопасность 
    и надёжность в приминение его системы типов) ссылки на методы. Они позволяют передавать методы как объекты, чтобы вызвать их позже
    или передавать в другие части кода. 

    То есть делегат это специальный тип данных, который определяет сигнатуру метода (тип возвращаемого значения и параметры).
    После того как делегат объявлен, можно создавать переменные этого типа и присваивать им методы, которые соответствуют этой сигнатуре.

    public delegate void MyDelegate(string message);

    public void SayHello(string text)
    {
        Debug.Log(text);
    }

    public void SayGoodBye(string text)
    {
        Debug.Log(text);
    }

    private void Awake()
    {
        MyDelegate delegate = SayHello; // добавляется первый метод 
        delegate += SayGoodBye; // добавляется второй метод
        delegate("Hello World !!!");
        delegate("See you on tomorrow :) !!!");
    }

    ------------------------------------------------------------------------------------------------------------------------------------

    Ключевые особенности делегатов: 
    1 Типобезопасность: делегат гарантирует, что метод, который ты ему присваиваешь, имеет точно такую же сигнатуру.
    2. Мультикастинг: один делегат может ссылаться на несколько методов одновременно. Они будут вызваны по очереди.

    Почему делегаты важны? 
    Они лежат  в основе событий и позволяют передавать поведение(логику) как параметр. 

    ------------------------------------------------------------------------------------------------------------------------------------

    События в C# - это механизм, построенный на основе делегатов, который позволяет объектам сообщать другим объектам, что что-то произошло.
    Это как система подписки: один объект объявляет событие, а другие подписываются на него, чтобы реагировать.
    События - это обёртка вокруг делегата, которая ограничевает к нему доступ.
    События обычно объявляются при помощи ключевого слова " event "
    

    public class Button 
    {
        public delegate void ClickHandler();
        public event ClickHandler OnClick;

        public void SimulateClick()
        {
            if(OnClick != null)
            {
                OnClick();
            }
        }
    }

    public class Game
    {
        public void Start()
        {
            Button button = new Button();
            button.OnClick += HandleClick;
            button.SimulateClick();
        }
        
        private void HandleClick()
        {
            Debug.Log("Кнопка нажата! :)");
        }
    }

    -----------------------------------------------------------------------------------------------------------------------------------

       Ключевые особенности событий 
    1) инкапсуляция: в отличии от делегатов события можно вызывать только внутри класса где они объявленны. Подписчики снаружи могут только
    добавлять или удалять свои методы.
    2) подписка и отписка: подписка на событие происходит через оператор += , а отписка через -=.
    3) проверки при вызове событий нужны для того, что бы избежать исключений, если никто не подписан.


    -----------------------------------------------------------------------------------------------------------------------------------

       Делегаты и события в юнити

    Допустим есть игрок, который должен уведомлять UI о своём здоровье

    public class Player : MonoBehaviour
    {
        public delegate void HealthChangedHandler(int newHealth);
        public event HealthChangedHandler OnHealthChanged;

        private int _health = 100;

        public void TakeDamage(int damage)
        {
            _health -= damage;
            OnHealthChanged?.Invoke(_health);
        }
    }

    public class UIController : MonoBehavior
    {
        public Player Player;

        private TMP_Text _healthText;

        privaye void Start()
        {
            _healthText = GetComponent<TMP_Text>();
            Player.OnHealthChanged += UpdateHealthUI;
        }

        private void OnDestroy()
        {
            Player.OnHealthChanged -= UpdateHealthUI;
        }

        private void UpdateHealthUI(int newHealth)
        {
            _healthtext.text = $"Health: {newHealth}";
        }
    }
    
    Что здесь происходит? 
    - Игрок (Player) объявляет событие OnHealthChanged.
    - Когда здоровье меняется (TakeDamage), вызывается событие.
    - UI (UIController) подписывается на это событие и обновляет текст.

    Почему отписка важна? 
    Если не отписаться (-=) от события, например, в OnDestroy, может возникнуть утечка памяти или вызов метода у уничтоженного объекта, что 
    приведёт к ошибке.
    
    -----------------------------------------------------------------------------------------------------------------------------------

    С# предоставляет готовые делегаты, что бы каждый раз не писать свои:
    1) Action - используется для методов без возвращаемого значения 
    Action<string> print = Debug.Log;
    print ("Hello");
    
    2) Func - используется для методов с возвращаемым значением. Возвращаемый параметр указывается на последнем месте.
    Func<int, int, int> add = (a, b) => a + b;
    int result = add(3, 5);

    3) Predicate - используется для методов, возвращающих bool 
    Predicate<int> isPositive = x => x > 0;
    bool check = isPositive(3);

    -----------------------------------------------------------------------------------------------------------------------------------
   
    Лямбда выражения - это короткий способ записать метод прямо в месте использования делегата.
    Player.OnHealthChanged += (newHealth) => Debug.Log($"Health: {newHealth}");
*/
