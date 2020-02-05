namespace ARI.EVOS.CompApp.Constant
{
    /// <summary>
    /// This class contains messages for dealer validation and view title constant strings
    /// </summary>
    public static class DealerConstant
    {
        public const string DealerNetworkTitle = "(c)ARI - Ordering System - Dealer Network (frmDealerNetwork.frm)";
        public const string ContactEmailTitle = "Additional Email Contacts";
        public const string ValidationMessageBoxTitle = "Message";
        public const string InsertingRecord = "Adding dealer into db";
        public const string InsertedRecord = "Dealer added successfully.";
        public const string InsertedFailRecord = "Adding dealer failed";
        public const string DealerSearchTitle = "(c)ARI - Ordering System - Dealer Network (frmMainDealer.frm)";
        public const string SearchDealer = "Enter dealer selection or press 'Add Dealer' to add new dealer";
        public const string ZipCode = "Zip Code is Invalid.";
        public const string UpdatingRecord = "Updating dealer into db";
        public const string UpdatedRecord = "Dealer updated successfully.";
        public const string FailedToUpdateRecord = "Updating dealer failed";
        public const string AddingDealer = "Enter new dealer info and press update when finished...";
        public const string RecordsNotFound = "No records found, Please try again.";
        public const string NotValidEntry = "Not a Valid Entry!";
        public const string UpdatedDealerRecordFailed = "Dealer not Updated - not found.";

        //delete dealer
        public const string DeletingDealer = "Deleting Dealer, please wait...";
        public const string DeletedDealer = "Dealer deleted successfully."; //custom message
        public const string DeleteDealerFail = "Dealer not Deleted - not found.";

        public const string InsertingGetReadyRecord = "Adding Get Ready into db";
        public const string InsertedGetReadyRecord = "Get Ready added successfully.";
        public const string UpdatingGetReadyRecord = "Updating Get Ready into db";
        public const string UpdatedGetReadyRecord = "Get Ready updated";
        public const string DeletingGetReadyRecord = "Deleting Get Ready into db";
        public const string DeletedGetReadyRecord = "Record(s) deleted.";
        public const string DeletedGetReadyRecordFailed = "Record not found, \n select from grid to delete.";

        // Logger
        public const string ExecutingInsert = "Add Dealer Executing ExecuteInsert()";
        public const string ExecutedInsert = "Add Dealer Executed ExecuteInsert()";
        public const string FailedInsert = "Add Dealer Failed ExecuteInsert()";

        public const string ExecutingUpdate = "Update Dealer Executing ExecuteUpdate()";
        public const string ExecutedUpdate = "Update Dealer Executed ExecuteUpdate()";
        public const string FailedUpdate = "Update Dealer Failed ExecuteUpdate()";

        public const string ExecutingGetReadyInsert = "Add Get Ready Executing InsertGetReady()";
        public const string ExecutedGetReadyInsert = "Add Get Ready Executed InsertGetReady()";
        public const string FailedGetReadyInsert = "Add Get Ready Failed InsertGetReady()";
        public const string ExecutingGetReadyUpdate = "Update Get Ready Executing UpdateGetReady()";
        public const string ExecutedGetReadyUpdate = "Update Get Ready Executed UpdateGetReady()";
        public const string FailedGetReadyUpdate = "Update Get Ready Failed UpdateGetReady()";
        public const string ExecutingGetReadyDelete = "Delete Get Ready Executing DeleteGetReady()";
        public const string ExecutedGetReadyDelete = "Delete Get Ready Executed DeleteGetReady()";
        public const string FailedGetReadyDelete = "Delete Get Ready Failed DeleteGetReady()";

        public const string ExecutingInsertEmail = "Add Email Executing ExecuteInsert()";
        public const string ExecutedInsertEmail = "Add Email Executed ExecuteInsert()";
        public const string FailedInsertEmail = "Add Email Failed ExecuteInsert()";

        // Others
        public const string CountryCodeRequired = "Country required.";
        public const string MakeCodeRequired = "Make Description required";
        public const string DealerIdRequired = "Dealer ID required";
    }
}
