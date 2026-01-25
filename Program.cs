using System;

var warrior = new Character("Rurik", new SwordAttack());
warrior.Describe();
warrior.Attack();

// Thay đổi hành vi chiến đấu mà không cần lớp con mới.
warrior.SetAttackBehavior(new BowAttack());
warrior.Attack();

Console.WriteLine();

var mage = new Character("Lora", new SpellAttack());
mage.Describe();
mage.Attack();

Console.WriteLine();
Console.WriteLine("Composition hơn inheritance giúp Character gọn nhẹ và dễ thay đổi hành vi khi đang chạy.");

interface IAttackBehavior
{
    void Attack(string characterName);
}

class Character
{
    private IAttackBehavior _attackBehavior;

    public Character(string name, IAttackBehavior attackBehavior)
    {
        Name = name;
        _attackBehavior = attackBehavior;
    }

    public string Name { get; }

    public void Attack()
    {
        _attackBehavior.Attack(Name);
    }

    public void SetAttackBehavior(IAttackBehavior newBehavior)
    {
        _attackBehavior = newBehavior;
    }

    public void Describe()
    {
        Console.WriteLine($"{Name} sẵn sàng chiến đấu với chiến thuật linh hoạt.");
    }
}

class SwordAttack : IAttackBehavior
{
    public void Attack(string characterName)
    {
        Console.WriteLine($"{characterName} vung kiếm chém mạnh.");
    }
}

class BowAttack : IAttackBehavior
{
    public void Attack(string characterName)
    {
        Console.WriteLine($"{characterName} bắn một mũi tên chuẩn xác từ xa.");
    }
}

class SpellAttack : IAttackBehavior
{
    public void Attack(string characterName)
    {
        Console.WriteLine($"{characterName} phóng ra một phép thuật rực rỡ.");
    }
}
