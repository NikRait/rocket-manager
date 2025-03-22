namespace RocketManager;

public static class Helper
{
    public static string NullCheck(this string str)
    {
        do
        {
            if (String.IsNullOrEmpty(str))
            {
                Console.Write("Enter correct value: ");
                str = Console.ReadLine();
            }
            else
            {
                return str;
            }
        } while (true);
    }
    public static int MyTryParse(this int result, string str)
    {
        do
        {
            if (int.TryParse(str, out result) == false)
            {
                Console.Write("Invalid value: ");
            }
            else
            {
                if(result < 0)
                    Console.Write("Enter correct value:");
                else
                    return result;
            }
            str = Console.ReadLine();
        } while (true);
    }
    public static int MyTryParse(this int result, string str, int max)
    {
        do
        {
            if (int.TryParse(str, out result) == false)
            {
                Console.Write("Invalid value: ");
            }
            else
            {
                if(result < 0 || result > max)
                    Console.Write("Enter correct value: ");
                else
                    return result;
            }
            str = Console.ReadLine();
        } while (true);
    }
    public static int MyTryParse(this int result, string str, int min, int max)
    {
        do
        {
            if (int.TryParse(str, out result) == false)
            {
                Console.Write("Invalid value: ");
            }
            else
            {
                if(result < min || result > max)
                    Console.Write("Enter correct value: ");
                else
                    return result;
            }
            str = Console.ReadLine();
        } while (true);
    }
}