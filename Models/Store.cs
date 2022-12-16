public class Store 
{
    public Store(Int64 id, Int64 ownerId, string name, string address1, string address2, string city, string state, string zip, bool verified)
    {
        Id = id;
        OwnerId = ownerId;
        Name = name;
        Address1 = address1;
        Address2 = address2;
        City = city;
        State = state;
        Zip = zip;
        Verified = verified;
    }

    public Int64 Id { get; set; }
    public Int64 OwnerId { get; set; }
    public string Name { get; set; }
    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Zip { get; set; }
    public bool Verified { get; set; }
}