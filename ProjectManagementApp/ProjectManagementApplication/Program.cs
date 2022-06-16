using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectManagementApplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var resourceManager = new ResourceManager(new ResourcePool());
            // adding sample resources
            resourceManager.AddResources(SampleResources.GetSampleResouce());

            var projectManager = new ProjectManager();
            // adding sample project
            projectManager.AssignProject(SampleProjects.GetSampleProject1());

            // check for project completion example 1

            Console.WriteLine("Project example 1");
            if (!projectManager.CheckIfProjectCanBeCompleted(projectManager.Project, resourceManager))
            {
                Console.WriteLine("Project cannot be completed ,");
                foreach (var error in projectManager.Errors)
                Console.WriteLine(error);
            }
            else
            {
                Console.WriteLine($"Project can be completed with the given deadline {projectManager.Project.DeadLine}");
            }

            Console.WriteLine();
            Console.WriteLine();

            projectManager.Errors.Clear();

            // check for project completion example 2

            projectManager.AssignProject(SampleProjects.GetSampleProject2());

            Console.WriteLine("Project example 2");
            if (!projectManager.CheckIfProjectCanBeCompleted(projectManager.Project, resourceManager))
            {
                Console.WriteLine("Project cannot be completed,");
                foreach (var error in projectManager.Errors)
                    Console.WriteLine(error);
            }
            else
            {
                Console.WriteLine($"Project can be completed with the given deadline {projectManager.Project.DeadLine}");
            }

            Console.WriteLine();
            Console.WriteLine();

            projectManager.Errors.Clear();

            // check for project completion example 3

            projectManager.AssignProject(SampleProjects.GetSampleProject3());

            Console.WriteLine("Project example 3");
            if (!projectManager.CheckIfProjectCanBeCompleted(projectManager.Project, resourceManager))
            {
                Console.WriteLine("Project cannot be completed,");
                foreach (var error in projectManager.Errors)
                    Console.WriteLine(error);
            }
            else
            {
                Console.WriteLine($"Project can be completed with the given deadline {projectManager.Project.DeadLine}");
            }

            Console.ReadKey();
        }
    }

    #region Resources

    // There can be mutiple levels of resources based on expereince from fresher to senior levels in a company.
    // Here we restrict to 4 types L1, L2, L3, L4
    public enum CareerLevel
    {
        L1,
        L2,
        L3,
        L4
    }

    
    public class Resource
    {
        public Guid ResourceId { get; set; }  // A resource can have unique Id
        public CareerLevel Level { get; set; } // has a Level any of the above listed CareerLevel
        public int CapacityPerDay { get; set; } // time in hours per day the resource can work
    }


    // ResourcePool gives us the total resources available,
    // here an assumption is made that the resource pool always has free resources who can be allocated to a project
    public class ResourcePool
    {
        public List<Resource> Resources { get; set; }

        public ResourcePool()
        {
            Resources = new List<Resource>();
        }
    }

    // IResourceManager will take care managing resources Add/ Remove / Fetch
    public interface IResourceManager
    {
        List<Resource> GetAllResources { get; } // get all Resources that are available in ResourcePool
        void AddResources(List<Resource> resources); // Add resources
        void RemoveResource(Guid resourceId); // Remove resource ( time being not used in this implementation)
    }

    // Implementation of IResourceManager is below

    public class ResourceManager : IResourceManager
    {
        private ResourcePool _resourcePool;
        public ResourceManager(ResourcePool resourcePool)
        {
            _resourcePool = resourcePool;
        }

        public void AddResources(List<Resource> resources)
        {
            _resourcePool.Resources.AddRange(resources);
        }

        public List<Resource> GetAllResources
        {
            get
            {
                return _resourcePool.Resources;
            }
        }

        public void RemoveResource(Guid resourceId)
        {
            var item = _resourcePool.Resources.Single(x => x.ResourceId == resourceId);
            _resourcePool.Resources.Remove(item);
        }
    }

    #endregion

    #region Project

    // here we assume the following
    // A project has project Id, List of tasks & Deadline to get it complete
    // DeadLine will give us the number of days to complete the project from the current date

    public class ProjectDetails
    {
        public Guid ProjectId { get; set; }
        public List<Task> Tasks { get; set; }
        public DateTime DeadLine { get; set; }
    }

    // each taks each has taskId, ManHours will give us total time required to complete that task
    // we assume that Manhours are well calculated based on career level

    public class Task
    {
        public Guid TaskId { get; set; }
        public int ManHours { get; set; }
        public CareerLevel Level { get; set; }
    }

    // ProjectManager will assign a project & calculates the project can be completed with the
    // availbale resource from the resource manager

    public interface IProjectManager
    {
        void AssignProject(ProjectDetails project);
        bool CheckIfProjectCanBeCompleted(ProjectDetails projectDetails, IResourceManager resourceManager);
    }

    public class ProjectManager
    {
        public List<string> Errors = new List<string>();

        public ProjectDetails Project = new ProjectDetails();

        // assigning a project
        public void AssignProject(ProjectDetails project)
        {
            Project = project;
        }

        // function to check if a project can be completed with the
        // availbale resource from the resource manager
        public bool CheckIfProjectCanBeCompleted(ProjectDetails projectDetails, IResourceManager resourceManager)
        {
            // get total working days
            var workDays = (projectDetails.DeadLine.Date - DateTime.Now.Date).Days;
            
            // get career level wise task hours required, example L1 - 160 hrs, L2 - 80 hrs etc
            var taskHours = GetTaskHoursRequired(projectDetails);

            // get career level wise resource hours required, example L1 - 160 hrs, L2 - 80 hrs etc
            var resourceHours = GetLevelWiseAvailableResourcesHours(resourceManager as ResourceManager, workDays);


            //*** We assume that total task hours for each career level
            // must be less than or equal to available resource hours for each career level for the project to complete ***//
            foreach (var key in taskHours.Keys)
            {
                if (taskHours[key] > resourceHours[key])
                {
                    // Add an error message if required resource hours is less than task hours
                    Errors.Add("Shortage of level : " + key + " by " + (taskHours[key] - resourceHours[key]) + " Man hours");
                }
            }

            return Errors.Count == 0;
        }

        private Dictionary<CareerLevel, int> GetTaskHoursRequired(ProjectDetails projectDetails)
        {
            var levelWiseTaskHours = new Dictionary<CareerLevel, int>();
            foreach (var task in projectDetails.Tasks)
            {
                if (!levelWiseTaskHours.ContainsKey(task.Level))
                    levelWiseTaskHours.Add(task.Level, task.ManHours);
                else
                    levelWiseTaskHours[task.Level] += task.ManHours;
            }

            return levelWiseTaskHours;
        }

        private Dictionary<CareerLevel, int> GetLevelWiseAvailableResourcesHours(ResourceManager resourceManager, int totalNumberOfDays)
        {
            var levelWiseResourceAvailableHours = new Dictionary<CareerLevel, int>();

            foreach (var resource in resourceManager.GetAllResources)
            {
                if (!levelWiseResourceAvailableHours.ContainsKey(resource.Level))
                    levelWiseResourceAvailableHours.Add(resource.Level, resource.CapacityPerDay * totalNumberOfDays);
                else
                    levelWiseResourceAvailableHours[resource.Level] += resource.CapacityPerDay * totalNumberOfDays;
            }

            return levelWiseResourceAvailableHours;
        }
    }
    #endregion
}
