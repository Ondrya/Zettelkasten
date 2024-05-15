namespace Zettelkasten.Domain.Models.Painting
{
    public class PolarBase
    {
        public int EntityId { get; set; }
        public int EntityType { get; set; }
        public string Tooltip { get; set; }

        public PolarBase()
        {
            
        }

        public PolarBase(int entityId)
        {
            EntityId = entityId;
            Tooltip = $"{entityId}";
        }

        public PolarBase(int entityId, string tooltip)
        {
            EntityId = entityId;
            Tooltip = tooltip;
        }

        public PolarBase(int entityId, int entityType, string tooltip)
        {
            EntityId = entityId;
            EntityType = entityType;
            Tooltip = tooltip;
        }
    }
}