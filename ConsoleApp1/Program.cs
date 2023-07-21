using System;
using System.Collections.Generic;
using System.Linq;

public class Role
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class Member
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Role AssignedRole { get; set; }
    public List<Task> AssignedTasks { get; set; }

    public Member()
    {
        AssignedTasks = new List<Task>();
    }
}

public class Project
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Status { get; set; }
    public List<Task> Tasks { get; set; }
    public List<Member> TeamMembers { get; set; }

    public Project()
    {
        Tasks = new List<Task>();
        TeamMembers = new List<Member>();
    }
}

public class Task
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public string Status { get; set; }
}

public class ProjectManager
{
    private List<Project> projects;
    public List<Role> roles;

    public ProjectManager()
    {
        projects = new List<Project>();
        roles = new List<Role>();
    }

    public void AddProject(Project project)
    {
        project.Id = projects.Count + 1;
        projects.Add(project);
    }

    public void UpdateProject(int projectId, Project updatedProject)
    {
        Project projectToUpdate = projects.Find(p => p.Id == projectId);
        if (projectToUpdate != null)
        {
            projectToUpdate.Name = updatedProject.Name;
            projectToUpdate.Description = updatedProject.Description;
            projectToUpdate.StartDate = updatedProject.StartDate;
            projectToUpdate.EndDate = updatedProject.EndDate;
            projectToUpdate.Status = updatedProject.Status;
        }
        else
        {
            Console.WriteLine("Project not found!");
        }
    }

    public void DeleteProject(int projectId)
    {
        Project projectToDelete = projects.Find(p => p.Id == projectId);
        if (projectToDelete != null)
        {
            projects.Remove(projectToDelete);
        }
        else
        {
            Console.WriteLine("Project not found!");
        }
    }

    public void AddTaskToProject(int projectId, Task task)
    {
        Project project = projects.Find(p => p.Id == projectId);
        if (project != null)
        {
            task.Id = project.Tasks.Count + 1;
            project.Tasks.Add(task);
        }
        else
        {
            Console.WriteLine("Project not found!");
        }
    }

    public void UpdateTaskInProject(int projectId, int taskId, Task updatedTask)
    {
        Project project = projects.Find(p => p.Id == projectId);
        if (project != null)
        {
            Task taskToUpdate = project.Tasks.Find(t => t.Id == taskId);
            if (taskToUpdate != null)
            {
                taskToUpdate.Name = updatedTask.Name;
                taskToUpdate.Description = updatedTask.Description;
                taskToUpdate.DueDate = updatedTask.DueDate;
                taskToUpdate.Status = updatedTask.Status;
            }
            else
            {
                Console.WriteLine("Task not found!");
            }
        }
        else
        {
            Console.WriteLine("Project not found!");
        }
    }

    public void DeleteTaskFromProject(int projectId, int taskId)
    {
        Project project = projects.Find(p => p.Id == projectId);
        if (project != null)
        {
            Task taskToDelete = project.Tasks.Find(t => t.Id == taskId);
            if (taskToDelete != null)
            {
                project.Tasks.Remove(taskToDelete);
            }
            else
            {
                Console.WriteLine("Task not found!");
            }
        }
        else
        {
            Console.WriteLine("Project not found!");
        }
    }

    public void AddMemberToProject(int projectId, Member member)
    {
        Project project = projects.Find(p => p.Id == projectId);
        if (project != null)
        {
            member.Id = project.TeamMembers.Count + 1;
            project.TeamMembers.Add(member);
        }
        else
        {
            Console.WriteLine("Project not found!");
        }
    }

    public void UpdateMemberInProject(int projectId, int memberId, Member updatedMember)
    {
        Project project = projects.Find(p => p.Id == projectId);
        if (project != null)
        {
            Member memberToUpdate = project.TeamMembers.Find(m => m.Id == memberId);
            if (memberToUpdate != null)
            {
                memberToUpdate.Name = updatedMember.Name;
                memberToUpdate.AssignedRole = updatedMember.AssignedRole;
            }
            else
            {
                Console.WriteLine("Member not found!");
            }
        }
        else
        {
            Console.WriteLine("Project not found!");
        }
    }

