using LibGit2Sharp;

namespace AspNETCoreSample
{
    public class RepoInfo
    {
        public static RepoInfo GetRepositoryInformationForPath(string path)
        {
            if (LibGit2Sharp.Repository.IsValid(path))
            {
                return new RepoInfo(path);
            }
            return null;
        }

        public string CommitHash
        {
            get
            {
                return _repo.Head.Tip.Sha;
            }
        }

        public string BranchName
        {
            get
            {
                return _repo.Head.FriendlyName;
            }
        }

        public string TrackedBranchName
        {
            get
            {
                return _repo.Head.IsTracking ? _repo.Head.TrackedBranch.FriendlyName : String.Empty;
            }
        }

        public bool HasUnpushedCommits
        {
            get
            {
                return _repo.Head.TrackingDetails.AheadBy > 0;
            }
        }

        public bool HasUncommittedChanges
        {
            get
            {
                return _repo.RetrieveStatus().Any(s => s.State != FileStatus.Ignored);
            }
        }

        public IEnumerable<Commit> Log
        {
            get
            {
                return _repo.Head.Commits;
            }
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
                _repo.Dispose();
            }
        }

        private RepoInfo(string path)
        {
            _repo = new Repository(path);
        }

        private bool _disposed;
        private readonly Repository _repo;
    }
}
