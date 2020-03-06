using NetkaCommitment.Data.EFModels;

namespace NetkaCommitment.Repository
{
    public class BaseRepository
    {
        protected NetkaCommitmentContext db = null;
        public BaseRepository() {
            db = new NetkaCommitmentContext();
        }
    }
}
