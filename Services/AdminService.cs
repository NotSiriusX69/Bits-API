using Bits_API.Models.DTOs;
using Microsoft.EntityFrameworkCore;

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

            // Fill Values
            appInfo.UsersNumber = usersCount;
            appInfo.ProjectsNumber = projectsCount;
            appInfo.TasksNumber = tasksCount;
            appInfo.AdminsNumber = adminsCount;

            return appInfo;

        }

        // Get All registered Users
        public List<UserData> GetUsers()
        {
            // List to store all retrieved users in database
            var retrieved_users = new List<UserData>();

            foreach (var user in _bitsContext.user)
            {
                // To store user's data
                UserData user_data = new UserData
                {
                    // Assign all values
                    userId = user.userId,
                    userName = user.userName,
                    password = user.password,
                    email = user.email,
                    createdAt = user.createdAt,
                    isAdmin = user.isAdmin,
                    projectsNumber = user.projects.Count
                };

                // Append user to the list
                retrieved_users.Add(user_data);
            }

            return retrieved_users;
        }

        // Filter and get Users by String
        public List<UserData> GetSearchedUsers(string searchString)
        {

            // Get all users that match the search string
            var searchedUsers = _bitsContext.user
            .Where(user =>
            user.userName.ToLower().StartsWith(searchString) ||
            user.email.ToLower().StartsWith(searchString) ||
            user.password.ToLower().StartsWith(searchString) ||
            user.userId.ToString().StartsWith(searchString))
            .Select(user => new UserData
            {
                userId = user.userId,
                userName = user.userName,
                password = user.password,
                email = user.email,
                createdAt = user.createdAt,
                isAdmin = user.isAdmin,
                projectsNumber = user.projects.Count
            })
            .ToList();

            return searchedUsers;

        }
    }
}
