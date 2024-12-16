namespace ProcessRoomsDemoApp.Attributes;

public class PacketIdAttribute(short id) : Attribute
{
    public short Id { get; } = id;
}