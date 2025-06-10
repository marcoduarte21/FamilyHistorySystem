# Family History System

A .NET-based web application for managing student information and automatically mapping their family relationships based on parental data.

## 📌 Overview

This system is designed for educational institutions to track current and past student family ties. Each student includes the following attributes:

- Name and last name
- Date of birth
- ID number
- Sex
- Mother ID
- Father ID

By entering only the student's mother and father IDs, the system automatically maps the extended family—parents, grandparents, siblings, children, uncles/aunts, and cousins—without requiring manual linking between all individuals.

## 🧠 Key Features

- 🔍 **Student List**: View all registered students.
- ➕ **Add Student**: Create a new student entry via a modal form.
- ✏️ **Edit Student**: Update student details.
- ❌ **Delete Student**: Remove a student from the system.
- 👨‍👩‍👧‍👦 **Family Tree Mapping**: Automatically view related family members.

## 🧪 Technologies Used

- .NET Core
- Entity Framework Core
- SQL Server
- LINQ, MVC & SOLID principles

## 🚀 Getting Started

### Prerequisites

- Visual Studio 2022 or newer
- .NET SDK 8.0+
- SQL Server (Express)

### Installation Steps

1. Clone the repository:

   ```bash
   git clone https://github.com/marcoduarte21/FamilyHistorySystem.git
