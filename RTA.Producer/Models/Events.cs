public class User
{
    public bool isAuthenticated { get; set; }
    public string provider { get; set; }
    public int id { get; set; }
    public string EMail { get; set; }
}

public class Attributes
{
    public int hotelId { get; set; }
    public string hotelRegion { get; set; }
    public string hotelName { get; set; }
}

public class Event
{
    public string app { get; set; }
    public string type { get; set; }
    public DateTime time { get; set; }
    public bool isSucceeded { get; set; }
    public Meta meta { get; set; }
    public User user { get; set; }
    public Attributes attributes { get; set; }
}

public class Meta
{
}

public class Events
{
    public List<Event> events { get; set; }
}