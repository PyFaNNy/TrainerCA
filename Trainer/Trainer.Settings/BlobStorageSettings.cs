namespace Trainer.Settings
{
    public class BlobStorageSettings : IApplicationSettings
    {
        public string ConnectionString
        {
            get;
            set;
        }

        public string TransferDocumentsContainer
        {
            get;
            set;
        }
    }
}
