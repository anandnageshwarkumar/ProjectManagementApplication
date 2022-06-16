using System;
using System.Collections.Generic;

namespace ProjectManagementApplication
{
    public static class SampleResources
    {
        public static List<Resource> GetSampleResouce()
        {
            var Resources = new List<Resource>(); 

            Resources.AddRange(new List<Resource>()
            {
                new Resource(){ResourceId = new Guid(), Level = CareerLevel.L1,CapacityPerDay = 8},
                new Resource(){ResourceId = new Guid(), Level = CareerLevel.L1,CapacityPerDay = 8},

                new Resource(){ResourceId = new Guid(), Level = CareerLevel.L2,CapacityPerDay = 8},
                new Resource(){ResourceId = new Guid(), Level = CareerLevel.L2,CapacityPerDay = 8},

                new Resource(){ResourceId = new Guid(), Level = CareerLevel.L3,CapacityPerDay = 8},
                new Resource(){ResourceId = new Guid(), Level = CareerLevel.L3,CapacityPerDay = 8},

                new Resource(){ResourceId = new Guid(), Level = CareerLevel.L4,CapacityPerDay = 8},
                new Resource(){ResourceId = new Guid(), Level = CareerLevel.L4,CapacityPerDay = 8}
            });

            return Resources;
        }
    }
}
