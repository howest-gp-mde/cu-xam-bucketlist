namespace XrnCourse.BucketList
{
    /// <summary>
    /// Contains various constant values use throughout the application in one place
    /// </summary>
    public static class Constants
    {
        public const string ApiBaseUrl = "https://enter-your-api-tunnel-url/";

        /// <summary>
        /// Classifies constant values used for Mocking purproses
        /// </summary>
        public static class Mocking
        {
            /// <summary>
            /// Delay in milliseconds to simulate long running service requests
            /// </summary>
            public const int FakeDelay = 1000;
        }

        public static class MessageNames
        {
            /// <summary>
            /// MessagingCenter key to notify a bucket has been saved
            /// </summary>
            public const string BucketSaved = "BUCKETSAVED";
        }
    }

}
