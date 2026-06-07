# 🎓 Student Information System (C# OOP Project)

A comprehensive Object-Oriented Programming (OOP) project implemented in **C#**, representing an academic management system. The project showcases standard software design principles and includes two different application modes: a C# Console Application and a Windows Forms Graphical User Interface (GUI).

---

## 🏗️ Architecture & Component Overview

The project is split into two primary components:

1. **Console Application (`course work student info system`)**:
   - A command-line interactive system that manages students, administrators, courses, departments, and course schedules.
   - Implements advanced features such as automatic validation, GPA calculation (A-F grading scale), and system diagnostics (timing metrics and memory tracking).

2. **Windows Forms GUI (`coursework`)**:
   - A desktop GUI interface developed with WinForms.
   - Provides text input panels, button events, and list controls for user registration, user logins, and automatic course enrollment.

---

## 🎨 Object-Oriented Programming (OOP) Implementation

This project was built to illustrate core Object-Oriented Programming principles:

* **Abstraction**:
  - The abstract base class `Account` defines the common structure (Username and Password properties) and an abstract behavior `DisplayInfo()` for all security principals in the system.
* **Inheritance**:
  - `Student` and `Admin` classes inherit directly from the `Account` base class, reusing code while extending it with role-specific attributes (e.g. Student ID, Enrolled Courses, Department).
* **Encapsulation**:
  - All classes protect their state through standard C# properties with accessors. For example, Student inputs undergo validation check loops before the model state is updated.
* **Polymorphism**:
  - *Method Overriding*: Both `Student` and `Admin` classes override the virtual `DisplayInfo()` method of `Account` to print relevant data.
  - *Method Overloading*: The `Student` class overloads the `Enroll` method to accept either a `Course` object directly or a `courseCode` string along with a lookup list:
    ```csharp
    public void Enroll(Course course) { ... }
    public void Enroll(string courseCode, List<Course> availableCourses) { ... }
    ```
* **Static Attributes**:
  - The `Student` class defines a static tracking variable `StudentCount` which increments automatically upon instantiation to count total students.

---

## 🚀 Key Features

* **Authentication & Role-Based Portals**: Log in as either an `Admin` or a `Student` with separate menus and access permissions.
* **Comprehensive Registration & Input Validation**: Checks IDs (must be exactly 9 numeric digits), name constraints (no empty inputs or numbers), and username uniqueness.
* **Course Enrollment & Schedule Previewing**: Students can register in courses, view class schedules, and preview timetable slots.
* **Dynamic GPA Calculator**: Students can input letter grades (A, B, C, D, F) for their registered courses to calculate their current GPA.
* **Department Management**: Administrators can assign students to various departments (e.g., Computer Science, Electrical Engineering, Communications Engineering) and view department analytics.
* **Execution Metrics**: Measures execution speed (milliseconds) and memory usage during runtime.

---

## 🛠️ Technology Stack

* **Language**: C# (.NET)
* **Framework**: .NET Framework / .NET Core
* **UI**: Windows Forms (WinForms) & Console UI

---

## ⚙️ Installation & Running

### Prerequisites
- Install [.NET SDK](https://dotnet.microsoft.com/download) (or Visual Studio 2019/2022 with C# workload).

### Option 1: Running with Visual Studio (Recommended)
1. Open Visual Studio.
2. Open the solution file `course work.sln` (for the Console App) or `coursework.sln` (for the WinForms App).
3. Set the target project as the startup project and click **Start** or press `F5`.

### Option 2: Running via .NET CLI
1. Open your terminal and navigate to the project directory:
   ```bash
   # For the Console App:
   cd "course work student info system"
   dotnet run
   
   # For the WinForms App:
   cd "coursework"
   dotnet run
   ```
