using System.Linq;
using Humanizer;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;

namespace Core.Models
{
    public abstract class EntityGeo : Entity
    {
        public Geometry Geom { get; protected set; }

        public void EditarGeom(Geometry geometry)
        {
            Geom = geometry;
        }

        public static implicit operator Feature(EntityGeo entity)
        {
            var type = entity.GetType();

            var attributes = type
                .GetProperties()
                .Where(x => x.PropertyType != typeof(Geometry) && (x.Name != "CreatedAt" && x.Name != "UpdatedAt"))
                .ToDictionary(key => key.Name.Camelize(), value => value.GetValue(entity));

            return new Feature(entity.Geom, new AttributesTable(attributes))
            {
                BoundingBox = entity.Geom.Envelope.EnvelopeInternal
            };
        }
    }
}