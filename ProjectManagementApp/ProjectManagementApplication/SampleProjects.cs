using System;
using System.Collections.Generic;

namespace ProjectManagementApplication
{
    public static class SampleProjects
    {
        public static ProjectDetails GetSampleProject1()
        {
            var project = new ProjectDetails()
            {
                DeadLine = DateTime.Now.AddDays(5),
                ProjectId = Guid.NewGuid(),
                Tasks = new List<Task>()
                {
                    new Task(){ Level = CareerLevel.L1,ManHours = 40},
                    new Task(){ Level = CareerLevel.L2,ManHours = 40},
                    new Task(){ Level = CareerLevel.L3,ManHours = 40},
                    new Task(){ Level = CareerLevel.L4,ManHours = 40},
                }
            };

            return project;
        }

        public static ProjectDetails GetSampleProject2()
        {
            var project = new ProjectDetails()
            {
                DeadLine = DateTime.Now.AddDays(5),
                ProjectId = Guid.NewGuid(),
                Tasks = new List<Task>()
                {
                    new Task(){ Level = CareerLevel.L1,ManHours = 160},
                    new Task(){ Level = CareerLevel.L2,ManHours = 40},
                    new Task(){ Level = CareerLevel.L3,ManHours = 40},
                    new Task(){ Level = CareerLevel.L4,ManHours = 160},
                }
            };

            return project;
        }

        public static ProjectDetails GetSampleProject3()
        {
            var project = new ProjectDetails()
            {
                DeadLine = DateTime.Now.AddDays(1),
                ProjectId = Guid.NewGuid(),
                Tasks = new List<Task>()
                {
                    new Task(){ Level = CareerLevel.L1,ManHours = 40},
                    new Task(){ Level = CareerLevel.L2,ManHours = 40},
                    new Task(){ Level = CareerLevel.L3,ManHours = 40},
                    new Task(){ Level = CareerLevel.L4,ManHours = 40},
                }
            };

            return project;
        }
    }
}
