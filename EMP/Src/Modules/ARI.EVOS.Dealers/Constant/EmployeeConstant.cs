namespace EMP.Management.Constant
{
    public static class EmployeeConstant
    {
        //Employee View
        public const string EmployeeViewTitle = "(c)EMP - Management System - Employee Form (frmEmployeManagement.frm)";
        public const string EmployeeListViewTitle = "(c))EMP - Management System- Employee List (frmMainEmployee.frm)";
        public const string ValidationMessageBoxTitle = "Message";

        //Insert Employee
        public const string InsertingRecord = "Adding employee into db";
        public const string InsertedRecord = "Employee added successfully.";
        public const string InsertedFailRecord = "Adding employee failed";
        public const string AddingEmployee = "Enter new employee info and press update when finished...";

        //Get Employee
        public const string SearchEmployee = "Enter employee selection or press 'Add Employee' to add new employee";

        //Update Employee
        public const string UpdatingRecord = "Updating employee into db";
        public const string UpdatedRecord = "Employee updated successfully.";
        public const string FailedToUpdateRecord = "Updating employee failed";
        public const string RecordsNotFound = "No records found, Please try again.";
        public const string NotValidEntry = "Not a Valid Entry!";
        public const string UpdatedEmployeeRecordFailed = "Employee not Updated - not found.";

        //delete employee
        public const string DeletingEmployee = "Deleting Employee, please wait...";
        public const string DeletedEmployee = "Employee deleted successfully."; //custom message
        public const string DeleteEmployeeFail = "Employee not Deleted - not found.";

        // Logger
        public const string ExecutingInsert = "Add Employee Executing ExecuteInsert()";
        public const string ExecutedInsert = "Add Employee Executed ExecuteInsert()";
        public const string FailedInsert = "Add Employee Failed ExecuteInsert()";
        public const string ExecutingUpdate = "Update Employee Executing ExecuteUpdate()";
        public const string ExecutedUpdate = "Update Employee Executed ExecuteUpdate()";
        public const string FailedUpdate = "Update Employee Failed ExecuteUpdate()";

    }
}
