namespace ArchivesSpace_.Net_Client
{
    public abstract class ArchivesSpaceObjectManagerBase
    {
        protected readonly ArchivesSpaceService ArchivesSpaceService;

        protected ArchivesSpaceObjectManagerBase(ArchivesSpaceService activeService)
        {
            ArchivesSpaceService = activeService;
        }

    }
}