namespace TextAdventure
{
    public class Duel
{
    private int health;
    private int defense;
    private int attack;
    private string OName;
    private int OHealth;
    private int OAttack;

    public Duel(string name)
    {
        Random rng = new Random();
        health = rng.Next(100, 151);
        defense = rng.Next(25, 50);
        attack = rng.Next(25, 45);

        OName = "The Monster";
        OHealth = 160;
        OAttack = 35;
    }

    public void StartBattle()
    {
        bool check = true;
        Console.WriteLine("As you unlock the door, a monster breaks through the door and attacks!!!");

        while (check)
        {
            Console.WriteLine("Do you want to attack, or defend?");
            string action = Console.ReadLine().ToLower();

            if (action == "attack")
            {
                Attack();
                CheckWin(ref check);
            }
            if (action == "defend")
            {
                Defend();
                CheckWin(ref check);
            }

            if (check) 
            {
                BobAttacks();
            }

            Console.WriteLine($"Your health: {health}");
            Console.WriteLine($"Monster's health: {OHealth}");
            CheckWin(ref check);
        }
    }

    private void Attack()
    {
        OHealth -= attack;
        Console.WriteLine($"You have done {attack} damage!");
    }

    private void Defend()
    {
        if (defense > OAttack)
        {
            health += OAttack;
            Console.WriteLine("You have successfully defended the attack!");
            health += 10; 
            Console.WriteLine("You drank a small potion that regained 10 HP!");
        }
        else
        {
            Console.WriteLine("You couldn't defend the attack!");
        }
    }

    private void CheckWin(ref bool check)
    {
        if (OHealth <= 0)
        {
            Console.WriteLine($"{OName} has been defeated!");
            check = false;
        }
        if (health <= 0)
        {
            Console.WriteLine($"You have been defeated by {OName}");
            check = false;
            Environment.Exit(0); 
        }
    }

    private void BobAttacks()
    {
        health -= OAttack;
        Console.WriteLine($"The Monster did {OAttack} damage to you!");
    }
}

}