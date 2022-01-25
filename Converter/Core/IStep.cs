namespace Core
{
	public interface IStep
    {
        bool CanValidate { get; }

        IStep Next { get; }

        void Add(IStep step);

        void Run(bool validate);
    }
}
