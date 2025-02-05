using System.Xml.Linq;
using Microsoft.Win32.SafeHandles;
using System.Collections.Generic;

namespace TextRPG_SpartaDungeon
{
    public interface ICharacter
    {
        string Name { get; }
        int Health { get; set; }
        int Attack { get; }
    }

    //플레이어 클래스
    public class Player : ICharacter
    {
        public int Lv { get; set; }
        public string Name { get;}
        public string Job { get; set; }
        public int Attack { get; set; }
        public int Defence { get; set; }
        public int Health { get; set; }
        public int Gold { get; set; }

        public Player(string name)
        {
            this.Lv = 1;
            this.Name = name;
            this.Job = "전사";
            this.Attack = 10;
            this.Defence = 5;
            this.Health = 100;
            this.Gold = 1500;
        }
    }

    public interface Item
    {
        string Name { get; }
        string Effect { get; }
        string Description { get; }
        bool isEquipped { get; set; }
        void Equip(Player warrior); // 전사에게 아이템을 사용하는 메서드
    }

    ////아이템
    class Sword : Item
    {
        public string Name => "낡은 검";
        public string Effect => "공격력 + 2";
        public string Description => "쉽게 볼 수 있는 낡은 검 입니다.";
        public bool isEquipped => false;
        public void Equip(Player warrior)
        {
            Console.WriteLine($"{Name}을 장착합니다.");
            warrior.Attack += 2;
        }
    }
    class Spear : Item
    {
        public string Name => "스파르타의 창";
        public string Effect => "공격력 + 7";
        public string Description => "스파르타의 전사들이 사용했다는 전설의 창입니다.";
        public void Equip(Player warrior)
        {
            Console.WriteLine($"{Name}을 장착합니다.");
            warrior.Attack += 7;
        }
    }
    class Armor : Item
    {
        public string Name => "무쇠갑옷";
        public string Effect => "방어력 + 5";
        public string Description => "무쇠로 만들어져 튼튼한 갑옷입니다.";
        public void Equip(Player warrior)
        {
            Console.WriteLine($"{Name}을 장착합니다.");
            warrior.Defence += 5;
        }
    }

    //인벤토리
    class Inventory
    {
        public Sword sword = new Sword();
        public Spear spear = new Spear();
        public Armor armor = new Armor();

        public List<Item> items = new List<Item>();
        
        public Inventory()
        {
            items.Add( sword );
            items.Add( spear );
            items.Add( armor );
        }
    }

    //게임시작 클래스
    class GameManager
    {
        public Player player;
        public Inventory inventory;
        public GameManager()
        {
            Console.WriteLine("이름을 입력해주세요!");
            player = new Player(Console.ReadLine());
            inventory = new Inventory();
        }

