using System.Security.Cryptography;

namespace RocketManager;

public class RocketInfo
{
    public static List<string> rocketsNames = new();
    public int id {get; set;}
    public string name {get; set;}
    public string material { get; set; }
    public string rocketFuel { get; set; }
    public string comment { get; set; }
    public string startingTime { get; set; }
    public RocketInfo()
    {
        
    }
    private RocketInfo(string name,string material, string rocketFuel, string comment)
    {
        if(!string.IsNullOrEmpty(name))
            this.name = name;
        else
            throw new ArgumentNullException("name");
        if (!String.IsNullOrEmpty(material))
            this.material = material;
        else
            throw new ArgumentNullException("material");
        if (!String.IsNullOrEmpty(rocketFuel))
            this.rocketFuel = rocketFuel;
        else
            throw new ArgumentNullException("rocketFuel");
        if (!String.IsNullOrEmpty(comment))
            this.comment = comment;
        else
            this.comment = "/";
        startingTime = "00:00:00";
    }
    public RocketInfo CreateRocketInfo()
    {
        using (var db = new RocketContext())
        {
            do
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("Creating new rocket.");
                    System.Console.Write("Enter name of this rocket: ");
                    var usersName = Console.ReadLine().NullCheck();
                    if (IsThisNameAlreadyExists(usersName, db))
                        continue;
                    Console.Write("Enter material that was used to create this model: ");
                    var userMaterial = Console.ReadLine().NullCheck();
                    System.Console.Write("Enter which fuel you are going to use: ");
                    var userFuel = Console.ReadLine().NullCheck();
                    System.Console.Write("Enter your comments about this rocket: ");
                    var userComment = Console.ReadLine();
                    var rocketInfo = new RocketInfo(usersName,
                        userMaterial,
                        userFuel,
                        userComment
                    );
                    NewRocketDbSave(rocketInfo, db);
                    return rocketInfo;
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine("You are stupid dumbass, nigga. Try again.");
                }
            } while (true);
        }
    }
    private void NewRocketDbSave(RocketInfo rocketInfo, RocketContext db)
    {
        rocketsNames.Add(rocketInfo.name);
        db.infos.Add(rocketInfo);
        db.SaveChanges();
    }

    private void RocketDbSave(string oldName, RocketInfo temp)
    {
        using (var db = new RocketContext())
        {
            var newInfo = db.infos.FirstOrDefault(x => x.name == oldName);
            if (newInfo == null)
                Console.WriteLine("SMTH went wrong.");
            else
            {
                newInfo.Clone(temp, ref newInfo);
                db.infos.Update(newInfo);
            }
            var didNameWasChanged = true;
            foreach (var info in rocketsNames)
            {
                if (temp.name == info)
                {
                    didNameWasChanged = false;
                    break;
                }
            }
            if (didNameWasChanged)
            {
                int i = 0;
                foreach (var rockName in rocketsNames)
                {
                    if (oldName == rockName)
                        break;
                    i++;
                }
                rocketsNames[i] = temp.name;
            }
            db.SaveChanges();
        }
    }

    private bool IsThisNameAlreadyExists(string rockName, RocketContext db)
    {
        if (db.infos.FirstOrDefault(x => x.name == rockName) == null)
        {
            return false;
        }
        else
        {
            Console.WriteLine("Rocket with this name already exists.");
            Thread.Sleep(1500);
            return true;
        }
    }
    public RocketInfo ReadRocketInfo(int min)
    {
        using (var db = new RocketContext())
        {
            int id = 0;
            if(WritingNames(db) == false)
                return new RocketInfo();
            Console.Write("Enter id of rocket that you want to read: ");
            id = id.MyTryParse(Console.ReadLine(), min, rocketsNames.Count);  
            if (id == 0)
                return null;
            var rockName = rocketsNames[--id];
            var rocket = db.infos.FirstOrDefault(x => x.name == rockName);
            if (rocket == null)
                Console.WriteLine("No such rocket");
            else
                return rocket;
        }
        return new RocketInfo();
    }

    private bool WritingNames(RocketContext db)
    {
        if (rocketsNames.Count == 0)
        {
            Console.WriteLine("You do not have any rockets.");
            return false;
        }
        int id = 0;
        while (id < rocketsNames.Count)
        {
            RocketInfo rocket = db.infos.First(x => x.name == rocketsNames[id]);
            Console.WriteLine($"{++id} - {rocket.name}");
        }
        return true;
    }

    public RocketInfo ChangeRocketInfo()
    {
        var rocketInfo = ReadRocketInfo(1);
        if(string.IsNullOrEmpty(rocketInfo.name))
            return rocketInfo;
        var tempRocket = new RocketInfo();
        tempRocket.Clone(rocketInfo, ref tempRocket);
        var isItStillRunning = true;
        do
        {
            Console.WriteLine("Which characteristic would you like to change? 1 - material, 2 - fuel, 3 - comment, 4 - exit");
            var charId = 0.MyTryParse(Console.ReadLine(), 1, 4);
            switch (charId)
            {
                case 1:
                    Console.Write("Enter new material: ");
                    tempRocket.material = Console.ReadLine().NullCheck();
                    break;
                case 2:
                    Console.Write("Enter new fuel: ");
                    tempRocket.rocketFuel = Console.ReadLine().NullCheck();
                    break;
                case 3:
                    Console.Write("Enter new comment: ");
                    tempRocket.comment = Console.ReadLine().NullCheck();
                    break;
                case 4:
                    isItStillRunning = false;
                    break;
            }
        } while (isItStillRunning);
        RocketDbSave(rocketInfo.name, tempRocket);
        return tempRocket;
    }

    public void DeleteRocketInfo(RocketContext db)
    {
        var rocketInfo = ReadRocketInfo(1);
        if(String.IsNullOrEmpty(rocketInfo.name))
            return;
        Console.WriteLine("Are you sure you want to delete this rocket? 1 - yes, 2 - no");
        var choice = 0.MyTryParse(Console.ReadLine(), 1, 2);
        switch (choice)
        {
            case 1:
                rocketsNames.Remove(rocketInfo.name);
                db.infos.Remove(rocketInfo);
                db.SaveChanges();
                Console.WriteLine("Rocket deleted.");
                break;
            case 2:
                Console.Clear();
                break;
        }
    }

    public void EnterTimeToStart()
    {
        var rocketInfo = ReadRocketInfo(1);
        if (String.IsNullOrEmpty(rocketInfo.name))
            return;
        Console.Write("Enter time in seconds: ");
        var secTime = 0.MyTryParse(Console.ReadLine());
        if (secTime < 3600)
        {
            if (secTime < 60)
            {
                rocketInfo.startingTime = $"00:00:{secTime:00}";
            }
            else
            {
                var minutes = secTime / 60;
                var seconds = secTime % 60;
                rocketInfo.startingTime = $"00:{minutes:00}:{seconds:00}";
            }
        }
        else
        {
            var hours = secTime / 3600;
            var minutes = secTime / 60;
            var seconds = secTime % 60;
            rocketInfo.startingTime = $"{hours:00}:{minutes:00}:{seconds:00}";
        }
        rocketInfo.RocketDbSave(rocketInfo.name, rocketInfo);
    }

    public void CompareTimeToStart()
    {
        do
        {
            if (rocketsNames.Count < 2)
            {
                Console.WriteLine("You do not have even 2 rockets.");
                return;
            }
            Console.Clear();
            Console.WriteLine("First rocket. 0 - exit");
            var firstRocket = ReadRocketInfo(0);
            if (firstRocket == null) 
               return; 
            if (string.IsNullOrEmpty(firstRocket.name))
                return;
            if (TimeGarbageValueCheck(firstRocket.startingTime))
                continue;
            Console.WriteLine("Second rocket. 0 - exit");
            var secondRocket = ReadRocketInfo(0);
            if (secondRocket == null)
                return;
            if (string.IsNullOrEmpty(secondRocket.name))
                return;
            if (TimeGarbageValueCheck(secondRocket.startingTime))
                continue;
            if (SameVarCheck(firstRocket.name, secondRocket.name))
                continue;
            byte i = 0;
            foreach (var firstsChar in firstRocket.startingTime)
            {
                if (firstsChar == ':')
                {
                    i++;
                    continue;
                }
                if (Convert.ToInt16(firstsChar) < Convert.ToInt16(secondRocket.startingTime[i]))
                {
                    Console.WriteLine("First rocket started faster than second.\n");
                    return;
                }
                i++;
            }
            Console.WriteLine("Second rocket started faster than first.\n");
            return;
        } while (true);
    }
    private bool TimeGarbageValueCheck(string time)
    {
        if (time == "00:00:00")
        {
            Console.WriteLine("This model do not have starting time.");
            Thread.Sleep(1500);
            return true;
        }
        return false; 
    }
    private bool SameVarCheck(string first, string second)
    {
        if (first == second)
        {
            Console.WriteLine("You cannot compare same rocket.");
            Thread.Sleep(1500);
            return true;
        }
        return false;
    }
    private void Clone(RocketInfo rocketInfo, ref RocketInfo temp)
    {
        temp.name = rocketInfo.name;
        temp.material = rocketInfo.material;
        temp.rocketFuel = rocketInfo.rocketFuel;
        temp.comment = rocketInfo.comment;
        temp.startingTime = rocketInfo.startingTime;
    }
    public void GetNames()
    {
        using (var db = new RocketContext())
        {
            rocketsNames = db.infos.Select(x => x.name).ToList();
        }
    }
    public override string ToString()
    {
        if (comment == "/")
        {
            return $"Name - {this.name},\nMaterial - {this.material},\nFuel - {this.rocketFuel},\n{this.startingTime}";
        }
        return $"Name - {this.name},\nMaterial - {this.material},\nFuel - {this.rocketFuel},\nComments: {this.comment},\n{this.startingTime}";
    }
}