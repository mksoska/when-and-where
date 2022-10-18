using PizzaShopDAL.Data;

using (var context = new WhenAndWhereDBContext())
{
    var users = context.User.ToList();

    foreach (var user in users)
    {
        Console.WriteLine($"User -> {user.Name}");

        foreach (var option in user.Options)
        {
            Console.WriteLine($"Option -> {option.Label}");
        }
    }
}