        //시작화면 마을화면
        public void ShowStartScene()
        {
            Console.Clear();
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
            Console.WriteLine("\n1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine("\n원하시는 행동을 입력해주세요.\n>>");

            while (true)
            {
                string input = Console.ReadLine();
                int sellecNumber;
                bool isNumber = int.TryParse(input, out sellecNumber);

                switch (sellecNumber)
                {
                    //상태창으로 이동
                    case 1:
                        StatusDisPlay(player);
                        break;
                    //인벤토리창으로 이동
                    case 2:
                        InvenDisPlay(inventory);
                        break;
                    //상점창으로 이동
                    case 3:
                        StoreDisplay(player, inventory);
                        break;
                    //그 외 명령어 입력시 출력
                    default:
                        Console.WriteLine("잘못된 입력입니다");
                        break;
                }
            }
        }

        //상태창
        public void StatusDisPlay(Player targetplayer)
        {
            Console.Clear();
            Console.WriteLine("상태보기\n캐릭터의 정보가 표시됩니다.");
            Console.WriteLine($"\nLv. {targetplayer.Lv}");
            Console.WriteLine($"{targetplayer.Name} ({targetplayer.Job})");
            Console.WriteLine($"공격력 : {targetplayer.Attack}");
            Console.WriteLine($"방어력 : {targetplayer.Defence}");
            Console.WriteLine($"체력 : {targetplayer.Health}");
            Console.WriteLine($"Gold : {targetplayer.Gold}");
            Console.WriteLine("\n0. 나가기\n");
            Console.WriteLine("\n원하시는 행동을 입력해주세요.\n>>");

            while (true)
            {
                string input = Console.ReadLine();
                int sellecNumber;
                bool isNumber = int.TryParse(input, out sellecNumber);

                switch (sellecNumber)
                {
                    //마을창으로 돌아가기
                    case 0:
                        ShowStartScene();
                        break;
                    //그 외 명령어 입력시 출력
                    default:
                        Console.WriteLine("잘못된 입력입니다");
                        break;

                }
            }
        }

        //인벤토리 창
        public void InvenDisPlay(Inventory itemlist)
        {
            
            Console.Clear();
            Console.WriteLine("인벤토리\n보유 중인 아이템을 관리할 수 있습니다.\n");
            Console.WriteLine("\n[아이템 목록]");
            foreach (Item item in itemlist.items)
            {
                Console.WriteLine($"{item.Name}|{item.Effect}|{item.Description}");
            }
            Console.WriteLine("\n1. 장착관리");
            Console.WriteLine("0. 나가기\n");
            Console.WriteLine("\n원하시는 행동을 입력해주세요.\n>>");

            while (true)
            {
                string input = Console.ReadLine();
                int sellecNumber;
                bool isNumber = int.TryParse(input, out sellecNumber);


                switch (sellecNumber)
                {
                    //마을창으로 돌아가기
                    case 0:
                        ShowStartScene();
                        break;
                    //장착 관리창으로 이동
                    case 1:
                        EquipDisPlay(inventory);
                        break;
                    //그 외 명령어 입력시 출력
                    default:
                        Console.WriteLine("잘못된 입력입니다");
                        break;

                }
            }
            
        }

        //장착 관리 창
        public void EquipDisPlay(Inventory itemlist)
        {

            Console.Clear();
            Console.WriteLine("인벤토리 - 장착 관리\n보유 중인 아이템을 관리할 수 있습니다.\n");
            Console.WriteLine("\n[아이템 목록]");
            foreach (Item item in itemlist.items)
            {
                int i =1;
                i++;
                Console.WriteLine($"- {i} {item.Name}|{item.Effect}|{item.Description}");
            }
            Console.WriteLine("\n0. 나가기\n");
            Console.WriteLine("\n원하시는 행동을 입력해주세요.\n>>");

            while (true)
            {
                string input = Console.ReadLine();
                int sellecNumber;
                bool isNumber = int.TryParse(input, out sellecNumber);


                switch (sellecNumber)
                {
                    //마을창으로 돌아가기
                    case 0:
                        ShowStartScene();
                        break;
                    //그 외 명령어 입력시 출력
                    default:
                        Console.WriteLine("잘못된 입력입니다");
                        break;

                }
            }

        }

        // 상점 창
        public void StoreDisplay(Player targetplayer, Inventory itemlist)
        {
            Console.Clear();
            Console.WriteLine("상점\n필요한 아이템을 얻을 수 있는 상점입니다.\n");
           
            Console.WriteLine("\n[보유골드]");
            Console.WriteLine($"\n{targetplayer.Gold} G");

            Console.WriteLine("\n[아이템 목록]");
            foreach (Item item in itemlist.items)
            {
                Console.WriteLine($"{item.Name}|{item.Effect}|{item.Description}");
            }

            Console.WriteLine("\n1. 아이탬 구매");
            Console.WriteLine("0. 나가기\n");

            Console.WriteLine("\n원하시는 행동을 입력해주세요.\n>>");

            while (true)
            {
                string input = Console.ReadLine();
                int sellecNumber;
                bool isNumber = int.TryParse(input, out sellecNumber);


                switch (sellecNumber)
                {
                    //마을창으로 돌아가기
                    case 0:
                        ShowStartScene();
                        break;
                    case 1:
                        //아이템 구매창으로 이동
                        //미구현
                        break;
                    //그 외 명령어 입력시 출력
                    default:
                        Console.WriteLine("잘못된 입력입니다");
                        break;

                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            GameManager Gm = new GameManager();
            Gm.ShowStartScene();
        }
    }
}
