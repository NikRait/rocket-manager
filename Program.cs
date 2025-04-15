namespace RocketManager;

class Program
{
    static void Main(string[] args)
    {
        Console.Clear();
        var rocketInfo = new RocketInfo();
        rocketInfo.GetNames();
        var choice = 0;
        var isItRunning = true;
        Console.SetCursorPosition(30, Console.CursorTop);
        Console.WriteLine("Rockets Database");
        while(isItRunning){
            Console.WriteLine("Whaddya wanna do now? 1 - Create new rocket, 2 - read rocket, 3 - change rocket's parametrs, 4 - delete rocket,\n5 - enter or change time it takes for a rocket to launch, 6 - compare rockets, 7 - exit");
            choice = choice.MyTryParse(Console.ReadLine(), 1, 7);
            Console.Clear();
            Console.SetCursorPosition(30, Console.CursorTop);
            Console.WriteLine("Rockets Database");
            switch (choice)
            {
                case 1:
                    rocketInfo = rocketInfo.CreateRocketInfo();
                    Console.WriteLine("New rocket was created.");
                    break;
                case 2:
                    rocketInfo = rocketInfo.ReadRocketInfo(1);
                    Console.Clear();
                    if(!string.IsNullOrEmpty(rocketInfo.name))
                        Console.WriteLine(rocketInfo.ToString());
                    break;
                case 3:
                    rocketInfo = rocketInfo.ChangeRocketInfo();
                    Console.Clear();
                    if(!string.IsNullOrEmpty(rocketInfo.name))
                        Console.WriteLine(rocketInfo.ToString());
                    break;
                case 4:
                    rocketInfo.DeleteRocketInfo(new RocketContext());
                    break;
                case 5:
                    rocketInfo.EnterTimeToStart();
                    break;
                case 6:
                    rocketInfo.CompareTimeToStart();
                    break;
                case 7:
                    isItRunning = false;
                    break;
            }
        }

    }
}
