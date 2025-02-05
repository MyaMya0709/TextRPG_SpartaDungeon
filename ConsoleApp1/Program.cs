namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Character player1 = new Character("철수");
            Character player2 = new Character("영희");
            ShowName(player1);
            ShowName(player2);
            GiveGold(player1, 50);
            GiveGold(player2, 50);
        }
        static void ShowName(Character target)
        {
            Console.WriteLine(player1.myName); // 틀림
            Console.WriteLine(target.myName);
        }
        static void GiveGold(Character target, int amt)
        {
            target.gold += amt;
            if (target.gold < 0) target.gold = 0;
        }
    }
    internal class Character
    {
        public string myName;
        public int gold;
        public Character(string name)
        {
            myName = name;
        }
    }
}
