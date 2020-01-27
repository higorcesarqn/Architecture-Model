using System.Collections.Generic;

namespace Egl.Sit.Infra.EntityFramework.UnitOfWork.Entities
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<City> Cities { get; set; }
    }
}
