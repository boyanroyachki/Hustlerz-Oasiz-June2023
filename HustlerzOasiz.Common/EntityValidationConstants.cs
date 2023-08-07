namespace HustlerzOasiz.Common
{
    public static class EntityValidationConstants
    {
        public static class  Category
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 50;
             


        }
        public static class Job
        {
            public const int TitleMinLength = 2;
            public const int TitleMaxLength = 50;

            public const int LocationMinLength = 5;
            public const int LocationMaxLength = 150;

            public const int DetailsMinLegth = 50;
            public const int DetailsMaxLength = 1000;

            public const int MinPrice = 0;

            public enum JobStatus
            {
                Active,
                Completed,
                Failed,
                Quited,
                Deleted
            }
        }
        public static class Contractor
        {
            public const int UsernameMinLength = 2;
            public const int UsernameMaxLength = 50;

            public const int PhoneNumberMinLength = 7;
            public const int PhoneNumberMaxLength = 15;
        }

        public static class AppUser
        {
            public const int UsernameMinLength = 2;
            public const int UsernameMaxLength = 15;
        }
    }
}
