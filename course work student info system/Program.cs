using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace SimpleStudentInfoSystem
{
   // Abstract base class for all accounts
   public abstract class Account
   {
      public string Username { get; set; }
      public string Password { get; set; }

      protected Account(string username, string password)
      {
         Username = username;
         Password = password;
      }

      // Virtual method to display basic info
      public virtual void DisplayInfo()
      {
         Console.WriteLine($"Username: {Username}");
      }
   }

   // Student class inherits Account
   public class Student : Account
   {
      public static int StudentCount = 0; // Static attribute to count Student objects

      public string StudentName { get; set; }
      public string StudentId { get; set; }
      public Department Department { get; set; }
      public List<Course> Courses { get; set; } = new List<Course>();

      public Student(string name, string id, string username, string password)
          : base(username, password)
      {
         StudentName = name;
         StudentId = id;
         StudentCount++; // Increase count when a new student is created
      }

      // Override to show student info including department and courses
      public override void DisplayInfo()
      {
         Console.WriteLine($"Student Name: {StudentName}");
         Console.WriteLine($"Student ID: {StudentId}");
         Console.WriteLine($"Department: {(Department != null ? Department.Name : "Not assigned")}");
         Console.WriteLine("Enrolled courses:");
         if (Courses.Count == 0)
            Console.WriteLine("  None");
         else
            foreach (var c in Courses)
               Console.WriteLine($"  - {c.CourseName}");
      }

      // Method overloading example: enroll using Course object
      public void Enroll(Course course)
      {
         if (!Courses.Contains(course))
         {
            Courses.Add(course);
            course.AddStudent(this);
            Console.WriteLine($"{StudentName} enrolled in {course.CourseName}");
         }
         else
         {
            Console.WriteLine($"{StudentName} is already enrolled in {course.CourseName}");
         }
      }

      // Overloaded enroll method using course code string
      public void Enroll(string courseCode, List<Course> availableCourses)
      {
         var course = availableCourses.FirstOrDefault(c => c.CourseCode == courseCode);
         if (course == null)
         {
            Console.WriteLine("Course code not found.");
            return;
         }
         Enroll(course);
      }
   }

   // Course class with list of students enrolled
   public class Course
   {
      public string CourseCode { get; set; }
      public string CourseName { get; set; }
      public int Credits { get; set; }
      public List<Student> Students { get; set; } = new List<Student>();

      public Course(string code, string name, int credits)
      {
         CourseCode = code;
         CourseName = name;
         Credits = credits;
      }

      // Add student to this course if not already added
      public void AddStudent(Student student)
      {
         if (!Students.Contains(student))
            Students.Add(student);
      }

      public void DisplayCourseInfo()
      {
         Console.WriteLine($"{CourseCode}: {CourseName} ({Credits} credits)");
      }
   }

   // Admin class inherits Account
   public class Admin : Account
   {
      public string AdminName { get; set; }

      public Admin(string name, string username, string password)
          : base(username, password)
      {
         AdminName = name;
      }

      // Admin can add a student to a department
      public void AddStudentToDepartment(Student student, Department department)
      {
         if (student.Department == department)
         {
            Console.WriteLine($"{student.StudentName} is already in the department {department.Name}.");
            return;
         }

         department.AddStudent(student);
         student.Department = department;
         Console.WriteLine($"{student.StudentName} is added to department {department.Name}.");
      }

      // Admin enrolls a student in a course by course code
      public void EnrollStudentInCourse(Student student, string courseCode, List<Course> courses)
      {
         student.Enroll(courseCode, courses);
      }
   }

   // Department class contains students and courses
   public class Department
   {
      public string Id { get; set; }
      public string Name { get; set; }
      public List<Student> Students { get; set; } = new List<Student>();
      public List<Course> Courses { get; set; } = new List<Course>();

      public Department(string id, string name)
      {
         Id = id;
         Name = name;
      }

      // Add student to this department
      public void AddStudent(Student student)
      {
         if (!Students.Contains(student))
            Students.Add(student);
      }

      public void DisplayInfo()
      {
         Console.WriteLine($"Department {Name} (ID: {Id})");
         Console.WriteLine($"Students in department: {Students.Count}");
      }
   }
   public class schedule
   {
      University uni = new University();
      public void sh()
      {

         int i = 0;
         foreach (var c in uni.Courses)
         {
            Console.WriteLine($"{c.CourseName} is from {8 + i}:00 to {10 + i}:00 ");
            i = i + 2;
         }
      }


   }


   // University class holding all data
   public class University
   {
      public List<Department> Departments { get; set; } = new List<Department>();
      public List<Student> Students { get; set; } = new List<Student>();
      public List<Course> Courses { get; set; } = new List<Course>();
      public List<Account> Accounts { get; set; } = new List<Account>();

      public University()
      {
         // Create a default admin
         Admin admin1 = new Admin("Mohamed walid", "mohamedwalid", "4122");
         Accounts.Add(admin1);

         // Add some departments
         Departments.Add(new Department("Cs", "Computer Science"));
         Departments.Add(new Department("EE", "Electrical Engineering"));
         Departments.Add(new Department("EC", "Comunication Engineering "));


         // Add some courses

         Courses.Add(new Course("EE102", "Introduction to Circuits", 4));
         Courses.Add(new Course("EE101", "Circuits 2", 2));
         Courses.Add(new Course("CS105", "Computer Networks", 3));
         Courses.Add(new Course("CS104", "Operating Systems", 4));
         Courses.Add(new Course("CS103", "Database Systems", 3));
         Courses.Add(new Course("EC101", "Signals 1", 3));
         Courses.Add(new Course("EC102", "Signals 2", 3));
      }
   }

   // UI class to interact with user
   public class UI
   {
      University university = new University();

      public void Run()
      {
         Console.WriteLine("Welcome to Simple Student Information System");

         while (true)
         {
            Console.WriteLine("\nMain Menu:");
            Console.WriteLine("1. Create Student Account");
            Console.WriteLine("2. Login");
            Console.WriteLine("3. Exit");
            Console.Write("Enter choice: ");
            string choice = Console.ReadLine()?.Trim();

            switch (choice)
            {
               case "1":
                  CreateStudentAccount();
                  break;
               case "2":
                  Login();
                  break;
               case "3":
                  Console.WriteLine("Exiting...");
                  return;
               default:
                  Console.WriteLine("Invalid choice. Try again.");
                  break;
            }
         }
      }

      // Create student account with exception handling for all inputs
      private void CreateStudentAccount()
      {
         try
         {
            // int exceptionCount = 0;
            //long memoryBefore = GC.GetTotalMemory(true);
            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();

            string name = null;
            while (true)
            {
               Console.Write("Enter student name: ");
               name = Console.ReadLine()?.Trim();
               if (string.IsNullOrEmpty(name))
               {
                  Console.WriteLine("Name cannot be empty.");
                  //exceptionCount++;
                  continue;
               }
               if (name.Any(char.IsDigit))
               {
                  Console.WriteLine("Name cannot contain numbers.");
                  //exceptionCount++;
                  continue;
               }
               break;
            }

            string id = null;
            while (true)
            {
               Console.Write("Enter your ID (9 digits only): ");
               id = Console.ReadLine()?.Trim();
               try
               {
                  if (id == null || id.Length != 9)
                     throw new FormatException("ID must be exactly 9 digits.");
                  if (!id.All(char.IsDigit))
                     throw new FormatException("ID must contain digits only.");
                  break; // Valid ID
               }
               catch (FormatException ex)
               {
                  Console.WriteLine("Wrong input, please try again: " + ex.Message);
                  // exceptionCount++;
               }
            }

            string username = null;
            while (true)
            {
               Console.Write("Enter username: ");
               username = Console.ReadLine()?.Trim();
               if (string.IsNullOrEmpty(username))
               {
                  Console.WriteLine("Username cannot be empty.");
                  //exceptionCount++;
                  continue;
               }
               if (university.Accounts.Any(a => a.Username == username))
               {
                  Console.WriteLine("Username already exists. Try a different one.");
                  //exceptionCount++;
                  continue;
               }
               break;
            }

            string password = null;
            while (true)
            {
               Console.Write("Enter password (4 characters only): ");
               password = Console.ReadLine()?.Trim();
               if (password == null || password.Length != 4)
               {
                  Console.WriteLine("Password must be exactly 4 characters.");
                  //exceptionCount++;
                  continue;
               }
               break;
            }

            var student = new Student(name, id, username, password);
            university.Students.Add(student);
            university.Accounts.Add(student);

            //stopwatch.Stop();
            //long memoryAfter = GC.GetTotalMemory(false);
            //long memoryUsed = memoryAfter - memoryBefore;

            Console.WriteLine("Student account created successfully!");
            Console.WriteLine($"Total students registered: {Student.StudentCount}");
            //Console.WriteLine("\nMetrics:");
            //Console.WriteLine($"Execution Time: {stopwatch.ElapsedMilliseconds} ms");
            //Console.WriteLine($"Memory Used: {memoryUsed} bytes");
            //Console.WriteLine($"Exceptions Caught: {exceptionCount}");
         }


         catch (Exception ex)
         {
            Console.WriteLine($"Error creating account: {ex.Message}");
         }
     
      
      }

      // Login method with exception handling
      private void Login()
      {
         try
         {
            string username = null;
            string password = null;

            while (true)
            {
               Console.Write("Enter username: ");
               username = Console.ReadLine()?.Trim();
               if (string.IsNullOrEmpty(username))
               {
                  Console.WriteLine("Username cannot be empty.");
                  continue;
               }
               break;
            }

            while (true)
            {
               Console.Write("Enter password: ");
               password = Console.ReadLine()?.Trim();
               if (string.IsNullOrEmpty(password))
               {
                  Console.WriteLine("Password cannot be empty.");
                  continue;
               }
               break;
            }

            var user = university.Accounts.FirstOrDefault(a => a.Username == username && a.Password == password);
            if (user == null)
            {
               Console.WriteLine("Invalid credentials.");
               return;
            }

            if (user is Admin admin)
               AdminMenu(admin);
            else if (user is Student student)
               StudentMenu(student);
         }
         catch (Exception ex)
         {
            Console.WriteLine($"Error during login: {ex.Message}");
         }
      }

      // Admin menu with exception handling on input
      private void AdminMenu(Admin admin)
      {
         Console.WriteLine($"\nWelcome Admin: {admin.AdminName}");

         while (true)
         {
            Console.WriteLine("\nAdmin Menu:");
            Console.WriteLine("1. Add student to department");
            Console.WriteLine("2. Enroll student in course");
            Console.WriteLine("3. View all students");
            Console.WriteLine("4. View all departments");
            Console.WriteLine("5. View all courses");
            Console.WriteLine("6. Logout");
            Console.Write("Enter choice: ");
            string choice = Console.ReadLine()?.Trim();

            switch (choice)
            {
               case "1":
                  AddStudentToDepartment(admin);
                  break;
               case "2":
                  EnrollStudentInCourse(admin);
                  break;
               case "3":
                  ViewAllStudents();
                  break;
               case "4":
                  ViewAllDepartments();
                  break;
               case "5":
                  ViewAllCourses();
                  break;
               case "6":
                  Console.WriteLine("Logging out...");
                  return;
               default:
                  Console.WriteLine("Invalid choice.");
                  break;
            }
         }
      }

      // Student menu with exception handling on input
      private void StudentMenu(Student student)
      {
         Console.WriteLine($"\nWelcome Student: {student.StudentName}");

         while (true)
         {
            Console.WriteLine("\nStudent Menu:");
            Console.WriteLine("1. View info");
            Console.WriteLine("2. View enrolled courses");
            Console.WriteLine("3. Calculate GPA");
            Console.WriteLine("4. Preview schedule");
            Console.WriteLine("5. Logout");
            Console.Write("Enter choice: ");
            string choice = Console.ReadLine()?.Trim();

            switch (choice)
            {
               case "1":
                  student.DisplayInfo();
                  break;
               case "2":
                  if (student.Courses.Count == 0)
                     Console.WriteLine("You are not enrolled in any courses.");
                  else
                     student.Courses.ForEach(c => Console.WriteLine($"- {c.CourseName}"));
                  break;
               case "3":
                  CalculateGPA(student);
                  break;
               case "4":
                  sh(student);
                  break;
               case "5":
                  Console.WriteLine("Logging out...");
                  return;
               default:
                  Console.WriteLine("Invalid choice.");
                  break;
            }
         }
      }

      // Admin functionality: add student to department
      private void AddStudentToDepartment(Admin admin)
      {
         try
         {
            string studentId = null;
            while (true)
            {
               Console.Write("Enter student ID (9 digits): ");
               studentId = Console.ReadLine()?.Trim();
               if (studentId == null || studentId.Length != 9 || !studentId.All(char.IsDigit))
               {
                  Console.WriteLine("ID must be exactly 9 digits and numeric.");
                  continue;
               }
               break;
            }

            Student student = university.Students.FirstOrDefault(s => s.StudentId == studentId);
            if (student == null)
            {
               Console.WriteLine("Student not found.");
               return;
            }

            Console.WriteLine("Departments:");
            foreach (var d in university.Departments)
               Console.WriteLine($"- {d.Id}: {d.Name}");

            string depId = null;
            while (true)
            {
               Console.Write("Enter department ID to add student to: ");
               depId = Console.ReadLine()?.Trim();
               if (string.IsNullOrEmpty(depId))
               {
                  Console.WriteLine("Department ID cannot be empty.");
                  continue;
               }
               break;
            }

            Department dept = university.Departments.FirstOrDefault(d => d.Id.Equals(depId, StringComparison.OrdinalIgnoreCase));
            if (dept == null)
            {
               Console.WriteLine("Department not found.");
               return;
            }

            admin.AddStudentToDepartment(student, dept);
         }
         catch (Exception ex)
         {
            Console.WriteLine($"Error adding student to department: {ex.Message}");
         }
      }

      // Admin functionality: enroll student in course
      private void EnrollStudentInCourse(Admin admin)
      {
         try
         {
            string studentId = null;
            while (true)
            {
               Console.Write("Enter student ID (9 digits only): ");
               studentId = Console.ReadLine()?.Trim();
               if (studentId == null || studentId.Length != 9 || !studentId.All(char.IsDigit))
               {
                  Console.WriteLine("ID must be exactly 9 digits and numeric.");
                  continue;
               }
               break;
            }

            Student student = university.Students.FirstOrDefault(s => s.StudentId == studentId);
            if (student == null)
            {
               Console.WriteLine("Student not found.");
               return;
            }

            Console.WriteLine("Courses:");
            foreach (var c in university.Courses)
               Console.WriteLine($"- {c.CourseCode}: {c.CourseName}");

            string courseCode = null;
            while (true)
            {
               Console.Write("Enter course code to enroll student in: ");
               courseCode = Console.ReadLine()?.Trim();
               if (string.IsNullOrEmpty(courseCode))
               {
                  Console.WriteLine("Course code cannot be empty.");
                  continue;
               }
               break;
            }

            admin.EnrollStudentInCourse(student, courseCode, university.Courses);
         }
         catch (Exception ex)
         {
            Console.WriteLine($"Error enrolling student in course: {ex.Message}");
         }
      }

      // View all students
      private void ViewAllStudents()
      {
         Console.WriteLine("\nAll Students:");
         foreach (var s in university.Students)
         {
            s.DisplayInfo();
            Console.WriteLine();
         }
      }

      // View all departments
      private void ViewAllDepartments()
      {
         Console.WriteLine("\nDepartments:");
         foreach (var d in university.Departments)
         {
            d.DisplayInfo();
            Console.WriteLine();
         }
      }

      // View all courses
      private void ViewAllCourses()
      {
         Console.WriteLine("\nCourses:");
         foreach (var c in university.Courses)
         {
            c.DisplayCourseInfo();
         }
      }

      // Calculate GPA for student
      private void CalculateGPA(Student student)
      {
         if (student.Courses.Count == 0)
         {
            Console.WriteLine("You have no courses enrolled to calculate GPA.");
            return;
         }

         Console.WriteLine("\nEnter your letter grades for the following courses:");
         List<string> grades = new List<string>();
         foreach (var course in student.Courses)
         {
            string grade;
            while (true)
            {
               Console.Write($"{course.CourseName} grade (A-F): ");
               grade = Console.ReadLine()?.Trim().ToUpper();
               if (grade == null || !"ABCDF".Contains(grade))
               {
                  Console.WriteLine("Invalid grade. Enter A, B, C, D, or F.");
                  continue;
               }
               break;
            }
            grades.Add(grade);
         }

         double totalPoints = 0;
         for (int i = 0; i < grades.Count; i++)
         {
            totalPoints += GradeToPoint(grades[i]);
         }

         double gpa = totalPoints / grades.Count;
         Console.WriteLine($"Your GPA is: {gpa:F2}");
      }

      private double GradeToPoint(string grade)
      {
         switch (grade)
         {
            case "A": return 4.0;
            case "B": return 3.0;
            case "C": return 2.0;
            case "D": return 1.0;
            case "F": return 0.0;
            default: return 0.0;
         }
      }


      // Preview schedule based on enrolled courses
      void sh(Student s1)
      {
         schedule s = new schedule();
         s.sh();
      }

   }

   class Program
   {
      static void Main()
      {
         UI ui = new UI();
         ui.Run();
      }
   }
}