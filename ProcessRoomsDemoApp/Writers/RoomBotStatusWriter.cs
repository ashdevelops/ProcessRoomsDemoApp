﻿using ProcessRoomsDemoApp.Attributes;

namespace ProcessRoomsDemoApp.Writers;

[PacketId(1234)]
public class RoomBotStatusWriter : AbstractPacketWriter
{
    public required ICollection<IRoomBot> Bots { get; init; }

    public override void OnSerialize(INetworkPacketWriter writer)
    {
        writer.WriteInteger(Bots.Count);

        foreach (var bot in Bots)
        {
            var statusList = bot.
                StatusMap.
                Select(x => x.Key + (string.IsNullOrEmpty(x.Value) ? "" : " " + x.Value));
            
            writer.WriteLong(bot.Bot.Id);
            writer.WriteInteger(bot.Point.X);
            writer.WriteInteger(bot.Point.Y);
            writer.WriteString(bot.PointZ + "");
            writer.WriteInteger((int) bot.DirectionHead);
            writer.WriteInteger((int) bot.Direction);
            writer.WriteString("/" + string.Join("/", statusList).TrimEnd('/'));
        }
    }
}