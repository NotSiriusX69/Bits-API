using Bits_API.Models.DTOs;

namespace Bits_API.Services
{
    public class AdminService
    {
        private readonly BitsContext _bitsContext;

        public AdminService(BitsContext bitsContext) 
        {
            _bitsContext = bitsContext;
        }


        // Get ApplicationInfo
        public ApplicationInfo GetApplicationInfo()
        {
            // Create new object to fill and return
            ApplicationInfo appInfo = new ApplicationInfo();
            
            // Get Numbers
            int usersCount = ( from users in _bitsContext.user select users.userId ).Count();
            int projectsCount = (from projects in _bitsContext.project select projects.projectId).Count();
            int tasksCount = (from tasks in _bitsContext.task select tasks.projectId).Count();
            int adminsCount = (from users in _bitsContext.user where users.isAdmin == true select users.userId).Count();


            // Fill values
            appInfo.UsersNumber = usersCount;
            appInfo.ProjectsNumber = projectsCount;
            appInfo.TasksNumber = tasksCount;
            appInfo.AdminsNumber = adminsCount;

            return appInfo;

        }
    }
}
