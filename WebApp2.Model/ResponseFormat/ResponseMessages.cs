namespace WebApp2.Model.ResponseFormat
{
    public class ResponseMessages
    {
        public static readonly string requestCancelledByUser = "Request has been cancelled by user";
        public static readonly string failedWithException = "Request failed with some technical issue, please try again later";
        public static readonly string commonaTryAgainMsg = "Some error occured please try again.";
        public static readonly string memberAddingSuccess = "Member Added Successfully";
        public static readonly string memberUpdatingSucess = "Member Updated Successfully";
        public static readonly string memberFirstNameRequired = "Member first name is required";
        public static readonly string memberLastNameRequired = "Member last name is required";
        public static readonly string phoneNumberInvalid = "Please pass appropriate phone number";
        public static readonly string addressRequired = "Member Address is required";
        public static readonly string genderRequired = "Member Gender is required";
        public static readonly string ageRequired = "Member Age is required";
        public static readonly string memberGettingSuccess = "All Members Details Retrived";
        public static readonly string memberGettingSuccess60 = "All Members Details Whose Age is above 60 is Retrived";
        public static readonly string memberNotFound = "Member Not Found";

        public static readonly string policyIdRequired = "Policy ID is required";
        public static readonly string policyNameRequired = "Policy name is required";
        public static readonly string policyTenureRequired = "Policy Tenure is required";
        public static readonly string invalidDates = "Policy Date is Invalid";
        public static readonly string policyAddingSucess = "Policy Added Successfully";
        public static readonly string policyGettingSuccess = "All Policies Details Retrived";
        public static readonly string agentPolicyNameUpdateSuccess = "Policy Name Updated Successfully";
        public static readonly string agentNotFound = "Agnet Not Found";




    }
}
