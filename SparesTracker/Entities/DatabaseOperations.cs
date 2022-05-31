using System.Collections.Generic;
using System.Linq;

namespace SparesTracker.Entities
{
    public static class DatabaseOperations
    {
        private static readonly RepairPartsEntities Entities;

        static DatabaseOperations()
        {
            Entities = new RepairPartsEntities();
        }

        public static List<Spares> GetAllSpares()
        {
            return Entities?.Spares.ToList();
        }

        public static List<Warehouses> GetAllWarehouses()
        {
            return Entities?.Warehouses.ToList();
        }

        public static void ChangeSparesAmount(List<Spares> sparesList, int newAmount)
        {
            sparesList.ForEach(s => s.amount = newAmount);
        
            Entities.SaveChanges();
        }
    }
}