    public void DeleteMemberFromProject(int projectId, int memberId)
    {
        Project project = projects.Find(p => p.Id == projectId);
        if (project != null)
        {
            Member memberToDelete = project.TeamMembers.Find(m => m.Id == memberId);
            if (memberToDelete != null)
            {
                project.TeamMembers.Remove(memberToDelete);
            }
            else
            {
                Console.WriteLine("Member not found!");
            }
        }
        else
        {
            Console.WriteLine("Project not found!");
        }
    }

    public void AssignRoleToMember(int memberId, Role role)
    {
        Member member = GetAllMembers().Find(m => m.Id == memberId);
        if (member != null)
        {
            member.AssignedRole = role;
        }
        else
        {
            Console.WriteLine("Member not found!");
        }
    }

    public List<Member> GetAllMembers()
    {
        return projects.SelectMany(p => p.TeamMembers).ToList();
    }

    public void AssignTaskToMember(int memberId, Task task)
    {
        Member member = GetAllMembers().Find(m => m.Id == memberId);
        if (member != null)
        {
            task.Id = member.AssignedTasks.Count + 1;
            member.AssignedTasks.Add(task);
        }
        else
        {
            Console.WriteLine("Member not found!");
        }
    }


    public void DisplayProjects()
    {
        foreach (var project in projects)
        {
            Console.WriteLine($"Project ID: {project.Id}");
            Console.WriteLine($"Name: {project.Name}");
            Console.WriteLine($"Description: {project.Description}");
            Console.WriteLine($"Start Date: {project.StartDate}");
            Console.WriteLine($"End Date: {project.EndDate}");
            Console.WriteLine($"Status: {project.Status}");
            Console.WriteLine("Tasks:");
            foreach (var task in project.Tasks)
            {
                Console.WriteLine($"- Task ID: {task.Id}, Name: {task.Name}, Due Date: {task.DueDate}, Status: {task.Status}");
            }
            Console.WriteLine("Team Members:");
            foreach (var member in project.TeamMembers)
            {
                string assignedRole = member.AssignedRole != null ? member.AssignedRole.Name : "No role assigned";
                Console.WriteLine($"- Member ID: {member.Id}, Name: {member.Name}, Assigned Role: {assignedRole}");
            }
            Console.WriteLine("-------------------------");
        }
    }

    // Other methods to manage roles, etc.
}

public class Program
{
    public static void Main(string[] args)
    {
        ProjectManager projectManager = new ProjectManager();

        // Thêm các vai trò vào danh sách vai trò
        Role role1 = new Role { Name = "Developer" };
        Role role2 = new Role { Name = "Tester" };
        projectManager.roles.Add(role1);
        projectManager.roles.Add(role2);

        // Thêm một dự án mới
        Project project1 = new Project
        {
            Name = "Project A",
            Description = "This is Project A",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(30),
            Status = "In progress"
        };
        projectManager.AddProject(project1);

        // Thêm các nhiệm vụ vào dự án
        Task task1 = new Task
        {
            Name = "Task 1",
            Description = "This is Task 1",
            DueDate = DateTime.Now.AddDays(7),
            Status = "In progress"
        };
        projectManager.AddTaskToProject(project1.Id, task1);

        Task task2 = new Task
        {
            Name = "Task 2",
            Description = "This is Task 2",
            DueDate = DateTime.Now.AddDays(14),
            Status = "Not started"
        };
        projectManager.AddTaskToProject(project1.Id, task2);

        // Thêm các thành viên vào dự án
        Member member1 = new Member { Name = "John" };
        Member member2 = new Member { Name = "Mary" };
        projectManager.AddMemberToProject(project1.Id, member1);
        projectManager.AddMemberToProject(project1.Id, member2);

        // Gán vai trò và phân công nhiệm vụ cho thành viên
        projectManager.AssignRoleToMember(member1.Id, role1);
        projectManager.AssignRoleToMember(member2.Id, role2);
        projectManager.AssignTaskToMember(member1.Id, task1);
        projectManager.AssignTaskToMember(member2.Id, task2);

        // Hiển thị danh sách các dự án và các thông tin của chúng
        projectManager.DisplayProjects();

        Console.ReadLine();
    }
}
