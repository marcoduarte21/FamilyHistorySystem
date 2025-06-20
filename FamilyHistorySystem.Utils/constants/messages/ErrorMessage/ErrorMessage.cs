﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHistorySystem.Utils.constants.messages.ErrorMessage
{
    public class ErrorMessage
    {
        public const string StudentNotFound = "Student not found";
        public const string StudentAlreadyExists = "Student with this ID or Cedula already exists";
        public const string StudentNotDeleted = "Student could not be deleted";
        public const string UnauthorizedAccess = "Unauthorized access";
        public const string Forbidden = "You do not have permission to perform this action";
        public const string NoParentsFound = "No parents found for this student";
        public const string NoChildrenFound = "No children found for this student";
        public const string NoSiblingsFound = "No siblings found for this student";
        public const string NoGrandParentsFound = "No grandparents found for this student";
        public const string NoUnclesFound = "No uncles found for this student";
        public const string NoCousinsFound = "No cousins found for this student";
    }
}
